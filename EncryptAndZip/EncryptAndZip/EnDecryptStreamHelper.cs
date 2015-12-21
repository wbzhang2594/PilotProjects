using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.IO.Compression;

namespace Motorola.Common.CommonLib
{
    /// <summary>
    /// This class is used for encrypting/decrypting files. It has its private Key to encrypt file.
    /// So any file encrypted by this class should only be decrypted by this class.
    /// </summary>
    public class EnDecryptStreamHelper : IDisposable
    {
        #region Constructor
        private EnDecryptStreamHelper()
        {
            cmCrypto = new RijndaelManaged();
        }

        private static EnDecryptStreamHelper instance;
        public static EnDecryptStreamHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnDecryptStreamHelper();
                }
                return instance;
            }
        }
        #endregion

        #region Crypto parameters

        private RijndaelManaged cmCrypto;
        private const string CrytoKey = "{E88707E6-353F-46F3-8021-C4597593FA48}";
        private const int keySize = 128;
        private const string defaultID = "{F05EECA8-6D69-4079-94F2-516473BF57A2}";

        private const string Header = "{F2B1CF7F-1F5B-406F-B7B9-D3FD76231055}";
        private static byte[] HeaderBuffer = GetBytes(Header);

        #endregion

        /// <summary>
        /// Encrypted a stream. The result stream can only be decrypted by the CreateDecryptStream(string id, Stream source) function with the same id.
        /// </summary>
        /// <param name="id">used to generate Key and IV</param>
        /// <param name="source">the stream will be encrypted</param>
        /// <returns>encrypted stream</returns>
        public Stream CreateEncryptStream(string id, Stream nonEncryptStream)
        {
            byte[] key = GetKey(id);
            return new CryptoStream(nonEncryptStream, cmCrypto.CreateEncryptor(key, key), CryptoStreamMode.Write);
        }

        /// <summary>
        /// Encrypted a stream, using defaultID to create the Key and IV.
        /// </summary>
        /// <param name="source">the stream will be encrypted</param>
        /// <returns>encrypted stream</returns>
        public Stream CreateEncryptStream(Stream nonEncryptStream)
        {
            return CreateEncryptStream(defaultID, nonEncryptStream);
        }

        /// <summary>
        /// Decrypt a stream from a encrypted stream. The encrypted stream should be encrypted by the CreateEncryptStream(string id, Stream source) function with the same id.
        /// </summary>
        /// <param name="id">used to generate Key and IV</param>
        /// <param name="encryptedStream">The encrypted stream should be encrypted by the CreateEncryptStream(string id, Stream source) function with the same id.</param>
        /// <returns>The decrypted stream</returns>
        public Stream CreateDecryptStream(string id, Stream encryptedStream)
        {
            byte[] key = GetKey(id);
            return new CryptoStream(encryptedStream, cmCrypto.CreateDecryptor(key, key), CryptoStreamMode.Read);
        }

        public Stream CreateDecryptStream(Stream encryptedStream)
        {
            return CreateDecryptStream(defaultID, encryptedStream);
        }

        private byte[] GetKey(string id)
        {
            byte[] rawKey = (new UnicodeEncoding()).GetBytes(CrytoKey + "-" + id);
            int keyLength = keySize / 8;
            byte[] key = new byte[keyLength];
            key.Initialize();
            for (int i = 0; i < rawKey.Length; ++i)
            {
                int index = i % keyLength;
                key[index] = (byte)(((int)key[index] + (int)rawKey[i]) & 0xff);
            }
            return key;
        }

        private bool VerifyHeader(ref Stream stream)
        {
            byte[] tmpByteArray = new byte[HeaderBuffer.Length];

            stream.Read(tmpByteArray, 0, tmpByteArray.Length);

            if (!GetString(tmpByteArray).Equals(Header))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 1. When reading either an encrypted or not encrypted file, returns a Stream 
        ///     a. If the source file is encrypted, returns a decrypted Stream. 
        ///     b. If the source file is not encrypted by Default ID, return a normal FileStream of the source file for Read.
        /// 2. When writing, returns a Stream for encrypting.
        /// </summary>
        /// <param name="path">When reading, it is the path of source file. When writing, it is the path of target file.</param>
        /// <param name="access">Only accept FileAccess.Read or FileAccess.Write for indicating the reading or writing purpose. FileAccess.ReadWrite is not accepted.</param>
        /// <returns></returns>
        //fix warning CA2000
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope")]
        public Stream CreateCryptoFlStream(string path, FileAccess access)
        {
            Stream outputStream = null;

            Stream tempOutputStream = null;
            Stream tempSourceStream = null;
            FileStream fileStream = null;

            {
                switch (access)
                {
                    case (FileAccess.Read):
                        {
                            FileMode mode = FileMode.Open; FileShare fileShare = FileShare.Read;

                            if (new FileInfo(path).Length < 80)  //If the Header, Key and IV are guid string,the length of an encrypted file is at least 80.
                            {
                                tempOutputStream = new FileStream(path, mode, access, fileShare);

                                break;
                            }
                            else
                            {

                                tempSourceStream = createFileStream(path, mode, access, fileShare);
                                tempOutputStream = CreateDecryptStream(tempSourceStream);
                                if (!VerifyHeader(ref tempOutputStream))
                                {
                                    tempSourceStream.Close();
                                    tempOutputStream = new FileStream(path, mode, access, fileShare);
                                }

                                break;
                            }
                        }
                    case (FileAccess.Write):
                        {
                            FileMode mode = FileMode.Create; FileShare fileShare = FileShare.None;
                            fileStream = createFileStream(path, mode, access, fileShare);
                            tempOutputStream = CreateEncryptStream(fileStream);
                            AddHead(tempOutputStream);
                            break;
                        }
                    default:
                        {
                            throw new Exception("Doesn't support the FileAccess value.");
                        }
                }

                outputStream = tempOutputStream;
                tempOutputStream = null;
            }

            return outputStream;
        }

        static void AddHead(Stream tempOutputStream)
        {
            tempOutputStream.Write(HeaderBuffer, 0, HeaderBuffer.Length);
        }

        /// <summary>
        /// Zip then Encrypt the inputByteArray.
        /// </summary>
        /// <param name="inputByteArray"></param>
        /// <returns>The Zipped and Encrypted result.</returns>
        public byte[] Zip_then_Encrypt_ByteArray(byte[] inputByteArray)
        {
            byte[] ZipedByteArray = ZipByteArray(inputByteArray);

            byte[] outputByteArray = null;
            Stream EnCryptoStream = null;

            using (MemoryStream inputMemStream = new MemoryStream(ZipedByteArray))
            {

                using (MemoryStream outMemStream = new MemoryStream())
                {
                    using (EnCryptoStream = CreateEncryptStream(outMemStream))
                    {

                        AddHead(EnCryptoStream);
                        inputMemStream.CopyTo(EnCryptoStream);
                        //EnCryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                    }

                    outputByteArray = outMemStream.ToArray();
                }
            }
            return outputByteArray;
        }



        /// <summary>
        /// Intelligently identify the input byte array, 
        /// 1. If it is generated by the Zip_then_Encrypt_ByteArray method, return an decrypted then unzipped byte array.
        /// 2. If not, return the inputByteArray directly
        /// </summary>
        /// <param name="inputByteArray"></param>
        /// <returns></returns>
        public byte[] Decrypt_then_UnZip_ByteArray(byte[] inputByteArray)
        {
            byte[] outputByteArray = null;

            if (inputByteArray.Length < 80)  //Length of a encrypted byte[] is at least 80.
            {
                outputByteArray = inputByteArray;
            }
            else
            {
                using (Stream tempInputStream = new MemoryStream(inputByteArray))
                {
                    Stream DecryptoStream = CreateDecryptStream(tempInputStream);

                    if (!VerifyHeader(ref DecryptoStream))
                    {
                        outputByteArray = inputByteArray;
                    }
                    else
                    {
                        outputByteArray = GetBytesFromStream(DecryptoStream);
                        outputByteArray = UnZipByteArray(outputByteArray);
                    }

                }
            }

            return outputByteArray;
        }

        public static byte[] ZipByteArray(byte[] inputByteArray)
        {
            byte[] outputByteArray = null;
            using (MemoryStream memStream = new MemoryStream(inputByteArray))
            {
                MemoryStream outMemStream = new MemoryStream();
                {
                    using (DeflateStream zipStream = new DeflateStream(outMemStream, CompressionLevel.Fastest, true))
                    {
                        memStream.CopyTo(zipStream);

                    }

                    outputByteArray = outMemStream.ToArray();
                }
            }
            return outputByteArray;
        }

        public static byte[] UnZipByteArray(byte[] outputByteArray)
        {
            byte[] outputByteArray2 = null;
            using (MemoryStream outMemStream = new MemoryStream())
            {
                MemoryStream memStream = new MemoryStream(outputByteArray);

                using (DeflateStream zipStream = new DeflateStream(memStream, CompressionMode.Decompress, true))
                {
                    zipStream.CopyTo(outMemStream);
                }
                outputByteArray2 = outMemStream.ToArray();
            }
            return outputByteArray2;
        }

        //private byte[] GetBytesFromStream1(Stream DecryptoStream)
        //{
        //    byte[] pbaBytes = new byte[0];


        //    int BufferLength = 40 * 1024 * 1024;
        //    byte[] buffer = new byte[BufferLength];
        //    int readLength = 0;

        //    while ((readLength = DecryptoStream.Read(buffer, 0, BufferLength)) > 0)
        //    {
        //        if (readLength < BufferLength)
        //        {
        //            byte[] cut_buffer = new byte[readLength];
        //            Array.Copy(buffer, cut_buffer, readLength);
        //            pbaBytes = pbaBytes.Concat(cut_buffer).ToArray();
        //        }
        //        else
        //        {
        //            pbaBytes = pbaBytes.Concat(buffer).ToArray();
        //        }
        //    }


        //    return pbaBytes;
        //}

        private FileStream createFileStream(string path, FileMode mode, FileAccess access, FileShare fileShare)
        {
            return new FileStream(path, mode, access, fileShare);
        }

        /// <summary>
        /// Get byte Array from sourceStream.
        /// </summary>
        /// <param name="sourceStream">The stream for getting byte Array.</param>
        /// <param name="encoding">The Encoding of the stream.</param>
        /// <returns>The byte Array got from sourceStream.</returns>
        public byte[] GetBytesFromStream(Stream sourceStream, Encoding encoding)
        {
            byte[] pbaBytes = new byte[0];

            using (BinaryReader pbaBinReader = new BinaryReader(sourceStream, encoding))
            {
                int BufferLength = 40 * 1024 * 1024;
                byte[] buffer = null;

                while ((buffer = pbaBinReader.ReadBytes(BufferLength)).Length > 0)
                {
                    pbaBytes = pbaBytes.Concat(buffer).ToArray();
                }
            }

            return pbaBytes;
        }

        /// <summary>
        /// Get byte Array from sourceStream whose encoding is Encoding.UTF8.
        /// </summary>
        /// <param name="sourceStream">The stream for getting byte Array. Its encoding is Encoding.UTF8.</param>
        /// <returns>The byte Array got from sourceStream.</returns>
        public byte[] GetBytesFromStream(Stream sourceStream)
        {
            return GetBytesFromStream(sourceStream, Encoding.UTF8);
        }

        public void WriteBytesToStream(Stream targetFileStream, byte[] buffer)
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(targetFileStream))
            {
                targetFileStream.Write(buffer, 0, buffer.Length);
            }
        }

        
        /// <summary>
        /// Check if a file is encrypted by this class by DefaultID.
        /// </summary>
        /// <param name="filePath">The full path of the file to check.</param>
        /// <returns>True if is encrypted by this class by DefaultID. Otherwise return false.</returns>
        public bool IsEncryptedByMe(string filePath)
        {
            if (new FileInfo(filePath).Length < 80) //If the Header, Key and IV are guid string,the length of an encrypted file is at least 80.
            {
                return false;
            }

            using (Stream tempSourceStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Stream outputStream = CreateDecryptStream(tempSourceStream); //CryptoStream can't be closed before reached to end. So just close the tempSourceStream in it.
                if (!VerifyHeader(ref outputStream))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Used to encrypt a file.
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        public void EncryptFileToFile(string sourcePath, string targetPath)
        {
            using (Stream source = EnDecryptStreamHelper.Instance.CreateCryptoFlStream(sourcePath, FileAccess.Read))
            {
                using(Stream target = EnDecryptStreamHelper.Instance.CreateCryptoFlStream(targetPath, FileAccess.Write))
                {
                    byte[] buffer = new byte[1024 * 1024];
                    int dataLength;
                    while ((dataLength = source.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        target.Write(buffer, 0, dataLength);
                    }
                }
            }
        }

        /// <summary>
        /// Used to decrypt a file.
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        public void DecryptFileToFile(string sourcePath, string targetPath)
        {
            using (Stream source = EnDecryptStreamHelper.Instance.CreateCryptoFlStream(sourcePath, FileAccess.Read))
            {
                using (Stream target = new FileStream(targetPath, FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = new byte[10 * 1024 * 1024];
                    int dataLength;
                    while ((dataLength = source.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        target.Write(buffer, 0, dataLength);
                    }



                }
            }
        }

        private static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        #region fix warning CA1001
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            //release unmanaged resources
            cmCrypto.Dispose();

            //release managed resources if disposing equals true
            if (disposing)
            {
                //do nothing
            }
        }


        ~EnDecryptStreamHelper()
        {
            Dispose(false);
        }
        #endregion
    }

}

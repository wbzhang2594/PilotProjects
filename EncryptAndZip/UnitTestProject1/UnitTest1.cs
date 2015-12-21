using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Motorola.Common.CommonLib;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private static byte[] InitByteArray_Ordinary_largerThan80()
        {
            byte[] inputByteArray = new byte[] {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,};
            return inputByteArray;
        }

        private static readonly byte[] headerData =
        {
            0x0c, 0x89, 0x62, 0x41, 0x06, 0x5f, 0xaf, 0x79
        };
        private static readonly byte[] headerData2 =
        {
            0xb9, 0x29, 0xc8, 0x7a, 0x86, 0xda, 0xc4, 0x98
        };

        [TestMethod]
        public void TestMethod1()
        {
            byte[] bytearray = InitByteArray_Ordinary_largerThan80();



            using (MemoryStream ms = new MemoryStream(bytearray))
            {
                // Create a new DES key.
                //DESCryptoServiceProvider key = new DESCryptoServiceProvider();
                RijndaelManaged key = new RijndaelManaged();

                byte[] ky = GetKey(defaultID);
                // Create a DecryptoStream using the memory stream and a DES key. 
                using (CryptoStream deCrypt = new CryptoStream(ms, key.CreateDecryptor(ky, ky), CryptoStreamMode.Read))
                {

                    byte[] tmpByteArray = new byte[15];

                    deCrypt.Read(tmpByteArray, 0, tmpByteArray.Length);
                    deCrypt.Close();
                }
            }
        }

        private RijndaelManaged cmCrypto;
        private const string CrytoKey = "{E88707E6-353F-46F3-8021-C4597593FA48}";
        private const int keySize = 128;
        private const string defaultID = "{F05EECA8-6D69-4079-94F2-516473BF57A2}";

        private const string Header = "{F2B1CF7F-1F5B-406F-B7B9-D3FD76231055}";

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

        [TestMethod]
        public void TestMethod2()
        {
            byte[] inputByteArray = InitByteArray_Ordinary_largerThan80();
            byte[] outputArray = EnDecryptStreamHelper.Instance.Decrypt_then_UnZip_ByteArray(inputByteArray);
        }

        [TestMethod]
        public void StringZipToString()
        {
            string path = @"D:\_Test\xmlZip";

            DirectoryInfo dir = new DirectoryInfo(path);

            StringBuilder inputString = new StringBuilder();

            foreach (FileInfo fi in dir.GetFiles())
            {
                using (FileStream fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    byte[] buffer = new byte[10 * 1024];
                    int dataLength;
                    while ((dataLength = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        inputString.Append(GetString(buffer));
                    }
                }


                string xml_input = inputString.ToString();

                byte[] byte_input = GetBytes(xml_input);

                byte[] byte_ziped = EnDecryptStreamHelper.ZipByteArray(byte_input);

                string xml_ziped = GetString(byte_ziped);

                byte[] byte_ziped_output = GetBytes(xml_ziped);

                byte[] byte_unziped_output = EnDecryptStreamHelper.UnZipByteArray(byte_ziped_output);

                string xml_unziped_output = GetString(byte_unziped_output);

                Console.WriteLine(string.Format("File {0}, zip from {1} size, to {2} size. Zip rate: {3}%", fi.Name, xml_input.Length, xml_ziped.Length, (int)((double)xml_ziped.Length / (double)xml_input.Length * 100)));
            }
        }

        private string filePassword;
        public string FilePassword
        {
            get
            {
                return filePassword;
            }
            set
            {
                filePassword = value;
            }
        }

        [TestMethod]
        public void nullStringCompare()
        {
            string strNull = FilePassword;
            string str2 = "str2";
            if(strNull.Equals(str2))
            {

            }


        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char) + 1];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static string Serialize_ToXMLString(object obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                XmlSerializer xs = new XmlSerializer(obj.GetType());
                xs.Serialize(memoryStream, obj);

                return _Encoding.GetString(memoryStream.ToArray());
            }
        }
        private readonly static Encoding _Encoding = new UTF8Encoding();

    }
}

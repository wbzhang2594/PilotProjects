using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Motorola.Common.CommonLib;

namespace EncryptAndZip
{
    class Program
    {
        private static byte[] InitByteArray_Ordinary()
        {
            byte[] inputByteArray = new byte[] { 
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
            return inputByteArray;
        }


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

        static byte[] initByteArray_EZ()
        {
            byte[] result = new byte[96];
            result[0] = 45;
            result[1] = 102;
            result[2] = 230;
            result[3] = 53;
            result[4] = 185;
            result[5] = 212;
            result[6] = 87;
            result[7] = 86;
            result[8] = 59;
            result[9] = 145;
            result[10] = 54;
            result[11] = 135;
            result[12] = 83;
            result[13] = 110;
            result[14] = 248;
            result[15] = 221;
            result[16] = 193;
            result[17] = 96;
            result[18] = 137;
            result[19] = 36;
            result[20] = 82;
            result[21] = 254;
            result[22] = 72;
            result[23] = 105;
            result[24] = 92;
            result[25] = 151;
            result[26] = 216;
            result[27] = 32;
            result[28] = 145;
            result[29] = 38;
            result[30] = 45;
            result[31] = 132;
            result[32] = 166;
            result[33] = 232;
            result[34] = 46;
            result[35] = 78;
            result[36] = 219;
            result[37] = 10;
            result[38] = 88;
            result[39] = 34;
            result[40] = 207;
            result[41] = 216;
            result[42] = 159;
            result[43] = 248;
            result[44] = 254;
            result[45] = 203;
            result[46] = 182;
            result[47] = 99;
            result[48] = 13;
            result[49] = 253;
            result[50] = 204;
            result[51] = 200;
            result[52] = 145;
            result[53] = 175;
            result[54] = 164;
            result[55] = 46;
            result[56] = 230;
            result[57] = 127;
            result[58] = 27;
            result[59] = 1;
            result[60] = 118;
            result[61] = 220;
            result[62] = 205;
            result[63] = 10;
            result[64] = 168;
            result[65] = 142;
            result[66] = 115;
            result[67] = 140;
            result[68] = 226;
            result[69] = 79;
            result[70] = 26;
            result[71] = 109;
            result[72] = 182;
            result[73] = 195;
            result[74] = 132;
            result[75] = 203;
            result[76] = 171;
            result[77] = 102;
            result[78] = 5;
            result[79] = 131;
            result[80] = 251;
            result[81] = 198;
            result[82] = 251;
            result[83] = 203;
            result[84] = 82;
            result[85] = 195;
            result[86] = 142;
            result[87] = 118;
            result[88] = 61;
            result[89] = 249;
            result[90] = 246;
            result[91] = 159;
            result[92] = 181;
            result[93] = 160;
            result[94] = 220;
            result[95] = 111;

            return result;
        }

        static void Main(string[] args)
        {
            //byte[] inputByteArray = new byte[0];
            byte[] inputByteArray = InitByteArray_Ordinary();

            //byte[] inputByteArray = initByteArray_EZ();


            #region Zip only
            //Zip only
            {
                byte[] outputByteArray = ZipByteArray(inputByteArray);

                byte[] outputByteArray2 = UnZipByteArray(outputByteArray);

                var o3 = outputByteArray2;
            }
            #endregion

            #region Crypto only
            //Crypto only
            {
                byte[] outputByteArray = EnDecryptStreamHelper.Instance.Zip_then_Encrypt_ByteArray(inputByteArray);

                byte[] outputByteArray2 = EnDecryptStreamHelper.Instance.Decrypt_then_UnZip_ByteArray(outputByteArray);


                var o3 = outputByteArray2;

            }
            #endregion

            //{
            //    byte[] outputByteArray = EnDecryptStreamHelper.Instance.EncryptByteArray(inputByteArray);
            //    byte[] outputByteArray2 = ZipByteArray(outputByteArray);

            //    byte[] outputByteArray3 = UnZipByteArray(outputByteArray2);
            //    byte[] outputByteArray4 = EnDecryptStreamHelper.Instance.DecryptByteArray(outputByteArray3);
            //}

            {
                byte[] outputByteArray = ZipByteArray(inputByteArray);
                byte[] outputByteArray11 = ZipByteArray(outputByteArray);
                byte[] outputByteArray111 = ZipByteArray(outputByteArray11);

                byte[] outputByteArray2 = EnDecryptStreamHelper.Instance.Zip_then_Encrypt_ByteArray(inputByteArray);

                byte[] outputByteArray3 = EnDecryptStreamHelper.Instance.Decrypt_then_UnZip_ByteArray(outputByteArray2);
            }

            {
                byte[] originalByteArray = InitByteArray_Ordinary();
                byte[] originalByteArray_largerThan80 = InitByteArray_Ordinary_largerThan80();
                byte[] EZ_ByteArray = initByteArray_EZ();

                byte[] result1 = EnDecryptStreamHelper.Instance.Decrypt_then_UnZip_ByteArray(originalByteArray);

                //ulong count = 0;
                //do
                //{
                //byte[] result_largerThan80 = EnDecryptStreamHelper.Instance.Decrypt_then_UnZip_ByteArray(originalByteArray_largerThan80);
                //    Console.WriteLine(++count);
                //    Thread.Sleep(10);
                //}
                //while (true);
                byte[] result2 = EnDecryptStreamHelper.Instance.Decrypt_then_UnZip_ByteArray(EZ_ByteArray);

                if (result1.SequenceEqual(result2))
                {

                }
            }

            {
                string InputFile_de = @"C:\new.xml";
                string outputFile_de = @"c:\new_decrypt.xml";
                string outputFile_en = @"c:\new_encrypt.xml";

                ulong count = 0;
                byte[] originalByteArray_largerThan80 = InitByteArray_Ordinary_largerThan80();

                do
                {
                    byte[] result_largerThan80 = EnDecryptStreamHelper.Instance.Decrypt_then_UnZip_ByteArray(originalByteArray_largerThan80);

                    EnDecryptStreamHelper.Instance.DecryptFileToFile(InputFile_de, outputFile_de);

                    EnDecryptStreamHelper.Instance.EncryptFileToFile(outputFile_de, outputFile_en);

                    EnDecryptStreamHelper.Instance.DecryptFileToFile(outputFile_en, outputFile_de);


                    Console.WriteLine(++count);
                    Thread.Sleep(10);
                } while (true);
            }

            {
                byte[] originalByteArray_largerThan80 = InitByteArray_Ordinary_largerThan80();
                string outputFile = @"c:\new_decrypt.xml";

                int count = 0;
                do
                {

                    WriteBytesToFile(outputFile, originalByteArray_largerThan80);
                    Console.WriteLine(++count);
                    Thread.Sleep(10);
                }
                while (true);
            }
        }







        private static byte[] UnZipByteArray(byte[] outputByteArray)
        {
            byte[] outputByteArray2 = null;
            using (MemoryStream outMemStream = new MemoryStream())
            {
                using (MemoryStream memStream = new MemoryStream(outputByteArray))
                {
                    DeflateStream zipStream = new DeflateStream(memStream, CompressionMode.Decompress, true);

                    zipStream.CopyTo(outMemStream);

                }
                outputByteArray2 = outMemStream.ToArray();
            }
            return outputByteArray2;
        }

        private static byte[] ZipByteArray(byte[] inputByteArray)
        {
            byte[] outputByteArray = null;
            using (MemoryStream memStream = new MemoryStream(inputByteArray))
            {
                using (MemoryStream outMemStream = new MemoryStream())
                {
                    DeflateStream zipStream = new DeflateStream(outMemStream, CompressionLevel.Fastest, true);
                    memStream.CopyTo(zipStream);

                    outputByteArray = outMemStream.ToArray();
                }
            }
            return outputByteArray;
        }

        private static void WriteBytesToFile(string targetFilePath, byte[] buffer)
        {
            using (FileStream fs = new FileStream(targetFilePath, FileMode.Create, FileAccess.Write))
            {
                BinaryWriter binaryWriter = new BinaryWriter(fs);
                binaryWriter.Write(buffer);
            }
        }
    }
}

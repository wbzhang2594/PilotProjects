using System;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Motorola.CommonCPS.Server.EntityModel.GenericModel;

namespace UnitTestProject1
{
    [TestClass]
    public class SerializeAndDererialize
    {
        [TestMethod]
        public void TestMethod1()
        {
            string path = Path.GetFullPath(@"TestData/ASTRO_DeviceInfo.xml");

            StreamReader streamReader = new StreamReader(path);
            string text = streamReader.ReadToEnd();
            streamReader.Close();

            MultipleFieldsXML result = MultipleFieldsXML.DeserializeFromXML(text);

            string output1 = result.SerializeToXML();
            MultipleFieldsXML result2 = MultipleFieldsXML.DeserializeFromXML(output1);

        }

        [TestMethod]
        public void SerializeWithDiffCulture()
        {
            string path = Path.GetFullPath(@"TestData/ASTRO_DeviceInfo.xml");

            StreamReader streamReader = new StreamReader(path);
            string text = streamReader.ReadToEnd();
            streamReader.Close();

            MultipleFieldsXML result = MultipleFieldsXML.DeserializeFromXML(text);
            double testNumericType = 2.222222;
            result.NameAndValueList.Add(new NameAndValue("Numeric", testNumericType));
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("de");
            string output1 = result.SerializeToXML();

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
            MultipleFieldsXML result2 = MultipleFieldsXML.DeserializeFromXML(output1);
        }
    }
}

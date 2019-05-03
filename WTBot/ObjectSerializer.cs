using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace WTBot
{
    class ObjectSerializer
    {
        public static void SerializeRawData<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        public static T DeSerializeRawData<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }
            T objectOut = default(T);
            try
            {
                string attributeXml = string.Empty;

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return objectOut;
        }

        public static void SerializeAndCipher<T>(T serializableObject, string fileName)
        {

            string xmlCode = "";
            if (serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlCode = xmlDocument.OuterXml;
                    
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            xmlCode = cipherString(xmlCode);
            File.WriteAllText(fileName, xmlCode);
        }

        public static T DeserializeCiphered<T>(string fileName)
        {
            string dataToDecipher = File.ReadAllText(fileName);
            string deciphered = decipherString(dataToDecipher);


            if (string.IsNullOrEmpty(fileName)) { return default(T); }
            T objectOut = default(T);
            try
            {
                string xmlString = deciphered;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return objectOut;


        }

        public static string cipherString(string dataToCipher)
        {
            string cpuInfo = "";
            ManagementClass managClass = new ManagementClass("win32_processor");
            ManagementObjectCollection managCollec = managClass.GetInstances();
            foreach (ManagementObject managObj in managCollec)
            {
                cpuInfo = managObj.Properties["processorID"].Value.ToString();
                break;
            }
            char[] chars = cpuInfo.ToCharArray();
            int[] addVals = new int[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                addVals[i] = chars[i] % 16;
            }

            char[] serializedCharArr = dataToCipher.ToCharArray();
            int valsCount = addVals.Length;

            for (int i = 0; i < serializedCharArr.Length; i++)
            {
                serializedCharArr[i] += (char)addVals[i % valsCount];
            }
            return new string(serializedCharArr);
        }

        public static string decipherString(string dataToDecipher)
        {
            string cpuInfo = "";
            ManagementClass managClass = new ManagementClass("win32_processor");
            ManagementObjectCollection managCollec = managClass.GetInstances();
            foreach (ManagementObject managObj in managCollec)
            {
                cpuInfo = managObj.Properties["processorID"].Value.ToString();
                break;
            }
            char[] chars = cpuInfo.ToCharArray();
            int[] addVals = new int[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                addVals[i] = chars[i] % 16;
            }

            char[] serializedCharArr = dataToDecipher.ToCharArray();
            int valsCount = addVals.Length;

            for (int i = 0; i < serializedCharArr.Length; i++)
            {
                serializedCharArr[i] -= (char)addVals[i % valsCount];
            }
            return new string(serializedCharArr);
        }


    }
}

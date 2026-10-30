/****************************************************************************************************************************************
 * XMLSerializer class: 
 * Desc:    This class is to serialize (write) and deserialize (read) setting datas, from and to xml files. 
 * Note:
 *               (usually C++ uses ini files but Microsoft recommend the usage of xml files to store data)
 *               (the usage of a static class to read and write xml files seems to have zero to very little memory leak if compared to a non static class)
 *               (the memory leak seems to come from the creation of the serializer class object)
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace StaticCoreLib
{
    public static class XMLSerializer
    {
        //Write to XML file (Serialize)
        public static void Serialize_Func<T>(T xmlObject, string strPath) where T : new()
        {
            //create directory if it does not exist
            String strFilePath = strPath.Remove(strPath.LastIndexOf("\\"));
            if (Directory.Exists(strFilePath) == false) Directory.CreateDirectory(strFilePath);

            //delete the file: if the total xml string is less or more than the current, additional symbols or things will appear. This will discrupt the serializer.
            File.Delete(strPath);

            //write to xml file
            XmlSerializer xs = new XmlSerializer(typeof(T));
            FileStream fs = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Write);
            xs.Serialize(fs, xmlObject);
            fs.Close();
            return;
        }

        //Read from XML file (Deserialize)
        public static T Deserialize_Func<T>(string strPath) where T : new()
        {
            T xmlObject = new T();
            if (File.Exists(strPath) == false) return xmlObject;
            
            XmlSerializer xs = new XmlSerializer(typeof(T));
            FileStream fs = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Read);
            xmlObject = (T)xs.Deserialize(fs);
            fs.Close();
            return xmlObject;
        }
    }
}

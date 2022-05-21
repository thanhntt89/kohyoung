using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Lab3
{
    class Program
    {
        //File path to save
        private static string saveToFilePath = string.Format("{0}\\person.bin", Path.GetDirectoryName(
System.Reflection.Assembly.GetExecutingAssembly().Location));

        static void Main(string[] args)
        {
            //Create person info
            Person person = new Person() { Id = 20072597, FristName = "Thanh", LastName = "Nguyen Tat " };

            //Save struct
            SavePerson(person, saveToFilePath);

            Console.WriteLine(string.Format("Write to file success!!\nFilePath: {0} ", saveToFilePath));

            //Read struct
            Person person_reader = GetPerson(saveToFilePath);
            string data = string.Format("Id:{0} | First Name:{1} | Last Name: {2}\n", person_reader.Id, person_reader.FristName, person_reader.LastName);

            Console.WriteLine("Read file success!!\n" + data);

            Console.ReadLine();
        }

        /// <summary>
        /// Write struct to files
        /// </summary>
        /// <param name="person"></param>
        /// <param name="saveToFilePath"></param>
        static void SavePerson(Person person, string saveToFilePath)
        {
            using (Stream stream = new FileStream(saveToFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                using (BinaryWriter writer = new BinaryWriter(stream, Encoding.Default))
                {
                    byte[] newBuffer = StructUtils.Serialize<Person>(person);
                    writer.Write(newBuffer);
                }
            }
        }

        /// <summary>
        /// Read person from files
        /// </summary>
        /// <param name="readerPath">File input</param>
        /// <returns></returns>
        static Person GetPerson(string readerPath)
        {
            //Check file exist
            if (!File.Exists(readerPath))
            {
                return new Person();
            }
            Person person = StructUtils.Deserialize<Person>(File.ReadAllBytes(readerPath));
            return person;
        }
    }

    /// <summary>
    /// Sharing class create functions use for struct
    /// </summary>
    public class StructUtils
    {
        /// <summary>
        /// Convert struct to byte
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] Serialize<T>(T s)
     where T : struct
        {
            var size = Marshal.SizeOf(typeof(T));
            var array = new byte[size];
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(s, ptr, true);
            Marshal.Copy(ptr, array, 0, size);
            Marshal.FreeHGlobal(ptr);
            return array;
        }

        /// <summary>
        /// Convert bytes to struct
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T Deserialize<T>(byte[] array)
            where T : struct
        {
            var size = Marshal.SizeOf(typeof(T));
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(array, 0, ptr, size);
            var s = (T)Marshal.PtrToStructure(ptr, typeof(T));
            Marshal.FreeHGlobal(ptr);
            return s;
        }

        //Prevent from create new object
        private StructUtils()
        {

        }
    }

    /// <summary>
    /// Person struct
    /// </summary>
    [Serializable]
    public struct Person
    {
        public int Id;
        //Must set before string field
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string FristName;
        //Must set before string field
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string LastName;
    }
}

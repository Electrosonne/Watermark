using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClientLoader
{
    static class StreamSaver
    {
        /// <summary>
        /// Сохраняет Stream 
        /// </summary>
        /// <param name="path">Путь сохранения</param>
        /// <param name="data">Сохраняемый Stream</param>
        public static void Save(string path, Stream data)
        {            
            using (var writer = File.Create(path))
            {
                data.CopyTo(writer);
            }

            Console.WriteLine("Файл сохранен");
        }
    }
}

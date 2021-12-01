using RestSharp;
using System;
using System.IO;
using System.Net;

namespace ClientLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Введите адрес сервера:");
                var serverUrl = Console.ReadLine();

                Console.Write("Введите путь картинки:");
                var pathImage = Console.ReadLine();
                var pathWaterMark = Directory.GetCurrentDirectory() + "\\" + Path.GetFileName(pathImage);

                StreamSaver.Save(pathWaterMark, Server.AddWaterMark(pathImage, serverUrl));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

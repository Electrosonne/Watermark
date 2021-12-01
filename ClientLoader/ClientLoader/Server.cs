using System;
using System.IO;
using System.Net;

namespace ClientLoader
{
    static class Server
    {
        /// <summary>
        /// Отправляет на сервер картинку и возвращает ее с водяным знаком
        /// </summary>
        /// <param name="pathImage">Путь до картинки</param>
        /// <param name="serverUrl">Url сервера</param>
        /// <returns></returns>
        public static Stream AddWaterMark(string pathImage, string serverUrl = "")
        {
            var streamImage = new FileStream(pathImage, FileMode.Open, FileAccess.Read);

            var request = WebRequest.Create(serverUrl);
            request.Method = "POST";            
            request.Headers.Add("Name", Path.GetFileName(pathImage));

            //Добавление в запрос файл картинки
            request.ContentLength = streamImage.Length;
            Stream streamRequest = request.GetRequestStream();
            streamImage.CopyTo(streamRequest);
            streamRequest.Close();

            Console.WriteLine("Отправлен запрос");

            var responce = request.GetResponse();

            Console.WriteLine("Получен ответ");

            return responce.GetResponseStream();
        }    
    }
}

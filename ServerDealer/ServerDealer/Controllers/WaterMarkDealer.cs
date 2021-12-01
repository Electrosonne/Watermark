using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace ServerDealer.Controllers
{
    [ApiController]
    [Route("")]
    public class WaterMarkDealer : ControllerBase
    {
        [HttpPost("")]
        public IActionResult GetImageWaterMark()
        {
            try
            {
                var context = HttpContext;
                context.Request.EnableBuffering();

                var name = context.Request.Headers["Name"].ToString();

                //Сохранение на сервере исходника
                var dir = $"{Directory.GetCurrentDirectory()}\\Image";

                if(!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                var path = $"{dir}\\{name}";

                using (var writer = System.IO.File.Create(path))
                {
                    context.Request.Body.CopyTo(writer);
                }
                context.Request.Body.Seek(0, SeekOrigin.Begin);

                // Добавить подпись для картинки
                string text = DateTime.Now.ToString();

                return new FileStreamResult(Watermark.SetTextWaterMark(context.Request.Body, text, name), "image/jpeg");
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

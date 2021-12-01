using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ServerDealer
{
    public static class Watermark
    {
        /// <summary>
        /// Добавляет текстовый водяной знак в правом нижнем углу
        /// </summary>
        /// <param name="file"></param>
        /// <param name="text"></param>
        /// <param name="extension"></param>
        /// <param name="fontSize"></param>
        /// <param name="fontName"></param>
        /// <returns></returns>
        public static Stream SetTextWaterMark(Stream file, string text, string extension, int fontSize = 20, string fontName = "Times New Roman")
        {
            MemoryStream result = new MemoryStream();

            Image image = Image.FromStream(file, true, true);
            int width = image.Width;
            int height = image.Height;

            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                //Основная картинка
                graphics.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel);

                //Шрифт
                Font font = new Font(fontName, fontSize, FontStyle.Bold);

                //Размеры надписи
                SizeF size = graphics.MeasureString(text, font);

                //Правый нижний угол
                float positionY = height - size.Height;
                float positionX = width - size.Width;

                //Цвет текста
                SolidBrush brush = new SolidBrush(Color.FromArgb(100, 255, 255, 255));

                //Добавление текста
                graphics.DrawString(text, font, brush, new PointF(positionX, positionY));

                // Сохранить картинку
                bitmap.Save(result, extension.Contains("png") ? ImageFormat.Png : ImageFormat.Jpeg);
            }

            result.Position = 0;
            return result;
        }
    }
}

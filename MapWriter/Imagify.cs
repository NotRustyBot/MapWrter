using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MapWriter
{
    class Imagify
    {
        static List<string> colors = new List<string>();
        static List<Color> colorspace = new List<Color>();
        static string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefgijklmnopqrstuvwxyz0123456789";
        static Imagify()
        {
            colorspace.Add(Color.FromArgb(255, 192, 192, 200));
            colorspace.Add(Color.FromArgb(255, 255, 72, 0));
            colorspace.Add(Color.FromArgb(255, 255, 104, 104));
            colorspace.Add(Color.FromArgb(255, 0, 176, 16));
            colorspace.Add(Color.FromArgb(255, 64, 64, 255));
            colorspace.Add(Color.FromArgb(255, 255, 116, 32));
            colorspace.Add(Color.FromArgb(255, 255, 192, 40));
            colorspace.Add(Color.FromArgb(255, 136, 128, 80));
            colorspace.Add(Color.FromArgb(255, 24, 144, 160));
            colorspace.Add(Color.FromArgb(255, 224, 64, 224));
            colorspace.Add(Color.FromArgb(255, 96, 96, 104));
            colorspace.Add(Color.FromArgb(255, 200, 32, 0));
            colorspace.Add(Color.FromArgb(255, 255, 64, 64));
            colorspace.Add(Color.FromArgb(255, 0, 112, 12));
            colorspace.Add(Color.FromArgb(255, 56, 56, 192));
            colorspace.Add(Color.FromArgb(255, 180, 176, 116));
            colorspace.Add(Color.FromArgb(255, 208, 176, 0));
            colorspace.Add(Color.FromArgb(255, 96, 88, 48));
            colorspace.Add(Color.FromArgb(255, 16, 104, 104));
            colorspace.Add(Color.FromArgb(255, 128, 16, 128));
            colorspace.Add(Color.FromArgb(255, 64, 64, 72));
            colorspace.Add(Color.FromArgb(255, 136, 24, 0));
            colorspace.Add(Color.FromArgb(255, 160, 24, 24));
            colorspace.Add(Color.FromArgb(255, 0, 80, 8));
            colorspace.Add(Color.FromArgb(255, 32, 32, 104));
            colorspace.Add(Color.FromArgb(255, 104, 96, 8));
            colorspace.Add(Color.FromArgb(255, 160, 80, 0));
            colorspace.Add(Color.FromArgb(255, 64, 56, 16));
            colorspace.Add(Color.FromArgb(255, 16, 64, 64));
            colorspace.Add(Color.FromArgb(255, 96, 0, 96));
            colorspace.Add(Color.FromArgb(255, 16, 16, 16));
            colorspace.Add(Color.FromArgb(255, 72, 8, 0));
            colorspace.Add(Color.FromArgb(255, 80, 4, 4));
            colorspace.Add(Color.FromArgb(255, 0, 24, 0));
            colorspace.Add(Color.FromArgb(255, 16, 16, 26));
            colorspace.Add(Color.FromArgb(255, 64, 32, 0));
            colorspace.Add(Color.FromArgb(255, 80, 24, 0));
            colorspace.Add(Color.FromArgb(255, 16, 12, 4));
            colorspace.Add(Color.FromArgb(255, 4, 16, 16));
            colorspace.Add(Color.FromArgb(255, 32, 0, 32));
            colorspace.Add(Color.FromArgb(255, 0, 0, 0));
            {
                colors.Add("BgLightGray");
                colors.Add("BgLightRed");
                colors.Add("BgLightPink");
                colors.Add("BgLightGreen");
                colors.Add("BgLightBlue");
                colors.Add("BgLightYellow");
                colors.Add("BgLightOrange");
                colors.Add("BgLightBrown");
                colors.Add("BgLightCyan");
                colors.Add("BgLightMagenta");
                colors.Add("BgGray");
                colors.Add("BgRed");
                colors.Add("BgPink");
                colors.Add("BgGreen");
                colors.Add("BgBlue");
                colors.Add("BgYellow");
                colors.Add("BgOrange");
                colors.Add("BgBrown");
                colors.Add("BgCyan");
                colors.Add("BgMagenta");
                colors.Add("BgDarkGray");
                colors.Add("BgDarkRed");
                colors.Add("BgDarkPink");
                colors.Add("BgDarkGreen");
                colors.Add("BgDarkBlue");
                colors.Add("BgDarkYellow");
                colors.Add("BgDarkOrange");
                colors.Add("BgDarkBrown");
                colors.Add("BgDarkCyan");
                colors.Add("BgDarkMagenta");
                colors.Add("BgBlackGray");
                colors.Add("BgBlackRed");
                colors.Add("BgBlackPink");
                colors.Add("BgBlackGreen");
                colors.Add("BgBlackBlue");
                colors.Add("BgBlackYellow");
                colors.Add("BgBlackOrange");
                colors.Add("BgBlackBrown");
                colors.Add("BgBlackCyan");
                colors.Add("BgBlackMagenta");
                colors.Add("Black");
            }
        }
        public static void FromTextFile(string path, MapWriter mapWriter)
        {
            string data = File.ReadAllText(path);
            Console.WriteLine(int.Parse(data.Split('/')[0]));
            Create(data.Split('/')[1].Trim(), int.Parse(data.Split('/')[0]), mapWriter);
        }

        public static void Create(string data, int width, MapWriter mapWriter)
        {

            for (int i = 0; i < data.Length; i++)
            {
                int ti = (i * 7919) % data.Length;
                mapWriter.AddTile(new Tile()
                {
                    Name = "BGSYMBOL00DOT",
                    Category = "BG",
                    PositionX = (ti % width - width / 2),
                    PositionY = (width / 2 - (ti / width)),
                    Color1 = colors[letters.IndexOf(data[ti])],
                });
            }
        }

        public static void FromImage(string path, MapWriter mapWriter, float xOffset = 0, float yOffset = 0)
        {
            Bitmap image = new Bitmap(path);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color c = image.GetPixel(x, y);
                    if(c.A > 0)
                    mapWriter.AddTile(new Tile()
                    {
                        Name = "BGSYMBOL00DOT",
                        Category = "BG",
                        PositionX = x  + xOffset,
                        PositionY = -(y - yOffset),
                        Color1 = colors[GetClosestColor(c)],
                    });
                }
            }
        }

        static int GetClosestColor(Color source)
        {
            int closest = 0;
            double distance = 99999999;
            for (int i = 0; i < colorspace.Count; i++)
            {
                Color c = colorspace[i];
                double tempDist = Math.Pow(((c.R - source.R) * 0.30), 2) + Math.Pow(((c.G - source.G) * 0.59), 2) + Math.Pow(((c.B - source.B) * 0.11), 2);
                if (tempDist < distance)
                {
                    distance = tempDist;
                    closest = i;
                }
            }
            return closest;
        }
    }
}

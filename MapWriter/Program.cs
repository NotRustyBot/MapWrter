using System;
using System.IO;

namespace MapWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            MapWriter mapWriter = new MapWriter()
            {
                MapName = "AutoGen",
                MapAuthor = "",
                IsTemplate = true,
            };

            mapWriter.AddTile(new Tile()
            {
                Name = "BgWindow00A",
                Color2 = "White",
                PositionX = -20f,
                PositionY = 12f,
                SizeX = 8,
                SizeY = 6,
            });

            for (int i = 0; i < 32; i++)
            {
                float angle = (float)(i / 32f * Math.PI * 2f);
                mapWriter.AddTile(new Tile()
                {
                    PositionX = (float)Math.Cos(angle) * i,
                    PositionY = (float)Math.Sin(angle) * i,
                    Name = "BgAdobe00E",
                    Color1 = "BgLightBlue",
                    Rotation = (float)(angle + Math.PI / 2f)
                });
            }
            Imagify.FromImage("Mythologic.png", mapWriter, -16, -32);
            mapWriter.Serialise();
            mapWriter.SaveToFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Superfighters Deluxe\\Maps\\Custom\\autogen.sfdm");

        }
    }
}

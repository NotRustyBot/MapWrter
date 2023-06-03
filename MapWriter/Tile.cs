using System;
using System.Collections.Generic;
using System.Text;

namespace MapWriter
{
    class Tile : DataBuilder
    {

        public void Parse()
        {
            WriteInt(0);
            WriteString(Name);

            WriteInt(1);
            WriteInt(0);
            WriteByte(10);
            WriteByte(0);
            WriteInt(0);

            WriteInt(2);
            WriteFloat(PositionX);
            WriteFloat(PositionY);

            WriteInt(3);
            WriteFloat(Rotation);

            WriteInt(4);
            WriteInt(LayerId);

            WriteInt(5);
            WriteInt(FaceDirection);

            WriteInt(6);
            WriteInt(3);
            WriteString(Color1);
            WriteString(Color2);
            WriteString(Color3);

            WriteInt(7);

            WriteInt(8);
            WriteInt(1);
            WriteInt(0);
            WriteByte(0);

            WriteInt(51);
            WriteInt(2);
            WriteInt(Dynamic ? 1 : 0);

            WriteInt(19);
            WriteInt(2);
            WriteInt(SizeX);

            WriteInt(20);
            WriteInt(2);
            WriteInt(SizeY);

            UTVW();

            WriteInt(9999);

        }

        public string Name { get; set; } = "ADOBE00A";
        public float PositionX { get; set; } = 0;
        public float PositionY { get; set; } = 0;
        public float Rotation { get; set; } = 0;
        public string Color1 { get; set; } = "StoneGray";
        public string Color2 { get; set; } = "";
        public string Color3 { get; set; } = "";
        public string CustomId { get; set; } = "";
        public bool Dynamic { get; set; } = false;
        public int SizeX { get; set; } = 1;
        public int SizeY { get; set; } = 1;
        public int FaceDirection { get; set; } = 1;
        public int LayerId { get; set; } = 0;

        public string Category { get; set; } = "SLD";

    }
}

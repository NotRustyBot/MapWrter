using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace MapWriter
{
    abstract class DataBuilder
    {
        List<byte> data = new List<byte>();

        protected void WriteByte(byte b)
        {
            data.Add(b);
        }

        protected void WriteBytes(byte[] b)
        {
            data.AddRange(b);
        }

        protected void WriteInt(int i)
        {
            data.AddRange(BitConverter.GetBytes(i));
        }

        protected void WriteFloat(float f)
        {
            data.AddRange(BitConverter.GetBytes(f));

        }

        protected void WriteString(string s)
        {
            WriteByte((byte)s.Length);
            foreach (char c in s)
            {
                WriteByte((byte)c);
            }
        }

        protected void WriteConstantString(string s)
        {
            foreach (char c in s)
            {
                WriteByte((byte)c);
            }
        }


        protected void Append(DataBuilder dataBuilder)
        {
            data.AddRange(dataBuilder.data);
        }

        protected void Clear()
        {
            data.Clear();
        }

        public void SaveToFile(string filename)
        {
            WriteString("EOF");
            File.WriteAllBytes(filename, data.ToArray());
        }

        protected void UTVW()
        {
            WriteInt(341); //U
            WriteInt(1);
            WriteFloat(-1);
            WriteInt(340); //T
            WriteInt(3);
            WriteByte(0);
            WriteInt(342); //V
            WriteInt(0);
            WriteByte(0);
            WriteInt(343); //W
            WriteInt(0);
            WriteByte(0);
        }
    }
}

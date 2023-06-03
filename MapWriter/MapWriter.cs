using System;
using System.Collections.Generic;
using System.Text;


namespace MapWriter
{
    class MapWriter : DataBuilder
    {

        public bool IsTemplate { get; set; } = true;
        public bool IsEditorLocked { get; set; } = false;
        public string MapName { get; set; } = "Unnamed";
        public string MapAuthor { get; set; } = "Anonymous";
        public int MapType { get; set; } = 1;
        public int MaxPlayers { get; set; } = 0;
        public string MapTags { get; set; } = "";
        public bool IsDamaged { get; set; } = false;
        public DateTime CreatedDateTime { get; set; } = new DateTime();
        public string WorldCameraString { get; set; } = "240,-320,-240,320";
        public string WorldBottomString { get; set; } = "-320";
        public string WeatherString { get; set; } = "None";
        public string MapScript { get; set; } = "";

        Dictionary<string, Category> categories = new Dictionary<string, Category>();

        HashSet<string> uids = new HashSet<string>();
        List<Tile> tiles = new List<Tile>();

        public MapWriter() {
            GenerateCategories();
        }

        public void Serialise() {
            Header();
        }

        void GenerateCategories() {
            CreateCategory("FBG");
            CreateCategory("UNUSED1");
            CreateCategory("BG");
            CreateCategory("LN");
            CreateCategory("SLD");
            CreateCategory("OBJ");
            CreateCategory("DEB");
            CreateCategory("UNUSED24");
            CreateCategory("UNUSED7");
            CreateCategory("UNUSED8");
            CreateCategory("UNUSED9");
            CreateCategory("PLR");
            CreateCategory("UNUSED11");
            CreateCategory("UNUSED12");
            CreateCategory("UNUSED13");
            CreateCategory("UNUSED14");
            CreateCategory("UNUSED15");
            CreateCategory("UNUSED16");
            CreateCategory("UNUSED17");
            CreateCategory("ITM");
            CreateCategory("THRN");
            CreateCategory("FG");
            CreateCategory("UNUSED21");
            CreateCategory("UNUSED22");
            CreateCategory("UNUSED23");
            CreateCategory("MAR");
            CreateCategory("PN");
            CreateCategory("UNUSED26");
            CreateCategory("UNUSED28");
            CreateCategory("INV");

        }

        void CreateCategory(string name) { 
            categories.Add(name, new Category() {CategoryName = name });

        }
        public void AddTile(Tile tile, string layer = "new layer") {
            tile.LayerId = categories[tile.Category].AddToLayer(layer);
            if(tile.CustomId != "" && !uids.Contains(tile.CustomId))uids.Add(tile.CustomId);
            tiles.Add(tile);
        }

        void Header() {
            WriteString("h_gv");
            WriteBytes(Guid.NewGuid().ToByteArray());
            WriteString("v.1.3.7d");

            WriteString("h_or");
            WriteBytes(Guid.NewGuid().ToByteArray());
            WriteString("I don't know what I'm doing");

            WriteString("h_tmp");
            WriteByte((byte)(IsTemplate?0:1));

            WriteString("h_el");
            WriteByte((byte)(IsEditorLocked?1:0));

            WriteString("h_wn");
            WriteString(MapName);

            WriteString("h_wa");
            WriteString(MapAuthor);

            WriteString("h_mtp");
            WriteInt(MapType);
            WriteInt(MaxPlayers);

            WriteString("h_tg");
            WriteString(MapTags);

            WriteString("h_wd");
            WriteByte((byte)(IsDamaged ? 1 : 0));

            WriteString("h_wdt");
            if (CreatedDateTime == new DateTime()) CreatedDateTime = DateTime.Now;
            WriteInt(CreatedDateTime.Year);
            WriteInt(CreatedDateTime.Month);
            WriteInt(CreatedDateTime.Day);
            WriteInt(CreatedDateTime.Hour);
            WriteInt(CreatedDateTime.Minute);

            WriteString("h_pei");
            WriteByte(0);

            WriteString("h_mt");
            WriteConstantString("SFDMAPEDIT");

            WriteString("h_pt");
            WriteByte(1);
            WriteInt(0);
            WriteByte(1);
            WriteInt(0);
            Ceader();
        }

        void Ceader() {
            WriteString("c_wp");
            WriteInt(27);
            UTVW();
            WriteInt(3);
            WriteInt(0);
            WriteString(MapAuthor);
            WriteInt(2);
            WriteInt(0);
            WriteString(MapName);

            //??
            WriteInt(334);
            WriteInt(0);
            WriteByte(0);
            WriteInt(311);
            WriteInt(0);
            WriteByte(0);
            WriteInt(103);
            WriteInt(2);
            WriteInt(1);
            WriteInt(339);
            WriteInt(0);
            //

            WriteString("Versus,Custom,Campaign,Survival");

            //??
            WriteInt(331);
            WriteInt(3);
            WriteByte(0);
            WriteInt(337);
            WriteInt(0);
            WriteByte(0);
            WriteInt(332);
            WriteInt(3);
            WriteByte(0);
            WriteInt(262);
            WriteInt(2);
            WriteInt(0);
            WriteInt(330);
            WriteInt(3);
            WriteByte(0);
            WriteInt(259);
            WriteInt(0);
            WriteByte(0);
            WriteInt(260);
            WriteInt(3);
            WriteByte(1);
            WriteInt(333);
            WriteInt(0);
            WriteByte(0);
            WriteInt(8);
            WriteInt(0);
            //

            WriteString(WorldCameraString);
            WriteInt(9);
            WriteInt(0);
            WriteString(WorldBottomString);
            WriteInt(12);
            WriteInt(0);
            WriteString(WeatherString);

            //??
            WriteInt(118);
            WriteInt(0);
            WriteByte(0);
            WriteInt(61);
            WriteInt(0);
            WriteByte(0);
            WriteInt(209);
            WriteInt(3);
            WriteByte(0);
            WriteInt(294);
            WriteInt(3);
            WriteByte(1);
            WriteInt(208);
            WriteInt(3);
            WriteByte(1);
            WriteByte(2);
            WriteByte(1);
            WriteByte(0);
            WriteByte(0);
            WriteInt(2);
            WriteInt(0);

            WriteString("c_scrpt");
            WriteString(MapScript);

            WriteString("c_lr");
            WriteInt(30);
            foreach (Category category in categories.Values)
            {
                category.LayersData();
                Append(category);
            }
            WriteString("c_lrp");
            WriteInt(30);
            foreach (Category category in categories.Values)
            {
                WriteString(category.CategoryName);
                WriteInt(category.GetCount());
                for (int i = 0; i < category.GetCount(); i++)
                {
                    WriteInt(4);
                    UTVW();
                }

            }

            WriteString("c_tl");
            WriteInt(uids.Count);
            foreach (string uid in uids)
            {
                WriteString(uid);
                WriteInt(1);
            }

            WriteString("c_sobjs");
            WriteInt(tiles.Count);


            foreach (Tile tile in tiles)
            {
                tile.Parse();
                Append(tile);
            }

        }

    }
}

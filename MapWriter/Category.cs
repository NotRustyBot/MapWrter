using System;
using System.Collections.Generic;
using System.Text;

namespace MapWriter
{
    class Category : DataBuilder
    {
        Dictionary<string, Layer> layers = new Dictionary<string, Layer>();
        public string CategoryName { get; set; }
        public int AddToLayer(string layer) {
            if(!layers.ContainsKey(layer)) layers.Add(layer, new Layer() {name = layer});
            return layer.IndexOf(layer);
        }

        public int GetLayerIndex(string layer) {
            return layer.IndexOf(layer);
        }

        public void LayersData() {
            WriteString(CategoryName);
            WriteInt(GetCount());
            foreach (Layer layer in layers.Values)
            {
                WriteByte((byte)(layer.locked?1:0));
                WriteByte((byte)(layer.visible?1:0));
                WriteString(layer.name);
            }

        }

        public int GetCount() {
            return layers.Count;
        }
    }

    class Layer
    {
        public string name;
        public bool visible = true;
        public bool locked = false;
    }
}

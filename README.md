# MapWrter

Code for programatically creating Superfighters Deluxe maps.  
Expected usage is to write the code in the project itself, and then run it.  

## Usage
Create an instance of `MapWriter`, set the fields as needed
```cs
MapWriter mapWriter = new MapWriter()
{
    MapName = "AutoGen",
    IsTemplate = true,
};
```
  
Create instance of a `Tile`, set the fields as needed, and add it to the writer
```cs
Tile tile = new Tile()
{
    Name = "BgWindow00A",
    Color2 = "White",
    PositionX = -20f,
    PositionY = 12f,
    SizeX = 8,
    SizeY = 6
}

mapWriter.AddTile(tile);
```

Call the `Serialize` method, and then `SaveToFile`. 

```cs
mapWriter.Serialise();
mapWriter.SaveToFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+ "\\Superfighters Deluxe\\Maps\\Custom\\autogen.sfdm");
```

_Note: the map editor won't refresh automatically. Fastest way to refresh is to close and open the map edior._

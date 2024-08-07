using System.Collections.Generic;
using Unity.Properties;

[GeneratePropertyBag]
public class InventoryData
{
    [CreateProperty] public double Money { get; set; }
    // Had to cut keeping track of all the different kinds of mined minerals because of time constraints :(
    // If only Unity's Serialization knew how to handle dictionaries...
    //[CreateProperty] public Dictionary<MineralType, int> MineralCount { get; set; } = new Dictionary<MineralType, int>();
    [CreateProperty] public int GoldCount { get; set; }
}
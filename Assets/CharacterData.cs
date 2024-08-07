using System.Collections.Generic;
using Unity.Properties;

[GeneratePropertyBag]
public class CharacterData
{
    [CreateProperty] public bool Destroyed { get; set; }
    [CreateProperty] public float FuelPoints { get; set; }
    [CreateProperty] public float HullPoints { get; set; }
}
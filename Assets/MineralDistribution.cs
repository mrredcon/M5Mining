using UnityEngine;

public class MineralDistribution
{
    private class MineralChance
    {
        public MineralType Mineral { get; private set; }
        public float SpawnChance { get; private set; }

        public MineralChance(MineralType m, float chance)
        {
            Mineral = m;
            SpawnChance = chance;
        }
    }

    private MineralChance[] mineralChances;
    private float yLevelMax;
    private float yLevelMin;

    private static readonly MineralDistribution SurfaceLitter = new()
    {
        mineralChances = new MineralChance[] { new(MineralType.Dirt, 90.0f), new(MineralType.Stone, 10.0f) },
        yLevelMax = 0,
        yLevelMin = -2
    };

    private static readonly MineralDistribution Topsoil = new()
    {
        mineralChances = new MineralChance[] { new(MineralType.Dirt, 75.0f), new(MineralType.Stone, 20.0f), new(MineralType.Gold, 5.0f) },
        yLevelMax = -3,
        yLevelMin = -7
    };

    private static readonly MineralDistribution Subsoil = new()
    {
        mineralChances = new MineralChance[] { new(MineralType.Dirt, 35.0f), new(MineralType.Stone, 50.0f), new(MineralType.Gold, 10.0f), new(MineralType.Lava, 5.0f) },
        yLevelMax = -7,
        yLevelMin = -12
    };

    private static readonly MineralDistribution ParentMaterial = new()
    {
        mineralChances = new MineralChance[] { new(MineralType.Stone, 75.0f), new(MineralType.Gold, 15.0f), new(MineralType.Lava, 10.0f) },
        yLevelMax = -12,
        yLevelMin = -20
    };

    private static readonly MineralDistribution Bedrock = new()
    {
        mineralChances = new MineralChance[] { new(MineralType.Stone, 50.0f), new(MineralType.Gold, 30.0f), new(MineralType.Lava, 20.0f) },
        yLevelMax = -20,
        yLevelMin = -50
    };

    private static readonly MineralDistribution[] allDistributions = { SurfaceLitter, Topsoil, Subsoil, ParentMaterial, Bedrock };

    public static MineralType GetRandomMineral(float yLevel) {
        MineralType chosenMineral = null;
        MineralDistribution distribution = null;

        foreach (MineralDistribution d in allDistributions)
        {
            if (yLevel <= d.yLevelMax && yLevel >= d.yLevelMin)
            {
                distribution = d;
                break;
            }
        }

        if (distribution == null) {
            Debug.LogError("Failed to find a valid mineral distribution table at y-level=" + yLevel);
            return null;
        }

        float diceRoll = Random.Range(1.0f, 100.0f);
        float temp = 0.0f;
        int i = 0;
        while (temp < diceRoll) {
            if (i >= distribution.mineralChances.Length) {
                Debug.LogError("Overflowed when choosing a random mineral at y-level=" + yLevel);
                return null;
            }

            chosenMineral = distribution.mineralChances[i].Mineral;
            temp += distribution.mineralChances[i].SpawnChance;
            i++;
        }

        return chosenMineral;
    }
}
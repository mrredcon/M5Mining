using System.Collections.Generic;
using Unity.Properties;
using Unity.Serialization.Json;
using UnityEngine;

[GeneratePropertyBag]
public class WorldData
{
    [CreateProperty] private HashSet<Vector2> destroyedEarth;
    [CreateProperty] private int seed;
    [CreateProperty] private int xSize;
    [CreateProperty] private int ySize;
    [CreateProperty] private Vector2 playerPos;
    [CreateProperty] private long lastPlayed;

    public WorldData()
    {

    }

    public WorldData(int x, int y)
    {
        xSize = x;
        ySize = y;

        destroyedEarth = new HashSet<Vector2>();
        seed = Random.Range(0, int.MaxValue);
    }

    public Vector2 GetPlayerPos()
    {
        return playerPos;
    }

    public void SetPlayerPos(Vector2 pos)
    {
        playerPos = pos;
    }

    public int GetXSize()
    {
        return xSize;
    }

    public int GetYSize()
    {
        return ySize;
    }

    public void UpdateTimestamp()
    {
        lastPlayed = System.DateTime.Now.Ticks;
    }

    public System.DateTime GetTimestamp()
    {
        return new System.DateTime(lastPlayed);
    }

    public void MarkDestroyed(Vector2 vector)
    {
        destroyedEarth.Add(vector);
    }

    public bool IsDestroyed(int x, int y)
    {
        return destroyedEarth.Contains(new Vector2(x, y));
    }

    public int GetEarthDestroyed()
    {
        return destroyedEarth.Count;
    }

    // public string GetJSON()
    // {
    //     return JsonSerialization.ToJson(this);
    // }

    public int GetSeed()
    {
        return seed;
    }
}
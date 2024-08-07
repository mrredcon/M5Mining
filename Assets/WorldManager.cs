using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Properties;
using Unity.Serialization.Json;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField] private GameObject dirtPrefab;
    private static WorldManager manager;
    [SerializeField] private int yDepth = 30;
    [SerializeField] private int xLength = 100;
    private WorldData worldData;
    private GameObject[,] world;
    [SerializeField] private Vehicle playerVehicle;
    [SerializeField] private float playerStartingY = 3.0f;
    private string filePath;
    private bool loadedSaveData = false;
    [SerializeField] private Blimp blimp;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/world.dat";
        manager = this;
    }

    public WorldData GetData()
    {
        worldData.UpdateTimestamp();
        worldData.SetPlayerPos(playerVehicle.transform.position);
        return worldData;
    }

    public void MarkDestroyed(Vector2 vector)
    {
        worldData.MarkDestroyed(vector);
    }

    public static WorldManager GetInstance()
    {
        return manager;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void LoadSave(WorldData data)
    {
        worldData = data;
        xLength = worldData.GetXSize();
        yDepth = worldData.GetYSize();
        loadedSaveData = true;
        InitializeWorld();
    }

    public void NewGame()
    {
        worldData = new(xLength, yDepth);
        InitializeWorld();
    }

    private void InitializeWorld()
    {
        Debug.Log("Setting seed to " + worldData.GetSeed());
        Random.InitState(worldData.GetSeed());
        world = new GameObject[xLength, yDepth];
        GenerateWorld();
        MoveActors();
    }

    private void MoveActors()
    {
        if (loadedSaveData)
        {
            playerVehicle.transform.position = worldData.GetPlayerPos();
        }
        else 
        {
            playerVehicle.transform.position = new Vector2(xLength / 2, playerStartingY);
            blimp.transform.position = new Vector2(xLength / 2, blimp.transform.position.y);
        }
    }

    private void GenerateWorld()
    {
        for (int y = 0; y > yDepth * -1; y--)
        {
            for (int x = 0; x < xLength; x++)
            {
                world[x, y * -1] = Instantiate(dirtPrefab, new Vector3(x, y, 0), Quaternion.identity);

                if (worldData.IsDestroyed(x, y))
                {
                    world[x, y * -1].SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

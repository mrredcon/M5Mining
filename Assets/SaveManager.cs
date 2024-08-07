using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Serialization.Json;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private WorldManager worldManager;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private Vehicle playerVehicle;
    [SerializeField] private ErrorPopup errorPopup;

    private int minSlot = 1;
    private int maxSlot = 3;
    private string basePath;
    private const string saveSlotDirectoryNamePrefix = "saveslot";
    private const string saveSlotButtonDefaultText = "Save Slot";
    private const string worldDataFileName = "worldData.json";
    private const string inventoryDataFileName = "inventoryData.json";
    private const string characterDataFilename = "characterData.json";

    void Awake()
    {
        basePath = Application.persistentDataPath;
    }

    // Start is called before the first frame update
    void Start()
    {
        int desiredSaveSlot = GameInitialization.LoadSaveSlot;
        if (desiredSaveSlot == 0)
        {
            worldManager.NewGame();
        } else {
            LoadSave(desiredSaveSlot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame(int slot)
    {
        if (slot < minSlot || slot > maxSlot)
        {
            throw new System.Exception("Save slot was not in valid range.");
        }

        string directoryName = Path.Combine(basePath, saveSlotDirectoryNamePrefix + slot);

        try
        {
            Directory.CreateDirectory(directoryName);

            string worldJson = JsonSerialization.ToJson(worldManager.GetData());
            File.WriteAllText(Path.Combine(directoryName, worldDataFileName), worldJson);

            string inventoryJson = JsonSerialization.ToJson(inventoryManager.GetData());
            File.WriteAllText(Path.Combine(directoryName, inventoryDataFileName), inventoryJson);

            string characterJson = JsonSerialization.ToJson(playerVehicle.GetCharacterData());
            File.WriteAllText(Path.Combine(directoryName, characterDataFilename), characterJson);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            errorPopup.ShowError(e.Message);
        }
    }

    private void LoadSave(int slot)
    {
        CheckSlotInRange(slot);

        string directoryName = Path.Combine(basePath, saveSlotDirectoryNamePrefix + slot);

        try
        {
            WorldData worldData = JsonSerialization.FromJson<WorldData>(File.ReadAllText(Path.Combine(directoryName, worldDataFileName)));
            worldManager.LoadSave(worldData);   

            InventoryData inventoryData = JsonSerialization.FromJson<InventoryData>(File.ReadAllText(Path.Combine(directoryName, inventoryDataFileName)));
            inventoryManager.SetData(inventoryData);

            CharacterData charData = JsonSerialization.FromJson<CharacterData>(File.ReadAllText(Path.Combine(directoryName, characterDataFilename)));
            playerVehicle.SetCharacterData(charData);
        }
        catch (DirectoryNotFoundException)
        {
            worldManager.NewGame();
            errorPopup.ShowError("Save folder not found at " + directoryName + ". Press OK to start a new game (no data has been overwritten yet.)");
        }
        catch (Exception e)
        {
            Debug.Log(e);
            worldManager.NewGame();
            errorPopup.ShowError(e.Message);
        }
    }

    private void CheckSlotInRange(int slot)
    {
        if (slot < minSlot || slot > maxSlot)
        {
            throw new System.Exception("Save slot was not in valid range.");
        }
    }

    public string GetLabel(int slot)
    {
        CheckSlotInRange(slot);

        string defaultName = saveSlotButtonDefaultText + " " + slot;

        string directoryName = Path.Combine(basePath, saveSlotDirectoryNamePrefix + slot);
        if (!Directory.Exists(directoryName))
        {
            return defaultName;
        }

        try
        {
            WorldData worldData = JsonSerialization.FromJson<WorldData>(File.ReadAllText(Path.Combine(directoryName, worldDataFileName)));
            InventoryData inventoryData = JsonSerialization.FromJson<InventoryData>(File.ReadAllText(Path.Combine(directoryName, inventoryDataFileName)));
            CharacterData charData = JsonSerialization.FromJson<CharacterData>(File.ReadAllText(Path.Combine(directoryName, characterDataFilename)));

            int goldCount = inventoryData.GoldCount;

            return saveSlotButtonDefaultText + " " + slot + "\n" +
                "last played on " + worldData.GetTimestamp().ToString() + "\n" +
                worldData.GetEarthDestroyed() + " chunks of earth mined" + "\n" +
                goldCount + " gold in inventory" + "\n" +
                (int)(charData.FuelPoints / playerVehicle.GetMaxFuel() * 100.0) + "% fuel remaining" + "\n" +
                (int)(charData.HullPoints / playerVehicle.GetMaxHull() * 100.0) + "% hull health";
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return defaultName;
        }
    }
}

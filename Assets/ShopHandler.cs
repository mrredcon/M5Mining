using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private Image image;
    [SerializeField] private InventoryManager inventory;
    [SerializeField] private Vehicle playerVehicle;

    //[Header("Counters")]
    //[SerializeField] private TMP_Text goldCounterText;
    //[SerializeField] private TMP_Text moneyText;

    [Header("Button Labels")]
    [SerializeField] private TMP_Text sellGoldText;
    [SerializeField] private string sellGoldButtonLabel = "Sell Gold";
    [SerializeField] private TMP_Text refuelText;
    [SerializeField] private string refuelButtonLabel = "Refuel";
    [SerializeField] private TMP_Text repairHullText;
    [SerializeField] private string repairHullButtonLabel = "Repair Hull";

    [Header("Gameplay")]
    
    [SerializeField] private float goldValue = 10.0f;
    [SerializeField] private float fuelCost = 10.0f;
    [SerializeField] private float fuelPercentageThreshold = 0.9f;
    [SerializeField] private float repairHullCost = 30.0f;
    [SerializeField] private float hullPercentageThreshold = 0.9f;

    // Start is called before the first frame update
    void Start()
    {
        uiCanvas.enabled = false;
        image.color = Color.white;

        sellGoldText.text = sellGoldButtonLabel + " ($" + Math.Round(goldValue, 2) + ")";
        refuelText.text = refuelButtonLabel + " ($" + Math.Round(fuelCost, 2) + ")";
        repairHullText.text = repairHullButtonLabel + " ($" + Math.Round(repairHullCost, 2) + ")";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SellGold()
    {
        int goldAmount = inventory.GetGoldCount();
        inventory.SetMoney(inventory.GetMoney() + (goldValue * goldAmount));
        inventory.RemoveGold(goldAmount);
        //goldCounterText.text = "Gold: 0";
        //moneyText.text = "Money: " + inventory.GetMoney();
    }

    public void Refuel()
    {
        if (playerVehicle.GetFuelPercentage() > fuelPercentageThreshold)
        {
            return;
        }

        double money = inventory.GetMoney();
        if (money < fuelCost)
        {
            return;
        }

        playerVehicle.Refuel();
        inventory.SetMoney(inventory.GetMoney() - fuelCost);
        //moneyText.text = "Money: " + inventory.GetMoney();
    }

    public void RepairHull()
    {
        if (playerVehicle.GetHullPercentage() > hullPercentageThreshold)
        {
            return;
        }

        double money = inventory.GetMoney();
        if (money < repairHullCost)
        {
            return;
        }

        playerVehicle.RepairHull();
        inventory.SetMoney(inventory.GetMoney() - repairHullCost);
        //moneyText.text = "Money: " + inventory.GetMoney();
    }

    public void OpenUI()
    {
        uiCanvas.enabled = true;
        UIManager.GetInstance().SetUIOpen(true);
    }

    public void CloseUI()
    {
        uiCanvas.enabled = false;
        UIManager.GetInstance().SetUIOpen(false);
    }
}

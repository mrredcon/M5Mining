using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image fuelBarFill;
    [SerializeField] private Image hullBarFill;
    [SerializeField] private Vehicle playerVehicle;
    [SerializeField] private TMP_Text goldCounterText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private InventoryManager inventoryManager;
    private static UIManager instance;
    private bool uiOpen = false;

    public static UIManager GetInstance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        fuelBarFill.transform.localScale = new Vector3(playerVehicle.GetFuelPercentage(), 1, 1);
        hullBarFill.transform.localScale = new Vector3(playerVehicle.GetHullPercentage(), 1, 1);

        int goldAmount = inventoryManager.GetGoldCount();
        goldCounterText.text = "Gold: " + goldAmount;

        double moneyAmount = inventoryManager.GetMoney();
        moneyText.text = "Money: $" + Math.Round(moneyAmount, 2);
    }

    public bool IsUIOpen()
    {
        return uiOpen;
    }

    public void SetUIOpen(bool open)
    {
        uiOpen = open;
    }
}

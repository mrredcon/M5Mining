using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private InventoryData data;
    [SerializeField] private TMP_Text goldText;

    void Awake()
    {
        data = new InventoryData();
    }

    public InventoryData GetData()
    {
        return data;
    }

    public void SetData(InventoryData data)
    {
        this.data = data;
    }

    // public int GetMineralAmount(MineralType mineral) {
    //     if (data.MineralCount.ContainsKey(mineral)) {
    //         return data.MineralCount[mineral];
    //     }

    //     return 0;
    // }

    public int GetGoldCount()
    {
        return data.GoldCount;
    }

    // public void AddMineral(MineralType mineral, int amount) {
    //     if (mineral == null) {
    //         return;
    //     }
        
    //     if (data.MineralCount.ContainsKey(mineral)) {
    //         data.MineralCount[mineral] += amount;
    //     } else {
    //         data.MineralCount[mineral] = amount;
    //     }
        
    //     if (mineral == MineralType.Gold) {
    //         goldText.text = "Gold: " + data.MineralCount[mineral];
    //     }
    // }

    public void AddGold(int amount)
    {
        data.GoldCount += amount;
    }

    // public void RemoveMineral(MineralType mineral, int amount) {
    //     if (data.MineralCount.ContainsKey(mineral))
    //     {
    //         int currentAmount = data.MineralCount[mineral];
    //         if (currentAmount - amount < 0) {
    //             throw new Exception("Not enough of that kind of mineral in inventory to drop.");
    //         }
    //         data.MineralCount[mineral] -= amount;
    //     }
    // }

    public void RemoveGold(int amount)
    {
        int currentAmount = data.GoldCount;
        if (currentAmount - amount < 0) {
            throw new Exception("Not enough of that kind of mineral in inventory to drop.");
        }

        data.GoldCount -= amount;
    }

    public double GetMoney() {
        return data.Money;
    }

    public void SetMoney(double money) {
        data.Money = money;
    }
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

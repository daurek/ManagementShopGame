﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class MaterialCraft : MonoBehaviour {


    public static MaterialCraft matCraft;

    [SerializeField] private GameObject materialsGrid;
    public Components.BasicMaterial[] BasicMaterials;

    private void Awake()
    {
        if (matCraft != null)
            Destroy(matCraft);
        else
            matCraft = this;

        DontDestroyOnLoad(this);

        BasicMaterials = (Components.BasicMaterial[])Enum.GetValues(typeof(Components.BasicMaterial));
    }

  
	void Update ()
    {
        for (int i = 0; i < materialsGrid.transform.childCount; i++)
        {
            UpdateCraft(i, CompDrawer.compDrawer.Components[i].timePerUnit);
            materialsGrid.transform.GetChild(i).GetChild(3).GetComponent<Text>().text = CompDrawer.compDrawer.Components[i].UpgradeCurrentPrice + "";
        }
    }

    public void UpdateCraft(int i, float time)
    {
        
        materialsGrid.transform.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount += 1.0f / time * Time.deltaTime;
        if(materialsGrid.transform.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount == 1)
        {
            materialsGrid.transform.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount = 0;
            Inventory.inventory.CraftMaterial(BasicMaterials[i]);
        }
    }

    public void UpgradeCraft(BasicMaterial mat)
    {
        if (Inventory.inventory.Gold >= mat.UpgradeCurrentPrice)
        {
            Inventory.inventory.Gold -= mat.UpgradeCurrentPrice;
            mat.UpgradeCurrentPrice = (int)(mat.upgradeMultiplicator * mat.UpgradeCurrentPrice);
            mat.timePerUnit = (float)(mat.timePerUnit / 1.2);
        }

        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;

	void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
	}
	
	void Update()
    {
		
	}

    void UpdateUI()
    {
        Debug.Log("Updating UI");
    }
}

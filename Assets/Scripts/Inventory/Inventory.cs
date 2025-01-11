using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    InventoryUI inventoryUI;
    [SerializeField]
    InventoryItemsFabric inventoryItemsFabric;
    private List<InventoryItemSO> inventoryItems = new List<InventoryItemSO>();
    private void Awake()
    {
        inventoryItemsFabric.itemCreated.AddListener(SubscribeOnItemCreated);
    }
    private void SubscribeOnItemCreated(InventoryItemGO inventoryItemGO)
    {
        inventoryItemGO.CollectEnd.AddListener(OnCollectEnd);
    }
    public void RemoveItem(InventoryItemSO inventoryItemSO)
    {
        inventoryItems.Remove(inventoryItemSO);
        inventoryUI.RemoveItem(inventoryItemSO);
    }
    private void OnCollectEnd(InventoryItemSO inventoryItemSO)
    {
        inventoryUI.AddItem(inventoryItemSO);
        inventoryItems.Add(inventoryItemSO);
    }

}
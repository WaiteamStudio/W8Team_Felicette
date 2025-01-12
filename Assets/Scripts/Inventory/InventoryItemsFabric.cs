using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class InventoryItemsFabric : MonoBehaviour, ISelectableItem
{
    public UnityEvent<InventoryItemGO> itemCreated = new();
    public List<InventoryItemGO> inventoryItemGOs;
    private void Start()
    {
       foreach(InventoryItemGO item in inventoryItemGOs)
        {
            itemCreated.Invoke(item);
        }
    }
}

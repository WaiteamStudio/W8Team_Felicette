using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventoryItemGO : MonoBehaviour, ICollectibleItem
{
    [SerializeField]
    public InventoryItemSO notesItemSO;
    public UnityEvent<InventoryItemSO> CollectStart;
    public UnityEvent<InventoryItemSO> CollectEnd;
    private void Awake()
    {
        
    }
    public void OnCollectEnd()
    {
        CollectEnd?.Invoke(notesItemSO);
        Debug.Log($"Предмет {notesItemSO.name} собран");
        Destroy(gameObject);
    }
    public void Collect()
    {
        Debug.Log($"Предмет {notesItemSO.name} начинает собираться");
        CollectStart.Invoke(notesItemSO);
    }
    public InventoryItemSO GetNotesItemsSO()
    {
        return notesItemSO;
    }
}

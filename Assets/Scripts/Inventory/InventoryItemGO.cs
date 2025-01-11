using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventoryItemGO : MonoBehaviour, ICollectibleItem
{
    [SerializeField]
    InventoryItemSO notesItemSO;
    public UnityEvent<InventoryItemSO> CollectStart;
    public UnityEvent<InventoryItemSO> CollectEnd;
    private void Awake()
    {
        
    }
    private void OnCollect_Start()
    {
        Debug.Log($"Предмет {notesItemSO.name} начинает собираться");
        OnCollectEnd();
    }
    private void OnCollectEnd()
    {
        CollectEnd?.Invoke(notesItemSO);
        Debug.Log($"Предмет {notesItemSO.name} собран");
        Destroy(gameObject);
    }
    public void Collect()
    {
        CollectStart.Invoke(notesItemSO);
        OnCollect_Start();
    }
    public InventoryItemSO GetNotesItemsSO()
    {
        return notesItemSO;
    }
}

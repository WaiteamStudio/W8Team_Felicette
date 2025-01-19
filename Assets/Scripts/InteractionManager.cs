using cakeslice;
using System;
using UnityEditor;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private InventoryItemSO selectedItem;
    private Collider2D selectedCollider;
    private Collider2D lastSelectedCollider;
    private void Update()
    {
        TrySelectCollider();
        ManageOutline();
        if (Input.GetMouseButtonDown(0)) // Левая кнопка мыши
        {
            //if (EventSystem.current.IsPointerOverGameObject()) 
            //{
            //    // Если клик по Canvas, игнорируем
            //    return;
            //}
            if (selectedCollider == null)
                Debug.Log("selectedCollider is null ");
            if (selectedItem != null)
            {
                TryUseSelectionOnTarget();
            }
            else
            {
                TryCollectItem2D();
            }
        }
    }

    private void ManageOutline()
    {
        HideLastSelectedCollider();
        HighlightTarget();
    }

    public void SelectItem(InventoryItemSO item)
    {
        selectedItem = item;
        Debug.Log($"Selected {item.ToString()}");
    }
    private void HighlightTarget()
    {
        if (selectedCollider == null)
            return;
        Outline selectedOutline = selectedCollider.GetComponent<Outline>();
        selectedOutline.eraseRenderer = false;
    }
    private void HideLastSelectedCollider()
    {
        if (lastSelectedCollider == null)
            return;
        Outline lastOutline = lastSelectedCollider.GetComponent<Outline>();
        lastOutline.eraseRenderer = true;
    }
    private void TrySelectCollider()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
        //if (hit.collider != null)
        //{
        //if (lastSelectedCollider!= hit.collider)
        //    lastSelectedCollider = selectedCollider;
        //selectedCollider = hit.collider;


        //}
        //else
        //{
        if (lastSelectedCollider != hit.collider)
        {
            lastSelectedCollider = selectedCollider;
            selectedCollider = hit.collider;
        }
        if(selectedCollider == null)
        {
            Debug.Log("no collider");
            return;
        }
        Debug.Log("collider selected: " + selectedCollider.ToString());
        //}
    }


    private void TryUseSelectionOnTarget()
    {
        if(selectedCollider == null) 
            return;
        InventoryItemGO TargetObject = selectedCollider.GetComponent<InventoryItemGO>();
        if (TargetObject != null)
        {
            InventoryItemSO itemSO = TargetObject.GetNotesItemsSO();
            selectedItem.Use(itemSO);
        }
        else
        {
            Debug.Log("selected collider is no InventoryItemSO");
        }
    }

    private void TryCollectItem2D()
    {
        if (selectedCollider == null)
            return;
        ICollectibleItem item = selectedCollider.GetComponent<ICollectibleItem>();
        if (item != null)
        {
            Debug.Log("TryCollectItem2D");
            item.Collect();
        }
    }
}
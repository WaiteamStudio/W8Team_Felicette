using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class ItemListController
{
    VisualTreeAsset m_ListEntryTemplate;
    ListView m_ListView;

    Label m_ItemDescription;
    VisualElement m_ItemSprite;

    List<InventoryItemSO> AllNotesItems;
    List<InventoryItemSO> AvailableNotesItems= new List<InventoryItemSO>();
    //List<ItemListEntryController> AvailableNotesItemsEntrys = new List<ItemListEntryController>();

    //по требованию
    public void AddItem(InventoryItemSO item)
    {
        if (!AvailableNotesItems.Contains(item))
        {
            AvailableNotesItems.Add(item);
            m_ListView.Rebuild();
            Debug.Log($"Предмет {item.name} добавлен в UI");
        }
        else
        {
            Debug.Log("It contains");
        }
    }
    internal void RemoveItem(InventoryItemSO inventoryItemSO)
    {
        AvailableNotesItems.Remove(inventoryItemSO);
        m_ListView.Rebuild();
    }
    //private ItemListEntryController GetInList(NotesItem notesItem)
    //{
    //    foreach(var entry in AvailableNotesItemsEntrys)
    //    {
    //        if(entry.NotesItem == notesItem) 
    //            return entry;
    //    }
    //    return null;
    //}

    void EnumerateAllItems()
    {
        AllNotesItems = new List<InventoryItemSO>();
        AllNotesItems.AddRange(Resources.LoadAll<InventoryItemSO>("NotesItems"));
        AvailableNotesItems.AddRange(AllNotesItems);
        // foreach (NotesItem item in AllNotesItems)
        // {
        //     Debug.Log(item);
        // }
        // foreach (NotesItem item in Resources.LoadAll<NotesItem>("NotesItems"))
        // {
        //     Debug.Log(item);
        // }
    }
    public void InitializeItemList(VisualElement root, VisualTreeAsset listElementTemplate, List<InventoryItemSO> initial)
    {
        InitializeItemList(root, listElementTemplate);
        AvailableNotesItems.AddRange(initial);
    }
    public void InitializeItemList(VisualElement root, VisualTreeAsset listElementTemplate,bool enumerateAllItems = false)
    {
        if(enumerateAllItems)
            EnumerateAllItems();

        m_ListEntryTemplate = listElementTemplate;

        m_ListView = root.Q("LeftPanel").Q<ListView>("ItemList");

        m_ItemSprite = root.Q<VisualElement>("CenterPanel").Q("ItemBack").Q("ItemSprite");
        m_ItemDescription = root.Q<VisualElement>("CenterPanel").Q("DescriptionBack").Q<Label>("Description");

        FillitemList();

        m_ListView.onSelectionChange += OnItemSelected;
        m_ListView.horizontalScrollingEnabled = false;
    }

    void FillitemList()
    {
        // Set up a make item function for a list entry
        m_ListView.makeItem = () =>
        {
            // Instantiate the UXML template for the entry
            var newListEntry = m_ListEntryTemplate.Instantiate();

            // Instantiate a controller for the data
            var newListEntryLogic = new ItemListEntryController();

            // Assign the controller script to the visual element
            newListEntry.userData = newListEntryLogic;

            newListEntryLogic.SetVisualElement(newListEntry);

            //AvailableNotesItemsEntrys.Add(newListEntryLogic);
            // Return the root of the instantiated visual tree
            // Set a fixed item height
            return newListEntry;

        };
        m_ListView.bindItem = (item, index) =>
        {
            (item.userData as ItemListEntryController).SetItemData(AvailableNotesItems[index]);
        };
        // Initialize the controller script
        m_ListView.fixedItemHeight = 241;

        // Set the actual item's source list/array
        m_ListView.itemsSource = AvailableNotesItems;
    }
    private void OnItemSelected(IEnumerable<object> enumerable)
    {
        // Get the currently selected item directly from the ListView
        var selectedItem = m_ListView.selectedItem as InventoryItemSO;

        // Handle none-selection (Escape to deselect everything)
        if (selectedItem == null)
        {
            // Clear
            m_ItemDescription.text = "";
            m_ItemSprite.style.backgroundImage = null;

            return;
        }

        // Fill in character details
        m_ItemDescription.text = selectedItem.Description.ToString();
        m_ItemSprite.style.backgroundImage = new StyleBackground(selectedItem.BigSprite);
    }

    
}

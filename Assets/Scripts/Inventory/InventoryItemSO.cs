using System;
using UnityEditor.U2D.Animation;
using UnityEngine;
[CreateAssetMenu(fileName = "InventoryItemSO", menuName = "History Trip/InventoryItemSO")]
public class InventoryItemSO : ScriptableObject
{
    public Sprite Icon;
    public Sprite BigSprite;
    public string Description;

    public virtual void Use(InventoryItemSO targetObject)
    {
        Debug.Log(targetObject.ToString() + " is used to " + this.ToString());
    }
}

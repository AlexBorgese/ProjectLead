using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{

    [SerializeField] private Text label;

    [SerializeField] private Image icon;

    public void Set(InventoryItem item)
    {
        Debug.Log("in the Slot"+item.data.icon);
        label.text = item.data.displayName;
        icon.sprite = item.data.icon;
    }
    
}

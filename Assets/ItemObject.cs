using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;

    public void OnCollisionEnter(Collision collision)
    {
        InventorySystem.Instance.Add(referenceItem);
        Destroy(gameObject);
    }
}
 
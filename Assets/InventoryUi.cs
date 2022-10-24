using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    public GameObject slotPrefab;
    // Start is called before the first frame update
    void Start()
    {
        InventorySystem.changeUI += onUpdateInventory;
        
    }

    private void onUpdateInventory()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        DrawInventory();
    }

    public void DrawInventory()
    {
        Debug.Log(InventorySystem.Instance.inventory[0].data);
        foreach (InventoryItem item in InventorySystem.Instance.inventory)
        {
            AddInventorySlot(item);
        }
    }

    public void AddInventorySlot(InventoryItem item)
    {
        GameObject obj = Instantiate(slotPrefab);
        obj.transform.SetParent(transform, false);
        InventorySlot slot = obj.GetComponentInChildren<InventorySlot>();
        slot.Set(item);
    }
}

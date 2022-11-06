using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTrigger : MonoBehaviour
{
    public TriggerableAction action = TriggerableAction.Activate;
    public Triggerable[] targets;
    public InventoryItemData item;
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player") && InventorySystem.Instance.Get(item) != null) {
            TriggerTargets();
            // InventorySystem.Instance.Remove(item);
        }
    }

    public void TriggerTargets() {
        foreach (Triggerable t in targets) {
            if (t != null) {
                t.Trigger(action);
            }
        }
    }
}

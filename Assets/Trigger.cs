using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public TriggerableAction action = TriggerableAction.Activate;
    public Triggerable[] targets;
   
   private void OnTriggerStay(Collider other) {
        if (Input.GetKeyDown("e")) {
            Debug.Log("Enter Door");
            TriggerTargets();
        }
   }

   public void TriggerTargets() {
    foreach (Triggerable t in targets) {
        if (t != null) {
            t.Trigger(action);
        }
    }
   }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, Vector3.one * 0.25f);
         if (targets != null) {
            foreach (Triggerable t in targets)
            {
                if (t != null)
                {
                    Gizmos.DrawLine(transform.
                    position, t.transform.position);
                }
            }
        }
    }
}

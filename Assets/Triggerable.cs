using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerableAction {
    Activate,
    Deacttivate,
    Toggle
}

public abstract class Triggerable : MonoBehaviour
{
    public abstract void Trigger (TriggerableAction action);
}

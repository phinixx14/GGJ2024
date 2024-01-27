using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behavior:MonoBehaviour
{
    public float Duration { get; set; }
    public abstract void Act();
}

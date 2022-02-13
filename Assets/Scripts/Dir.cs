using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Dir : MonoBehaviour
{
    public abstract GameObject[] Nodes { get; set; }

    public abstract bool isCompleted { get; set; }
}

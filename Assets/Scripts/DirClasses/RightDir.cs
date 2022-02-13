using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightDir : Dir
{
    public GameObject[] nodes;
    public override GameObject[] Nodes { get; set; }
    public override bool isCompleted { get; set; }
    private void Awake()
    {
        Nodes = nodes;
    }
}

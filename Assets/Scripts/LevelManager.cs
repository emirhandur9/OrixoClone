using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    //public static LevelManager instance;
    
    public Canvas canvas;

    [SerializeField] GameObject[] levelPrefabs;
    private void Awake()
    {
        //instance = this;
        //foreach (var item in FindObjectsOfType<EmptyNode>())
        //{
        //    nodes.Add(item);
        //}

        //Instantiate(levelPrefabs[0]);
    }

    private void Start()
    {
        //CreateLevel(levelPrefabs[0]);
    }

    

    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    SemanticBlock[] allBlocks;
    Transform table;

    private void Awake()
    {
        allBlocks = table.GetComponentsInChildren<SemanticBlock>();
    }

    void Update()
    {
        
    }
}

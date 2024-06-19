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

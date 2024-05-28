using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockDeck : MonoBehaviour
{
    [SerializeField]
    SemanticBlock block;
    [SerializeField]
    int blockType;

    private void Start()
    {
        if (block is LogicGateBlock)
        {
            ((LogicGateBlock)block).Init(blockType);
            GetComponentInChildren<TMP_Text>().text = block.BlockTitle;
        }
    }

    public void SpawnBlock()
    {
        if (block)
        {
            var _block = Instantiate(block, 
                block.transform.position + new Vector3(transform.position.x, transform.position.y, 0), 
                Quaternion.identity, 
                GameObject.Find("Table").transform
                ).GetComponent<SemanticBlock>();

            if (_block is LogicGateBlock)
            {
                ((LogicGateBlock)_block).Init(blockType);
            }

        }
        else
            Debug.LogWarning("Block is null");
    }
}

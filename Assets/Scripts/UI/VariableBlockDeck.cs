using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VariableBlockDeck : MonoBehaviour
{
    [SerializeField]
    SemanticBlock block;
    [SerializeField]
    char variableBlockName = 'A';
    [SerializeField]
    VariableType variableType = VariableType.Variable;

    // greek symbols from 945 to 970
    private void Awake()
    {
        GetComponentInChildren<TMP_Text>().text = variableBlockName.ToString();
    }

    public void SpawnBlock()
    {
        if (block)
        {
            VariableBlock _block = Instantiate(block, 
                block.transform.position + new Vector3(transform.position.x, transform.position.y, 0), 
                Quaternion.identity, 
                GameObject.Find("Table").transform
                ).GetComponent<VariableBlock>();

            _block.VariableName = variableBlockName;
            _block.SetBlockType(variableType);
        }
        else
            Debug.LogWarning("Block is null");
    }
}

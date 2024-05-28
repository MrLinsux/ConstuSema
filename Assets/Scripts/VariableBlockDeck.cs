using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VariableBlockDeck : MonoBehaviour
{
    [SerializeField]
    SemanticBlock block;
    static char variableBlockNextName = 'A';

    private void Start()
    {
        GetComponentInChildren<TMP_Text>().text = variableBlockNextName.ToString();
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
            _block.VariableName = variableBlockNextName.ToString();
            variableBlockNextName++;
            GetComponentInChildren<TMP_Text>().text = variableBlockNextName.ToString();
            _block.Init(VariableType.Variable);
        }
        else
            Debug.LogWarning("Block is null");
    }
}

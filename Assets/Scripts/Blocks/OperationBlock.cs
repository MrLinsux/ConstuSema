using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Operation { Plus, Minus, Multiply, Division }

public class OperationBlock : SemanticBlock
{
    [SerializeField]
    Operation operaion;
    public Operation Operation { get { return operaion; } }
    [SerializeField]
    Transform leftBlockPlace;
    [SerializeField]
    Transform rightBlockPlace;
    SemanticBlock leftBlock;
    SemanticBlock rightBlock;

    // system
    [SerializeField]
    TMP_Text operationTitle;
    string[] operationTitles = new string[] { "+", "-", "×", "÷" };

    public void Init(Operation operation)
    {
        this.operaion = operation;
        operationTitle.text = operationTitles[(int)operation];
    }

    protected override void SetBlockShadowParams()
    {
        throw new System.NotImplementedException();
    }
}

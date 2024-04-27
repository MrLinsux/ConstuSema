using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TotalBlockData;

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

    // system
    [SerializeField]
    TMP_Text operationTitle;
    string[] operationTitles = new string[] { "+", "-", "×", "÷" };
    string operatorsAsChar = "+-*/";

    public void Init(Operation operation)
    {
        this.operaion = operation;
        operationTitle.text = operationTitles[(int)operation];
    }

    public override string ToString()
    {
        return $"({arguments[0]}){operatorsAsChar[(int)operaion]}({arguments[1]})";
    }
}

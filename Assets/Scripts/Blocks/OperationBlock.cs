using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum OperationsType { Plus, Minus, Multiply, Division }

public class OperationBlock : SemanticBlock
{
    public OperationsType Operation { get { return (OperationsType)blockType; } }

    // system
    string[] operationTitles = new string[] { "+", "-", "×", "÷" };
    string operatorsAsChar = "+-*/";

    public override string ToString()
    {
        if (Operation == OperationsType.Minus && arguments.Length == 1)
        {
            return $"{operatorsAsChar[blockType]}{arguments[0]}";
        }
        return $"({arguments[0]}){operatorsAsChar[blockType]}({arguments[1]})";
    }

    public override void SetBlockType(int blockTypeNumber)
    {
        blockType = blockTypeNumber;
        blockTitle.text = operationTitles[blockType];
    }

    public void SetBlockType(OperationsType operation)
    {
        SetBlockType((int)operation);
    }
}

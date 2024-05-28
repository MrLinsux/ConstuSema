using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum VariableType { Variable, Constatnt, Set }

public class VariableBlock : SemanticBlock
{
    // inspector
    [SerializeField]
    VariableType variableType;
    public VariableType VariableType { get { return variableType; } }
    [SerializeField]
    string variableName;
    public string VariableName { get { return variableName; } set { variableName = value; } }

    private void Start()
    {
        Init(VariableType);
    }

    public void Init(VariableType type)
    {
        variableType = type;
        blockTitle.text = VariableName;
    }

    public override string ToString()
    {
        return VariableName;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum VariableType { Letter, Number, Set, Interval, Logic }

public class VariableBlock : SemanticBlock
{
    // inspector
    [SerializeField]
    VariableType variableType;
    public VariableType VariableType { get { return variableType; } }
    [SerializeField]
    string variableName;
    public string VariableName { get { return variableName; } set { variableName = value; } }
    [SerializeField]
    TMP_Text variableNameTitle;

    public void Init(VariableType type)
    {
        variableType = type;
        variableNameTitle.text = VariableName;
    }

    public override string ToString()
    {
        return VariableName;
    }
}

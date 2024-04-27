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
    [SerializeField]
    TMP_Text variableNameTitle;

    private void Awake()
    {
        variableNameTitle.text = variableName;
    }

    public void Init(VariableType type)
    {
        variableType = type;
    }

    public override string ToString()
    {
        return variableName;
    }
}

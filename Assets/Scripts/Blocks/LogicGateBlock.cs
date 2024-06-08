using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum LogicTypes { NOT, OR, AND, XOR, IMPLICATION, EQUAL, PIRS, SHEF, TABOO }

public class LogicGateBlock : SemanticBlock
{
    public LogicTypes LogicGate { get { return (LogicTypes)blockType; } }

    // system
    static string[] logicGatesTitles = new string[] { "¬", "∨", "∧", "⊕", "→", "↔", "↓", "|", "∆" };
    static string asChar = "¬∨∧⊕→↔↓|∆";

    private void Awake()
    {
        SetBlockType(blockType);
    }

    public override void SetBlockType(int blockTypeNumber)
    {
        blockType = blockTypeNumber;
        BlockTitle = logicGatesTitles[blockType];
        if (blockType == 0)
        {
            NumberOfPlaces = 1;
        }
        else
        {
            NumberOfPlaces = 2;
        }

    }

    public override string ToString()
    {
        if(LogicGate == LogicTypes.NOT)
        {
            return $"(!{arguments[0]})";
        }
        else
        {
            return $"({arguments[0]}{asChar[blockType]}{arguments[1]})";
        }
    }
}

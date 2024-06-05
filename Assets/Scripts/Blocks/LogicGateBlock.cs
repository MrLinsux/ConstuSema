using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum LogicTypes { NOT, OR, AND }

public class LogicGateBlock : SemanticBlock
{
    public LogicTypes LogicGate { get { return (LogicTypes)blockType; } }

    // system
    string[] logicGatesTitles = new string[] { "¬", "∨", "∧" };
    string asChar = "!|&";

    private void Awake()
    {
        SetBlockType(blockType);
    }

    public override void SetBlockType(int blockTypeNumber)
    {
        blockType = blockTypeNumber;
        BlockTitle = logicGatesTitles[blockType];
        switch (blockType)
        {
            case 0:
                NumberOfPlaces = 1;
                break;
            case 1:
                NumberOfPlaces = 2;
                break;
            case 2:
                NumberOfPlaces = 2;
                break;
            default:
                throw new System.Exception("Unknown logic gate type");
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

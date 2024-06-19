using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if (LogicGate == LogicTypes.NOT)
        {
            return $"(¬{arguments[0]})";
        }
        else
        {
            return $"({arguments[0]}{asChar[blockType]}{arguments[1]})";
        }
    }

    protected override bool CheckCorrectBlock()
    {
        try
        {
            return arguments.All(e =>
            ((e is QuantiferBlock) && (LogicGate == LogicTypes.NOT)) ||
            (e is LogicGateBlock) ||
            (e is FunctionBlock) ||
            ((e is VariableBlock) && e.BlockType < 2) ||
            (e is UserBlock));
        }
        catch
        {
            return false;
        }
    }
}

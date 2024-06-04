using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum LogicGates { NOT, OR, AND }

public class LogicGateBlock : SemanticBlock
{
    [SerializeField]
    LogicGates logicGate;
    public LogicGates LogicGate { get { return logicGate; } }

    // system
    string[] logicGatesTitles = new string[] { "¬", "∨", "∧" };
    string asChar = "!|&";

    private void Awake()
    {
        SetBlockType(logicGate);
    }

    public void Init(LogicGates logicGate)
    {
        SetBlockType(logicGate);
    }

    public void Init(int logicGate)
    {
        SetBlockType((LogicGates)logicGate);
    }

    public void SetBlockType(LogicGates gateType)
    {
        logicGate = gateType;
        blockTitle.text = logicGatesTitles[(int)gateType];
        switch ((int)gateType)
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
        if(logicGate == LogicGates.NOT)
        {
            return $"(!{arguments[0]})";
        }
        else
        {
            return $"({arguments[0]}{asChar[(int)logicGate]}{arguments[1]})";
        }
    }
}

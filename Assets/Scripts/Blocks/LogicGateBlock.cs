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
    string[] logicGatesTitles = new string[] { "NOT", "OR", "AND" };
    string gatesAsChar = "!|&";

    private void Awake()
    {
        SetBlockType(logicGate);
        tmpTitle.text = logicGatesTitles[(int)logicGate];
    }

    public void Init(LogicGates logicGate)
    {
        this.logicGate = logicGate;
        tmpTitle.text = logicGatesTitles[(int)logicGate];
        SetBlockType(logicGate);
    }
    public void Init(int logicGate)
    {
        this.logicGate = (LogicGates)logicGate;
        tmpTitle.text = logicGatesTitles[logicGate];
        SetBlockType((LogicGates)logicGate);
    }

    public void SetBlockType(LogicGates gateType)
    {
        logicGate = gateType;
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

    public bool LogicNOT(bool x)
    {
        return !x;
    }

    public bool LogicAND(bool x, bool y)
    {
        return x && y;
    }

    public bool LogicOR(bool x, bool y)
    {
        return x || y;
    }

    public override string ToString()
    {
        if(logicGate == LogicGates.NOT)
        {
            return $"(!{arguments[0]})";
        }
        else
        {
            return $"({arguments[0]}{gatesAsChar[(int)logicGate]}{arguments[1]})";
        }
    }
}

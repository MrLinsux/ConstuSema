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
    [SerializeField]
    Transform leftBlockPlace;
    [SerializeField]
    Transform rightBlockPlace;

    // system
    [SerializeField]
    TMP_Text logicGateTitle;
    string[] logicGatesTitles = new string[] { "NOT", "OR", "AND" };
    string gatesAsChar = "!|&";

    private void Awake()
    {
        SetBlockType(logicGate);
        logicGateTitle.text = logicGatesTitles[(int)logicGate];
    }

    public void Init(LogicGates logicGate)
    {
        this.logicGate = logicGate;
        logicGateTitle.text = logicGatesTitles[(int)logicGate];
    }

    public void SetBlockType(LogicGates gateType)
    {
        switch ((int)gateType)
        {
            case 0:
                logicGate = LogicGates.NOT;
                NumberOfPlaces = 1;
                break;
            case 1:
                logicGate = LogicGates.AND;
                NumberOfPlaces = 2;
                break;
            case 2:
                logicGate = LogicGates.OR;
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

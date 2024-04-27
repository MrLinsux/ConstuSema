using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    SemanticBlock[] allBlocks;
    Transform table;

    private void Awake()
    {
        allBlocks = table.GetComponentsInChildren<SemanticBlock>();
    }

    void Update()
    {
        
    }
}

public static class TotalBlockData
{
    public static List<Variable> variables = new List<Variable>();
    public static List<LogicGate> logicGates = new List<LogicGate>();

    public class Variable
    {
        VariableType type;
        object val;

        public Variable(VariableType type, object val)
        {
            this.type = type;
            this.val = val;
        }
    }

    public class LogicGate
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public enum RelationType { Equal, NotEqual, Bigger, BiggerOrEqual, Less, LessOrEqual, Member, Subset }

public class RelationBlock : SemanticBlock
{
    public RelationType Relation { get { return (RelationType)blockType; } }

    // system
    string[] relationTitles = new string[] { "=", "≠", ">", "≥", "<", "≤", "∊", "⊂" };
    string asChar = "=≠>≥<≤∈⊂";

    public override void SetBlockType(int blockTypeNumber)
    {
        blockType = blockTypeNumber;
        blockTitle.text = relationTitles[blockType];
    }

    public void SetBlockType(RelationType operation)
    {
        SetBlockType((int)operation);
    }

    public override string ToString()
    {
        return $"({arguments[0]}{asChar[blockType]}{arguments[1]})";
    }

    protected override bool CheckCorrectBlock()
    {
        return arguments.All(e =>
        (e is FunctionBlock) ||
        (e is OperationBlock) ||
        ((e is VariableBlock) && (((VariableBlock)e).VariableType == VariableType.Variable) || ((VariableBlock)e).VariableType == VariableType.Constatnt) ||
        (e is UserBlock));
    }
}

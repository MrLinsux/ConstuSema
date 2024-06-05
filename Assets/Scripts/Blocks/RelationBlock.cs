using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static TotalBlockData;

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
}

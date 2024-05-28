using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static TotalBlockData;

public enum Relation { Equal, NotEqual, Bigger, BiggerOrEqual, Less, LessOrEqual, Member, Subset }

public class RelationBlock : SemanticBlock
{
    [SerializeField]
    Relation relation;
    public Relation Relation { get { return relation; } }

    // system
    string[] relationTitles = new string[] { "=", "≠", ">", "≥", "<", "≤", "∊", "⊂" };
    string asChar = "=≠>≥<≤∈⊂";

    private void Start()
    {
        Init(Relation);
    }

    public void Init(Relation relation)
    {
        this.relation = relation;
        blockTitle.text = relationTitles[(int)relation];
    }

    public override string ToString()
    {
        return $"({arguments[0]}{asChar[(int)relation]}{arguments[1]})";
    }
}

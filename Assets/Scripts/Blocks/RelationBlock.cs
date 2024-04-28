using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Relation { Equal, NotEqual, Bigger, BiggerOrEqual, Less, LessOrEqual }

public class RelationBlock : SemanticBlock
{
    [SerializeField]
    Relation relation;
    public Relation Relation { get { return relation; } }

    // system
    [SerializeField]
    TMP_Text relationTitle;
    string[] relationTitles = new string[] { "=", "≠", ">", "≥", "<", "≤" };

    public void Init(Relation relation)
    {
        this.relation = relation;
        relationTitle.text = relationTitles[(int)relation];
    }

    public override string ToString()
    {
        throw new System.NotImplementedException();
    }
}

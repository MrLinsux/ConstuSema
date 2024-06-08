using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuantifersType { ANY, EXIST, UNIQUE }

public class QuantiferBlock : SemanticBlock
{
    string[] quantiferTitles = new string[] { "∀", "∃", "∄" };
    string asChar = "∀∃∄";

    public QuantifersType QuantiferType { get { return (QuantifersType)blockType; } }

    private void Start()
    {
        blockTitle.text = quantiferTitles[(int)QuantiferType];
    }

    public override void SetBlockType(int blockTypeNumber)
    {
        blockType = blockTypeNumber;
        blockTitle.text = quantiferTitles[blockType];
    }

    public void SetBlockType(QuantifersType operation)
    {
        SetBlockType((int)operation);
    }

    public override string ToString()
    {
        if(arguments.Length == NumberOfPlaces)
        {
            return asChar[blockType] + arguments[0].ToString();
        }
        else
        {
            return null;
        }
    }
}

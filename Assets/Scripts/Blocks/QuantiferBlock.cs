using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Quantifers { ANY, EXIST, UNIQUE }

public class QuantiferBlock : SemanticBlock
{
    string[] quantiferTitles = new string[] { "ANY", "EXIST", "UNIQUE" };
    string asChar = "∀∃∄";

    [SerializeField]
    Quantifers quantiferType;
    public Quantifers QuantiferType { get { return quantiferType; } }

    private void Start()
    {
        blockTitle.text = quantiferTitles[(int)QuantiferType];
    }

    public override string ToString()
    {
        if(arguments.Length == NumberOfPlaces)
        {
            return asChar[(int)quantiferType] + arguments[0].ToString();
        }
        else
        {
            return null;
        }
    }
}

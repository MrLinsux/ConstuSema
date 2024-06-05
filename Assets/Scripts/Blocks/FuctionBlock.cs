using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static TotalBlockData;

public enum FunctionsType
{ 
    SIN, COS, TAN, CTAN, ARCSIN, ARCCOS, ARCTAN, ARCCTAN, ABS, LN, // function with one argument
    LOG, POW                                                       // functions with two arguments
}

public class FuctionBlock : SemanticBlock
{
    string[] functionTitles = new string[] { "SIN", "COS", "TAN", "CTAN", "ARCSIN", "ARCCOS", "ARCTAN", "ARCCTAN", "ABS", "POW", "LOG", "LN" };
    const string asChar = "!|&^";  // TODO: define unique symbol for each function

    [SerializeField]
    FunctionsType function;
    public FunctionsType Function {  get { return function; } }

    public override string ToString()
    {
        if(function <= FunctionsType.LN)
        {
            return asChar[blockType]+arguments[0].ToString();
        }
        else
        {
            return arguments[0].ToString() + asChar[blockType] + arguments[1].ToString();
        }
    }

    public override void SetBlockType(int blockTypeNumber)
    {
        blockType = blockTypeNumber;
        blockTitle.text = functionTitles[blockType];
        if(blockType <= (int)FunctionsType.LN)
        {
            NumberOfPlaces = 1;
        }
        else
        {
            NumberOfPlaces = 2;
        }
    }

    public void SetBlockType(OperationsType function)
    {
        SetBlockType((int)function);
    }
}

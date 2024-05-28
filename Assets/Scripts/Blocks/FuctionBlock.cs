using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public enum Functions
{ 
    SIN, COS, TAN, CTAN, ARCSIN, ARCCOS, ARCTAN, ARCCTAN, ABS, LN, // function with one argument
    LOG, POW                                                       // functions with two arguments
}

public class FuctionBlock : SemanticBlock
{
    string[] functionTitles = new string[] { "SIN", "COS", "TAN", "CTAN", "ARCSIN", "ARCCOS", "ARCTAN", "ARCCTAN", "ABS", "POW", "LOG", "LN" };
    const string asChar = "!|&^";  // TODO: define unique symbol for each function

    [SerializeField]
    Functions function;
    public Functions Function {  get { return function; } }

    public override string ToString()
    {
        if(function <= Functions.LN)
        {
            return asChar[(int)function]+arguments[0].ToString();
        }
        else
        {
            return arguments[0].ToString() + asChar[(int)function] + arguments[1].ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        blockTitle.text = functionTitles[(int)function];
    }
}

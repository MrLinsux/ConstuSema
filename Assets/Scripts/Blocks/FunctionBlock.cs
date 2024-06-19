using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum FunctionsType
{ 
    SIN, COS, TAN, CTAN, ARCSIN, ARCCOS, ARCTAN, ARCCTAN, ABS, LN, // function with one argument
    LOG, POW,                                                      // functions with two arguments
    PREDICATE                                                      // function with n (max is 5) arguments
}

public class FunctionBlock : SemanticBlock
{
    string[] functionTitles = new string[] { 
        "SIN", "COS", "TAN", "CTAN", "ARCSIN", "ARCCOS", "ARCTAN", "ARCCTAN", "ABS", "LN", 
        "LOG", "POW", 
        "A" };
    public const string asChar = "⊲⊳⊴⊵⊶⊷⊸⊹⊺⋄⋆⋇";
    public char PredicateTitle { 
        get { return functionTitles[(int)Function][0]; } 
        private set { functionTitles[(int)Function] = value.ToString(); blockTitle.text = value.ToString(); } }

    public FunctionsType Function { get { return (FunctionsType)blockType; } }

    public override string ToString()
    {
        if (Function <= FunctionsType.LN)
        {
            return asChar[blockType] + arguments[0].ToString();
        }
        else if (Function <= FunctionsType.POW)
        {
            return arguments[0].ToString() + asChar[blockType] + arguments[1].ToString();
        }
        else
        {
            return PredicateTitle + arguments[0].ToString();
        }
    }

    public override void SetBlockType(int blockTypeNumber)
    {
        blockType = blockTypeNumber;
        blockTitle.text = functionTitles[blockType];
        if (blockType <= (int)FunctionsType.LN)
        {
            SetBothNumberOfPlaces(1);
        }
        else if(blockType <= (int)FunctionsType.POW)
        {
            SetBothNumberOfPlaces(2);
        }
        else
        {
            MaxNumberOfPlaces = 5;
            MinNumberOfPlaces = 1;
        }
        
    }

    public void SetBlockType(OperationsType function)
    {
        SetBlockType((int)function);
    }

    protected override bool CheckCorrectBlock()
    {
        return arguments.All(e =>
        (e is FunctionBlock) ||
        (e is OperationBlock) ||
            ((e is VariableBlock) && e.BlockType < 2) ||
        (e is UserBlock));
    }

    char GetNextSymbol(char c)
    {
        if ('A' <= c && c <= 'Z')
        {
            if (c == 'Z')
            {
                return 'A';
            }
            else
            {
                return ++c;
            }
        }
        else
        {
            return c;
        }
    }
    char GetPreviousSymbol(char c)
    {
        if ('A' <= c && c <= 'Z')
        {
            if (c == 'A')
            {
                return 'Z';
            }
            else
            {
                return --c;
            }
        }
        else
        {
            return c;
        }
    }

    void Update()
    {
        if(Function == FunctionsType.PREDICATE)
            if (PointerIsInObject)
            {
                float scrollInput = Input.GetAxis("Mouse ScrollWheel");
                if (scrollInput > 0f || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    PredicateTitle = GetPreviousSymbol(PredicateTitle);
                }
                else if (scrollInput < 0f || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    PredicateTitle = GetNextSymbol(PredicateTitle);
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    Destroy(gameObject);
                }

            }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public enum FunctionsType
{ 
    PREDICATE, SIN, COS, TAN, CTAN, ARCSIN, ARCCOS, ARCTAN, ARCCTAN, ABS, LN, // function with one argument
    LOG, POW                                                       // functions with two arguments
}

public class FunctionBlock : SemanticBlock
{
    string[] functionTitles = new string[] { "A", "SIN", "COS", "TAN", "CTAN", "ARCSIN", "ARCCOS", "ARCTAN", "ARCCTAN", "ABS", "POW", "LOG", "LN" };
    public const string asChar = " ⊲⊳⊴⊵⊶⊷⊸⊹⊺⋄⋆⋇";
    public char PredicateTitle { get { return functionTitles[0][0]; } set { functionTitles[0] = value.ToString(); blockTitle.text = value.ToString(); } }

    public FunctionsType Function { get { return (FunctionsType)blockType; } }

    public override string ToString()
    {
        if(Function == FunctionsType.PREDICATE)
        {
            return PredicateTitle + arguments[0].ToString();
        }
        else if (Function <= FunctionsType.LN)
        {
            return asChar[blockType] + arguments[0].ToString();
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
        if (blockType <= (int)FunctionsType.LN)
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
        if(blockType == 0)
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

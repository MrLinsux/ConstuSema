using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public enum VariableType { Variable, Constatnt, Set }

public class VariableBlock : SemanticBlock, IPointerEnterHandler, IPointerExitHandler
{
    // inspector
    [SerializeField]
    VariableType variableType;
    public VariableType VariableType { get { return variableType; } }
    [SerializeField]
    string variableName;
    public string VariableName { get { return variableName; } set { variableName = value; blockTitle.text = value; } }

    bool pointerIsInObject = false;

    private void Start()
    {
        Init(VariableType);
    }

    public void Init(VariableType type)
    {
        variableType = type;
        blockTitle.text = VariableName;
    }

    public override string ToString()
    {
        return VariableName;
    }

    public void Update()
    {
        if (pointerIsInObject)
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput > 0f)
            {
                VariableName = GetPreviousSymbol(VariableName[0]).ToString();
            }
            else if (scrollInput < 0f)
            {
                VariableName = GetNextSymbol(VariableName[0]).ToString();
            }

        }
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
        else if('a' <= c && c <= 'z')
        {
            if (c == 'z')
            {
                return 'a';
            }
            else
            {
                return ++c;
            }
        }
        else if('α' <= c && c <= 'ω')
        {
            if (c == 'ω')
            {
                return 'α';
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
        else if ('a' <= c && c <= 'z')
        {
            if (c == 'a')
            {
                return 'z';
            }
            else
            {
                return --c;
            }
        }
        else if ('α' <= c && c <= 'ω')
        {
            if (c == 'α')
            {
                return 'ω';
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

    public new void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerEnter)
        {
            pointerIsInObject = true;
        }
    }

    public new void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter)
        {
            pointerIsInObject = false;
        }
    }
}

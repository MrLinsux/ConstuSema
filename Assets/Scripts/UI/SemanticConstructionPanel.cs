using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SemanticConstructionPanel : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    GameObject resultPanel;
    [SerializeField]
    TMP_Text resultText;
    [SerializeField]
    GameObject textHint;

    string GetBinarySet(int dec, int n)
    {
        string res = Convert.ToString(dec, 2);

        for(int i = res.Length; i < n; i++)
        {
            res = "0" + res;
        }

        return res;
    }

    public void CheckConstruction()
    {
        var mainBlock = transform.GetComponentInChildren<SemanticBlock>();
        if (mainBlock)
        {
            CheckConstruction(mainBlock.ToString(), true);
        }
    }

    public string CheckConstruction(bool fullTable)
    {
        var mainBlock = transform.GetComponentInChildren<SemanticBlock>();
        if (mainBlock)
        {
            return CheckConstruction(mainBlock.ToString(), fullTable);
        }
        return null;
    }

    public string CheckConstruction(string construction, bool fullTable)
    {
        // get polish notation
        // let there is only one block
        var mainBlock = transform.GetComponentInChildren<SemanticBlock>();
        if (mainBlock)
        {
            string res = string.Empty;

            Debug.Log(construction);
            var arguments = transform.GetComponentsInChildren<SemanticBlock>(true).
                Where(e => 
                    (e is VariableBlock) && 
                    e.GetComponent<VariableBlock>().VariableType == VariableType.Variable
                ).ToList();
            for(int i = 0; i < arguments.Count; i++)
            {
                if(arguments.Count(e => e.ToString() == arguments[i].ToString()) > 1)
                {
                    arguments.RemoveAt(i--);
                }
            }

            int argumentsNum = arguments.Count;

            string argsNames = "";

            for (int i = 0; i < argumentsNum; i++)
            {
                argsNames += arguments[i].ToString();
            }

            if (IsBooleanFunction())
                res += BuildBooleanTableByConstruction(construction, argsNames, fullTable);
            else if (IsPredicate())
                res += $"[{argsNames}]{construction}";

            return res;
        }
        return null;
    }

    public bool IsBooleanFunction()
    {
        return transform.GetComponentsInChildren<SemanticBlock>(true).All(e => (e is VariableBlock) || (e is LogicGateBlock) || (e is UserBlock));
    }

    public bool IsPredicate()
    {
        return transform.GetComponentsInChildren<SemanticBlock>(true).All(e => (e is VariableBlock) || (e is LogicGateBlock) || (e is QuantiferBlock) || (e is UserBlock));
    }

    string BuildBooleanTableByConstruction(string construction, string argsNames, bool fullTable)
    {
        int argumentsNum = argsNames.Length;

        string calConstruction = construction;

        string res = string.Empty;

        if (fullTable)
        {
            for (int i = 0; i < argumentsNum; i++)
            {
                res += argsNames[i];
            }
            res += "<i>f</i>\n";
        }

        for (int argValue = 0; argValue < Mathf.Pow(2, argsNames.Length); argValue++)
        {
            string valsSet = GetBinarySet(argValue, argumentsNum);

            for (int i = 0; i < argumentsNum; ++i)
            {
                calConstruction = calConstruction.Replace(argsNames[i], valsSet[i]);
            }

            if (fullTable)
                res += $"{valsSet} {PolishNotation.Calculate(calConstruction)}\n";
            else
                res += PolishNotation.Calculate(calConstruction).ToString();

            calConstruction = construction;
        }

        Debug.Log(res);
        if (resultPanel)
        {
            resultPanel.SetActive(true);
            resultText.text = res;
        }
        return res;
    }

    public string GetVariablesNames(SemanticBlock construction)
    {
        var arguments = construction.GetComponentsInChildren<SemanticBlock>(true).
            Where(e =>
                (e is VariableBlock) &&
                e.GetComponent<VariableBlock>().VariableType == VariableType.Variable
            ).ToArray();

        if (arguments != null && arguments.Length >= 1)
        {
            string argumentsNames = arguments[0].ToString();

            for(int i = 1; i < arguments.Length; i++)
            {
                argumentsNames += arguments[i].ToString();
            }

            return argumentsNames;
        }
        else
        {
            return null;
        }

    }

    public void CloseResultPanel()
    {
        resultPanel.SetActive(false);
    }

    void SetActiveHint(bool isActive)
    {
        textHint.SetActive(isActive);
    }


    // movement
    public void OnDrop(PointerEventData eventData)
    {
        SemanticBlock semanticBlock = eventData.pointerDrag.GetComponent<SemanticBlock>();

        if(semanticBlock)
        {
            semanticBlock.DefaultParent = transform;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerDrag == null)
        {
            return;
        }

        SemanticBlock semanticBlock = eventData.pointerDrag.GetComponent<SemanticBlock>();

        if(semanticBlock)
        {
            semanticBlock.SetBlockShadowActive(true);
            semanticBlock.SetBlockShadowForm();
            semanticBlock.DefaultShadowParent = transform;
            SetActiveHint(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            return;
        }

        SemanticBlock semanticBlock = eventData.pointerDrag.GetComponent<SemanticBlock>();

        if (semanticBlock)
        {
            semanticBlock.SetBlockShadowActive(false);
            semanticBlock.DefaultShadowParent = semanticBlock.DefaultParent;
            if(transform.GetComponentsInChildren<SemanticBlock>().Length == 0)
            {
                SetActiveHint(true);
            }
        }
    }
}

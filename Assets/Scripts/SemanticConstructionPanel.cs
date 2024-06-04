using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SemanticConstructionPanel : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    GameObject resultPanel;

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
            var arguments = transform.GetComponentsInChildren<SemanticBlock>().
                Where(e => 
                    (e is VariableBlock) && 
                    e.GetComponent<VariableBlock>().VariableType == VariableType.Variable
                ).ToArray();
            int argumentsNum = arguments.Length;

            string[] argsNames = new string[argumentsNum];

                for (int i = 0; i < argumentsNum; i++)
                {
                    argsNames[i] = arguments[i].ToString();
                    if (fullTable)
                    {
                        res += argsNames[i];
                    }
                }
                if (fullTable)
                {
                    res += "Vec\n";
                }

            string calConstruction = construction;

            for (int argValue = 0; argValue < Mathf.Pow(2, argsNames.Length); argValue++)
            {
                string valsSet = GetBinarySet(argValue, argumentsNum);

                for (int i = 0; i < argumentsNum; ++i)
                {
                    calConstruction = calConstruction.Replace(argsNames[i].ToString(), valsSet[i].ToString());
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
                resultPanel.GetComponentInChildren<TMP_Text>().text = res;
            }
            return res;
        }
        return null;
    }

    public void CloseResultPanel()
    {
        resultPanel.SetActive(false);
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
        }
    }
}

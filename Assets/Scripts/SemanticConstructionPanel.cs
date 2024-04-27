using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SemanticConstructionPanel : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    string s;
    public void CheckConstruction()
    {
        // get polish notation
        // let there is only one block
        var mainBlock = transform.GetComponentInChildren<SemanticBlock>();
        if (mainBlock)
        {
            string construction = mainBlock.ToString();
            Debug.Log(construction);
            var tempArguments = transform.GetComponentsInChildren<SemanticBlock>().Where(e => e is VariableBlock).ToArray();
            string[] argsNames = new string[tempArguments.Length];
            for(int i = 0; i < tempArguments.Length; i++)
            {
                argsNames[i] = tempArguments[i].ToString();
            }

            // iter 0 0 ... 0
            int[] argsVals = new int[tempArguments.Length];
            for (int i = 0; i < argsVals.Length; ++i) argsVals[i] = 0;
            string calConstruction = construction;
            string valsSet = "";
            for (int j = 0; j < argsVals.Length; ++j)
            {
                valsSet += argsVals[j].ToString() + " ";
                calConstruction = calConstruction.Replace(argsNames[j].ToString(), argsVals[j].ToString());
                Debug.Log(calConstruction);
            }
            Debug.Log($"For Set: {valsSet}res is {PolishNotation.Calculate(calConstruction)}");

            // other iters
            for (int i = 1; i < Mathf.Pow(2, argsNames.Length); i++)
            {
                if ((++argsVals[argsNames.Length-1]) % 2 == 0)
                {
                    argsVals[argsNames.Length - 1] %= 2;
                    int j = argsNames.Length - 2;
                    while (j >= 0 && (++argsVals[j]) % 2 == 0)
                    {
                        argsVals[j--] %= 2;
                    }
                }
                calConstruction = construction;
                valsSet = "";
                for(int j = 0; j < argsVals.Length; ++j)
                {
                    valsSet += argsVals[j].ToString() + " ";
                    calConstruction = calConstruction.Replace(argsNames[j].ToString(), argsVals[j].ToString());
                    //Debug.Log(calConstruction);
                }
                Debug.Log($"For Set: {valsSet}res is {PolishNotation.Calculate(calConstruction)}");
            }
        }
    }

    // movement
    public void OnDrop(PointerEventData eventData)
    {
        SemanticBlock semanticBlock = eventData.pointerDrag.GetComponent<SemanticBlock>();

        if(semanticBlock)
        {
            semanticBlock.DefaultParent = transform;
            CheckConstruction();
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
            semanticBlock.DefaultShadowParent = semanticBlock.DefaultParent;
        }
    }
}

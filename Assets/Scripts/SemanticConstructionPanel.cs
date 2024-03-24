using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SemanticConstructionPanel : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    void CheckConstruction()
    {

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SemanticConstructionPanel : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
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

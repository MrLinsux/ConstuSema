using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Polyplace : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    int numberOfPlaces = 1;
    int CurrentPlacesOccupied { get { return transform.GetComponentsInChildren<SemanticBlock>().Length; } }

    private void Awake()
    {
        if(numberOfPlaces < 1)
        {
            numberOfPlaces = 1;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        SemanticBlock semanticBlock = eventData.pointerDrag.GetComponent<SemanticBlock>();

        if (semanticBlock && CurrentPlacesOccupied < numberOfPlaces)
        {
            semanticBlock.DefaultParent = transform;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            return;
        }

        SemanticBlock semanticBlock = eventData.pointerDrag.GetComponent<SemanticBlock>();

        if (semanticBlock && CurrentPlacesOccupied < numberOfPlaces)
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

        if (semanticBlock && CurrentPlacesOccupied < numberOfPlaces)
        {
            semanticBlock.DefaultShadowParent = semanticBlock.DefaultParent;
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class Table : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        SemanticBlock semanticBlock = eventData.pointerDrag.GetComponent<SemanticBlock>();

        if (semanticBlock)
        {
            semanticBlock.DefaultParent = transform;
        }
    }
}

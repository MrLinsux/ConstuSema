using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class SemanticBlock : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // qol
    Camera MainCamera { get { return Camera.main; } }

    // system
    Vector3 offset;
    Transform defaultParent;
    public Transform DefaultParent { get { return defaultParent; } set { defaultParent = value; } }
    public Transform DefaultShadowParent { get { return BlockShadow.parent; } set { BlockShadow.SetParent(value); } }
    Transform BlockShadow { get { return GameObject.Find("BlockShadow").transform; } }
    protected abstract void SetBlockShadowParams();

    // inspector


    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);

        DefaultParent = DefaultShadowParent = transform.parent;
        transform.SetParent(DefaultParent.parent);
        BlockShadow.SetSiblingIndex(transform.GetSiblingIndex());

        BlockShadow.GetComponent<Image>().enabled = true;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = (Vector2)(MainCamera.ScreenToWorldPoint(eventData.position) + offset);

        SetNewBlockShadowPosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        transform.SetSiblingIndex(BlockShadow.GetSiblingIndex());

        BlockShadow.SetParent(GameObject.Find("Canvas").transform);
        BlockShadow.GetComponent<Image>().enabled = false;
    }

    void SetNewBlockShadowPosition()
    {
        int newIndex = DefaultShadowParent.childCount;

        for(int i = 0; i < DefaultShadowParent.childCount; i++)
        {
            if(transform.position.x < DefaultShadowParent.GetChild(i).position.x)
            {
                newIndex = i;

                if(BlockShadow.GetSiblingIndex() < newIndex)
                {
                    newIndex--;
                }

                break;
            }
        }

        BlockShadow.SetSiblingIndex(newIndex);
    }    
}

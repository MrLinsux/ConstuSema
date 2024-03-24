using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class SemanticBlock : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    // qol
    Camera MainCamera { get { return Camera.main; } }

    // system
    Vector3 offset;
    Transform defaultParent;
    public Transform DefaultParent { get { return defaultParent; } set { defaultParent = value; } }
    public Transform DefaultShadowParent { get { return BlockShadow.parent; } set { BlockShadow.SetParent(value); } }
    Transform BlockShadow { get { return GameObject.Find("BlockShadow").transform; } }

    // inspector
    [SerializeField]
    int numberOfPlaces = 2;
    int NumberOfPlaces { get { return numberOfPlaces; } }
    int CurrentPlacesOccupied { get { return arguments.Length; } }

    // semantic params
    protected SemanticBlock[] arguments { get { return transform.GetComponentsInChildren<SemanticBlock>().Where(e => e.transform.parent == transform).ToArray(); } }

    // movement
    private void Update()
    {
        var pos = new Vector3(transform.position.x, transform.position.y, 0);
        transform.position = pos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);

        DefaultParent = DefaultShadowParent = transform.parent;
        transform.SetParent(DefaultParent.parent);
        BlockShadow.SetSiblingIndex(transform.GetSiblingIndex());

        GetComponent<CanvasGroup>().blocksRaycasts = false;

        if (!DefaultParent.GetComponent<Table>())
        {
            BlockShadow.GetComponent<Image>().enabled = true;
            SetBlockShadowForm();
        }
    }

    void SetBlockShadowForm()
    {
        var currentRect = GetComponent<RectTransform>();
        BlockShadow.GetComponent<RectTransform>().sizeDelta = currentRect.sizeDelta;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!DefaultParent.GetComponent<Table>())
        {
            BlockShadow.GetComponent<Image>().enabled = true;
            SetBlockShadowForm();
        }

        transform.position = (Vector2)(MainCamera.ScreenToWorldPoint(eventData.position) + offset);
        transform.localPosition -= Vector3.forward * transform.localPosition.z;

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

    public void OnDrop(PointerEventData eventData)
    {
        SemanticBlock semanticBlock = eventData.pointerDrag.GetComponent<SemanticBlock>();

        if (semanticBlock && CurrentPlacesOccupied < NumberOfPlaces)
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

        if (semanticBlock && CurrentPlacesOccupied < NumberOfPlaces)
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

        if (semanticBlock && CurrentPlacesOccupied < NumberOfPlaces)
        {
            semanticBlock.DefaultShadowParent = semanticBlock.DefaultParent;
        }
    }
}

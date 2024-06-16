using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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
    [SerializeField]
    protected TMP_Text blockTitle;
    public string BlockTitle { get { return blockTitle.text; } protected set { blockTitle.text = value; } }
    bool pointerIsInObject = false;
    protected bool PointerIsInObject { get { return pointerIsInObject; } }

    public void SetBlockShadowActive(bool isActive)
    {
        BlockShadow.GetComponent<Image>().enabled = isActive;
    }

    bool IsTestScene { get { return SceneManager.GetActiveScene().buildIndex == 2; } }

    public bool IsCorrectBlock { get { return CheckCorrectBlock() && AllPlacesOccepied; } }

    protected abstract bool CheckCorrectBlock();

    public abstract override string ToString();

    public abstract void SetBlockType(int blockTypeNumber);
    [SerializeField]
    protected int blockType;
    public int BlockType { get { return blockType; } }

    // inspector
    [SerializeField]
    int numberOfPlaces = 2;
    public int NumberOfPlaces { get { return numberOfPlaces; } 
        protected set { 
            numberOfPlaces = value;
        } }
    public int CurrentPlacesOccupied { get { return arguments.Length; } }
    public bool AllPlacesOccepied { get { return NumberOfPlaces == CurrentPlacesOccupied; } }

    [SerializeField]
    Color standartOutlineColor = Color.white;
    [SerializeField]
    Color incorrectOutlineColor = Color.red;
    void SetCorrectOutlineColor(bool isCorrect)
    {
        if(!IsTestScene)
            GetComponent<Outline>().effectColor = isCorrect ? standartOutlineColor : incorrectOutlineColor;
    }

    public void SetCorrectOutlineColor()
    {
        SetCorrectOutlineColor(CheckCorrectBlock() && AllPlacesOccepied);
    }

    // semantic params
    protected SemanticBlock[] arguments { get { return transform.GetComponentsInChildren<SemanticBlock>(true).Where(e => e.transform.parent == transform).ToArray(); } }
    protected object[] avaliableInputTypes;

    // movement
    private void Update()
    {
        var pos = new Vector3(transform.position.x, transform.position.y, 0);
        transform.position = pos;

        if (PointerIsInObject)
        {
            if(Input.GetKeyDown(KeyCode.D))
            {
                Destroy(gameObject);
            }
        }
    }

    #region DragAndDrop

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);
        if(DefaultParent?.GetComponent<SemanticBlock>())
            DefaultParent.GetComponent<SemanticBlock>().SetCorrectOutlineColor();

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

    public void SetBlockShadowForm()
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
        if (DefaultParent?.GetComponent<SemanticBlock>())
            DefaultParent.GetComponent<SemanticBlock>().SetCorrectOutlineColor();
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

        if (eventData.pointerEnter)
            pointerIsInObject = true;

        if (eventData.pointerDrag == null)
        {
            return;
        }

        SemanticBlock semanticBlock = eventData.pointerDrag.GetComponent<SemanticBlock>();

        if (semanticBlock && CurrentPlacesOccupied < NumberOfPlaces)
        {
            semanticBlock.SetBlockShadowActive(true);
            semanticBlock.SetBlockShadowForm();
            semanticBlock.DefaultShadowParent = transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        if (eventData.pointerEnter)
            pointerIsInObject = false;

        if (eventData.pointerDrag == null)
        {
            return;
        }
        SemanticBlock semanticBlock = eventData.pointerDrag.GetComponent<SemanticBlock>();


        if (semanticBlock && CurrentPlacesOccupied < NumberOfPlaces)
        {
            semanticBlock.SetBlockShadowActive(false);
            semanticBlock.DefaultShadowParent = semanticBlock.DefaultParent;
        }
    }
    #endregion
}

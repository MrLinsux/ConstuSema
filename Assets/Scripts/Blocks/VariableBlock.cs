using UnityEngine;
using UnityEngine.EventSystems;

public enum VariableType { Variable, Constatnt, Set }

public class VariableBlock : SemanticBlock, IPointerEnterHandler, IPointerExitHandler
{
    // inspector
    public VariableType VariableType { get { return (VariableType)blockType; } }
    [SerializeField]
    char variableName;
    public char VariableName { get { return variableName; } set { variableName = value; blockTitle.text = value.ToString(); } }

    private void Start()
    {
        SetBlockType(VariableType);
        blockTitle.text = variableName.ToString();
    }

    public override string ToString()
    {
        return VariableName.ToString();
    }

    void Update()
    {
        if (PointerIsInObject)
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput > 0f || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                VariableName = GetPreviousSymbol(VariableName);
            }
            else if (scrollInput < 0f || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                VariableName = GetNextSymbol(VariableName);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Destroy(gameObject);
            }

        }
    }

    public override void SetBlockType(int blockTypeNumber)
    {
        blockType = blockTypeNumber;
    }

    public void SetBlockType(VariableType operation)
    {
        SetBlockType((int)operation);
    }

    char GetNextSymbol(char c)
    {
        if ('A' <= c && c <= 'Z')
        {
            if (c == 'Z')
            {
                return 'A';
            }
            else
            {
                return ++c;
            }
        }
        else if('a' <= c && c <= 'z')
        {
            if (c == 'z')
            {
                return 'a';
            }
            else
            {
                return ++c;
            }
        }
        else if('α' <= c && c <= 'ω')
        {
            if (c == 'ω')
            {
                return 'α';
            }
            else
            {
                return ++c;
            }
        }
        else
        {
            return c;
        }
    }
    char GetPreviousSymbol(char c)
    {
        if ('A' <= c && c <= 'Z')
        {
            if (c == 'A')
            {
                return 'Z';
            }
            else
            {
                return --c;
            }
        }
        else if ('a' <= c && c <= 'z')
        {
            if (c == 'a')
            {
                return 'z';
            }
            else
            {
                return --c;
            }
        }
        else if ('α' <= c && c <= 'ω')
        {
            if (c == 'α')
            {
                return 'ω';
            }
            else
            {
                return --c;
            }
        }
        else
        {
            return c;
        }
    }

    protected override bool CheckCorrectBlock()
    {
        return true;
    }
}

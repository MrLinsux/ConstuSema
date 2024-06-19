using System.Linq;

public enum QuantifersType { ANY, EXIST, UNIQUE }

public class QuantiferBlock : SemanticBlock
{
    string[] quantiferTitles = new string[] { "∀", "∃", "∃!" };
    string asChar = "∀∃∄";

    public QuantifersType QuantiferType { get { return (QuantifersType)blockType; } }

    private void Start()
    {
        blockTitle.text = quantiferTitles[(int)QuantiferType];
    }

    public override void SetBlockType(int blockTypeNumber)
    {
        blockType = blockTypeNumber;
        blockTitle.text = quantiferTitles[blockType];
    }

    public void SetBlockType(QuantifersType operation)
    {
        SetBlockType((int)operation);
    }

    public override string ToString()
    {
        return asChar[blockType] + arguments[0].ToString();
    }

    protected override bool CheckCorrectBlock()
    {
        try
        {
            return arguments.Any(e =>
            (e is RelationBlock) ||
            ((e is VariableBlock) && e.BlockType < 2) ||
            (e is UserBlock));
        }
        catch
        {
            return false;
        }
    }
}

using TMPro;
using UnityEngine;

public class BlockDeck : MonoBehaviour
{
    [SerializeField]
    SemanticBlock block;
    [SerializeField]
    int blockType;

    private void Start()
    {
        block.SetBlockType(blockType);
        if(!(block is UserBlock))
            GetComponentInChildren<TMP_Text>().text = block.BlockTitle;
    }

    public void SpawnBlock()
    {
        if (block != null)
        {
            var _block = Instantiate(block, 
                block.transform.position + new Vector3(transform.position.x, transform.position.y, 0), 
                Quaternion.identity, 
                GameObject.Find("Table").transform
                ).GetComponent<SemanticBlock>();

            _block.SetBlockType(blockType);

        }
        else
            Debug.LogWarning("Block is null");
    }
}

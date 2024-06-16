using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public enum UserType { Standart }

public class UserBlock : SemanticBlock
{
    SemanticBlock SavedBlock { get { if (arguments.Length == 1) { return arguments[0]; } else { return null; }; } }
    [SerializeField]
    TMP_InputField inputNameField;
    [SerializeField]
    GameObject saveButton;
    [SerializeField]
    string standartBlockName = "MyBlock";

    private void Start()
    {
        SetBlockType(0);
    }

    public override void SetBlockType(int blockTypeNumber)
    {
        NumberOfPlaces = 1;
    }

    public override string ToString()
    {
        if(!SavedBlock)
        {
            throw new System.Exception("User Block must have Saved Block");
        }
        return SavedBlock.ToString();
    }

    public void SaveBlock()
    {
        if (SavedBlock != null)
        {
            SavedBlock.gameObject.SetActive(false);

            if (inputNameField.text == "")
            {
                int standartBlockNum = 0;
                while (GameObject.Find(standartBlockName + "(" + standartBlockNum + ")")) ;
                blockTitle.text = standartBlockName + "(" + (standartBlockNum++) + ")";
            }
            else
            {
                blockTitle.text = inputNameField.text;
            }

            blockTitle.gameObject.SetActive(true);      // also edit button
            inputNameField.gameObject.SetActive(false);
            saveButton.SetActive(false);
        }
    }

    public void EditBlock()
    {
        if(SavedBlock != null)
        {
            SavedBlock.gameObject.SetActive(true);
        }
        blockTitle.gameObject.SetActive(false);     // also edit button
        inputNameField.text = BlockTitle;
        inputNameField.gameObject.SetActive(true);
        saveButton.SetActive(true);
    }

    protected override bool CheckCorrectBlock()
    {
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.Mathematics;

public class TestCreatingController : MonoBehaviour
{
    [SerializeField]
    TMP_InputField taskTextInputField;
    [SerializeField]
    TMP_Text taskNumber;
    [SerializeField]
    GameObject table;
    [SerializeField]
    GameObject semanticConstruction;
    [SerializeField]
    TMP_Text nextQuestionButtonText;
    [SerializeField]
    GameObject removeQuestionButton;
    [SerializeField]
    string standartFileName = "MyFileOfTest";

    List<GameObject> tables = new List<GameObject>();
    List<GameObject> semanticConstructions = new List<GameObject>();

    int currentQuestionNumber;
    List<Question> questions;
    Question CurrentQuestion { get { return questions[currentQuestionNumber]; } }
    int MaxQuestionsNumber { get { return questions.Count; } }

    private void Start()
    {
        questions = new List<Question>();

        CreateNewQuestions();   

        SetQuestionWithIndex(0);
    }

    public void SaveTestFile()
    {
        for (int i = 0; i < questions.Count; i++)
        {
            questions[i].Answer = semanticConstructions[i].GetComponent<SemanticConstructionPanel>().CheckConstruction(false);
        }

        int standartFileNameNumber = -1;

        while (File.Exists(standartFileName + "(" + ++standartFileNameNumber + ")")) ;
        string fileName = standartFileName;
        if (standartFileNameNumber != 0)
        {
            fileName += "(" + standartFileNameNumber + ")";
        }
        fileName += "." + MainMenuPanel.fileOfTestFormat;

        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
        {
            formatter.Serialize(fs, questions.ToArray());
        }
    }

    public void SetQuestionWithIndex(int questionIndex)
    {
        // from 0
        // switch work areas
        tables[currentQuestionNumber].SetActive(false);
        tables[questionIndex].SetActive(true);
        semanticConstructions[currentQuestionNumber].SetActive(false);
        semanticConstructions[questionIndex].SetActive(true);
        // update current question number
        currentQuestionNumber = questionIndex;
        // change tasks
        taskNumber.text = (currentQuestionNumber) + ": ";
        taskTextInputField.text = CurrentQuestion.Task;
    }

    public void SaveCurrentTaskText()
    {
        CurrentQuestion.Task = taskTextInputField.text;
    }

    void CreateNewQuestions()
    {
        Question newQuestion = new Question();
        
        tables.Add(Instantiate(table, table.transform.position, Quaternion.identity, table.transform.parent));
        tables.Last().SetActive(false);
        semanticConstructions.Add(Instantiate(semanticConstruction, semanticConstruction.transform.position, Quaternion.identity, semanticConstruction.transform.parent));
        semanticConstructions.Last().SetActive(false);

        questions.Add(new Question());
    }

    public void RemoveCurrentQuestion()
    {
        if (MaxQuestionsNumber == 1)
        {
            return;
        }

        RemoveQuestionsWithIndex(currentQuestionNumber--);
    }

    void RemoveQuestionsWithIndex(int questionIndex)
    {
        questions.RemoveAt(questionIndex);
        tables.RemoveAt(questionIndex);
        semanticConstructions.RemoveAt(questionIndex);
    }

    public void SetNextQuestion()
    {
        if (currentQuestionNumber == MaxQuestionsNumber - 1)
        {
            CreateNewQuestions();
            SetNextQuestion();
        }
        else if(currentQuestionNumber == MaxQuestionsNumber - 2)
        {
            SetQuestionWithIndex((currentQuestionNumber + 1) % MaxQuestionsNumber);
            nextQuestionButtonText.text = "Добавить";
        }
        else
        {
            SetQuestionWithIndex((currentQuestionNumber + 1) % MaxQuestionsNumber);
            nextQuestionButtonText.text = ">";
        }
    }
    public void SetPreviousQuestion()
    {
        if (currentQuestionNumber < 1)
        {
            SetQuestionWithIndex(MaxQuestionsNumber - 1);
            nextQuestionButtonText.text = "Добавить";
        }
        else
        {
            SetQuestionWithIndex(currentQuestionNumber - 1);
            nextQuestionButtonText.text = ">";
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

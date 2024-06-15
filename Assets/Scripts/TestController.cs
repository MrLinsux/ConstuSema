using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestController : MonoBehaviour
{
    public static bool isEasyTest = false;

    [SerializeField]
    TMP_Text taskPanel;
    [SerializeField]
    GameObject table;
    [SerializeField]
    GameObject semanticConstruction;
    [SerializeField]
    GameObject resultPanel;
    [SerializeField]
    TMP_Text resultText;

    List<GameObject> tables = new List<GameObject>();
    List<GameObject> semanticConstructions = new List<GameObject>();

    int currentQuestionNumber;
    Question[] questions;
    Question CurrentQuestion { get { return questions[currentQuestionNumber]; } }
    int MaxQuestionsNumber {  get { return questions.Length; } }

    private void Start()
    {
        if(isEasyTest)
            questions = MakeTestQuestionSet();
        else
            LoadTestFile();

        StartTest(questions);
    }

    void ClearTest()
    {
        for(int i = 0; i < tables.Count; i++)
        {
            Destroy(tables[i]);
            Destroy(semanticConstructions[i]);
        }
        tables.Clear();
        semanticConstructions.Clear();
    }

    void LoadTestFile()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream fs = new FileStream(MainMenuPanel.testFileSrc, FileMode.OpenOrCreate))
        {
            questions = (Question[])formatter.Deserialize(fs);
        }
    }

    void StartTest(Question[] questions)
    {
        for(int i = 0; i < questions.Length; i++)
        {
            tables.Add(Instantiate(table, table.transform.position, Quaternion.identity, table.transform.parent));
            tables[i].SetActive(false);
            semanticConstructions.Add(Instantiate(semanticConstruction, semanticConstruction.transform.position, Quaternion.identity, semanticConstruction.transform.parent));
            semanticConstructions[i].SetActive(false);
        }
        SetQuestionWithNumber(0);
    }

    public void FinishTest()
    {
        string res = "";
        for(int i = 0; i < questions.Length; i++)
        {
            var answer = semanticConstructions[i].GetComponent<SemanticConstructionPanel>().CheckConstruction(false);

            res += $"Вопрос {i.ToString()} : ответ {(questions[i].AnswerIsCorrect(answer) ? "верный" : "неверный")}\n";
        }
        ClearTest();
        resultPanel.SetActive(true);
        resultText.text = res;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }


    Question[] MakeTestQuestionSet()
    {
        var questions = new Question[5];
        questions[0] = new Question("Соберите конъюнкцию.", "0001");
        questions[1] = new Question("Соберите исключающее или (сложение по модулю 2).", "0110");
        questions[2] = new Question("Соберите импликацию.", "1101");
        questions[3] = new Question("Соберите функцию с вектором (1).", "1");
        questions[4] = new Question("Соберите функцию с вектором (01010111).", "01010111");

        return questions;
    }

    public void SetQuestionWithNumber(int newQuestionNumber)
    {
        // from 0
        // switch work areas
        tables[currentQuestionNumber].SetActive(false);
        tables[newQuestionNumber].SetActive(true);
        semanticConstructions[currentQuestionNumber].SetActive(false);
        semanticConstructions[newQuestionNumber].SetActive(true);
        // update current question number
        currentQuestionNumber = newQuestionNumber;
        // change tasks
        taskPanel.text = (currentQuestionNumber) + ": " + CurrentQuestion.Task;
    }

    public void SetNextQuestion()
    {
        SetQuestionWithNumber((currentQuestionNumber + 1) % MaxQuestionsNumber);
    }
    public void SetPreviousQuestion()
    {
        if(currentQuestionNumber-1 < 0)
            SetQuestionWithNumber(MaxQuestionsNumber-1);
        else
            SetQuestionWithNumber(currentQuestionNumber-1);
    }
}

[Serializable]
public class Question
{
    string task;                // task title
    public string Task {  get { return task; } set { task = value; } }

    string answer;       // stantartizated answer
    public string Answer { get { return answer; } set { answer = value; } }

    public bool AnswerIsCorrect(string answer)
    {
        return answer == this.answer;
    }

    public Question(string task, string answer)
    {
        this.task = task; this.answer = answer;
    }

    public Question()
    {
        task = ""; answer = "";
    }
}

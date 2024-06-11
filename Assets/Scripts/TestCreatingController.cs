using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    List<GameObject> tables = new List<GameObject>();
    List<GameObject> semanticConstructions = new List<GameObject>();

    int currentQuestionNumber;
    List<Question> questions;
    Question CurrentQuestion { get { return questions[currentQuestionNumber]; } }
    int MaxQuestionsNumber { get { return questions.Count; } }

    public void SaveTestFile()
    {
        string res = "";
        for (int i = 0; i < questions.Count; i++)
        {
            var answer = semanticConstructions[i].GetComponent<SemanticConstructionPanel>().CheckConstruction(false);

            res += $"Вопрос {i.ToString()} : ответ {(questions[i].BooleanAnswerIsCorrect(answer) ? "верный" : "неверный")}\n";
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
        }
        else if(currentQuestionNumber == MaxQuestionsNumber - 2)
        {
            SetQuestionWithIndex((currentQuestionNumber + 1) % MaxQuestionsNumber);
            nextQuestionButtonText.text = "+";
        }
        else
        {
            SetQuestionWithIndex((currentQuestionNumber + 1) % MaxQuestionsNumber);
            nextQuestionButtonText.text = ">";
        }
    }
    public void SetPreviousQuestion()
    {
        if (currentQuestionNumber - 1 < 0)
            SetQuestionWithIndex(MaxQuestionsNumber - 1);
        else
            SetQuestionWithIndex(currentQuestionNumber - 1);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

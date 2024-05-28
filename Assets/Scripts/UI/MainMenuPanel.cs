using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    public string testFileSrc = @"C:\";
    [SerializeField]
    Button startTestButton;

    string GetTestName(string src)
    {
        var arrPath = src.Split('\\');
        string name = arrPath.Last().Remove(arrPath.Last().LastIndexOf('.'));
        return name;
    }

    void SetStartTestButton(bool isActive)
    {
        startTestButton.interactable = isActive;
        startTestButton.GetComponentInChildren<TMP_Text>().text = isActive ? "Начать тест\n" + GetTestName(testFileSrc) : "Тест не загружен";
    }

    private void Start()
    {
        if(File.Exists(testFileSrc))
        {
            LoadTest(testFileSrc);
        }
        else
        {
            SetStartTestButton(false);
        }
    }

    public void LoadCreateTestScene()
    {

    }

    public void LoadStartTestScene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadStartEasyTestScene()
    {
        TestController.isEasyTest = true;
        SceneManager.LoadScene(2);
    }

    public void LoadSandboxScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadTest()
    {
        // with dialog
        OpenFileName openFileName = new OpenFileName();
        openFileName.structSize = Marshal.SizeOf(openFileName);
        openFileName.filter = "";
        openFileName.file = new string(new char[256]);
        openFileName.maxFile = openFileName.file.Length;
        openFileName.fileTitle = new string(new char[64]);
        openFileName.maxFileTitle = openFileName.fileTitle.Length;
        openFileName.initialDir = Application.streamingAssetsPath.Replace('/', '\\');
        openFileName.title = "Выберите файл теста";
        openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;

        LocalDialog.GetOFN(openFileName);

        if(File.Exists(openFileName.file))
        {
            LoadTest(openFileName.file);
        }

    }

    void LoadTest(string src)
    {
        // from constructed file name

        testFileSrc = src;
        SetStartTestButton(true);
    }
}

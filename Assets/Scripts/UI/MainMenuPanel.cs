using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    public static string testFileSrc = @"C:\\";
    [SerializeField]
    Button startTestButton;
    public const string fileOfTestFormat = "mytl";

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
        Screen.fullScreen = true;
        if(File.Exists(testFileSrc))
        {
            LoadTest(testFileSrc);
        }
        else
        {
            SetStartTestButton(false);
        }
    }

    public void LoadTest()
    {
        // with dialog
        OpenFileName openFileName = new OpenFileName();
        openFileName.structSize = Marshal.SizeOf(openFileName);
        openFileName.filter = $".{fileOfTestFormat}\0*.{fileOfTestFormat}\0";
        openFileName.file = new string(new char[256]);
        openFileName.maxFile = openFileName.file.Length;
        openFileName.fileTitle = new string(new char[64]);
        openFileName.maxFileTitle = openFileName.fileTitle.Length;
        openFileName.initialDir = Application.streamingAssetsPath.Replace('/', '\\');
        openFileName.title = "Выберите файл теста";
        openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;
        openFileName.defExt = fileOfTestFormat;

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

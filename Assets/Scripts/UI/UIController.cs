using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadCreateTestScene()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadStartTestScene()
    {
        TestController.isEasyTest = false;
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

    public void MinimizeApplication()
    {
        ShowWindow(GetActiveWindow(), 2);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}

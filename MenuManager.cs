using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public static MenuManager sharedInstance;
    public Canvas menuCanvas, gameCanvas, gameOverCanvas, contCanvas;

    public GameObject signiv;


    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    public void ShowMainMenu()
    {
        menuCanvas.enabled = true;
    }

    public void HideMainMenu()
    {
        menuCanvas.enabled = false;
    }

    public void ShowGameMenu()
    {
        gameCanvas.enabled = true;
    }

    public void HideGameMenu()
    {
        gameCanvas.enabled = false;
    }

    public void ShowGameOverMenu()
    {
        gameOverCanvas.enabled = true;
    }

    public void HideGameOverMenu()
    {
        gameOverCanvas.enabled = false;
    }


    public void ShowContMenu()
    {
        contCanvas.enabled = true;
    }

    public void HideContMenu()
    {
        contCanvas.enabled = false;
    }

    public void ActivarSig()
    {
        signiv.SetActive(true);
    }
    

    public void ExitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}

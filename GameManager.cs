using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{
    menu,
    inGame,
    gameOver,
    controls
}


public class GameManager : MonoBehaviour {

    public GameState currentGameState = GameState.menu;

    public static GameManager sharedInstance;

    private PlayerController controller;

    public int collectedObject = 0;

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        SetGameState(GameState.menu);
    }

    void Start () {
        controller = GameObject.Find("Player").
                               GetComponent<PlayerController>();
	}
	
    public void StartGame(){
        SetGameState(GameState.inGame);
    } 

    public void GameOver(){
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu(){
        SetGameState(GameState.menu);
    }


    public void Controls()
    {
        SetGameState(GameState.controls);
    }

    public void VisitarClaro() {

        Application.OpenURL("https://www.claro.com.co");
    }


    private void SetGameState(GameState newGameSate){
        if(newGameSate == GameState.menu){
            MenuManager.sharedInstance.ShowMainMenu();
            MenuManager.sharedInstance.HideGameMenu();
            MenuManager.sharedInstance.HideGameOverMenu();
            MenuManager.sharedInstance.HideContMenu();
        }
        else if(newGameSate == GameState.inGame && currentGameState != GameState.inGame)
        {
            LevelManager.sharedInstance.RemoveAllLevelBlocks();
            LevelManager.sharedInstance.GenerateInitialBlocks();
            controller.StartGame();
            MenuManager.sharedInstance.HideMainMenu();
            MenuManager.sharedInstance.ShowGameMenu();
            MenuManager.sharedInstance.HideGameOverMenu();
            MenuManager.sharedInstance.HideContMenu();
        }
        else if(newGameSate == GameState.gameOver){
            MenuManager.sharedInstance.ShowMainMenu();
            MenuManager.sharedInstance.HideGameMenu();
            MenuManager.sharedInstance.HideGameOverMenu();
            MenuManager.sharedInstance.HideContMenu();
        }
        else if (newGameSate == GameState.controls)
        {
            MenuManager.sharedInstance.HideMainMenu();
            MenuManager.sharedInstance.HideGameMenu();
            MenuManager.sharedInstance.HideGameOverMenu();
            MenuManager.sharedInstance.ShowContMenu();
        }
        this.currentGameState = newGameSate;
    }

    public void CollectObject(Collectable collectable){
        collectedObject += collectable.value;
    }

}

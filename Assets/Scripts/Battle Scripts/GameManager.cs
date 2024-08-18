using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState { InGame,Pause, Win, Lose }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public BattleManager bManager = BattleManager.instance;

    public GameState gameState = GameState.InGame;


    public GameObject pauseUI;
    public GameObject winUI;
    public GameObject loseUI;

    KeyCode pause = KeyCode.P;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame


    void Update()
    {
        switch (gameState)
        {
            case GameState.InGame:
                if (bManager.player.isKnockedOut)
                {
                    gameState = GameState.Lose;
                }

                else if (bManager.enemy.isKnockedOut)
                {
                    gameState = GameState.Win;
                }

                if (Input.GetKeyUp(pause))
                {
                    gameState = GameState.Pause;
                }
                break;

            case GameState.Pause:
                Time.timeScale = 0;
                pauseUI.SetActive(true);

                if (Input.GetKeyUp(pause))
                {
                    Time.timeScale = 1;
                    pauseUI.SetActive(false);
                    gameState = GameState.InGame;
                }
                break;

            case GameState.Win:
                winUI.SetActive(true);
                break;

            case GameState.Lose:
                loseUI.SetActive(true);
                break;
        }

    }
}

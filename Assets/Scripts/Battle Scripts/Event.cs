using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Event : MonoBehaviour
{
    BattleManager bManager;

    public string sceneName;

    public GameObject Object;

    bool isTutorialDone= false;
    bool isAttackDone = false;
    bool isEnraged = false;


    public Sequence punch;
    public Sequence dodge;
    public Sequence special;

    public LevelManager levelManager;

    private void Start()
    {
        bManager = BattleManager.instance;
    }

    private void Update()
    {
        
        if (bManager.enemy.health <= 20)
        {
            PlayerPrefs.SetInt("Level", 2);
            SceneManager.LoadScene(sceneName);
        }
        if (Object.activeSelf) 
        {
            Time.timeScale = 0f;
        }
        else if (!Object.activeSelf&& !isTutorialDone)
        {
            Time.timeScale = 1;
            isTutorialDone = true;
        }

        if (bManager.enemy.health < 100 && !isEnraged) 
        {
            isEnraged=true;
        }

        if (!isAttackDone&& isEnraged) 
        {
            bManager.enemy.ChangeMoveList(dodge.moveList);
            bManager.enemy.moveSequence.Add(dodge);
            bManager.enemy.moveSequence.Add(dodge);
            bManager.enemy.moveSequence.Add(punch);
            bManager.enemy.moveSequence.Add(punch);
            bManager.enemy.moveSequence.Add(special);
            isAttackDone = true;
        }
    }
}

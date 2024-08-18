using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{

    BattleManager bManager;

    public Slider playerSlider;
    public Slider enemySlider;

    // Start is called before the first frame update
    void Start()
    {
        bManager = BattleManager.instance;

        playerSlider.maxValue = bManager.player.health;
        playerSlider.value = bManager.player.health;

        enemySlider.maxValue = bManager.enemy.health;
        enemySlider.value = bManager.player.health;

    }

    // Update is called once per frame
    void Update()
    {
        playerSlider.value = bManager.player.health;
        enemySlider.value = bManager.enemy.health;
    }
}

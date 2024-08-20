using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{

    BattleManager bManager;

    public Slider playerSlider;
    public Slider enemySlider;

    public TextMeshProUGUI StaminaPoints;

    // Start is called before the first frame update
    void Start()
    {
        bManager = BattleManager.instance;

        try
        {
            playerSlider.maxValue = bManager.player.health;
            playerSlider.value = bManager.player.health;

            enemySlider.maxValue = bManager.enemy.health;
            enemySlider.value = bManager.player.health;

            StaminaPoints.text = bManager.player.stamina.ToString();
        }
        catch 
        {
            Debug.Log("Some UI Is Missing Check BattleUI In BattleManager");
        }

    }

    // Update is called once per frame
    void Update()
    {
        playerSlider.value = bManager.player.health;
        enemySlider.value = bManager.enemy.health;
        StaminaPoints.text = bManager.player.stamina.ToString();
    }
}

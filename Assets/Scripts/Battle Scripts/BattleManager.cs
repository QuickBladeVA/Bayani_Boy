using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Move { Idle, LPunch, RPunch, LDodge, RDodge }

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public PlayerController player;

    public AIController enemy;

    public Move playerMove;
    public Move enemyMove;


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

        player = GetComponent<PlayerController>();
        enemy = GetComponent<AIController>();

        playerMove = Move.Idle;
        enemyMove = Move.Idle;

        StartCoroutine(Enemy(enemy.speed));
        StartCoroutine(Player(enemy.speed));
    }

    IEnumerator Enemy(int speed)
    {
        while (enemy.isKnockedOut == false)
        {
            yield return new WaitForSeconds(0.8f);
            enemy.NextMove();
            enemyMove = enemy.move;
            if (enemy.isHit)
            {
                enemy.health -= player.attack;
                enemy.Hit();

                if (enemy.health <= 0)
                {

                    enemy.isKnockedOut = true;
                }
            }
        }
    }
    IEnumerator Player(int speed)
    {
        while (player.isKnockedOut == false)
        {
            yield return new WaitForSeconds(0.01f);
            playerMove = player.move;
            if (player.isHit)
            {
                player.health -= enemy.attack;
                player.isHit = false;

                if (player.health <= 0) 
                {

                    player.isKnockedOut = true;
                }

            }
        }



    }
}

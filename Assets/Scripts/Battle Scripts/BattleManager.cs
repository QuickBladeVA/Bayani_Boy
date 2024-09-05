using System;
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


    public GameObject playerObj;
    public GameObject enemyObj;

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

        if (playerObj == null)
        {
            try
            {
                playerObj = GameObject.FindWithTag("Player");
            }
            catch 
            {
                Debug.Log("Objects with Tag:Player is Missing or More than 1");
            }
        }
        if (enemyObj == null)
        {
            try
            {
                enemyObj = GameObject.FindWithTag("Enemy");
            }
            catch
            {
                Debug.Log("Objects with Tag:Enemy is Missing or More than 1");
            }
        }
        List<Move> Idle = new List<Move>() { Move.Idle, Move.Idle, Move.Idle };
        enemy.ChangeMoveList(Idle);
    }


    private void Start()
    {

    }

    IEnumerator Enemy(int speed)
    {
        List<Move> Punch = new List<Move>() { Move.LPunch, Move.RPunch };

        while (!player.isKnockedOut && !enemy.isKnockedOut)
        {
            // Check if the player is tired
            if (player.isTired && enemy.moveList!= Punch)
            {
                enemy.ChangeMoveList(Punch);
            }

            yield return new WaitForSeconds(0.8f);

            // Make the enemy's next move
            enemy.NextMove();
            enemyMove = enemy.move;

            if (enemy.isHit)
            {
                if (player.hasSuper)
                {
                    enemy.health -= player.attack*4;
                    player.superPunch = 0;
                    player.hasSuper = false;
                }
                else
                {
                    enemy.health -= player.attack;
                }
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
        while (!player.isKnockedOut&&!enemy.isKnockedOut)
        {
                yield return new WaitForSeconds(0.01f);
                playerMove = player.move;
                if (player.isHit)
                {
                    player.health -= enemy.attack;
                    player.superPunch += enemy.attack;
                    player.isHit = false;
                    if (player.health <= 25 && !player.hasSuperGained) 
                    {
                        player.hasSuper= true;
                        player.hasSuperGained = true;
                    }


                    if (player.health <= 0)
                    {
                        player.isKnockedOut = true;
                    }

                }
                
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    BattleManager bManager;
    PlayerController player;
    AIController enemy;

    void Start()
    {
        bManager = BattleManager.instance;
        player = bManager.player;
        enemy = bManager.enemy;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.CompareTag("Player"))
        {
            if (other.CompareTag("Enemy"))
            {
                Hit(player, enemy, Move.LPunch, Move.RDodge);
                Hit(player, enemy, Move.RPunch, Move.LDodge);

            }

        }

        if (this.CompareTag("Enemy"))
        {

            if (other.CompareTag("Player"))
            {
                Hit(enemy, player, Move.LPunch, Move.RDodge);
                Hit(enemy, player, Move.RPunch, Move.LDodge);
            }
        }

    }

    void Hit(PlayerController self, AIController target, Move punch, Move dodge)
    {
        // Check if self uses punch and target does not use dodge or any punch move
        if (self.move == punch)
        {
            if (target.move == Move.LPunch || target.move == Move.RPunch)
            {
                bManager.player.stamina = 0;
            }
            else if (!(target.move == dodge || target.move == Move.LPunch || target.move == Move.RPunch))
            {
                // Target does not dodge or punch back, apply damage to the target only
                target.isHit = true;
            }
        }
    }


    void Hit(AIController self, PlayerController target, Move punch, Move dodge)
    {
        // Check if self uses punch
        if (self.move == punch)
        {

            if (!(target.move == dodge || target.move == Move.LPunch || target.move == Move.RPunch))
            {
                // Target does not dodge or punch back, apply damage to the target only
                target.isHit = true;
            }
        }
    }


}

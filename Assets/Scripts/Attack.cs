using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Attack : MonoBehaviour
{

    Fighter self;
   
    private void Start()
    {

        Fighter self = transform.parent.GetComponent<Fighter>();
        if (self == null)
        {
            Debug.Log("Attack Script - Parent GameObject is Missing Fighter Script");
        }

        BoxCollider2D hitBox = GetComponent<BoxCollider2D>();
        if (hitBox == null)
        {
            Debug.Log("Attack Script - " + (self != null ? self.name : "Unknown") + " : BoxCollider2D is Missing");
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Attempt to get the Fighter script from the other collider
        Fighter enemy = other.GetComponent<Fighter>();

        if (enemy != null)
        {
            if (enemy.state != State.Block && enemy.isHit == false)
            {
                enemy.hp -= 10 + (5 * self.attack);
                enemy.isHit = true;
                self.stamina -= 20;
            }
            else if (enemy.state == State.Block)
            {
                enemy.hp -= 1;
                enemy.stamina -= 5;
                self.stamina -= 30;
            }
        }
        else 
        {
            Debug.Log("Not targeting an enemy // ~ maybe air or the script of the object is missing ");
        }
    }

}

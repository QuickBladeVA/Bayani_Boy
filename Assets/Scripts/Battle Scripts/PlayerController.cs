using System.Collections;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public Character character;
    public Move move = Move.Idle;

    public string characterName;
    public int health, attack, speed, stamina, superPunch;

    public bool isKnockedOut = false;
    public bool isHit;
    public bool isTired = false;
    public bool hasSuper = false;
    public bool hasSuperGained = false;


    KeyCode LPunchKey = KeyCode.Z;
    KeyCode RPunchKey = KeyCode.X;
    KeyCode LDodgeKey = KeyCode.LeftArrow;
    KeyCode RDodgeKey = KeyCode.RightArrow;

    bool isNotMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        if (character == null)
        {
            Debug.Log("Player Character Script is missing: Check Character Folder for reference");
        }
        else
        {
            characterName = character.name;
            health = character.hp;
            superPunch = 0;
            attack = character.atk;
            speed = character.spd;
            stamina = 5;
        }

        StartCoroutine(Action());
        StartCoroutine(StaminaRegen());
    }

    IEnumerator StaminaRegen()
    {
        while (true)
        {
            if (stamina < 5 && !isTired)
            {
                yield return new WaitForSeconds(5f); // Wait for 5 seconds
                stamina = 5; 
            }
            else
            {
                yield return null; // If stamina is full, just wait for the next frame
            }
        }
    }


    IEnumerator Action()
    {
        while (true) // Continuously check for input
        {
            if (!isTired)
            {
                if (isNotMoving)
                {
                    move = GetMoveInput();
                    if (move != Move.Idle)
                    {
                        isNotMoving = false;
                        if (move == Move.LPunch || move == Move.RPunch)
                        {
                            stamina-=1;
                        }
                        yield return new WaitForSeconds(0.5f);
                        isNotMoving = true;
                    }
                }
                yield return null; // Wait for next frame
            }

            if (stamina <= 0) 
            {
                isTired = true;
                move = Move.Idle;
                yield return new WaitForSeconds(5f);
                stamina = 5;
                isTired = false;
            }

        }
    }

    private Move GetMoveInput()
    {
        if (Input.GetKeyDown(LPunchKey))
        {
            return Move.LPunch;
        }
        if (Input.GetKeyDown(RPunchKey))
        {
            return Move.RPunch;
        }
        if (Input.GetKeyDown(LDodgeKey))
        {
            return Move.LDodge;
        }
        if (Input.GetKeyDown(RDodgeKey))
        {
            return Move.RDodge;
        }

        return Move.Idle; // If no keys are pressed, return Idle
    }
}

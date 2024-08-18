using System.Collections;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public Character character;
    public Move move = Move.Idle;

    public string characterName;
    public int health, attack, speed;

    public bool isKnockedOut = false;
    public bool isHit;

    KeyCode LPunchKey = KeyCode.Z;
    KeyCode RPunchKey = KeyCode.X;
    KeyCode LDodgeKey = KeyCode.LeftArrow;
    KeyCode RDodgeKey = KeyCode.RightArrow;

    private bool isNotMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        if (character == null)
        {
            Debug.LogError("Player Character Script is missing");
        }
        else
        {
            characterName = character.name;
            health = character.hp;
            attack = character.atk;
            speed = character.spd;
        }

        StartCoroutine(Action());
    }

    IEnumerator Action()
    {
        while (true) // Continuously check for input
        {
            if (isNotMoving)
            {
                move = GetMoveInput();
                if (move != Move.Idle)
                {
                    isNotMoving = false;
                    yield return new WaitForSeconds(0.5f);
                    isNotMoving = true;
                }
            }
            yield return null; // Wait for next frame
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

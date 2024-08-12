using System.Collections;
using UnityEngine;

public enum State { Idle, Block, Attack,Downed}

public class Fighter : MonoBehaviour
{
    public Character character;
    public State state;
    public KeyCode blockKey = KeyCode.Z;
    public KeyCode attackKey = KeyCode.X;
    public int hp;
    public int attack;
    public float stamina = 100;
    public bool canBlock = true;
    public bool canAttack = true;
    public bool isHit= false;
    public bool isTired=false;

    public Transform attackBox;

    SpriteRenderer sr;
    Rigidbody2D rb;

    void Awake()
    {
        Setup();    
    }

    private void Start()
    {
        StartCoroutine(StaminaRegen());
        hp = hp + 100;
        stamina = 100;

        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (state != State.Downed) 
        {
            Controls();
            Actions();
        }

        if (hp <= 0)
        {
            hp = Mathf.Max(hp, 0);
            sr.color = Color.black;
            state = State.Downed;
        }

        if (stamina <= 0)
        {
            isTired = true;
        }
        else if (stamina >= 100)
        {
            isTired = false;
            sr.color = Color.white;
        }

    }

    void Setup()
    {
        // Check if character is assigned
        if (character == null)
        {
            Debug.Log("Fighter Script - " + gameObject.name + " : Character Script is Missing");
        }
        else
        {
            hp = character.hp;
            attack = character.atk;
            name = character.name;
        }

        try
        {
            attackBox = transform.Find("Attack");
            try
            {
                Attack attack = attackBox.gameObject.GetComponent<Attack>();
            }
            catch
            {
                Debug.Log(name + " : Child  Attack Script  is Missing");
            }

            attackBox.gameObject.SetActive(false);

        }
        catch
        {
            Debug.Log("Figther Script - " + name + " : Child  Attack  is Missing");
        }

        // Attempt to get Rigidbody2D and set gravity scale
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.Log("Fighter Script - " + name + " : Rigidbody2D is Missing");
        }
        else
        {
            rb.gravityScale = 0f;
        }
    }

    void Controls() 
    {
        // Blocking logic
        if (Input.GetKey(blockKey) && canBlock && state != State.Attack && isHit == false&& isTired == false)
        {
            state = State.Block;

        }
        else if (Input.GetKeyUp(blockKey)|| stamina<=0)
        {
            StartCoroutine(ReturnIdle(0));
        }

        // Attacking logic
        if (Input.GetKeyDown(attackKey) && canAttack && state != State.Block && isHit == false&& isTired == false)
        {
            state = State.Attack;
        }

    }

    void Actions() 
    {
        if (state == State.Block) 
        {

            stamina -= Time.deltaTime * 10;
            stamina = Mathf.Max(stamina, 0f);
            sr.color = Color.blue;

        }
        if (state == State.Attack)
        {
            StartCoroutine(Attack());
            StartCoroutine(ReturnIdle(0.5f));
        }
        if (isHit)
        {
            sr.color = Color.red;
            StartCoroutine(Recover(0.3f));
        }
        if (isTired)
        {
            sr.color = Color.yellow;
        }
    }

    IEnumerator StaminaRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);

            if (state == State.Idle)
            {
                stamina += 5f;
                stamina = Mathf.Clamp(stamina, 0f, 100); // Clamp the stamina between 0 and 100
            }
        }
    }

    IEnumerator ReturnIdle(float sec) 
    {
        yield return new WaitForSeconds(sec);

        state = State.Idle;
        sr.color = Color.white;
    }
    IEnumerator Recover(float sec)
    {
        yield return new WaitForSeconds(sec);

        isHit = false;
        sr.color = Color.white;
    }
    IEnumerator Attack() 
    {
        canAttack = false;
        attackBox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        attackBox.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        canAttack = true;

    }

}

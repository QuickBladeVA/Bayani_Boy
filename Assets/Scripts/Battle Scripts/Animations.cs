using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public Animator pAnimator;
    public Animator eAnimator;

    public BattleManager bManager;


    // Start is called before the first frame update
    void Start()
    {
        bManager = BattleManager.instance;

        if (pAnimator == null)
        {
            GameObject obj;
            obj = GameObject.FindWithTag("Player");
            pAnimator = obj.GetComponent<Animator>();
        }

        if (eAnimator == null)
        {
            GameObject obj;
            obj = GameObject.FindWithTag("Enemy");
            eAnimator = obj.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bManager.player.isHit)
        {
            AnimationHit(bManager.enemy.move,pAnimator);
        }
        else if (bManager.player.isKnockedOut) 
        {
            AnimationKnocked(pAnimator);   
        }
        else
        {
            AnimationState(bManager.player.move, pAnimator);
        }
        if (bManager.enemy.isHit)
        {
            AnimationHit(bManager.player.move, eAnimator);
        }
        else if (bManager.enemy.isKnockedOut)
        {
            AnimationKnocked(eAnimator);
        }
        else
        {
            AnimationState(bManager.enemy.move, eAnimator);
        }
    }

    private void AnimationState(Move move, Animator animator)
    {
        switch (move)
        {
            case Move.LPunch:
                animator.SetTrigger("LPunch");
                break;
            case Move.RPunch:
                animator.SetTrigger("RPunch");
                break;
            case Move.LDodge:
                animator.SetTrigger("LDodge");
                break;
            case Move.RDodge:
                animator.SetTrigger("RDodge");
                break;
            case Move.Idle:
                animator.SetTrigger("Idle");
                break;
        }
    }
    private void AnimationHit(Move attackerMove, Animator animator) 
    {
        if (attackerMove == Move.LPunch)
        {
            animator.SetTrigger("RHit");
        }
        if (attackerMove == Move.RPunch)
        {
            animator.SetTrigger("LHit");
        }
    }

    private void AnimationKnocked(Animator animator) 
    {
        animator.SetTrigger("Knocked");
    }

}


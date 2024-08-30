using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    Animator pAnimator;
    Animator eAnimator;
    BattleManager bManager;

    SpriteRenderer pSR;
    SpriteRenderer eSR;

    AudioSource pAS;
    AudioSource eAS;

    public AudioClip phit;
    public AudioClip ehit;
    public AudioClip dodge;
    public AudioClip super;
    public AudioClip tired;
    public AudioClip defeat;


    bool pHitSoundPlayed;
    bool pKnockedSoundPlayed;
    bool pDodgeSoundPlayed;

    bool eHitSoundPlayed;
    bool eKnockedSoundPlayed;
    bool eDodgeSoundPlayed;


    // Start is called before the first frame update
    void Start()
    {
        bManager = BattleManager.instance;

        if (pAnimator == null)
        {
            pAnimator = bManager.playerObj.GetComponent<Animator>();
        }

        if (eAnimator == null)
        {
            eAnimator = bManager.enemyObj.GetComponent<Animator>();
        }

        pSR = pAnimator.gameObject.GetComponent<SpriteRenderer>();
        eSR = eAnimator.gameObject.GetComponent<SpriteRenderer>();

        pAS = eAnimator.gameObject.GetComponent<AudioSource>();
        eAS = eAnimator.gameObject.GetComponent<AudioSource>();

        StartCoroutine(AnimationSuper());

        pAS.volume = 0.25f;
        eAS.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle player animations and sounds
        if (bManager.player.isHit)
        {
            AnimationHit(bManager.enemy.move, pAnimator);
            if (!pHitSoundPlayed)
            {
                pAS.PlayOneShot(phit);
                pHitSoundPlayed = true;
            }
        }
        else if (bManager.player.isKnockedOut && !pKnockedSoundPlayed)
        {
            AnimationKnocked(pAnimator);
            pAS.PlayOneShot(defeat);
            pKnockedSoundPlayed = true;
        }
        else
        {
            AnimationState(bManager.player.move, pAnimator);
            if ((bManager.player.move == Move.LDodge || bManager.player.move == Move.RDodge) && !pDodgeSoundPlayed)
            {
                pAS.PlayOneShot(dodge);
                pDodgeSoundPlayed = true;
            }
        }

        // Reset player sound flags based on conditions
        if (!bManager.player.isHit)
            pHitSoundPlayed = false;

        if (!bManager.player.isKnockedOut)
            pKnockedSoundPlayed = false;

        if (bManager.player.move != Move.LDodge && bManager.player.move != Move.RDodge)
            pDodgeSoundPlayed = false;

        if (bManager.player.isTired&& !bManager.player.isKnockedOut)
        {
            AnimationColor(pSR, Color.blue);
 
            pAS.PlayOneShot(tired); 
            
        }
        else if (!bManager.player.isTired || bManager.player.isKnockedOut)
        {
            if (!bManager.player.hasSuper)
            {
                AnimationColor(pSR, Color.white);
            }
        }

        // Handle enemy animations and sounds
        if (bManager.enemy.isHit)
        {
            AnimationHit(bManager.player.move, eAnimator);
            if (!eHitSoundPlayed)
            {
                pAS.PlayOneShot(ehit);
                eHitSoundPlayed = true;
            }
        }
        else if (bManager.enemy.isKnockedOut)
        {
            AnimationKnocked(eAnimator);
            if (!eKnockedSoundPlayed)
            {
                pAS.PlayOneShot(defeat);
                eKnockedSoundPlayed = true;
            }
        }
        else
        {
            AnimationState(bManager.enemy.move, eAnimator);
            if ((bManager.enemy.move == Move.LDodge || bManager.enemy.move == Move.RDodge) && !eDodgeSoundPlayed)
            {
                pAS.PlayOneShot(dodge);
                eDodgeSoundPlayed = true;
            }
        }

        // Reset enemy sound flags based on conditions
        if (!bManager.enemy.isHit)
            eHitSoundPlayed = false;

        if (!bManager.enemy.isKnockedOut)
            eKnockedSoundPlayed = false;

        if (bManager.enemy.move != Move.LDodge && bManager.enemy.move != Move.RDodge)
            eDodgeSoundPlayed = false;
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

    private void AnimationColor(SpriteRenderer sr, Color color)
    {
        sr.color = color;
    }

    IEnumerator AnimationSuper()
    {
        while (true)
        {
            if (bManager.player.hasSuper && !bManager.player.isTired && !bManager.player.isKnockedOut)
            {
                pAS.PlayOneShot(super);
                yield return new WaitForSeconds(0.2f);
                AnimationColor(pSR, Color.yellow);
                yield return new WaitForSeconds(0.2f);
                AnimationColor(pSR, Color.white);

            }
            yield return null; // Wait for the next frame before rechecking
        }
    }
}

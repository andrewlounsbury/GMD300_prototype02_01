using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimation : MonoBehaviour
{
    //animator componenet 
    private Animator animator;

    //private variables 
    private float currentAttackCombo = 0;
    private float maxAttackCombo = 3;

    //sets animator equal to the component on obj start 
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //each of these ifs/else ifs sets the attack combo back to zero if a player breaks the 3 attack combo at any point and plays the corresponding 
        //transition animation back to idle 
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("AttackDone"))
        {
            currentAttackCombo = 0;
            animator.SetFloat("AttackCombo", currentAttackCombo);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BackToIdle2"))
        {
            currentAttackCombo = 0;
            animator.SetFloat("AttackCombo", currentAttackCombo);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BackToIdle3"))
        {
            currentAttackCombo = 0;
            animator.SetFloat("AttackCombo", currentAttackCombo);
        }
    }

    //function for setting walking animation true if player walking 
    public void WalkAnim(bool IsWalking)
    {
        animator.SetBool("IsWalking", IsWalking); 
    }

    //function for setting attack animation true if player attacks once 
    public void AttackAnim1(bool Attack1)
    {
        animator.SetBool("Attack1", Attack1);
    }

    //function for setting attack animation true if player attacks twice 
    public void AttackAnim2(bool Attack2)
    {
        animator.SetBool("Attack2", Attack2);
    }

    //function for setting attack animation true if player attacks thrice 
    public void AttackAnim3(bool Attack3)
    {
        animator.SetBool("Attack3", Attack3);
    }

    //function for ending attack 1 if player does not press attack twice 
    public void AttackEnd1()
    {
        animator.SetBool("Attack1", false);
    }

    //function for endin attack 2 if player does not attakc thrice 
    public void AttackEnd2()
    {
        animator.SetBool("Attack2", false);
    }

    //sets attack 3 false on attack 3 anim end 
    public void AttackEnd3()
    {
        animator.SetBool("Attack3", false);
    }

    //clamps the attack combo between zero and the max of 3, adding one to it each time playe presses attack input in ThirdPersonController script 
    public void TriggerAttackCombo()
    {
        currentAttackCombo = Mathf.Clamp(currentAttackCombo + 1, 0, maxAttackCombo);

        //changes the value of the attack combo float 
        animator.SetFloat("AttackCombo", currentAttackCombo);
    }

}

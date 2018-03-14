using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Combat")]
    private bool canAttack = true;
    public float attackDamage;
    public float attackSpeed;

    Animator anim;
    PlayerMovement playerMovement;

    public int attackType = 0;




    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            attackType = 1;
            Attack();
        }
        if ((Input.GetKey(KeyCode.Mouse1)))
        {
            attackType = 2;
            Attack(); 
            Debug.Log("Special Attack");
        }

    }

    public void Attack()
    {
        if (!canAttack) return;
        if(attackType == 1)
        {
            int randomNumber = UnityEngine.Random.Range(1, 4);
            Debug.Log(randomNumber);
            anim.SetTrigger("Attack" + randomNumber);
        }
        if(attackType == 2)
        {
            anim.SetTrigger("SpecialAttack");
            Debug.Log(attackType);
        }
        // anim.SetBool("IsAttacking", true);
        StartCoroutine(AttackCooldown());
        Debug.Log("Attacking");

    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        playerMovement.enabled = false;
        yield return new WaitForSeconds(1 / attackSpeed);
        playerMovement.enabled = true;
        canAttack = true;
    }
}

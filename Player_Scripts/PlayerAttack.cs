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
    private List<Transform> enemiesInRange = new List<Transform>();

    Animator anim;
    PlayerMovement playerMovement;

    EnemyController ec;

    private int attackType = 0;


    private void OnSlashAnimationHit()
    {
        Debug.Log("DealDamage ON");
        DealDamage();
    }


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        ec = GetComponent<EnemyController>();
    }

    private void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            attackType = 1;
            Attack();
            StartCoroutine(AttackCooldown());
        }
        if ((Input.GetMouseButton(1)))
        {
            attackType = 2;
            Attack();
            StartCoroutine(AttackCooldown());
            Debug.Log("Special Attack");
        }

    }

    public void Attack()
    {
        if (!canAttack) return;
        if (attackType == 1)
        {
            int randomNumber = UnityEngine.Random.Range(1, 4);
            Debug.Log(randomNumber);
            anim.SetTrigger("Attack" + randomNumber);
        }
        if (attackType == 2)
        {
            anim.SetTrigger("SpecialAttack");
            Debug.Log(attackType);
        }
        Debug.Log("Attacking");

    }

    void DealDamage()
    {
        Debug.Log("OnSlashAnimationHit Played");
        GetEnemiesInRange();
        foreach (Transform enemy in enemiesInRange)
        {
            EnemyController ec = enemy.GetComponent<EnemyController>();
            if (ec == null)
            {
                continue;
            }
            Debug.Log("Enemies in range -" +enemiesInRange.Count);
            ec.GetHit(attackDamage);
        }
    }

    void GetEnemiesInRange()
    {
        enemiesInRange.Clear();
        foreach (Collider c in Physics.OverlapSphere((transform.position + transform.forward * 0.5f), 0.5f))
        {
            if (c.gameObject.CompareTag("Enemy"))
            {
                enemiesInRange.Add(c.transform);
            }
        }
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

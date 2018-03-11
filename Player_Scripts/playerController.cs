using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour
{
    Animator anim;
    [Header("Movement")]
    private bool canMove = true;
    public float movementSpeed;
    public float rotSpeed = 3f;
    public float crouchSpeed;
    public bool isAttacking = false;
    Rigidbody rb;
    // private float jumpHeight = 200f;

    [Header("Combat")]
    private List<Transform> enemiesInRange = new List<Transform>();
    private bool canAttack = true;
    private bool attacking;
    public float attackDamage = 5f;
    public float attackSpeed;
    public float attackRange;

    private void Start()
    {
        AnimationsEvent.OnSlashAnimationHit += DealDamage;

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //isGrounded = true;

    }

    private void FixedUpdate()
    {
        // Move script 
        var z = Input.GetAxis("Vertical") * movementSpeed;
        var y = Input.GetAxis("Horizontal") * rotSpeed;

        transform.Translate(0, 0, z);
        transform.Rotate(0, y, 0);


        // Animation event
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            movementSpeed = 0.08f;
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = crouchSpeed;
            anim.SetBool("IsWalkingBack", true);
        }
        else
        {
            anim.SetBool("IsWalkingBack", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            movementSpeed = 0;
            Attack();
        }
        else
        {
            anim.SetBool("IsAttacking", false);

        }
    }
    // COMBAT
    void Attack()
    {
        if (!canAttack) return;
        anim.SetBool("IsAttacking", true);
        StartCoroutine(AttackRoutine());
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackRoutine()
    {
        canMove = false;
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(1 / attackSpeed);
        canMove = true;

    }


    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(1 / attackSpeed);
        canAttack = true;
    }

    void DealDamage()
    {
        GetEnemiesInRange();
        foreach (Transform enemy in enemiesInRange)
        {
            EnemyController mc = enemy.GetComponent<EnemyController>();
            if (mc == null)
            {
                continue;
            }
            mc.GetHit(attackDamage);
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

   
}



//private void OnCollisionEnter(Collision collision)
//{
//    isGrounded = true;
//}





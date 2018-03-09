using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    public Animator anim;
    public float totalHealth;
    public float currentHealth;
    public float extGranted;
    //CapsuleCollider capsuleCollider;
    //public float atkDamage;
    //public float atkSpeed;
    EnemyController mummyController;
    SphereCollider sphereCollider;
    private GameObject[] players;
    private bool isAttacked = false;
    EnemyAttack enemyAttack;
    public float lookRaduis = 5f;
    Transform target;
    NavMeshAgent agent;
    private bool dead;


    // Use this for initialization
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        currentHealth = totalHealth;
        //  target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        sphereCollider = GetComponent<SphereCollider>();
        enemyAttack = GetComponent<EnemyAttack>();
        mummyController = GetComponent<EnemyController>();
    }

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRaduis)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
                anim.SetBool("isIdle", true);
                anim.SetBool("isWalking", false);
            }
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
        }

    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRaduis);
    }

    public void GetHit(float damage)
    {
        isAttacked = true;
        if (isAttacked)
        {
            anim.SetBool("isAttacked", true);
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isAttacked", false);
        }

        if (dead)
        {
            return;
        }
        currentHealth = currentHealth - damage;
        if (currentHealth <= 0)
        {
            Die();
            return;
        }
        StartCoroutine(RecoverFromHit());
    }

    void DropLoot()
    {
        print("Wow");
    }

    private void Die()
    {
        dead = true;
        enemyAttack.enabled = false;
        mummyController.enabled = false;
        //sphereCollider.enabled = false;
        //sphereCollider.isTrigger = false;

        agent.speed = 0;
        anim.SetBool("isDead", true);
        anim.SetBool("isAttacked", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isWalking", false);
        DropLoot();
        foreach (GameObject go in players)
        {
            go.GetComponent<playerController>().GetExperience(extGranted / players.Length);
        }
        GameObject.Destroy(this.gameObject, 15);
    }

    IEnumerator RecoverFromHit()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("isIdle", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isAttacked", false);
    }
}

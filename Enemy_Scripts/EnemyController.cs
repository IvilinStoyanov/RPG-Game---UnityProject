using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    private Animator anim;
    public float totalHealth;
    public float currentHealth;
    public float extGranted;
    //CapsuleCollider capsuleCollider;
    //public float atkDamage;
    //public float atkSpeed;
    CapsuleCollider capsuleCollider;
    private GameObject[] players;
    private bool isAttacked = false;
    EnemyAttack enemyAttack;
    public float lookRaduis = 5f;
    Transform target;
    NavMeshAgent agent;
    public bool IsDead;
    //Audio
    public AudioClip deathClip;
    AudioSource enemyAudio;


    // Use this for initialization
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        currentHealth = totalHealth;
        //  target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        enemyAttack = GetComponent<EnemyAttack>();
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();
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
            //anim.SetBool("isWalking", true);
            //anim.SetBool("isIdle", false);
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
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
        //if (isAttacked)
        //{
        //    //anim.SetTrigger("Attacking");
        //}

        if (IsDead)
        {
            return;
        }
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        StartCoroutine(RecoverFromHit());
    }

    void DropLoot()
    {
        print("Wow");
    }

    private void Die()
    {
        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        IsDead = true;
        enemyAttack.enabled = false;
        capsuleCollider.isTrigger = true;
        agent.speed = 0;;
        anim.SetTrigger("isDead");
        DropLoot();
        foreach (GameObject go in players)
        {
           go.GetComponent<playerLevelSystem>().GetExperience(extGranted / players.Length);
        }
        GameObject.Destroy(this.gameObject, 15);
    }

    IEnumerator RecoverFromHit()
    {
        yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("Attacking");
    }
}

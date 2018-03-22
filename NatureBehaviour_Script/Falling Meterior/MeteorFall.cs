using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorFall : MonoBehaviour
{
    public float radius = 10f;
    public float force = 3000f;

    bool hasExploded = false;
    public bool IsPlayerHit = false;


    public GameObject explosionEffect;
    public GameObject craterMeteor;

    private AudioSource audioSource;
    public AudioClip meteorFall;

    PlayerHealth playerHealth;
    EnemyController enemyController;

    GameObject player;
    GameObject enemy;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        enemyController = enemy.GetComponent<EnemyController>();
        playerHealth = player.GetComponent<PlayerHealth>();

        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(meteorFall);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasExploded)
        {
            if (collision.gameObject.tag == "Player")
            {
                playerHealth.TakeDamage(10000000);
                Debug.Log("Player hit by meteor");
                IsPlayerHit = true;
            }
            if (collision.gameObject.tag == "Enemy")
            {
                enemyController.Die();
                IsPlayerHit = true;
                Debug.Log("Enemy hit by meteor");
            }
            Explode();
            hasExploded = true;
        }
    }

    private void HitEnemy()
    {
        throw new NotImplementedException();
    }

    private void Explode()
    {
        if (!IsPlayerHit)
        {
            Instantiate(craterMeteor, transform.position, transform.rotation);
        }

        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObjects in colliders)
        {
            Rigidbody rb = nearbyObjects.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // rb.AddExplosionForce(force, transform.position, radius);
                rb.AddForce(0, 200, 0);
            }
        }
        Destroy(gameObject);
    }
}

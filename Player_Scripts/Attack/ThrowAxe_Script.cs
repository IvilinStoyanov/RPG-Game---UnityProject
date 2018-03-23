using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAxe_Script : MonoBehaviour
{

    [SerializeField]
    [Range(0.5f, 1.5f)]
    private float fireRate = 1;

    [SerializeField]
    [Range(50, 100)]
    private int damage = 50;

    private float timer;

    [SerializeField]
    GameObject axe;

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timer >= fireRate)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                timer = 0f;
                ThrowAxe();
            }
        }
    }

    private void ThrowAxe()
    {
        axe = Instantiate(axe, transform.position, transform.rotation) as GameObject;
        var axeRb = axe.GetComponent<Rigidbody>();

        axeRb.AddForce(transform.forward * 1000);



        //Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        //Ray ray = new Vector3(transform.position, transform.rotation);
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
        //Debug.Log("Axe throwed");

        //Ray ray = new Ray(firePoint.position, firePoint.forward);
        //RaycastHit hitinfo;

        //if (Physics.Raycast(ray, out hitinfo, 100))
        //{
        //    Destroy(hitinfo.collider.gameObject);
        //}
    }
}

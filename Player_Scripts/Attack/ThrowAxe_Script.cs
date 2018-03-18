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
    [Range(1, 10)]
    private int damage = 1;

    private float timer;

    // Update is called once per frame
    void Update()
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
       Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
        Debug.Log("Axe throwed");

       // Ray ray = new Ray(firePoint.position, firePoint.forward);
        //RaycastHit hitinfo;

        //if(Physics.Raycast(ray, out hitinfo, 100))
        //{
        //    Destroy(hitinfo.collider.gameObject);
        //}
    }
}

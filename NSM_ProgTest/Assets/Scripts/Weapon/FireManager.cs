using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject Arrow;

    private void Update()
    {
       // if (Input.GetButtonDown("Fire1"))
            //Shoot();
    }

    private void Shoot()
    {
        Instantiate(Arrow, shootPoint.position, shootPoint.rotation);
    }
}

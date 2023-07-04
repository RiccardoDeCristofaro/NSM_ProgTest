using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject Arrow;

    [SerializeField] float arrowForce;
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    private void Shoot()
    {
        GameObject arrow = Instantiate(Arrow, shootPoint.position, shootPoint.rotation);
        Rigidbody2D arrowRB = arrow.GetComponent<Rigidbody2D>();
        arrowRB.AddForce(shootPoint.right * arrowForce, ForceMode2D.Impulse);
    }
}

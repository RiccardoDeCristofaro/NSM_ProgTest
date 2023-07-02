using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private Rigidbody2D bulletRb;
    private void Awake()
    {
        bulletRb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        bulletRb.velocity = transform.right * bulletSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemie enemie = collision.GetComponent<Enemie>();
        if(enemie != null )
        {
            enemie.TakeDamage(enemie.health);
        }
        Debug.LogWarning(collision.name);
        Destroy(bulletRb);
    }
}

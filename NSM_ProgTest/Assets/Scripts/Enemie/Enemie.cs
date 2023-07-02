using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : MonoBehaviour
{
    public int health;
    private Animator enemieAnimator;
    private void Awake()
    {
        enemieAnimator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       switch(tag)
        {
            case "Player":
                if(collision.gameObject.CompareTag("Player"))
                 Destroy(collision.gameObject);
                break;

            case "Arrow":
                Destroy(gameObject); break;
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
            Die();
    }

    private void Die()
    {
        enemieAnimator.Play("EnemieDie");
        Destroy(gameObject);
    }
}

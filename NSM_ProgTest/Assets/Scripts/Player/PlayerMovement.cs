using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("movement speed:")]
    [SerializeField] private float speed;
    private Vector2 MoveDir;

    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.velocity = rb.position + MoveDir * speed * Time.fixedDeltaTime;
    }
    private void Update()
    {
        Inputs();
    }
    private void Inputs()
    {
        MoveDir.x = Input.GetAxis("Horizontal");
        MoveDir.y = Input.GetAxis("Vertical");
    }
}
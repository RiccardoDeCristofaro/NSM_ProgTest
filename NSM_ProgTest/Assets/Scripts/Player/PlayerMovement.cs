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
    private Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
       rb.MovePosition(rb.position + MoveDir * speed * Time.fixedDeltaTime);
    }
    private void Update()
    {
        Inputs();
        AnimatorSetUp();
    }
    private void Inputs()
    {
        MoveDir.x = Input.GetAxisRaw("Horizontal");
        MoveDir.y = Input.GetAxisRaw("Vertical");
    }
    private void AnimatorSetUp()
    {
        animator.SetFloat("Horizontal", MoveDir.x);
        animator.SetFloat("Vertical", MoveDir.y);
        animator.SetFloat("Speed", MoveDir.sqrMagnitude);
    }
}
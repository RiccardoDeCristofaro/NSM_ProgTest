using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("movement speed:")]
    [SerializeField] private float speed;
    [SerializeField] private Inputs inputsType;
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
        SetInputs();
    }
    private void Update()
    {
        AnimatorSetUp();
        InputsSetUp();
    }
    private void SetInputs()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        switch(inputsType)
            {
            case Inputs.top:
                rb.MovePosition(new Vector2(rb.position.x, rb.position.y * inputY) * speed * Time.fixedDeltaTime);
                break;
            case Inputs.bottom:
                rb.MovePosition(new Vector2(rb.position.x, rb.position.y / inputY) * speed * Time.fixedDeltaTime);                
                break;
            case Inputs.left:
                rb.MovePosition(new Vector2(rb.position.x / inputX, rb.position.y) * speed * Time.fixedDeltaTime);
                break;
            case Inputs.right:
                rb.MovePosition(new Vector2(rb.position.x * inputX, rb.position.y) * speed * Time.fixedDeltaTime);
                break;
        }
    }
    private void InputsSetUp()
    {
        if (Input.GetKeyDown(KeyCode.W))
            inputsType = Inputs.top;
        if (Input.GetKeyDown(KeyCode.D))
            inputsType = Inputs.right;
        if (Input.GetKeyDown(KeyCode.S))
            inputsType = Inputs.bottom;
        if (Input.GetKeyDown(KeyCode.A))
            inputsType = Inputs.left;

    }
    private void AnimatorSetUp()
    {
        animator.SetFloat("Horizontal", MoveDir.x);
        animator.SetFloat("Vertical", MoveDir.y);
        animator.SetFloat("Speed", MoveDir.sqrMagnitude);
    }
    private enum Inputs { top, right, left, bottom }

}
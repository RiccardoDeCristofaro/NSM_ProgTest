using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GridBasedMovement : MonoBehaviour
{
    [Header("movement speed :")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform pointToReach;
    private Animator animator;

    [SerializeField]
    private LayerMask whatisWall;

    private float xInput;
    private float yInput;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start() => pointToReach.parent = null;

    private void Update()
    {
        SetInputs();
        AnimatorSetUp();
    }
    private void SetInputs()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        transform.position = Vector3.MoveTowards(transform.position, pointToReach.position, 1f * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, pointToReach.position) <= 0.05f)
        {
            if (Mathf.Abs(xInput) == 1f)
                if (!CheckIfWallOnX())
                    pointToReach.position += new Vector3(xInput, 0f, 0f);

            if (Mathf.Abs(yInput) == 1f)
                if (!CheckIfWallOnY())
                    pointToReach.position += new Vector3(0f, yInput, 0f);
        }
    }
    private Collider2D CheckIfWallOnX()
    {
        return Physics2D.OverlapCircle(pointToReach.position + new Vector3(xInput, 0f, 0f), .2f, whatisWall);
    }
    private Collider2D CheckIfWallOnY()
    {
        return Physics2D.OverlapCircle(pointToReach.position + new Vector3(0f, yInput, 0f), .2f, whatisWall);
    }
    private void AnimatorSetUp()
    {
        animator.SetFloat("Horizontal", transform.position.x);
        animator.SetFloat("Vertical", transform.position.y);
        animator.SetFloat("Speed", transform.position.sqrMagnitude);
    }
}
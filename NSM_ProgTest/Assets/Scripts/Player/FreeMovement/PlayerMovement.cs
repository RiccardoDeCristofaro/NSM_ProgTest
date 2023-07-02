using UnityEngine;
using S = System;
public class PlayerMovement : MonoBehaviour
{
    [Header("movement speed:")]
    [SerializeField] private float speed;
    [Header("movement State:")]
    [SerializeField] private Inputs inputsType;
    [Header("main camera:")]
    [SerializeField] private Camera mainCam;

    private Vector2 MoveDir;
    private Vector2 MouseDir;
    private float xInput;
    private float yInput;
    private Rigidbody2D rb;
    private Animator animator;
 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }   
    private void Update()
    {
        SetInputs();
        SetCameraRotation();
        AnimatorSetUp();
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + MouseDir * speed * Time.fixedDeltaTime);
        //CalculateLookDirection();
    }
    private void SetInputs()
    {     
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        MouseDir.y = yInput;
        MouseDir.x = xInput;

        if (Input.GetKeyDown(KeyCode.W))
            inputsType = Inputs.top;
        if (Input.GetKeyDown(KeyCode.D))
            inputsType = Inputs.right;
        if (Input.GetKeyDown(KeyCode.S))
            inputsType = Inputs.bottom;
        if (Input.GetKeyDown(KeyCode.A))
            inputsType = Inputs.left;
    }
    private void SetCameraRotation() => mainCam.ScreenToWorldPoint(Input.mousePosition);
    
    private void CalculateLookDirection()
    {
        float rectAngle = 90;
        Vector2 lookDir = MouseDir - rb.position;
        float angleRad = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - rectAngle;
        rb.rotation = angleRad;
    }
    private void AnimatorSetUp()
    {
        animator.SetFloat("Horizontal", MoveDir.x);
        animator.SetFloat("Vertical", MoveDir.y);
        animator.SetFloat("Speed", MoveDir.sqrMagnitude);
    }
    private enum Inputs { top, right, left, bottom }

}
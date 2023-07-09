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
    [Range(0.5f, 10f)]
    private float speed;
    [Header("movement State:")]
    [SerializeField]
    private Inputs inputsPlayer;

    [Header("hitObj Pos :")]
    [SerializeField]
    private Transform pointToReach;

    private Animator animator;
    [Header("Player Mask:")]
    [SerializeField]
    [Range(0, 5)]
    private float sightDistance;
    [SerializeField]
    private float checkInterval;

    [SerializeField]
    private LayerMask whatisWall;
    [SerializeField]
    private LayerMask whatisTunnel;

    [SerializeField]
    private Transform shootPos;
    [SerializeField]
    private Camera mainCam;
    [SerializeField]
    private float angle;
    [HideInInspector]
    public Vector2 move;
    private float xInput;
    private float yInput;

    private GridBasedMovement instance;

    protected GridBasedMovement Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        instance = this;
    }
    private void Start()
    {
        pointToReach.parent = null;
    }
    private void Update()
    {
        SetInputs();
        AnimatorSetUp();
    }
    private void SetInputs()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        move = new Vector2 (xInput, yInput);
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

        if (Input.GetKey(KeyCode.W))
        {
            shootPos.localPosition = new Vector3(0,.5f,0);
            inputsPlayer = Inputs.top;
        }                
                             
        if (Input.GetKey(KeyCode.S))
        {
            shootPos.localPosition = new Vector3(0, -.5f, 0);
            inputsPlayer = Inputs.bottom;
        }                 
                    
        if (Input.GetKey(KeyCode.A))
        {
            shootPos.localPosition = new Vector3(-.5f, 0f, 0);
            inputsPlayer = Inputs.left;
        }                      
        
        if (Input.GetKey(KeyCode.D))
        {
            shootPos.localPosition = new Vector3(0.5f, 0f, 0);
            inputsPlayer = Inputs.right;
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
        animator.SetFloat("Horizontal", xInput);
        animator.SetFloat("Vertical", yInput);
        animator.SetFloat("Speed", new Vector2(xInput, yInput).sqrMagnitude);
    }    
}
public enum Inputs { top, right, left, bottom }
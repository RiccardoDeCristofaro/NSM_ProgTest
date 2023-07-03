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

    [Header("player Pos :")]
    [SerializeField]
    private Transform pointToReach;
    private Animator animator;
    [Header("Fog Of War :")]
    [SerializeField]
    private FogOfWar fog_Class;
    [SerializeField]
    private Transform fogOfWar;
    [SerializeField]
    [Range(0, 5)]
    private float sightDistance;
    [SerializeField]
    private float checkInterval;

    [SerializeField]
    private LayerMask whatisWall;

    private float xInput;
    private float yInput;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(CheckFogOfWar(checkInterval));
        fogOfWar.localScale = new Vector2(sightDistance, sightDistance) * 10f;

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

        if (Input.GetKeyDown(KeyCode.W))
            inputsPlayer = Inputs.top;
        if (Input.GetKeyDown(KeyCode.D))
            inputsPlayer = Inputs.right;
        if (Input.GetKeyDown(KeyCode.S))
            inputsPlayer = Inputs.bottom;
        if (Input.GetKeyDown(KeyCode.A))
            inputsPlayer = Inputs.left;
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
    private IEnumerator CheckFogOfWar(float checkInterval)
    {
        while (true)
        {
            fog_Class.MakeHole(transform.position, sightDistance);
            yield return new WaitForSeconds(checkInterval);
        }
    }
}
public enum Inputs { top, right, left, bottom }
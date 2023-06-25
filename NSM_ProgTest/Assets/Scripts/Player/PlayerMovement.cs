using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("movement speed:")]
    [SerializeField] private float speed;
    [SerializeField] private direction playerDirection;
    private Vector2 MoveDir;

    private Rigidbody2D rb;
    private float inputX;
    private float inputZ;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.velocity = MoveDir * speed * Time.fixedDeltaTime;
    }
    private void Update()
    {
        Inputs();
        DirectionSet();
    }
    private void Inputs()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
        MoveDir = new Vector2(inputX * speed, inputZ * speed);
    }
    private void DirectionSet()
    {
        switch (playerDirection)
        {
            case direction.Forward:
                Vector3 playerScaleUp = transform.localScale;
                playerScaleUp.z *= 1f;
                break;
            case direction.Backward:
                Vector3 playerScaleDown = transform.localScale;
                playerScaleDown.z *= -1f;
                break;
            case direction.right:
                Vector3 playerScaleRight = transform.localScale;
                playerScaleRight.x *= 1f;
                break;
            case direction.left:
                Vector3 playerScaleLeft = transform.localScale;
                playerScaleLeft.x *= -1f;
                break;
        }
    }
    public enum direction
    {
        Forward = 0,
        Backward = 1,
        right = 2,
        left = 3,
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootSystem : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject Arrow;
    [SerializeField] Text arrowNumbers;
    [SerializeField] int arrows;
    [SerializeField] float arrowForce;
    private GridBasedMovement playerMove;


    private void Awake()
    {
        playerMove = GetComponent<GridBasedMovement>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    /// <summary>
    /// every single shot has the move component direction based on inputs
    /// </summary>
    private void Shoot()
    {       
        if (playerMove.move != new Vector2(0,0) && arrows >0)
        {
            GameObject arrow = Instantiate(Arrow, shootPoint.position, Quaternion.identity);
            arrows--;
            arrowNumbers.text = arrows.ToString();
            
            Arrow ArrowObj = arrow.GetComponent<Arrow>();
            ArrowObj.transform.Rotate(0,0,Mathf.Atan2(playerMove.move.y,playerMove.move.x) * Mathf.Rad2Deg);
            ArrowObj.velocity = playerMove.move * arrowForce;
            Destroy(ArrowObj, 4f);
        }      
    }
}

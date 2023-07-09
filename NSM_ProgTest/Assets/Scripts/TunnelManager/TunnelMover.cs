using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public class TunnelMover : MonoBehaviour
{
    [Tooltip("Tunnel Implementation")]
    [Header("Tunnel:")]
    [SerializeField] private WayPoints wayPoints;
    [SerializeField] private LayerMask tunneLayer;
    [SerializeField] private float tunnelSpeed;
    [SerializeField] private float distanceTreshHoldfromPointToPoint = 0.1f;
    [SerializeField] private int currentPos;
    [SerializeField] private bool ObjectInside;

    [SerializeField] GameObject playerPoint;
    private GameObject gameObjectInside;

    private void Start() => ObjectInside = false;
    private void Update()
    {
        if(ObjectInside)
        {
            MoveUnderTunnel();

            DistanceBeetweenPoints();

            if (currentPos == wayPoints.points.Count)
            {
                ObjectInside = false;
                gameObjectInside.GetComponent<GridBasedMovement>().enabled = true ? gameObjectInside.name == "Player" : gameObjectInside.GetComponent<Arrow>().enabled = true;
                playerPoint.transform.SetParent(gameObjectInside.transform, false);
            }
                
        }                  
    }
    private void MoveUnderTunnel()
    {
        gameObjectInside.transform.position = Vector2.MoveTowards(gameObjectInside.transform.position, wayPoints.points[currentPos].position, tunnelSpeed * Time.deltaTime);        
    }
    private void DistanceBeetweenPoints()
    {
        if (Vector2.Distance(gameObjectInside.transform.position, wayPoints.points[currentPos].position) < distanceTreshHoldfromPointToPoint && currentPos <= wayPoints.points.Count -1)
        {
            wayPoints.GetNextPoint(wayPoints.points[currentPos]);
            currentPos++;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ObjectInside = true;
            collision.gameObject.GetComponent<GridBasedMovement>().enabled = false;
            currentPos = 0;
            gameObjectInside = collision.gameObject;
            playerPoint.transform.SetParent(gameObjectInside.transform, true);
        }
        else if (collision.CompareTag("Arrow"))
        {
            ObjectInside = true;
            collision.gameObject.GetComponent<Arrow>().enabled = false;
            currentPos = 0;
            gameObjectInside = collision.gameObject;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelMover : MonoBehaviour
{
    [Tooltip("Tunnel Implementation")]
    [Header("Tunnel:")]
    [SerializeField] private WayPoints wayPoints;
    [SerializeField] private float tunnelSpeed;
    [SerializeField] private float distanceTreshHoldfromPointToPoint = 0.1f;
    public Transform currentWayPoint;

    private void Start()
    {
        
    }
    private void Update()
    {
          if(gameObject.GetComponent<CollisionManager>().TunnelTriggered)
               MoveUnderTunnel();                   
        
       
    }
    private void MoveUnderTunnel()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentWayPoint.position, tunnelSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, currentWayPoint.position) < distanceTreshHoldfromPointToPoint)
        {
            currentWayPoint = wayPoints.GetNextPoint(currentWayPoint);
        }
    }          
}

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    [SerializeField] public List<Transform> points = new List<Transform>();
    [SerializeField] Color GizmosColor;
    [Range(0f, 2f)]
    [SerializeField] float radius;
    public int currentIndex =0;
    private void Awake()
    {
        points.Clear();
    }
    private void Start()
    {
         foreach (Transform t in transform)
        {
            points.Add(t);
        }
    }
    private void OnDrawGizmos()
    {
        foreach (Transform t in points)
        {
            Gizmos.color = GizmosColor;
            Gizmos.DrawWireSphere(t.position, radius);         
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }        
    }
    public Transform GetNextPoint(Transform currentWaypoint)
    {
        if (currentWaypoint == null)
            return transform.GetChild(0);

        currentIndex = currentWaypoint.GetSiblingIndex();

        if (currentWaypoint.GetSiblingIndex() < transform.childCount -1)
        {
            return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }
        else
            return transform.GetChild(transform.childCount -1);
    }
}


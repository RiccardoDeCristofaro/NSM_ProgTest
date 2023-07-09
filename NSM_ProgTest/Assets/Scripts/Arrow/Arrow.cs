using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class Arrow : MonoBehaviour
{
    public Vector2 velocity;
    public LayerMask moveTiles;
    private void Update()
    {
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPos = currentPos + velocity * Time.deltaTime;

        RaycastHit2D[] hitsObjects = Physics2D.LinecastAll(currentPos, newPos, ~ moveTiles);

        hitsObjects.Where(hit => hit.collider != null)
                   .Select(hit => hit.collider.gameObject).ToList();

        foreach (RaycastHit2D hit in hitsObjects)
        {
            GameObject hitObj = hit.collider.gameObject; 
            Debug.Log(hitObj.name);
        }

        transform.position = newPos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class Arrow : MonoBehaviour
{
    public Vector2 velocity;
    private GameManager gameManager;
    GameObject hitObj;
    private void Update()
    {
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPos = currentPos + velocity * Time.deltaTime;

        RaycastHit2D[] hitsObjects = Physics2D.LinecastAll(currentPos, newPos);

        hitsObjects.Where(hit => hit.collider != null)
                   .Select(hit => hit.collider.gameObject).ToList();
        
        foreach(RaycastHit2D hit in hitsObjects)
        {
            hitObj = hit.collider.gameObject;
            if (hitObj.CompareTag("Player"))
            {
                Destroy(hitObj.gameObject);             
                break;
            }
            if (hitObj.CompareTag("Walls"))
            {
                Destroy(gameObject);
                break;
            }
        }

        transform.position = newPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Tilemaps;

public class CollisionManager : MonoBehaviour
{

    [SerializeField] GameObject EndGame;

    [SerializeField] Tilemap backGround;

    public GameManager  gameManager;

    System.Random rand = new System.Random();

    public string colliderName;
    public bool TunnelTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Monster":
                Debug.Log(collision.gameObject.name);               
                GetComponent<GridBasedMovement>().enabled = false;
                EndGame.SetActive(true);
                break;

            case "Puddle":
                Debug.Log(collision.gameObject.name);
                GetComponent<GridBasedMovement>().enabled = false;
                EndGame.SetActive(true);
                break;

            case "Teleport":
                Debug.Log(collision.gameObject.name);
                Vector3Int newPlayerPos = new Vector3Int(
                Mathf.Clamp(rand.Next(), -backGround.size.x / 4, backGround.size.x / 4),
                    Mathf.Clamp(rand.Next(), -backGround.size.y / 4, backGround.size.y / 4),
                    0);
                gameObject.GetComponent<GridBasedMovement>().enabled = false;
                transform.position = newPlayerPos;
                break;
            case "Tunnel":
                Debug.Log(collision.gameObject.name);                
                colliderName = collision.gameObject.name;
                TunnelTriggered = true;

                break;
            case "TunnelExit":
                Debug.Log(collision.gameObject.name);
                colliderName = collision.gameObject.name;
                TunnelTriggered = false;
                break;
        }       
    }
}

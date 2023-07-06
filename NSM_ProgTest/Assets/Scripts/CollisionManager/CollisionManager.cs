using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Tilemaps;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] GameObject EndGame;
    [SerializeField] Tilemap backGround;

    System.Random rand = new System.Random();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Monster":
                Debug.Log(collision.gameObject.name);
                EndGame.SetActive(true);
                break;

            case "Puddle":
                Debug.Log(collision.gameObject.name);
                EndGame.SetActive(true);
                break;

            case "Teleport":
                Debug.Log(collision.gameObject.name);
                Vector3Int newPlayerPos = new Vector3Int(
                Mathf.Clamp(rand.Next(), -backGround.size.x / 4, backGround.size.x / 4),
                    Mathf.Clamp(rand.Next(), -backGround.size.y / 4, backGround.size.y / 4),
                    0);
                GetComponent<GridBasedMovement>().enabled = false;
                gameObject.transform.position = newPlayerPos;
                GetComponent<GridBasedMovement>().enabled = true;
                break;
        }
    }
}

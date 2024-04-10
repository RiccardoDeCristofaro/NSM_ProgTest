using JetBrains.Annotations;
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

    [SerializeField] Tilemap twentyxtwenty;

    [SerializeField] GameObject pointToReach;
    [SerializeField] LayerMask monsterLayer;
    [SerializeField] LayerMask puddleLayer;
    [SerializeField] LayerMask teleportLayer;

    [SerializeField] GameObject monsterUI;
    [SerializeField] GameObject puddleUI;
    [SerializeField] GameObject TeleportUI;
    protected GameManager gameManager;

    internal int randomNumber1;
    internal int randomNumber2;

    internal int maxTeleportAreaX;
    internal int maxTeleportAreaY;

    public string colliderName;
    private float radius = 1f;

    private void Start()
    {
        maxTeleportAreaX = twentyxtwenty.size.x / 2;
        maxTeleportAreaY = twentyxtwenty.size.y / 2;
    }

    private void Update()
    {

        if (Physics2D.OverlapCircle(transform.position, radius, monsterLayer))
            monsterUI.SetActive(true);
        else
        { monsterUI.SetActive(false); }

        if (Physics2D.OverlapCircle(transform.position, radius, puddleLayer))
            puddleUI.SetActive(true);
        else
        { puddleUI.SetActive(false); }

        if (Physics2D.OverlapCircle(transform.position, radius, teleportLayer))
            TeleportUI.SetActive(true);
        else
        { TeleportUI.SetActive(false); }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Monster":
                Debug.Log(collision.gameObject.name);
                GetComponent<GridBasedMovement>().enabled = false;
                EndGame.SetActive(true);
                gameManager.interactablegameObjectsrandomPos = 0;
                break;

            case "Puddle":
                Debug.Log(collision.gameObject.name);
                GetComponent<GridBasedMovement>().enabled = false;
                EndGame.SetActive(true);
                break;

            case "Teleport":
                randomNumber1 = Random.Range(-maxTeleportAreaX, maxTeleportAreaX);
                randomNumber1 = Random.Range(-maxTeleportAreaY, maxTeleportAreaY);
                Debug.Log(collision.gameObject.name);
                Vector3Int newPlayerPos = new(randomNumber1, randomNumber2, 0);
                if (newPlayerPos != new Vector3Int(8, -2, 0))
                {
                    transform.position = newPlayerPos;
                    pointToReach.transform.position = newPlayerPos;

                    break;
                }
                break;
            case "Tunnel":
                Debug.Log(collision.gameObject.name);
                colliderName = collision.gameObject.name;
                break;

        }
    }
}

using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    protected GameManager instance;
  
    [Header("backgrounds tiles")]
    [SerializeField] public Tilemap BackgroundMap;
    [SerializeField] LayerMask backgroundTiles;
    [SerializeField] public Tile passedTile;

    [Header("blurred tiles")]
    [SerializeField] public Tilemap BlurredMap;
    [SerializeField] public Tile BlurredTile;
    [SerializeField] private Vector3 gridOffset;
    private MapSingleTile _tile;

    [Header("player info")]
    [SerializeField] GameObject player;
    [SerializeField] private Vector3Int spawnPoint;
    [SerializeField] Transform playerPos;
    [SerializeField] float overLapCircleRadius;

    [Header("arrow info")]
    [SerializeField] GameObject arrow;

    [Header("enemie info")]
    [SerializeField] List<GameObject> interactablegameObjects;
    public int interactablegameObjectsrandomPos;


    void Start()
    {
        BlurredMap.origin = BackgroundMap.origin;
        BlurredMap.size = BackgroundMap.size;

        foreach (Vector3Int bgTiles in BackgroundMap.cellBounds.allPositionsWithin)
        {
            BackgroundMap.SetTile(bgTiles, passedTile);
        }
        foreach (Vector3Int blurTilespos in BlurredMap.cellBounds.allPositionsWithin)
        {
            BlurredMap.SetTile(blurTilespos, BlurredTile);
        }
        BlurredMap.transform.position = new Vector3(-.5f, -.5f, 0f);

        for (int i = 0; i < interactablegameObjects.Count; i++)
        {       
            interactablegameObjectsrandomPos = Random.Range(-10, 10);
            Debug.Log(interactablegameObjectsrandomPos);
            interactablegameObjects[i].transform.position = new Vector3(interactablegameObjectsrandomPos,interactablegameObjectsrandomPos, 0);
        }
    }
    private void Update()
    {
        if (Physics2D.OverlapCircle(playerPos.position, overLapCircleRadius, backgroundTiles))
        {
            foreach (Vector3Int blurTilespos in BlurredMap.cellBounds.allPositionsWithin)
            {
                if (blurTilespos == playerPos.position)
                {
                    BlurredMap.SetTile(blurTilespos, null);
                }
                SingleTile();
            }
        }
    }
    private void SingleTile()
    {
        var worldPoint = new Vector3Int(Mathf.FloorToInt(playerPos.transform.position.x), Mathf.FloorToInt(playerPos.transform.position.y), 0);
        var tiles = GameTiles.instance.tiles;

        if (tiles.TryGetValue(worldPoint, out _tile))
        {
            print("Tile " + _tile.tileName);
            _tile.tileMap.SetTileFlags(_tile.localPlace, TileFlags.None);
            _tile.tileMap.SetColor(_tile.localPlace, passedTile.color);
        }
    }    
}

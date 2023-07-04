using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [Header("backgrounds tiles")]
    [SerializeField] public Tilemap BackgroundMap;
    [SerializeField] LayerMask backgroundTiles;
    [SerializeField] public Tile passedTile;
    [Header("blurred tiles")]
    [SerializeField] public Tilemap BlurredMap;
    [SerializeField] public Tile BlurredTile;
    [SerializeField] private Vector3 gridOffset;

    [SerializeField] Transform playerPos;
    [SerializeField] float overLapCircleRadius;
    [SerializeField] private MapSingleTile _tile;
    // Use this for initialization
    void Start()
    {
         BlurredMap.origin = BackgroundMap.origin;
         BlurredMap.size = BackgroundMap.size;

        foreach (Vector3Int blurTilespos in BlurredMap.cellBounds.allPositionsWithin)
        {
            BlurredMap.SetTile(blurTilespos, BlurredTile);
        }
        BlurredMap.transform.position = new Vector3(-.5f, -.5f, 0f);

    }
    private void Update()
    {
        if (Physics2D.OverlapCircle(playerPos.position, overLapCircleRadius, backgroundTiles))
        {
            foreach (Vector3Int blurTilespos in BlurredMap.cellBounds.allPositionsWithin)
            {
                if(blurTilespos == playerPos.position)
                {
                    BlurredMap.SetTile(blurTilespos, null);
                }              
            }
            SingleTile();
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(playerPos.position, overLapCircleRadius);
    }
}

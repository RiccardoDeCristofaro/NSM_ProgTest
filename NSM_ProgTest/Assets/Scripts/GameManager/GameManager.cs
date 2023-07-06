using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Runtime.InteropServices.WindowsRuntime;

public class GameManager : MonoBehaviour
{

    protected GameManager instance;

    [Tooltip("Tunnel Implementation")]    
    [SerializeField] private int tunnelNumbers;
    [Header("Tunnel:")]
    public Tunnel tunnel;
    //public Tunnel[] tunnels;

    [Serializable]
    public struct Tunnel
    {
        public GameObject[] tunnelPoints;
        [Range(.2f, 5f)]
        public float playerSpeedUnderTunnel;
        public int numberOfPoints;
        public Vector3 actualPos;
        public int nextPoint;
    }
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

    private void Awake()
    { 
        if(instance == null) instance = this;

        DontDestroyOnLoad(gameObject);
    }
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
            }
            SingleTile();
        }
        // PathScriptingTunnel(tunnelNumbers, player.gameObject);
        //else if (arrow.gameObject.CompareTag("Arrow"))
        //    SingleTunnel();
           //PathScriptingTunnel(tunnelNumbers, arrow.gameObject);
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
    //private void PathScriptingTunnel(int tunnels, GameObject obj)
    //{       
    //        this.tunnels[i].actualPos = obj.transform.position;
    //        obj.transform.position = Vector3.MoveTowards(
    //            this.tunnels[i].actualPos,
    //            this.tunnels[i].tunnelPoints[this.tunnels[i].currentPoint].transform.position,
    //            this.tunnels[i].playerSpeedUnderTunnel * Time.deltaTime
    //            );

    //        if (this.tunnels[i].actualPos == this.tunnels[i].tunnelPoints[this.tunnels[i].currentPoint].transform.position
    //            && this.tunnels[i].currentPoint != this.tunnels[i].numberOfPoints - 1)
    //            this.tunnels[i].currentPoint++;        
    //}
    public void SingleTunnel(GameObject player)
    {
        tunnel.actualPos = player.transform.position;
        player.transform.position = Vector3.MoveTowards(tunnel.actualPos, tunnel.tunnelPoints[tunnel.nextPoint].transform.position, tunnel.playerSpeedUnderTunnel * Time.deltaTime);

        if(tunnel.actualPos == tunnel.tunnelPoints[tunnel.nextPoint].transform.position && tunnel.nextPoint != tunnel.numberOfPoints -1)
        {
            tunnel.nextPoint++;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
///  abstact class of a single tile
/// </summary>
/// 
[System.Serializable]
public class MapSingleTile
{
    public Vector3Int localPlace;
    public Vector3 worldLocation;
    public TileBase tileBase;
    public Tilemap tileMap;
    public string tileName;
    public bool isExplored; 
}

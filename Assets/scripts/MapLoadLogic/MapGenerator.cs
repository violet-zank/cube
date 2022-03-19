using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform tilePrefab;
    public Transform boxPrefab;
    public Transform wallPrefab;
    public Transform playerPrefab;
    public Transform targetPrefab;

    public Vector2 mapSize;
    [Range(0,1)]
    public float outlinePercent;

    List<Coord> BoxCoords;//存储箱子的坐标

    MapData mapdata;
    void Start(){
        generateMap();
        
        MapDataReader reader = new MapDataReader();
        reader.readFile();
        mapdata = reader.getData();

        generateBox();
        generateWall();
        generatePlayer();
        generateTarget();
    }

    public void generateMap()
    {
        string holdername = "Generated Map";
        if(transform.Find(holdername)){
            DestroyImmediate(transform.Find(holdername).gameObject);
        }

        Transform mapHolder = new GameObject (holdername).transform;
        mapHolder.parent = transform;

        for( int x = 0; x < mapSize.x; x ++ ){
            for( int y = 0; y < mapSize.y; y ++){
                Vector3 tilePosition = CoordToPosition(x , y);
                Transform newTile = Instantiate(tilePrefab,tilePosition,Quaternion.Euler(Vector3.right*90)) as Transform;
                newTile.localScale = Vector3.one * (1 - outlinePercent);
                newTile.parent = mapHolder;
            }
        }
    }

    public void generateBox(){

        // BoxCoords = new List<Coord>();
        // BoxCoords.Add(new Coord(1,1));
        BoxCoords = mapdata.boxCoords;

        string holdername = "Generated Box";
        if(transform.Find(holdername)){
            DestroyImmediate(transform.Find(holdername).gameObject);
        }

        Transform mapHolder = new GameObject (holdername).transform;
        mapHolder.parent = transform;

        foreach (Coord item in BoxCoords)
        {            
            Vector3 boxPosition = CoordToPosition(item.x , item.y);
            Transform newBox = Instantiate(boxPrefab,boxPosition,Quaternion.identity) as Transform;
            newBox.parent = mapHolder;
        }
    }

    public void generateWall(){

        List<Coord> Coords = mapdata.wallCoords;

        string holdername = "Generated Wall";
        if(transform.Find(holdername)){
            DestroyImmediate(transform.Find(holdername).gameObject);
        }

        Transform mapHolder = new GameObject (holdername).transform;
        mapHolder.parent = transform;

        foreach (Coord item in Coords)
        {            
            Vector3 Position = CoordToPosition(item.x, item.y);
            Transform newBox = Instantiate(wallPrefab,Position,Quaternion.identity) as Transform;
            newBox.parent = mapHolder;
        }
    }

    public void generateTarget(){

        List<Coord> Coords = mapdata.targetCoords;

        string holdername = "Generated Target";
        if(transform.Find(holdername)){
            DestroyImmediate(transform.Find(holdername).gameObject);
        }

        Transform mapHolder = new GameObject (holdername).transform;
        mapHolder.parent = transform;

        foreach (Coord item in Coords)
        {            
            Vector3 Position = CoordToPosition(item.x, item.y);
            Transform newBox = Instantiate(targetPrefab,Position,Quaternion.identity) as Transform;
            newBox.parent = mapHolder;
        }
    }

    public void generatePlayer()
    {

        Coord Coords = mapdata.playerCoords;
        //Debug.Log(Coords.x+","+Coords.y);       
        Vector3 Position = CoordToPosition(Coords.x , Coords.y);
        Transform newBox = Instantiate(playerPrefab,Position,Quaternion.identity) as Transform;
    }

    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-mapSize.x/2+0.5f + x, 0, -mapSize.y/2+0.5f + y);
    }
}

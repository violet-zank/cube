using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData
{
    public List<Coord> boxCoords;
    public List<Coord> wallCoords;
    public List<Coord> targetCoords;
    public Coord playerCoords;

    public MapData()
    {
        boxCoords = new List<Coord>();
        wallCoords = new List<Coord>();
        targetCoords = new List<Coord>();
        playerCoords = new Coord(0,0);
    }

    public void AddBox(int x , int y){
        //Debug.Log(x+","+y);
        Coord temp = new Coord(x,y);
        boxCoords.Add(temp);
    }
    public void AddWall(int x , int y){
        //Debug.Log(x+","+y);
        Coord temp = new Coord(x,y);
        wallCoords.Add(temp);
    }
    public void AddTarget(int x , int y){
        //Debug.Log(x+","+y);
        Coord temp = new Coord(x,y);
        targetCoords.Add(temp);
    }
    public void AddPlayer(int x , int y){
        playerCoords = new Coord(x,y);
    }
}

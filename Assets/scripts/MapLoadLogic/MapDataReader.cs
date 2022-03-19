using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapDataReader{

    MapData mapdata = new MapData();
    public void readFile(){
        string [] textlines = File.ReadAllLines(Application.dataPath + "/level1.txt");
        for(int i = 0 ; i < textlines.Length ;i ++ ){
            for(int j = 0 ; j < textlines[i].Length; j ++){
                switch (textlines[i][j])
                {
                    case '#': mapdata.AddWall(i,j);break;
                    case '$': mapdata.AddBox(i, j);break;
                    case '.': mapdata.AddTarget(i, j);break;
                    case '@': mapdata.AddPlayer(i, j);break;
                    case '-':break;
                    default:
                    break;
                }
            }
        }
    }
    public MapData getData(){
        return mapdata;
    }
}

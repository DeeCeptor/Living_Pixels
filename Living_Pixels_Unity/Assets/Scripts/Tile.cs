using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Cells are placed on grid tiles
public class Tile : MonoBehaviour 
{
    public int curX, curY;
    public List<Tile> neighbour_tiles;
    public bool occupied = false;
    public Cell cell;
    public int tileType;


    public Tile(int x, int y, int type)
    {
        curX = x;
        curY = y;
        tileType = type;
    }


    public List<Cell> get_neighbours_of_type(Cell cell)
    {
        List<Cell> neighbours = new List<Cell>();



        return neighbours;
    }


    void Start()
    {
        this.renderer.material.color = Color.black;
    }


    void OnMouseDown()
    {
        if (!occupied && tileType == 1)
        {
            LevelController.CreateCellAt(curX, curY, false);
        }
    }
}

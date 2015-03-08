using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cell : MonoBehaviour 
{
    public int curX;
    public int curY;
    public Tile cur_tile;

    public Cell(int x, int y)
    {
        curX = x;
        curY = y;
    }


    // Method checks death condition of this cell
    public bool do_I_die()
    {
        if (count_occupied(cur_tile.neighbour_tiles) > 3)
            return true;

        return false;
    }
    public void Die()
    {
        cur_tile.occupied = false;
        Destroy(gameObject);
    }


    private int count_occupied(List<Tile> list)
    {
        int num = 0;

        foreach (Tile tile in list)
        {
            if (tile.occupied)
                num++;
        }

        return num;
    }


    public void reproduce()
    {
        foreach (Tile tile in cur_tile.neighbour_tiles)
        {
            if (!tile.occupied && tile.tileType == 1)
            {
                LevelController.CreateCellAt((int) tile.curX, (int) tile.curY, true);
            }
        }
    }


	void Start () 
    {
	    
	}
	

	void Update () 
    {
	    
	}


    void OnGUI()
    {
        GUI.Label(new Rect(curX * 30, curY * 30, 100, 100), "" + cur_tile.neighbour_tiles.Count);
    }
}

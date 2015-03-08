using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour 
{
    private int[,] level_layout = new int[10, 10]   // 1 are normal tiles that are placeable on
    { 
        {1, 1, 0, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 0, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 0, 1, 1, 1, 1, 1, 1, 1},
        {1, 0, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 0, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 0, 0, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 0, 1, 1, 1, 1, 1, 1, 1}
    };

    private static int xBounds = 10;
    private static int yBounds = 10;
    public static Tile[,] Level_Grid = new Tile[xBounds, yBounds];
    public static List<Cell> Living_Cells = new List<Cell>();
    public static List<Cell> Newborn_Cells = new List<Cell>();
    public static List<Cell> Dying_Cells = new List<Cell>();


	void Start () 
    {
        for (int x = 0; x < xBounds; x++)
        {
            for (int y = 0; y < yBounds; y++)
            {
                // Create a tile at each point
                GameObject go = (GameObject)GameObject.Instantiate(Resources.Load("TileObj"), new Vector3(x, y, 1), Quaternion.identity);
                Tile tile = go.GetComponent<Tile>();
                Level_Grid[x, y] = tile;
                tile.curX = x;
                tile.curY = y;
                tile.tileType = level_layout[x, y];
            }
        }

        // Populate neighbour lists for each tile
        for (int x = 0; x < xBounds; x++)
        {
            for (int y = 0; y < yBounds; y++)
            {
                if (level_layout[x, y] == 1)
                {
                    // Get a list of all neighbours of this tile
                    List<Tile> valid_neighbours = new List<Tile>();

                    // Top left
                    if (x - 1 >= 0 && y - 1 >= 0) {
                        valid_neighbours.Add(Level_Grid[x - 1, y -1]);
                    }
                    // Top
                    if (y - 1 >= 0) {
                        valid_neighbours.Add(Level_Grid[x, y - 1]);
                    }
                    // Top right
                    if (x + 1 < xBounds && y - 1 >= 0) {
                        valid_neighbours.Add(Level_Grid[x + 1, y - 1]);
                    }
                    // Left
                    if (x - 1 >= 0) {
                        valid_neighbours.Add(Level_Grid[x - 1, y]);
                    }
                    // Right
                    if (x + 1 < xBounds) {
                        valid_neighbours.Add(Level_Grid[x + 1, y]);
                    }
                    // Bottom left
                    if (x - 1 >= 0 && y + 1 < yBounds) {
                        valid_neighbours.Add(Level_Grid[x - 1, y + 1]);
                    }
                    // Bottom
                    if (y + 1 < yBounds) {
                        valid_neighbours.Add(Level_Grid[x, y + 1]);
                    }
                    // Bottom right
                    if (x + 1 < xBounds && y + 1 < yBounds)
                    {
                        valid_neighbours.Add(Level_Grid[x + 1, y + 1]);
                    }

                    // Remove unsuitable tile types
                    for (int z = 0; z < valid_neighbours.Count; z++)
                    {
                        if (valid_neighbours[z].tileType != 1)
                        {
                            valid_neighbours.Remove(valid_neighbours[z]);
                            z--;
                        }
                    }

                    Level_Grid[x, y].neighbour_tiles = valid_neighbours;
                }
            }
        }

        // Place on cell on the map
        //CreateCellAt(1, 1, false);
	}


    public void End_Turn()
    {
        Dying_Cells.Clear();
        Newborn_Cells.Clear();

        // Goes through every living cell to see if it dies
        for (int x = 0; x < Living_Cells.Count; x++)
        {
            bool dies = Living_Cells[x].do_I_die();

            if (dies)
            {
                Cell cell = Living_Cells[x];
                Living_Cells.Remove(cell);
                Dying_Cells.Add(cell);
                x--;
            }
        }

        // Checks for new births
        foreach (Cell cell in Living_Cells)
        {
            cell.reproduce();
        }

        for (int x = 0; x < Dying_Cells.Count; x++)
        {
            // Kill each dying cell
            Dying_Cells[x].Die();
        }

        // Add newborns to alive cells list
        Living_Cells.AddRange(Newborn_Cells);
    }


    // is_newborn indicates which list to put the newborn cell into
    public static Cell CreateCellAt(int x, int y, bool is_newborn)
    {
        GameObject go = (GameObject)GameObject.Instantiate(Resources.Load("CellObj"), new Vector3(x, y, 0), Quaternion.identity);
        Cell cell = go.GetComponent<Cell>();
        cell.curX = x;
        cell.curY = y;
        Level_Grid[x, y].occupied = true;
        Level_Grid[x, y].cell = cell;
        cell.cur_tile = Level_Grid[x, y];

        if (is_newborn)
            Newborn_Cells.Add(cell);
        else
            Living_Cells.Add(cell);

         return cell;
    }


    void Update()
    {
	
	}


    void OnGUI()
    {
        //GUI.Label(new Rect(leftAlignment, yStart, 100, 50), "Select a level");

        if (GUI.Button(new Rect(10, 10, 100, 40), "Next Turn"))
            End_Turn();
       
        /*
        if (GUI.Button(new Rect(leftAlignment, yStart + yOffset + buttonHeight * 1, buttonWidth, buttonHeight), "Lamenteur"))
            Application.LoadLevel("MitchellsLevel");

        // Display credits
        GUI.Label(new Rect((float)(leftAlignment + buttonWidth * 2.2f), yStart, 100, 50), "Controls");
        GUI.Box(new Rect((float)(leftAlignment + buttonWidth * 2.2f), yStart + yOffset, 500, 400), controlsString);
         * */
    }
}

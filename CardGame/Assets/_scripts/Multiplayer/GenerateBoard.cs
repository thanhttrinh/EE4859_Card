using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour {
    private Vector2 Tiles;
    public GameObject BlueTile;
    public GameObject RedTile;
    public Vector2 BoardOffset = new Vector3(0.5f, 0.5f);

    // Use this for initialization
    void Start () {
        GenerateGameBoard();

    }

    private void GenerateGameBoard()
    {
        //generate blue tiles
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 6; x++)
            {
                GenerateBlueTile(x, y);
            }
        }

        //generate red tiles
        for (int y=3; y < 6; y++)
        {
            for (int x = 0; x < 6; x++)
            {
                GenerateRedTile(x, y);
            }
        }
    }

    private void GenerateBlueTile(int x, int y)
    {
        Vector2 Tiles = new Vector2(x,y);
        Instantiate(BlueTile, Tiles+BoardOffset, Quaternion.identity);
    }

    private void GenerateRedTile(int x, int y)
    {
        Vector2 Tiles = new Vector2(x, y);
        Instantiate(RedTile, Tiles + BoardOffset, Quaternion.identity);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmaera : MonoBehaviour {
    public Units[,] units = new Units[6, 6];
    public Units[,] enemyunits = new Units[6, 6];
    private Units selectedUnit;
    private Vector2 mouseOver;
    private Vector2 enemymouseOver;
    public GameObject playerUnit;
    public GameObject player2Unit;
    public Vector2 BoardOffset = new Vector3(0.5f, 0.5f);
    private Vector2 startDrag;
    private Vector2 endDrag;

    private void Start()
    {
        GenerateBoard();
        
    }

    private void Update()
    {
        UpdateMouseOver();
        int x = (int)mouseOver.x;
        int y = (int)mouseOver.y;

        if (Input.GetMouseButtonDown(0))
        {
            SelectPiece(x, y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            tryMove((int)startDrag.x, (int)startDrag.y, x, y);
            selectedUnit = null;

        }

    }

    private void tryMove(int x1, int y1, int x2, int y2)
    {
        startDrag = new Vector2(x1, y1);
        endDrag = new Vector2(x2, y2);
        selectedUnit = units[x1, y1];

        MovePiece(selectedUnit,x2,y2);
        units[x2, y2] = units[x1, y1];
        units[x1, y1] = null;

    }

        private void GenerateBoard()
    {
        for (int y=0;y<3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                GeneratePiece(x,y);
            }
        }
    }

    private void GeneratePiece(int x, int y)
    {
        GameObject go = Instantiate(playerUnit) as GameObject;
        go.transform.SetParent(transform);
        Units p = go.GetComponent<Units>();
        units[x, y] = p;
        MovePiece(p, x, y);
    }

    private void MovePiece(Units p, int x, int y)
    {
        p.transform.position = (Vector2.right * x) + (Vector2.up * y) + BoardOffset;

    }

        public void UpdateMouseOver()
    {

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Board")))
        {
            mouseOver.x = (int)hit.point.x;
            mouseOver.y = (int)hit.point.y;
            Debug.Log(mouseOver);
            if (Input.GetMouseButtonDown(1))
            {
                Vector2 enemymouseOver = mouseOver;
                Instantiate(player2Unit, mouseOver+BoardOffset, Quaternion.identity);

            }
           

        }

        else
        {
            mouseOver.x = -1;
            mouseOver.y = -1;
        }
    }

    private void SelectPiece(int x, int y)
    {
        if (x<0 || x>=7 || y<0 || y >= 7)
        {
            return;
        }
        Units p = units[x, y];
        if(p != null)
        {
            selectedUnit = p;
            startDrag = mouseOver;
          
        }
    }

}

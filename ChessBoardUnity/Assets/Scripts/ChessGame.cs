using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessGame : MonoBehaviour
{

    public GameObject chessPiece;

    // Position for each chesspiece
    private GameObject[,] position = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    private string currentPlayer = "white";

    // Start is called before the first frame update
    void Start()
    {
        
        Instantiate(chessPiece, new Vector3(0, 0, -1), Quaternion.identity);
        
        playerBlack = new GameObject[]
        {
            Create("blackrook", 0, 7), Create("blackknight", 1, 7), Create("blackbishop", 2, 7), Create("blackqueen", 3, 7),
            Create("blackking", 4, 7), Create("blackbishop", 5, 7), Create("blackknight", 6, 7), Create("blackrook", 7, 7),
            Create("blackpawn", 0, 6), Create("blackpawn", 1, 6), Create("blackpawn", 2, 6), Create("blackpawn", 3, 6),
            Create("blackpawn", 4, 6), Create("blackpawn", 5, 6), Create("blackpawn", 6, 6), Create("blackpawn", 7, 6) 
        };

        playerWhite = new GameObject[]
        {
            Create("whiterook", 0, 0), Create("whiteknight", 1, 0), Create("whitebishop", 2, 0), Create("whitequeen", 3, 0),
            Create("whiteking", 4, 0), Create("whitebishop", 5, 0), Create("whiteknight", 6, 0), Create("whiterook", 7, 0),
            Create("whitepawn", 0, 1), Create("whitepawn", 1, 1), Create("whitepawn", 2, 1), Create("whitepawn", 3, 1),
            Create("whitepawn", 4, 1), Create("whitepawn", 5, 1), Create("whitepawn", 6, 1), Create("whitepawn", 7, 1) 
        };
        
        // Put pieces on board
        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }
        
    }


    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chessPiece, new Vector3(0, 0, -1), Quaternion.identity);
        ChessManuscript cm = obj.GetComponent<ChessManuscript>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();

        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        ChessManuscript cm = obj.GetComponent<ChessManuscript>();

        position[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        position[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return position[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 00 || x >= position.GetLength(0) || y >= position.GetLength(1))
        {
            return false;
        } else
        {
            return true;
        }
    }
}
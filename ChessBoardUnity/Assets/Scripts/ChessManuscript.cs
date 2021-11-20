using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessManuscript : MonoBehaviour
{
    // Refrence
    public GameObject controller;
    public GameObject movePlate;

    // Position
    private int xBoard = -1;
    private int yBoard = -1;

    // Store if player is black/ white
    private string player;

    // every chesspiece
    public Sprite blackpawn, blackrook, blackknight, blackbishop, blackqueen, blackking;
    public Sprite whitepawn, whiterook, whiteknight, whitebishop, whitequeen, whiteking;

    public ChessGame sc;

    public void Activate()
    {
        //Get the game controller
        controller = GameObject.FindGameObjectWithTag("GameController");

        // adjust unity coords to chess grid
        SetCoords();

        switch (this.name)
        {
            case "blackqueen": this.GetComponent<SpriteRenderer>().sprite = blackqueen; player = "black"; break;
            case "blackking": this.GetComponent<SpriteRenderer>().sprite = blackking; player = "black"; break;
            case "blackrook": this.GetComponent<SpriteRenderer>().sprite = blackrook; player = "black"; break;
            case "blackknight": this.GetComponent<SpriteRenderer>().sprite = blackknight; player = "black"; break;
            case "blackbishop": this.GetComponent<SpriteRenderer>().sprite = blackbishop; player = "black"; break;
            case "blackpawn": this.GetComponent<SpriteRenderer>().sprite = blackpawn; player = "black"; break;

            case "whitepawn": this.GetComponent<SpriteRenderer>().sprite = whitepawn; player = "white"; break;
            case "whiterook": this.GetComponent<SpriteRenderer>().sprite = whiterook; player = "white"; break;
            case "whiteknight": this.GetComponent<SpriteRenderer>().sprite = whiteknight; player = "white"; break;
            case "whitebishop": this.GetComponent<SpriteRenderer>().sprite = whitebishop; player = "white"; break;
            case "whitequeen": this.GetComponent<SpriteRenderer>().sprite = whitequeen; player = "white"; break;
            case "whiteking": this.GetComponent<SpriteRenderer>().sprite = whiteking; player = "white"; break;
        }
    }

    public void SetCoords()
    {
        //Get the board value in order to convert to xy coords
        float x = xBoard;
        float y = yBoard;

        // Chess piece placement offset
        x *= 0.6f;
        y *= 0.6f;

        x += -2.1f;
        y += -2.1f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    private void OnMouseUp()
    {
        DestroyMovePlates();

        InitiateMovePlates();
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "blackqueen":
            case "whitequeen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "blackknight":
            case "whiteknight":
                LMovePlate();
                break;
            case "blackbishop":
            case "whitebishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "blackking":
            case "whiteking":
                SurroundMovePlate();
                break;
            case "blackrook":
            case "whiterook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "blackpawn":
                PawnMovePlate(xBoard, yBoard - 1);
                break;
            case "whitepawn":
                PawnMovePlate(xBoard, yBoard + 1);
                break;
        }
    }

    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        ChessGame sc = controller.GetComponent<ChessGame>();

        int x = xBoard + xIncrement;
        int y = xBoard + yIncrement;

        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<ChessManuscript>().player != player)
        {
            MovePlateAttackSpawn(x, y);
        }
    }

    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard + 1);
        PointMovePlate(xBoard - 1, yBoard);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 1);
    }

    public void PointMovePlate(int x, int y)
    {
        ChessGame sc = controller.GetComponent<ChessGame>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);
            if (cp == null)
            {
                MovePlateSpawn(x, y);
            }
            else if (cp.GetComponent<ChessManuscript>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    public void PawnMovePlate(int x, int y)
    {

        ChessGame sc = controller.GetComponent<ChessGame>();
        if (sc.PositionOnBoard(x, y))
        {
            if (sc.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            }

            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && sc.GetPosition(x + 1, y).GetComponent<ChessManuscript>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y);
            }

            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).GetComponent<ChessManuscript>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }

    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        // Chess piece placement offset
        x *= 0.6f;
        y *= 0.6f;

        x += -2.1f;
        y += -2.1f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -2.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        // Chess piece placement offset
        x *= 0.6f;
        y *= 0.6f;

        x += -2.1f;
        y += -2.1f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -2.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

}
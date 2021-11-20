using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    // Position on grid
    int matrixX;
    int matrixY;

    // false: movement, true: attacking
    public bool attack = false;

    public void start()
    {
        if (attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        if (attack)
        {
            GameObject cp = controller.GetComponent<ChessGame>().GetPosition(matrixX, matrixY);

            Destroy(cp);
        }

        controller.GetComponent<ChessGame>().SetPositionEmpty(reference.GetComponent<ChessManuscript>().GetXBoard(), reference.GetComponent<ChessManuscript>().GetYBoard());

        reference.GetComponent<ChessManuscript>().SetXBoard(matrixX);
        reference.GetComponent<ChessManuscript>().SetYBoard(matrixY);
        reference.GetComponent<ChessManuscript>().SetCoords();

        controller.GetComponent<ChessGame>().SetPosition(reference);

        controller.GetComponent<ChessManuscript>().DestroyMovePlates();
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }


    public GameObject GetReference()
    {
        return reference;
    }


}

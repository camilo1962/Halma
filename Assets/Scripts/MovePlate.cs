using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    GameObject reference = null;

    int matrixX;
    int matrixY;

    public bool attack = false;

   public void OnMouseUp()
    {
        GameObject cp = Game.instance.GetPosition(matrixX, matrixY);

        Destroy(cp);

        Game.instance.SetPositionEmpty(reference.GetComponent<Checker>().GetXBoard(),
        reference.GetComponent<Checker>().GetYBoard());

        reference.GetComponent<Checker>().SetXBoard(matrixX);
        reference.GetComponent<Checker>().SetYBoard(matrixY);
        reference.GetComponent<Checker>().SetCoords();

        Game.instance.SetPosition(reference);

        Game.instance.NextTurn();

        reference.GetComponent<Checker>().DestroyMovePlates();
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference (GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}

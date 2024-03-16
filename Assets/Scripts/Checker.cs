using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checker : MonoBehaviour
{
    public GameObject controller;
    public GameObject movePlate;

    private int _xBoard = -1;
    private int _yboard = -1;

    private string _player;

    public Sprite black_pawn;
    public Sprite white_pawn;

    public bool checkWhite;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        SetCoords();

        switch (this.name)
        {
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; _player = "black"; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; _player = "white"; break;
        }
    }

    public void SetCoords()
    {
        float x = _xBoard;
        float y = _yboard;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public int GetXBoard()
    {
        return _xBoard;
    }

    public int GetYBoard()
    {
        return _yboard;
    }

    public void SetXBoard(int x)
    {
        _xBoard = x;
    }

    public void SetYBoard(int y)
    {
        _yboard = y;
    }

    private void OnMouseUp()
    {
        if (!Game.instance.IsGameOver() && Game.instance.GetCurrentPlayer() == _player)
        {
            DestroyMovePlates();
            InitiateMovePlates();
        } 
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
            case "black_pawn":
                if (StartGame.oneMode == true)
                {
                    SurroundMovePlateDiag();
                    SurroundMovePlate();
                }
                if (StartGame.twoMode == true)
                {
                    SurroundMovePlate();
                    SurroundMovePlateDiag();
                    SurroundMovePlateVertic();
                }
               
                if(StartGame.threeMode == true)
                {
                    SurroundMovePlate();
                }
                break;
            case "white_pawn":
                if (StartGame.oneMode == true)
                {
                    SurroundMovePlateDiag();
                    SurroundMovePlate();
                }
                if (StartGame.twoMode == true)
                {
                    SurroundMovePlate();
                    SurroundMovePlateDiag();
                    SurroundMovePlateVertic();
                }
             
                if (StartGame.threeMode == true)
                {
                    SurroundMovePlate();
                }
                break;
        }
    }

    public void SurroundMovePlate()
    {
            // движение по вертикали 
        PointMovePlate(_xBoard, _yboard + 1);
        PointMovePlate(_xBoard, _yboard - 1);
        PointMovePlate(_xBoard - 1, _yboard - 0);
        PointMovePlate(_xBoard + 1, _yboard - 0);

            // движение по горизонтали
        PointMovePlate(_xBoard - 1, _yboard + 1);
        PointMovePlate(_xBoard - 1, _yboard - 1);
        PointMovePlate(_xBoard + 1, _yboard + 1);
        PointMovePlate(_xBoard + 1, _yboard - 1);
    }

    public void SurroundMovePlateDiag()
    {
        // движение по диагонали через одну клетку
        if (PointMovePl(_xBoard - 1, _yboard + 1) == true)
        {
            PointMovePlate(_xBoard - 2, _yboard + 2);
        }
        if( PointMovePl(_xBoard - 1, _yboard - 1) == true)
        {
            PointMovePlate(_xBoard - 2, _yboard - 2);
        }
        if(   PointMovePl(_xBoard + 1, _yboard + 1) == true)
        {
            PointMovePlate(_xBoard + 2, _yboard + 2);
        }
        if(  PointMovePl(_xBoard + 1, _yboard - 1) == true)
        {
            PointMovePlate(_xBoard + 2, _yboard - 2);
        }
    }

    public void SurroundMovePlateVertic()
    {
        // движение по вертикали через одну клетку 
        if(PointMovePl(_xBoard, _yboard + 1) == true)
        {
            PointMovePlate(_xBoard, _yboard + 2);
        }
        if(PointMovePl(_xBoard, _yboard - 1) == true)
        {
            PointMovePlate(_xBoard, _yboard - 2);
        }
        if(PointMovePl(_xBoard - 1, _yboard - 0) == true)
        {
            PointMovePlate(_xBoard - 2, _yboard - 0);
        }
        if (PointMovePl(_xBoard + 1, _yboard - 0) == true)
        {
            PointMovePlate(_xBoard + 2, _yboard - 0);
        }
    }

    public void PointMovePlate(int x, int y)
    {
        if (Game.instance.PositionOnBoard(x, y))
        {
            if (Game.instance.GetPosition(x,y) == null)
            {
                MovePlateSpawn(x , y );
            }
        }
    }

    public bool PointMovePl(int x, int y)
    {
        if (Game.instance.PositionOnBoard(x, y))                                                                               
        {
            if (Game.instance.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
                return false;
            }
        }
        return true;
    }

    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void Jugar(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }


    public void Salir()
    {
        Application.Quit();
    }


}

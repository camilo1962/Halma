using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Game : MonoBehaviour
{
    public GameObject chessPiece;

    private GameObject[,] _positions = new GameObject[9, 9];
    private GameObject[] _playerBlack = new GameObject[9];
    private GameObject[] _playerWhite = new GameObject[9];

    private  List<Vector3> winWhitePosition = new List<Vector3>()
    {
        new Vector3  (-1.64f, 1.0f, -1.0f),new Vector3 (-0.9799999f, 1.0f, -1.0f),new Vector3(-2.3f, 1.0f, -1.0f),
        new Vector3(-2.3f, 1.66f, -1.0f), new Vector3(-0.9799999f, 1.66f, -1.0f), new Vector3(-1.64f, 2.32f, -1.0f),
        new Vector3(-2.3f, 2.32f, -1.0f), new Vector3(-1.64f, 1.66f, -1.0f), new Vector3(-0.9799999f, 2.32f, -1.0f)
    };

    private List<Vector3> winBlackPosition = new List<Vector3>()
    {
        new Vector3  (1.0f, -2.3f, -1.0f),new Vector3 (1.66f, -2.3f, -1.0f),new Vector3(2.32f, -2.3f, -1.0f),
        new Vector3(1.0f, -1.64f, -1.0f), new Vector3(1.66f, -1.64f, -1.0f), new Vector3(2.32f, -1.64f, -1.0f),
        new Vector3(1.0f, -0.9799999f, -1.0f), new Vector3(1.66f, -0.9799999f, -1.0f), new Vector3(2.32f, -0.9799999f, -1.0f)
    };

    private string _currentPlayer = "white";

    private bool _gameOver = false;

    public static Game instance = null;

    public event Action Win;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance == this)
        {
            Destroy(gameObject);
        }
            _playerWhite = new GameObject[]
            {
            Create("white_pawn", 5, 0), Create("white_pawn",  6, 0), Create("white_pawn",7, 0),
            Create("white_pawn", 5, 1), Create("white_pawn", 6, 1), Create("white_pawn", 7, 1),
            Create("white_pawn", 5, 2), Create("white_pawn", 6, 2), Create("white_pawn", 7, 2)
            };
            _playerBlack = new GameObject[]
            {
            Create("black_pawn", 0, 7), Create("black_pawn", 1, 7), Create("black_pawn", 2, 7),
            Create("black_pawn", 0, 6), Create("black_pawn", 1, 6), Create("black_pawn", 2, 6),
            Create("black_pawn", 0, 5), Create("black_pawn", 1, 5), Create("black_pawn", 2, 5)

            };
        
        for (int i = 0; i < _playerBlack.Length; i++)
        {
            SetPosition(_playerBlack[i]);
            SetPosition(_playerWhite[i]);
        }
    }

    private void Update()
    {
       WinBlackPlayer();
       WinWhitePlayer();
    }

    private void WinBlackPlayer()
    {
        int winBlackPositionsCount = 0;
        foreach (var pawn in _playerBlack)
        {
            foreach (var pos in winBlackPosition)
            {
                if (pawn.transform.position == pos)
                {
                    winBlackPositionsCount++;
                    break;
                }
            }
        }
        if (winBlackPositionsCount >= 9)
            Winner("Negras");
    }

    private void WinWhitePlayer()
    {
        int winWhitePositionsCount = 0;
        foreach (var pawn in _playerWhite)
        {
            foreach (var pos in winWhitePosition)
            {
                if (pawn.transform.position == pos)
                {
                    winWhitePositionsCount++;
                    break;
                }
            }
        }
        if (winWhitePositionsCount >= 9)
            Winner("Blancas");
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chessPiece, new Vector3(0, 0, -1), Quaternion.identity);
        Checker cm = obj.GetComponent<Checker>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        Checker cm = obj.GetComponent<Checker>();
        _positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        _positions[x, y] = null;
    }
    public bool SetPositionEmptyCheck(int x, int y)
    {
        _positions[x, y] = null;
        return true;
    }
    public GameObject GetPosition(int x, int y)
    {
        return _positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= _positions.GetLength(0) || y >= _positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return _currentPlayer;
    }

    public bool IsGameOver()
    {
        return _gameOver;
    }

    public void NextTurn()
    {
        if(_currentPlayer == "white")
        {
            _currentPlayer = "black";
        }
        else
        {
            _currentPlayer = "white";
        }
    }

    public void Winner(string playerWinner)
    {
        _gameOver = true;

        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " ganan";

        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("MenuText").GetComponent<Text>().enabled = true;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Start");
    }

    public void Restart()
    {
        _gameOver = false;

        SceneManager.LoadScene("Game");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject _button1;
    [SerializeField] private GameObject _button2;
    [SerializeField] private GameObject _button3;
    [SerializeField] private GameObject _modeSelectionButton;

    public static bool oneMode;
    public static bool twoMode;
    public static bool threeMode;

    public void ModeSelectionButton()
    {
        _button1.SetActive(true);
        _button2.SetActive(true);
        _button3.SetActive(true);

        _modeSelectionButton.SetActive(false);
    }
    public void OneMode()
    {
        oneMode = true;

        SceneManager.LoadScene("Game");
    }

    public void TwoMode()
    {
        twoMode = true; 

        SceneManager.LoadScene("Game");
    }

    public void ThreeMode()
    {
        threeMode = true; 

        SceneManager.LoadScene("Game");
    }

    public void Salir()
    {
        Application.Quit();
    }
}

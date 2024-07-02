using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ATMMenu : MonoBehaviour
{
    [SerializeField] GameObject AtmPanel;
    [SerializeField] GameObject Player;
    private HandleInput handleInput;
    public void OpenMap()
    {
        SceneManager.LoadScene("TopViewGame");
    }
    public void SaveCoin()
    {

    }
    public void ExitMenu()
    {
        handleInput = Player.GetComponent<HandleInput>();
        handleInput.EnableMove = true;
        AtmPanel.SetActive(false);
    }
}
using UnityEngine;
using System.Collections;

public class GameMenuUI : MonoBehaviour {
    public void PlayGame()
    {
        Application.LoadLevel("Level1");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Howtoplay()
    {
        Application.LoadLevel("Howtoplay");
    }
    public void Backtomenu()
    {
        Application.LoadLevel("Menu");
    }
}

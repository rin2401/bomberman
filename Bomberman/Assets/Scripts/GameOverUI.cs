using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverUI : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }
    public void Retry()
    {
        if (Application.loadedLevelName == "Level1")
        {
            Application.LoadLevel("Level1");
        }
        if (Application.loadedLevelName == "Level2")
        {
            Application.LoadLevel("Level2");
        }
        if (Application.loadedLevelName == "Level3")
        {
            Application.LoadLevel("Level3");
        }
        if (Application.loadedLevelName == "Boss")
        {
            Application.LoadLevel("Boss");
        }
    }
}

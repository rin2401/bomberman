using UnityEngine;
using System.Collections;

public class NextLevelUI : MonoBehaviour {

	public void NextLevel()
    {
        if (Application.loadedLevelName == "Level1")
        {
            Application.LoadLevel("Level2");
        }
        if (Application.loadedLevelName == "Level2")
        {
            Application.LoadLevel("Level3");
        }
        if (Application.loadedLevelName == "Level3")
        {
            Application.LoadLevel("Boss");
        }
        if (Application.loadedLevelName == "Boss")
        {
            Application.Quit();
        }
    }
    public void Restart()
    {
        Application.LoadLevel("Level1");
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class NextUI2 : MonoBehaviour {

	public void OK()
    {
        Application.Quit();
    }
    public void Restart()
    {
        Application.LoadLevel("Level1");
    }
}

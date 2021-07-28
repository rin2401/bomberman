using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
    public GameObject pauseButton, pausePanel;
	// Use this for initialization
    void Start()
    {
        ResumeButton();
    }
	public void PauseButton()
    {
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }
    public void ResumeButton()
    {
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }
}

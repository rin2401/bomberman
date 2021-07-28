using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public GameObject levelHolder;
    public int gameScore = 0;
    public Text txtScore;
    public int FirePoint = 1;
    public Text txtFire;
    public int BombPoint = 1;
    public Text txtBomb;
    public int SpeedPoint = 1;
    public int LifePoint = 3;
    public Text txtSpeed;
    public Text txtLife;
    public AudioClip gameoverClip;
    public AudioClip winSound;
    public AudioClip respawnSound;
    public AudioClip deadSound;
    private AudioSource audioSource;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject nextlevelUI;
    public const int X = 23;
    public const int Y = 13;
    public GameObject[,] level = new GameObject[X, Y];
    public static GameController gameController;
    // Use this for initialization
    void Start () {
        if (gameController == null)
        {
            gameController = GameObject.FindGameObjectWithTag("GC").GetComponent<GameController>();
        }
        gameOverUI.SetActive(false);
        nextlevelUI.SetActive(false);
        var objects = levelHolder.GetComponentsInChildren<Transform>();
        foreach (var child in objects)
        {
            level[(int)child.position.x, (int)child.position.y] = child.gameObject;
        }
        level[0, 0] = null;
        audioSource = GetComponent<AudioSource>();   
	}	
    public void GetPoint()
    {
        gameScore = gameScore + 100;
        txtScore.text = "Score:" + gameScore.ToString();
        if (gameScore == 500)
        {
            audioSource.clip = winSound;
            audioSource.Play();
            nextlevelUI.SetActive(true);
        }
    }
    public void Victory()
    {
        audioSource.clip = winSound;
        audioSource.Play();
        nextlevelUI.SetActive(true);
    }
    public void GetFire()
    {
        FirePoint++;
        txtFire.text = ": " + FirePoint.ToString();
    }
    public void GetBomb()
    {
        BombPoint++;
        txtBomb.text = ": " + BombPoint.ToString();
    }
    public void GetSpeed()
    {
        SpeedPoint++;
        txtSpeed.text = ": " + SpeedPoint.ToString();
    }
    public void GetLife()
    {
        LifePoint--;
        txtLife.text = ": " + LifePoint.ToString();
    }
    public GameObject playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;
    public Transform spawnPrefab;
    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        Instantiate(spawnPrefab, spawnPoint.position, Quaternion.identity);
        audioSource.clip = respawnSound;
        audioSource.Play();
        txtFire.text = ": " + FirePoint;
        txtBomb.text = ": " + BombPoint;
        txtSpeed.text = ": " + SpeedPoint;

    }
    public void isDead()
    {
        audioSource.clip = deadSound;
        audioSource.Play();
    }
    public void EndGame()
    {
        audioSource.clip = gameoverClip;
        audioSource.Play();
        gameOverUI.SetActive(true);
    }
}

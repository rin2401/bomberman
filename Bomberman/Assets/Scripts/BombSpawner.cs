using UnityEngine;
using System.Collections;

public class BombSpawner : MonoBehaviour {
    public GameObject bomb;
    public int numberOfBomb = 1;
    public int firePower = 1;
    public float fuse = 2;
    public AudioClip DatBom;
    private AudioSource audioSource;
    GameController gameController;
	// Update is called once per frame
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        numberOfBomb = gameController.BombPoint;
        firePower = gameController.FirePoint;
        //bomb = gameObject;
        audioSource = GetComponent<AudioSource>();
    }
	void Update () {      
        if (Input.GetButtonDown("Jump") && numberOfBomb >= 1)
        {
            audioSource.clip = DatBom;
            audioSource.Play();
            Vector2 spawnPos = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
            var newBomb = Instantiate(bomb, spawnPos, Quaternion.identity) as GameObject;
            newBomb.GetComponent<Bomb>().firePower = firePower;
            newBomb.GetComponent<Bomb>().fuse = fuse;
            numberOfBomb--;
            Invoke("AddBomb", fuse);
        }
    }

    public void AddBomb()
    {
        numberOfBomb++;
    }
}

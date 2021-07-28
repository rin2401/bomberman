using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {
    public int bombs;
    public int firePower;
    public float speed;
    GameController gameController;
    public AudioClip PowerUpSound;
    private AudioSource audioSource;
    public void Start()
    {       
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gameController.level[(int)transform.position.x, (int)transform.position.y] = gameObject;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        // see if we have collider with the player
        if (collision.gameObject.tag == "Player")
        {
            
            // Gather references to components
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            audioSource = collision.gameObject.GetComponent<AudioSource>();
            audioSource.clip = PowerUpSound;
            audioSource.Play();
            BombSpawner BombSpawner = collision.gameObject.GetComponent<BombSpawner>();
            // adjust the values
            playerController.speed += speed;
            BombSpawner.numberOfBomb += bombs;
            BombSpawner.firePower += firePower;
            if (firePower == 1)
            {
                gameController.GetFire();
            }
            if (bombs == 1)
            {
                gameController.GetBomb();
            }
            if (speed == 0.5)
            {
                gameController.GetSpeed();
            }
            //remove the powerup
            Destroy(gameObject);
        }
    }
}

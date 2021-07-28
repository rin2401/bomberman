using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Fire : MonoBehaviour {
    public AudioClip BomNo;
    private AudioSource audioSource;
    GameController gameController;
    // BossHealth bossHealth;
    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = BomNo;
        audioSource.Play();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        //remove fire when it's done
        Destroy(gameObject, 0.4f);
	}
	void Update()
    {
        transform.Rotate(0, 0, -45);
    }
	public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.GetComponent<PowerUpSpawner>() != null)
        {
            // make sure bomb don't destroy your powerup
            GetComponent<CircleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<PowerUpSpawner>().SpawnPowerUp();
        }
        // don't destroy other fires
        else if (collision.gameObject.GetComponent<Fire>() != null)
        {
            return;
        }
        // if we have found a boob, trigger it  
        else if (collision.gameObject.GetComponent<Bomb>() != null)
        {
            collision.gameObject.GetComponent<Bomb>().Explode();
        }
        else if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            gameController.GetPoint();
        }
        else if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().speed = 0;
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            gameController.GetLife();
            gameController.isDead();
            Animator animator = collision.gameObject.GetComponent<Animator>();
            animator.SetBool("Die", true);
            Destroy(collision.gameObject,1f);
                  
            if (gameController.LifePoint>0)
            {
                gameController.StartCoroutine(gameController.RespawnPlayer());
            }
            else
            {
                gameController.EndGame();
            }

        }
        else if (collision.gameObject.tag=="Boss")
        {
            BossHealth bossHealth = collision.gameObject.GetComponentInChildren<BossHealth>();

            if (bossHealth.curHP > 0)
            {
                print("-5");
                bossHealth.DecreaHeahlth();
            }
            else
            {
                print("Die");
                Destroy(collision.gameObject);
                gameController.Victory();
            }
            return;

        }
        if (collision.gameObject.tag != "Player" || collision.gameObject.tag != "Boss")
        {
            Destroy(collision.gameObject);
        }
    }
}

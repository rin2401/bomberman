using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    public Transform[] waypoints;
    int cur = 0;
    public Vector2 dir;
    public AudioClip firesound;
    private AudioSource audioSource;
    public float speed = 0.3f;
    [SerializeField]
    private GameObject fire;
    GameController gameController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        StartCoroutine(BossShoot());
        
        //   StartCoroutine(WallShoot());

    }
    void FixedUpdate()
    {
        // Waypoint not reached yet? then move closer
        if (transform.position != waypoints[cur].position)
        {
            Vector2 p = Vector2.MoveTowards(transform.position,
                                            waypoints[cur].position,
                                            speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
        // Waypoint reached, select next one
        else cur = (cur + 1) % waypoints.Length;
        // Animation
        dir = waypoints[cur].position - transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);      
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }
    IEnumerator BossShoot()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        Vector3 temp = transform.position;
        if (dir.x < 0)
        {
            temp.y += 0.5f;
            temp.x -= 2f;
        }
        if (dir.x > 0)
        {
            temp.y += 0.5f;
            temp.x += 2f;
        }
        if (dir.y < 0)
        {
            temp.y -= 0.5f;
            GetComponent<SpriteRenderer>().sortingOrder = 3;
        }
        if (dir.y > 0)
        {
            temp.y -= 0.5f;
            GetComponent<SpriteRenderer>().sortingOrder = 10;
        }
        Instantiate(fire, temp, Quaternion.identity);
        
        StartCoroutine(BossShoot());
        audioSource.clip = firesound;
        audioSource.Play();

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().speed = 0;
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            gameController.GetLife();
            gameController.isDead();
            Animator animator = collision.gameObject.GetComponent<Animator>();
            animator.SetBool("Die", true);
            Destroy(collision.gameObject, 1f);

            if (gameController.LifePoint > 0)
            {
                gameController.StartCoroutine(gameController.RespawnPlayer());
            }
            else
            {
                gameController.EndGame();
            }
            return;
        }
        if (collision.gameObject.GetComponent<Fire>() != null){

            Debug.Log("Boss dinh fire");
        }

    }
}
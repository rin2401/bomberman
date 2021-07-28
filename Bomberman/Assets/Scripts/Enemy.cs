using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private Animator playeranimator;
    public Transform[] waypoints;
    int cur = 0;
    public float speed = 0.05f;
    GameController gameController;
    PlayerController pc;
    public bool direc = false;
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
    void Update()
    {
        // Waypoint not reached yet? then move closer
        Move();
    }
    void Move() { 
        if (transform.position != waypoints[cur].position)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[cur].position, speed);
        }
        else if (direc==false)
            cur = (cur + 1) % waypoints.Length;
        else
            cur = (cur + waypoints.Length - 1) % waypoints.Length;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Destroyable" || collision.gameObject.tag == "Bomb" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Boss")
        {
            direc = !direc;
            if(direc)
                cur = (cur + waypoints.Length - 1) % waypoints.Length;
            else
                cur = (cur + 1) % waypoints.Length;
            Debug.Log("Dung roi");
        }
        if (collision.gameObject.tag == "Player")
        {
            //
            collision.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            //
            gameController.GetLife();
            gameController.isDead();
            collision.gameObject.GetComponent<PlayerController>().speed = 0;
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
        }
    }
    //public void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
            
    //        GetComponent<CircleCollider2D>().isTrigger = false;
    //    } 
    //}

}
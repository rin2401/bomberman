using UnityEngine;
using System.Collections;

public class BossFire : MonoBehaviour
{
    GameController gameController;
    public float speed;
    public Sprite up;
    public Sprite down;
    public Sprite right;
    public Sprite left;
    private Rigidbody2D myBody;
    SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider2D;
    public Vector2 dir;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        dir = FindObjectOfType<Boss>().dir;
        myBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        if (dir.x < 0)
        {
            spriteRenderer.sprite = left;
            circleCollider2D.offset = new Vector2(-0.7f, 0f);
            myBody.velocity = new Vector2(-speed, 0);
        }
        if (dir.x > 0)
        {
            spriteRenderer.sprite = right;
            circleCollider2D.offset = new Vector2(0.7f, 0f);
            myBody.velocity = new Vector2(speed, 0);
        }
        if (dir.y < 0)
        {
            spriteRenderer.sprite = down;
            circleCollider2D.offset = new Vector2(0f, -0.7f);
            myBody.velocity = new Vector2(0, -speed);
        }
        if (dir.y > 0)
        {
            spriteRenderer.sprite = up;
            circleCollider2D.offset = new Vector2(0f, 0.7f);
            myBody.velocity = new Vector2(0, speed);
        }
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
        }
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Bomb")
        {
            collision.gameObject.GetComponent<Bomb>().Explode();
            Destroy(gameObject);
        }

    }

}
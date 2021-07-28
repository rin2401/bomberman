using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour {
    public float speed;
    public int life;
    public int startlife = 3;
    private Animator animator;
    private Rigidbody2D rb2d;
    GameController gameController;
    // Use this for initialization
    void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        animator = GetComponent<Animator>();
        speed = gameController.SpeedPoint + 1;
        life = gameController.LifePoint;
        rb2d = GetComponent<Rigidbody2D>();
	}
    // Update is called once per frame
    void Update () {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(x, y) * speed;
        rb2d.velocity = movement;
        if (y > 0)
        {
            animator.SetInteger("Direction", 2);
        }
        else if (y < 0)
        {
            animator.SetInteger("Direction", 0);

        }
        else if (x > 0)
        {
            animator.SetInteger("Direction", 3);
        }
        else if (x < 0)
        {
            animator.SetInteger("Direction", 1);
        }
    }

}

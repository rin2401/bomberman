using UnityEngine;
using System.Collections;
using System;

public class Bomb : MonoBehaviour {
    public GameObject fire;
    internal int firePower;
    internal float fuse;
    GameController gc;
    Vector3[] directions = new Vector3[]
       {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right
       };
    // Use this for initialization
    void Start () {
        Invoke("Explode", fuse);
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }
	public void Explode()
    {
        //prevent double
        CancelInvoke("Explode");       
        Instantiate(fire, transform.position, Quaternion.identity);
        foreach(var direction in directions)
        {
            SpawnFire(direction);
        }
        Destroy(gameObject);
    }
    private void SpawnFire(Vector3 offset, int fire = 1)
    {
        int x = (int)transform.position.x + (int)offset.x * fire;
        int y = (int)transform.position.y + (int)offset.y * fire;
        x = Mathf.Clamp(x, 0, GameController.X - 1);
        y = Mathf.Clamp(y, 0, GameController.Y - 1);
        // 4 goc deu free
        if ((gc.level[x, y] == null || gc.level[x, y].tag == "WayPoint" || gc.level[x, y].tag == "Enemy") && fire <= firePower)
        {         
            Instantiate(this.fire, transform.position + (offset * fire ), Quaternion.identity);
            SpawnFire(offset, ++fire);
        }
        else if (fire <= firePower)
        {
            if (gc.level[x, y] != null && gc.level[x, y].tag == "Destroyable")
            {
                Instantiate(this.fire, transform.position + (offset * fire), Quaternion.identity);
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collsion)
    {
        GetComponent<BoxCollider2D>().isTrigger = false;
    }
}

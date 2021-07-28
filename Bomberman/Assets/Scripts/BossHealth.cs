using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {
    public float maxHP = 200f;
    public float curHP = 0f;
    public GameObject healthbar;
	// Use this for initialization
	void Start () {
        curHP = maxHP;
	}
	
	// Update is called once per frame
    public void DecreaHeahlth()
    {
        curHP -= 1;
        float scaleHP = curHP / maxHP;
        healthbar.transform.localScale = new Vector3(scaleHP, healthbar.transform.localScale.y, healthbar.transform.localScale.z);
    }

}

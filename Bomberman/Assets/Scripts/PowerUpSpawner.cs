using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour {
    public GameObject[] powerUps;
    // public int numberOfFirePowerUps;
    public void SpawnPowerUp()
    {
        //todo keep trach of stpawner powerups
        if (Random.Range(0f, 1f) > 0.8)
        {
            int ramdomIndex = Random.Range(0, powerUps.Length);
            Instantiate(powerUps[ramdomIndex], transform.position, Quaternion.identity);
        }
    }
}

using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

    public GameObject enemy;
    public float cooldownSpawn;

	void Start () {

	}
	
    private float tmpSpawnTime;

	void Update () {
        if (tmpSpawnTime <= 0)
        {
            GameObject e = Instantiate(enemy);
            e.transform.position = this.transform.position;
            tmpSpawnTime = cooldownSpawn;
        }
        else
        {
            tmpSpawnTime -= Time.deltaTime;
        }
    }
}

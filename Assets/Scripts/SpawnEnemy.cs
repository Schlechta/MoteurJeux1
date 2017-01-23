using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

    public GameObject enemy;
    public float cooldownSpawn;

	void Start () {

	}
	
    private float tmpSpawnTime;

	void Update () {
        if (tmpSpawnTime <= 0.0f)
        {
            GameObject e = Instantiate(enemy);
            Vector3 playerPos = GameObject.Find("PlayerControler").transform.position;
            Vector3 newPos = new Vector3(playerPos.x + 20, playerPos.y + 4, 0);
            e.transform.position = newPos;
            tmpSpawnTime = cooldownSpawn;
        }
        else
        {
            tmpSpawnTime -= Time.deltaTime;
        }
    }
}

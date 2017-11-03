using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombie : MonoBehaviour {

    [SerializeField]
    GameObject zombie;
    GameObject manager;
    int minWaitTime;
    int maxWaitTime;
    int waitTime;
    bool spawn;

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        minWaitTime = manager.GetComponent<Manager>().minZombieSpawnTime;
        maxWaitTime = manager.GetComponent<Manager>().maxZombieSpawnTime;
    }

    // Use this for initialization
    void Start () {
        waitTime = Random.Range(minWaitTime, maxWaitTime);
        StartCoroutine(spawnZombie());
    }
	
	// Update is called once per frame
	void Update () {
	}

    private IEnumerator spawnZombie()
    {
        yield return new WaitForSeconds(2);
        Instantiate(zombie, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(spawnZombie());
    }
}

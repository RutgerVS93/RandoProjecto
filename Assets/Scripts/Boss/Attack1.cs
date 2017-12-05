using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour {

    public GameObject projectile;
    private GameObject projectileClone;
    public GameObject impact;

    public Vector2 spawnArea;
    public float spawnWidth;

    public float numberOfProjectiles;


    void Start ()
    {
        StartCoroutine(SpawnProjectiles());
	}
	
	void Update ()
    {
		
	}

    IEnumerator SpawnProjectiles()
    {
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            GameObject projectileClone = Instantiate(projectile, new Vector2(spawnArea.x + Random.Range(-spawnWidth, spawnWidth), spawnArea.y) , Quaternion.identity);
            yield return new WaitForSeconds(.2f);
            Destroy(projectileClone, 5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject impactClone = Instantiate(impact, projectileClone.transform.position, projectileClone.transform.rotation);
        }
    }
}

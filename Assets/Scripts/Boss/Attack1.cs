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

    public bool fallingObjectsAttack;
    public bool circleAttack;
    public bool backAndForthAttack;

    void Start ()
    {
        if (fallingObjectsAttack)
        {
            StartCoroutine(FallingObject());
        }
        if (circleAttack)
        {
            StartCoroutine(CircleObjects());
        }
        if (backAndForthAttack)
        {
            StartCoroutine(BackAndForthObjects());
        }
    }
	
	void Update ()
    {
		
	}

    //Bulletscript speed op 0
    IEnumerator FallingObject()
    {
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            GameObject projectileClone = Instantiate(projectile, new Vector2(spawnArea.x + Random.Range(-spawnWidth, spawnWidth), spawnArea.y) , Quaternion.identity);
            yield return new WaitForSeconds(.2f);
            Destroy(projectileClone, 5f);
        }
    }
    
    //Gravity uitzetten of rb kinematic
    IEnumerator CircleObjects()
    {
        float bulletRotation = 0;
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            GameObject projectileClone = Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + bulletRotation));
            yield return new WaitForSeconds(.1f);
            bulletRotation += 10;
            Destroy(projectileClone, 5f);
        }
    }

    //Gravity projectiles aanpassen
    IEnumerator BackAndForthObjects()
    {
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float bulletRotation = Random.Range(0, 180);
            GameObject projectileClone = Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, bulletRotation));
            yield return new WaitForSeconds(.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject impactClone = Instantiate(impact, projectileClone.transform.position, projectileClone.transform.rotation);
            Destroy(impactClone, 1f);
        }
    }
}

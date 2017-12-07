using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour {

    public GameObject projectile;
    private GameObject projectileClone;

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
            projectileClone.AddComponent<Rigidbody2D>().gravityScale = 1;
            projectileClone.AddComponent<BulletScript>().baseSpeed = 0;
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
            projectileClone.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            projectileClone.AddComponent<BulletScript>().baseSpeed = 10;
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
            projectileClone.AddComponent<Rigidbody2D>().gravityScale = 5;
            projectileClone.AddComponent<BulletScript>().baseSpeed = 10;
            yield return new WaitForSeconds(.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Collision met dingen
        }
    }
}

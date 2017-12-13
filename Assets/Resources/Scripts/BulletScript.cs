using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Events;

public class BulletScript : MonoBehaviour {

    public float baseSpeed;
    public GameObject impact;

    [Space]
    public bool speedUp;
    public float _speedUp;

    [Space]
    public bool increaseSize;
    public float _increaseSize;

    void Update ()
    {
        Movement();
        BulletMods();

        Destroy(gameObject, 5f);
	}

    void Movement()
    {
        transform.Translate(Vector2.right * baseSpeed * Time.deltaTime);
    }

    public void BulletMods()
    {
        if (speedUp)
        {
            StartCoroutine(SpeedUpCoroutine());
        }
        if (increaseSize)
        {
            IncreaseSizeFunction();
        }
    }

    IEnumerator SpeedUpCoroutine()
    {
        yield return new WaitForSeconds(.5f);
        baseSpeed += _speedUp;
    }

    void IncreaseSizeFunction()
    {
        transform.localScale = new Vector3(transform.localScale.x + _increaseSize, transform.localScale.y + _increaseSize, transform.localScale.z + _increaseSize);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            GameObject impactClone = Instantiate(impact, transform.position, transform.rotation) as GameObject;
            Destroy(impactClone, 2f);
            Destroy(gameObject);
        }

        if (gameObject.tag == "EnemyBullet" || collision.gameObject.CompareTag("Player"))
        {
            //GameObject impactClone = Instantiate(impact, transform.position, transform.rotation) as GameObject;
            //Destroy(impactClone, 2f);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {

    Vector3 mousePos;
    Vector3 gunPos;
    float angle;
    Rigidbody2D playerRb;
    GameObject player;

    //Bullet
    [Space]
    public GameObject[] bullets;
    public int bulletNumber;

    [Space]
    public Transform firePoint;
    public float coolDown;
    public float timeStamp;
    public float knockBackForce;

    public enum BulletMod
    {
        SpeedUp,
        ScaleUp,
        RapidFire,
        SplitShot
    }
    public BulletMod bulletMod;

    private void Start()
    {
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        Spin();
        BulletVisual();

        if (bulletMod == BulletMod.SplitShot)
        {
            StartCoroutine(SplitShot());
        }
        else if(bulletMod == BulletMod.RapidFire)
        {
            StartCoroutine(RapidFireShot());
        }
        else
        {
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            bulletMod += 1;
            if ((int)bulletMod > bullets.Length - 1)
            {
                bulletMod = 0;
            }
        }

    }

    void Spin()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 5.23f;
        gunPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - gunPos.x;
        mousePos.y = mousePos.y - gunPos.y;
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Fire()
    {
        if (Input.GetMouseButton(0) && timeStamp <= Time.time)
        {         
            GameObject bulletClone = Instantiate(bullets[bulletNumber], firePoint.position, firePoint.rotation);
            timeStamp = Time.time + coolDown;
            KnockBack(knockBackForce);
        }
    }

    IEnumerator SplitShot()
    {        
        if (Input.GetMouseButton(0) && timeStamp <= Time.time)
        {
            float bulletRotation = -20;
            for (int i = 1; i < 4; i++)
            {
                GameObject bulletClone = Instantiate(bullets[bulletNumber], firePoint.position, Quaternion.Euler(0, 0, angle + bulletRotation)) as GameObject;                    
                bulletRotation += 20;
            }
            KnockBack(knockBackForce);
            timeStamp = Time.time + coolDown;
        }

        yield return new WaitForEndOfFrame();
    }

    IEnumerator RapidFireShot()
    {
        if (Input.GetMouseButton(0) && timeStamp <= Time.time)
        {
            GameObject bulletClone = Instantiate(bullets[bulletNumber], firePoint.position, Quaternion.Euler(0, 0, angle + Random.Range(-10, 10)));
            timeStamp = Time.time + coolDown;
            KnockBack(knockBackForce);
        }
        yield return new WaitForEndOfFrame();
    }

    void KnockBack(float _knockBack)
    {
        Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
        playerRb.AddForce(-_knockBack * dir, ForceMode2D.Impulse);
    }

    void BulletVisual()
    {
        switch (bulletMod)
        {
            case BulletMod.SpeedUp:
                bulletNumber = 0;
                coolDown = .1f;
                knockBackForce = 0;
                break;
            case BulletMod.ScaleUp:
                bulletNumber = 1;
                coolDown = .5f;
                knockBackForce = 20;
                break;
            case BulletMod.RapidFire:
                bulletNumber = 2;
                coolDown = .05f;
                knockBackForce = 2;
                break;
            case BulletMod.SplitShot:
                bulletNumber = 3;
                coolDown = .3f;
                knockBackForce = 10;
                break;
        }
    }
}

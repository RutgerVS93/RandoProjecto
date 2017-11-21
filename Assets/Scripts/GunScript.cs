using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {

    public Vector3 mousePos;
    public Vector3 gunPos;
    public float angle;

    //Bullet
    public GameObject bullet;
    public Transform firePoint;
    public float coolDown;
    public float timeStamp;
	
	void Update ()
    {
        Spin();
        Fire();
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
            GameObject bulletClone = Instantiate(bullet, firePoint.position, firePoint.rotation);
            timeStamp = Time.time + coolDown;
        }
    }
}

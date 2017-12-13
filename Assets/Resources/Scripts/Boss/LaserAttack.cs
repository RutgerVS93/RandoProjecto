using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : MonoBehaviour {

    public Transform[] firePoint;

    public GameObject beamStartPrefab;
    public GameObject beamEndPrefab;
    public GameObject beamLinePrefab;

    private GameObject beamStart;
    private GameObject beamLine;
    private GameObject beamEnd;

    private LineRenderer line;

    public float beamEndOffset;
    public float textureScrollSpeed;
    public float textureLengthScale;

    bool spawned = false;
    
	void Start ()
    {
		
	}
	
	void Update ()
    {
        if (!spawned)
        {
            SpawnBeam();
            spawned = true;
        }
	}

    void SpawnBeam()
    {
        beamStart = Instantiate(beamStartPrefab, firePoint[0].position, firePoint[0].rotation);
        beamEnd = Instantiate(beamEndPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        beamLine = Instantiate(beamLinePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        line = beamLine.GetComponent<LineRenderer>();

        //RaycastHit2D hit = Physics2D.Raycast(firePoint[0].position, Vector2.right);
        //if (hit.collider != null)
        //{
        //    float distance = Mathf.Abs(hit.point.x - firePoint[0].position.x);
        //    ShootBeam(firePoint[0].position, distance);
        //}

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Ray ray = firePoint[0].transform.position;
        //RaycastHit hit;
        //if (Physics.Raycast(ray.origin, ray.direction, out hit))
        //{
        //    Vector3 tdir = hit.point - transform.position;
        //    ShootBeam(transform.position, tdir);
        //}
    }
    void ShootBeam(Vector3 start, Vector3 dir)
    {
        line.SetVertexCount(2);
        line.SetPosition(0, start);
        firePoint[0].position = start;

        Vector2 end = Vector2.zero;
        RaycastHit hit;
        if (Physics.Raycast(start, dir, out hit))
        {
            end = hit.point - (dir.normalized * beamEndOffset);
        }
        else
        {
            end = firePoint[0].position + (dir * 100);
        }

        beamEnd.transform.position = end;
        line.SetPosition(1, end);

        beamStart.transform.LookAt(beamEnd.transform.position);
        beamEnd.transform.LookAt(beamStart.transform.position);

        float distance = Vector3.Distance(start, end);
        line.sharedMaterial.mainTextureScale = new Vector2(distance / textureLengthScale, 1);
        line.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);
    }
}

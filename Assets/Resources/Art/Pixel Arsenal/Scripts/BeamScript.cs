using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BeamScript : MonoBehaviour {

    public GameObject beamStart;
    public GameObject beamEnd;
    public GameObject beam;
    public LineRenderer line;

    public GameObject bossObject;

    [Header("Adjustable Variables")]
    public float beamEndOffset = 0f; //How far from the raycast hit point the end effect is positioned
    public float textureScrollSpeed = 8f; //How fast the texture scrolls along the beam
	public float textureLengthScale = 3; //Length of the beam texture

    [Space]
    public Transform firePoint;
    public bool spawned = false;

    void Update()
    {
        line = beam.GetComponent<LineRenderer>();
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, transform.up);
        if (hit.collider != null)
        {
            Vector2 tdir = hit.point - (Vector2)firePoint.position;
            ShootBeamInDir(firePoint.position, tdir);
        }

        if (!spawned)
        {               
            beamStart = Instantiate(beamStart, firePoint.position, transform.rotation) as GameObject;
            beamEnd = Instantiate(beamEnd, hit.point, transform.rotation) as GameObject;
            beam = Instantiate(beam, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

            spawned = true;
        }

        beamStart.transform.rotation = bossObject.transform.rotation;
        beamEnd.transform.rotation = bossObject.transform.rotation;
        beamEnd.transform.position = hit.point;

        beam.transform.rotation = bossObject.transform.rotation;
    }

    void ShootBeamInDir(Vector2 start, Vector2 dir)
    {
        line.SetVertexCount(2);
        line.SetPosition(0, start);
        beamStart.transform.position = start;

        Vector2 end = Vector2.zero;
        RaycastHit2D hit = Physics2D.Raycast(start, dir);
        if (hit.collider != null)
        {
            end = hit.point;
        }
        else if (hit.collider == null)
        {
            end = (Vector2)transform.position + (dir * 100);
        }

        beamEnd.transform.position = end;
        line.SetPosition(1, end);

        float distance = Vector2.Distance(start, end);
        line.sharedMaterial.mainTextureScale = new Vector2(distance / textureLengthScale, 1);
        line.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);
    }
}

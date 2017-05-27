using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMask : MonoBehaviour {
    public float range = 3.0F;
    public int levelOfDetails = 1;
    public LayerMask raycastLayer;
    public bool staticLight = false;
    public float stability = 1.0F;
    public float changeSpeed = 1.0F;

    private Vector2 direction;
    private int index = 0;
    private int triIndex = 0;
    private float frames = 0;
    private int lod = 1;
    private float width;
    private GameObject go;
    private Mesh mesh;
    private Vector3 worldPos;

    private Vector3[] verts;
    private int[] tris;
    private Vector2[] uvs;
    private int mask;


    private void ReshapeLight()
    {
        index = 0;
        triIndex = 0;
        worldPos = go.transform.parent.transform.position;
        verts[index++] = new Vector3(0, 0, 0);

        for (var a = 0; a < 360; a += lod, ++index)
        {
            direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * a), Mathf.Sin(Mathf.Deg2Rad * a));

            RaycastHit2D hit = Physics2D.Raycast(worldPos, direction, width, mask);
            if (hit.collider != null)
            {
                verts[index] = new Vector3(hit.point.x - worldPos.x, hit.point.y - worldPos.y, worldPos.z);
            }
            else
            {
                verts[index] = new Vector3(direction.x * width, direction.y * width, worldPos.z);
            }
        }

        for (var i = 1; i < (360 / lod); ++i, triIndex += 3)
        {
            tris[triIndex] = 0;
            tris[triIndex + 1] = i + 1;
            tris[triIndex + 2] = i;
        }
        tris[(360 / lod) * 3 - 3] = 0;
        tris[(360 / lod) * 3 - 2] = 1;
        tris[(360 / lod) * 3 - 1] = 360 / lod;

        int j = 0;
        while (j < uvs.Length)
        {
            uvs[j] = new Vector2(verts[j].x, verts[j].y);
            ++j;
        }

        mesh.Clear();
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }

    // Use this for initialization
    void Start()
    {
        go = gameObject;
        go.GetComponent<MeshFilter>().mesh = mesh = new Mesh();

        lod = levelOfDetails;
        mask = raycastLayer;

        verts = new Vector3[(360 / lod) + 1];
        tris = new int[(360 / lod) * 3];
        uvs = new Vector2[verts.Length];

        if (staticLight)
        {
            ReshapeLight();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!staticLight)
        {
            if (stability != 1.0F)
            {
                frames += changeSpeed;
                if (frames >= 60)
                {
                    frames = 0;
                }

                width = range * Mathf.Sin(Mathf.Deg2Rad * frames * 3) * (1 - stability) + range * stability;
            }
            
            ReshapeLight();
        }
    }
}

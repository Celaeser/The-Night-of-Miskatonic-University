using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVMesh : MonoBehaviour {
    public float range = 3;
    public int levelOfDetails = 1;
    public LayerMask[] layerMask;

    private Vector2 direction;
    private int index = 0;
    private int triIndex = 0;
    private int lod = 1;
    private float width;
    private GameObject go;
    private GameObject pGo;
    private Mesh mesh;
    private Vector3 worldPos;

    private Vector3[] verts;
    private int[] tris;
    private Vector2[] uvs;
    private List<GameObject> sawWalls;

    private int mask;

    public void ResetWalls()
    {
        sawWalls.Clear();
    }

	// Use this for initialization
	void Start () {
        go = gameObject;
        go.GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        sawWalls = new List<GameObject>();

        lod = levelOfDetails;
        width = range;

        verts = new Vector3[(360 / lod) + 1];
        tris = new int[(360 / lod) * 3];
        uvs = new Vector2[verts.Length];

        foreach (LayerMask lm in layerMask)
        {
            mask |= lm;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (pGo == null)
        {
            pGo = GameObject.FindGameObjectWithTag("Player");
        }

        foreach (var wall in sawWalls)
        {
            wall.layer = 8;
        }
        sawWalls.Clear();

        index = 0;
        triIndex = 0;
        worldPos = pGo.transform.position;
        verts[index++] = worldPos;

        for (var a = 0; a < 360; a += lod, ++index)
        {
            direction = new Vector2(Mathf.Sin(Mathf.Deg2Rad * a), Mathf.Cos(Mathf.Deg2Rad * a));

            RaycastHit2D hit = Physics2D.Raycast(worldPos, direction, width, mask);
            if (hit.collider != null)
            {
                var wall = hit.collider.gameObject;
                if (hit.distance + 1 < width)
                {
                    wall.layer = 9;
                    sawWalls.Add(wall);
                }
                verts[index] = new Vector3(hit.point.x, hit.point.y, worldPos.z);
            }
            else
            {
                verts[index] = new Vector3(worldPos.x + direction.x * width, worldPos.y + direction.y * width, worldPos.z);
            }
        }

        for (var i = 1; i < (360 / lod); ++i, triIndex += 3)
        {
            tris[triIndex] = 0;
            tris[triIndex + 1] = i;
            tris[triIndex + 2] = i + 1;
        }
        tris[(360 / lod) * 3 - 3] = 0;
        tris[(360 / lod) * 3 - 2] = 360 / lod;
        tris[(360 / lod) * 3 - 1] = 1;

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
}

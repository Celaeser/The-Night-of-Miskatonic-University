using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVMesh : MonoBehaviour
{
    public float range = 3;
    public float outlineRange = 6;
    public int levelOfDetails = 1;
    public LayerMask[] layerMask;

    private Vector2 direction;
    private int index = 0;
    private int triIndex = 0;
    private int lod = 1;
    private float width;
    private float outlineWidth;
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
    void Start()
    {
        go = gameObject;
        go.GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        sawWalls = new List<GameObject>();

        lod = levelOfDetails;
        width = range;
        outlineWidth = outlineRange;

        verts = new Vector3[(360 / lod) * 2];
        tris = new int[(360 / lod) * 6];
        uvs = new Vector2[verts.Length];

        foreach (LayerMask lm in layerMask)
        {
            mask |= lm;
        }
    }

    // Update is called once per frame
    void Update()
    {
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

        for (var a = 0; a < 360; a += lod, ++index)
        {
            direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * a), Mathf.Sin(Mathf.Deg2Rad * a));

            RaycastHit2D hit = Physics2D.Raycast(worldPos, direction, width, mask);
            if (hit.collider != null)
            {
                var wall = hit.collider.gameObject;
                if (hit.distance + 1 < width)
                {
                    wall.layer = 9;
                    sawWalls.Add(wall);
                }
                verts[index * 2] = new Vector3(hit.point.x - worldPos.x, hit.point.y - worldPos.y, worldPos.z - worldPos.z);
                verts[index * 2 + 1] = new Vector3(direction.x * outlineWidth, direction.y * outlineWidth, 0);
            }
            else
            {
                verts[index * 2] = new Vector3(direction.x * width, direction.y * width, 0);
                verts[index * 2 + 1] = new Vector3(direction.x * outlineWidth, direction.y * outlineWidth, 0);
            }
        }

        for (var i = 0; i < (360 / lod) - 1; ++i, triIndex += 6)
        {
            tris[triIndex] = i * 2;
            tris[triIndex + 1] = i * 2 + 2;
            tris[triIndex + 2] = i * 2 + 1;

            tris[triIndex + 3] = i * 2 + 1;
            tris[triIndex + 4] = i * 2 + 2;
            tris[triIndex + 5] = i * 2 + 3;
        }

        int lastTriIndex = ((360 / lod) - 1) * 6;

        tris[lastTriIndex] = ((360 / lod) - 1) * 2;
        tris[lastTriIndex + 1] = 0;
        tris[lastTriIndex + 2] = ((360 / lod) - 1) * 2 + 1;

        tris[lastTriIndex + 3] = ((360 / lod) - 1) * 2 + 1;
        tris[lastTriIndex + 4] = 0;
        tris[lastTriIndex + 5] = 1;

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

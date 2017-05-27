using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerLighter : MonoBehaviour {
    public int targetLayer;
    public int litLayer;
    public float range = 1.0F;

    private HashSet<GameObject> litObjs = new HashSet<GameObject>();
    private int layerMask;

    public void ResetLit()
    {
        foreach (GameObject go in litObjs)
        {
            go.layer = targetLayer;
        }
        litObjs.Clear();
    }

    public void Clear()
    {
        litObjs.Clear();
    }

    private void Start()
    {
        layerMask = 1 << targetLayer;
        layerMask |= transform.parent.gameObject.GetComponentInChildren<LightMask>().raycastLayer;
    }

    private void Update()
    {
        ResetLit();

        for (var a = 0; a < 360; a += 18)
        {
            var direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * a), Mathf.Sin(Mathf.Deg2Rad * a));

            RaycastHit2D hit = Physics2D.Raycast(transform.parent.parent.position, direction, range, layerMask);
            if (hit.collider != null && hit.collider.gameObject.layer == targetLayer)
            {
                if (litObjs.Add(hit.collider.gameObject))
                {
                    hit.collider.gameObject.layer = litLayer;
                }
            }
        }
    }
}

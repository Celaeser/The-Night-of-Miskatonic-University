using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCtr : MonoBehaviour {
    public float range = 3;
    [SerializeField]
    private GameObject mask = null;
    [SerializeField]
    private GameObject monLit = null;
    [SerializeField]
    private GameObject wallLit = null;

    public void Clear()
    {
        monLit.GetComponent<LayerLighter>().Clear();
        wallLit.GetComponent<LayerLighter>().Clear();
    }

    public void ResetLit()
    {
        monLit.GetComponent<LayerLighter>().ResetLit();
        wallLit.GetComponent<LayerLighter>().ResetLit();
    }

    // Use this for initialization
    void Start () {
        mask.GetComponent<LightMask>().range = range;
        monLit.GetComponent<LayerLighter>().range = range - 0.5F;
        wallLit.GetComponent<LayerLighter>().range = range - 1;
    }
}

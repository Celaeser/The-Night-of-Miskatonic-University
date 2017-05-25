using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPixelTexture : MonoBehaviour {
    public List<GameObject> cameras;
    public GameObject image;

    private Material imageMat;
    private List<RenderTexture> textures;
    private bool init = false;

    private void Start()
    {
        textures = new List<RenderTexture>();
        imageMat = image.GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        if (!init)
        {
            foreach (GameObject cm in cameras)
            {
                textures.Add(
                cm.GetComponent<LowPixelCamera>().LowPixelRenderTexture);
            }

            imageMat.SetTexture("_MainTex", textures[0]);
            imageMat.SetTexture("_SubTex", textures[1]);

            init = true;
        }
    }
}

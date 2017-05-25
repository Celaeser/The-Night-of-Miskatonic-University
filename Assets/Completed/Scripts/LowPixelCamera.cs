using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LowPixelCamera : MonoBehaviour
{
    public float lowPixelRatio = 0.5f;

    public static LowPixelCamera Ins = null;
    private void Awake() { Ins = this; }
    private void OnDestroy() { Ins = null; }
    
    private RenderTexture lowPixelRenderTexture;
    private Camera mCamera;

    private void Start()
    {
        mCamera = gameObject.GetComponent<Camera>();
        
        mCamera.depthTextureMode = DepthTextureMode.DepthNormals;
        lowPixelRenderTexture = mCamera.targetTexture;
        lowPixelRenderTexture.height = (int)(Screen.height * lowPixelRatio);
        lowPixelRenderTexture.width = (int)(Screen.width * lowPixelRatio);
            //new RenderTexture((int)(Screen.width * lowPixelRatio), (int)(Screen.height * lowPixelRatio), 32, RenderTextureFormat.ARGB32);
        lowPixelRenderTexture.filterMode = FilterMode.Point;
    }

    public RenderTexture LowPixelRenderTexture
    {
        get
        {
            return lowPixelRenderTexture;
        }
    }
}


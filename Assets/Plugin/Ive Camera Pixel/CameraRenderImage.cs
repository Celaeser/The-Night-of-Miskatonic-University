using UnityEngine;
using System.Collections;
using Ive;

[ExecuteInEditMode]
public class CameraRenderImage : IveBehaviour
{
    public static CameraRenderImage Ins = null;
    private void Awake() { Ins = this; }
    private void OnDestroy() { Ins = null; }

    public float lowPixelRatio = 0.5f;

    private RenderTexture lowPixelRenderTexture;

    private void Start()
    {
        //_lowPixelMode = archive...;
        _lowPixelMode = true;
        mCamera.depthTextureMode = DepthTextureMode.DepthNormals;
        lowPixelRenderTexture = new RenderTexture((int)(Screen.width * lowPixelRatio), (int)(Screen.height * lowPixelRatio), 24);
        lowPixelRenderTexture.filterMode = FilterMode.Point;
        lowPixelModeCamera.GetComponent<PixelCamera>().InputTexture = lowPixelRenderTexture;
            

        SetLowPixelMode(_lowPixelMode);
    }
    private bool _lowPixelMode = false;
    public bool LowPixelMode
    {
        get { return _lowPixelMode; }
        set
        {
            if (value == _lowPixelMode) return;
            _lowPixelMode = value;
            SetLowPixelMode(_lowPixelMode);
        }
    }
    private void SetLowPixelMode(bool value)
    {
        if (value)
        {
            mCamera.targetTexture = lowPixelRenderTexture;
            lowPixelModeCamera.SetActive(true);
        }
        else
        {
            mCamera.targetTexture = null; 
            lowPixelModeCamera.SetActive(false);
        }
    }


    [SerializeField]
    private GameObject lowPixelModeCamera;
}

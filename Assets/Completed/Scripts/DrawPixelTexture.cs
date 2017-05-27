using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPixelTexture : MonoBehaviour {
    public List<GameObject> cameras;
    public GameObject image;
    public float highLightDark = 0.9f;
    public float lowLightDark = 0.7f;

    private Material imageMat;
    private List<RenderTexture> textures;
    private bool init = false;
    private enum LightState
    {
        On, OnToOff, OffToOn, Off
    };
    private LightState lState = LightState.On;
    private float dark;

    public void SwitchLight()
    {
        switch (lState)
        {
            case LightState.On:
            case LightState.OffToOn:
                lState = LightState.OnToOff;
                break;

            case LightState.OnToOff:
            case LightState.Off:
                lState = LightState.OffToOn;
                break;
        }
    }

    private void Start()
    {
        textures = new List<RenderTexture>();
        imageMat = image.GetComponent<MeshRenderer>().material;
        dark = highLightDark;
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

            image.transform.localScale = new Vector3(textures[0].width * 1.0F / textures[1].height, 1, 1);
            imageMat.SetTexture("_MainTex", textures[0]);
            imageMat.SetTexture("_SubTex", textures[1]);

            init = true;
        }

        switch (lState)
        {
            case LightState.OffToOn:
                dark += (highLightDark - lowLightDark) / 60f;
                break;

            case LightState.OnToOff:
                dark += (lowLightDark - highLightDark) / 60f;
                break;
        }


        if (dark >= highLightDark)
        {
            dark = highLightDark;
            lState = LightState.On;
        }
        if (dark <= lowLightDark)
        {
            dark = lowLightDark;
            lState = LightState.Off;
        }

        imageMat.SetFloat("_Darkness", dark);
    }
}

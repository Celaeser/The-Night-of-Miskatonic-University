using UnityEngine;
using System.Collections;
using Ive;

public class PixelCamera : IveBehaviour
{
    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(InputTexture, dst);
    }
    public RenderTexture InputTexture;
}

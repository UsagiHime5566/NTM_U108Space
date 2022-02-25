using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderTextureReceiver : MonoBehaviour
{
    public SignalServer server;
    public RenderTexture target;

    void Start()
    {
        server.OnSignalReceivedByte += x => {
            //Debug.Log($"Parse data length: {x.Length}");

            Texture2D texture2D = new Texture2D(2,2);
            texture2D.LoadImage(x);
            RenderTexture.active = target;
            // Copy your texture ref to the render texture
            Graphics.Blit(texture2D, target);
            RenderTexture.active = null;
        };
    }
}

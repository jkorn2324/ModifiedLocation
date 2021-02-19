using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedLocation.Scripts.Utils
{

    public static class GameUtils
    {

        /// <summary>
        /// Used to create a readable texture for the image.
        /// 
        /// Taken from this thread:
        /// https://stackoverflow.com/questions/44733841/how-to-make-texture2d-readable-via-scripts
        /// </summary>
        /// <param name="image">The texture image.</param>
        /// <returns>A new readable image.</returns>
        public static Texture2D CreateReadableTexture(Texture2D image)
        {
            RenderTexture renderTexture = RenderTexture.GetTemporary(
                image.width, image.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
            Graphics.Blit(image, renderTexture);
            RenderTexture prevTexture = RenderTexture.active;
            Texture2D readableTexture = new Texture2D(image.width, image.height);
            readableTexture.ReadPixels(new Rect(0.0f, 0.0f, image.width, image.height), 0, 0);
            readableTexture.Apply();
            RenderTexture.active = prevTexture;
            RenderTexture.ReleaseTemporary(renderTexture);
            return readableTexture;
        }
    }
}


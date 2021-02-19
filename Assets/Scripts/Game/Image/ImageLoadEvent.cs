using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImageAndVideoPicker;

namespace ModifiedLocation.Scripts.Game
{
    /// <summary>
    /// The image loaded data.
    /// </summary>
    public struct ImageLoadedData
    {
        public string imagePath;
        public Texture2D imageTexture;
        public ImageOrientation orientation;
    }

    [CreateAssetMenu(fileName = "Image Loaded Event", menuName = "Events/Image Loaded Event")]
    public class ImageLoadEvent : Utils.GenericEvent<ImageLoadedData> { }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImageAndVideoPicker;

namespace ModifiedLocation.Scripts.Game
{
    /// <summary>
    /// The image picker listener.
    /// </summary>
    public class ImagePickerListener : Utils.EventsListener
    {
        [SerializeField]
        private Utils.GameEvent playerSelectImageEvent;
        [SerializeField]
        private ImageLoadEvent imageLoadEvent;
        [SerializeField]
        private Utils.GameEvent imageCancelledEvent;

        protected override void HookEvents()
        {
            this.playerSelectImageEvent?.AddListener(this.OnPlayerSelectImage);
            PickerEventListener.onImageSelect += this.OnImageSelect;
            PickerEventListener.onImageLoad += this.OnImageLoad;
            PickerEventListener.onError += this.OnError;
            PickerEventListener.onCancel += this.OnCancel;
        }

        protected override void UnHookEvents()
        {
            this.playerSelectImageEvent?.RemoveListener(this.OnPlayerSelectImage);
            PickerEventListener.onImageSelect -= this.OnImageSelect;
            PickerEventListener.onImageLoad -= this.OnImageLoad;
            PickerEventListener.onError -= this.OnError;
            PickerEventListener.onCancel -= this.OnCancel;
        }

        private void OnPlayerSelectImage()
        {
            IOSPicker.BrowseImage(false);
        }

        private void OnImageSelect(string imagePath, ImageOrientation orientation)
        {

            // TODO:
        }

        private void OnImageLoad(string imagePath, Texture2D texture2D, ImageOrientation orientation)
        {
            ImageLoadedData loadedData = new ImageLoadedData();
            loadedData.imagePath = imagePath;
            loadedData.imageTexture = texture2D;
            loadedData.orientation = orientation;
            this.imageLoadEvent?.CallEvent(loadedData);
        }

        private void OnError(string error)
        {
            // TODO: 
        }

        private void OnCancel()
        {
            this.imageCancelledEvent?.CallEvent();
        }
    }
}

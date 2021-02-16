using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace ModifiedLocation.Scripts.Game
{
    [RequireComponent(typeof(ARTrackedImageManager))]
    public class ARTrackedImageEventListener : Utils.EventsListener
    {
        [SerializeField]
        private Utils.ARTrackedImageEvent addedImageEvent;
        [SerializeField]
        private Utils.ARTrackedImageEvent removedImageEvent;
        [SerializeField]
        private RuntimeReferenceImageReference imageLibraryReference;
        [SerializeField]
        private Utils.BoolReference trackingSupported;

        private ARTrackedImageManager _imageManager;

        private void Awake()
        {
            this._imageManager = this.GetComponent<ARTrackedImageManager>();
        }

        protected override void OnStart()
        {
            if(this.trackingSupported.Value)
            {
                this.imageLibraryReference.Value = this._imageManager.CreateRuntimeLibrary() as MutableRuntimeReferenceImageLibrary;
            }
        }

        protected override void HookEvents()
        {
            this._imageManager.trackedImagesChanged += this.OnTrackedImagesChanged;
        }

        protected override void UnHookEvents()
        {
            this._imageManager.trackedImagesChanged -= this.OnTrackedImagesChanged;
        }

        /// <summary>
        /// Called when the tracked images has changed.
        /// </summary>
        /// <param name="args">The args for the image changed.</param>
        private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
        {
            foreach(ARTrackedImage image in args.added)
            {
                this.addedImageEvent?.CallEvent(image);
            }

            foreach(ARTrackedImage image in args.removed)
            {
                this.removedImageEvent?.CallEvent(image);
            }
        }
    }

}

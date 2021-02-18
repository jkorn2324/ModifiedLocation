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
        private Utils.ARTrackedImageEvent updatedImageEvent;
        [SerializeField]
        private Utils.ARTrackedImageEvent removedImageEvent;
        [SerializeField]
        private RuntimeReferenceImageReference imageLibraryReference;
        [SerializeField]
        private Utils.BoolReference trackingSupported;
        [SerializeField]
        private Utils.GameEvent createReferenceLibEvent;

        private ARTrackedImageManager _imageManager;

        private void Awake()
        {
            this._imageManager = this.GetComponent<ARTrackedImageManager>();
            if(this._imageManager.descriptor != null)
            {
                Debug.Log("Supports a mutable library" +
                    this._imageManager.descriptor.supportsMutableLibrary);
            }
        }

        protected override void HookEvents()
        {
            this.createReferenceLibEvent?.AddListener(this.CreateReferenceLibrary);
            this._imageManager.trackedImagesChanged += this.OnTrackedImagesChanged;
        }

        protected override void UnHookEvents()
        {
            this.createReferenceLibEvent?.RemoveListener(this.CreateReferenceLibrary);
            this._imageManager.trackedImagesChanged -= this.OnTrackedImagesChanged;
        }

        private void CreateReferenceLibrary()
        {
            if(this.trackingSupported.Value)
            {
                Debug.Log("Creating image library reference.");
                MutableRuntimeReferenceImageLibrary runtimeRefImageLib =
                    this._imageManager.CreateRuntimeLibrary() as MutableRuntimeReferenceImageLibrary;
                this.imageLibraryReference.Value = runtimeRefImageLib;
                this._imageManager.referenceLibrary = runtimeRefImageLib;
                this._imageManager.enabled = true;
            }
        }

        /// <summary>
        /// Called when the tracked images has changed.
        /// </summary>
        /// <param name="args">The args for the image changed.</param>
        private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
        {
            foreach(ARTrackedImage addedImage in args.added)
            {
                this.addedImageEvent?.CallEvent(addedImage);
            }

            foreach(ARTrackedImage updatedImage in args.updated)
            {
                if(updatedImage.trackingState == TrackingState.Tracking)
                {
                    this.updatedImageEvent?.CallEvent(updatedImage);
                    continue;
                }
                // Tracks whether the image is removed.
                this.removedImageEvent?.CallEvent(updatedImage);
            }

            foreach(ARTrackedImage removedImage in args.removed)
            {
                this.removedImageEvent?.CallEvent(removedImage);
            }
        }
    }

}

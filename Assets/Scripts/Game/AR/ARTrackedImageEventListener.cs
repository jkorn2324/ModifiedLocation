using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ModifiedLocation.Scripts.Game
{
    [RequireComponent(typeof(ARTrackedImageManager))]
    public class ARTrackedImageEventListener : Utils.EventsListener
    {
        [SerializeField]
        private ARTrackedImageManager _imageManager;

        private void Awake()
        {
            this._imageManager = this.GetComponent<ARTrackedImageManager>();
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
            // TODO: Implementation
        }
    }

}

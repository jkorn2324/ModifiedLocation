using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace ModifiedLocation.Scripts.Game
{
    /// <summary>
    /// The AR Tracked image data.
    /// </summary>
    public struct ARTrackedImageData
    {
        public ARTrackedImage imageObject;
        public XRTrackedImage imageData;
    }

    public class ClueManager : Utils.EventsListener
    {
        [SerializeField]
        private GameClueSet clueSet;
        [SerializeField]
        private Utils.ARTrackedImageEvent addedImageEvent;
        [SerializeField]
        private Utils.ARTrackedImageEvent updatedImageEvent;
        [SerializeField]
        private RuntimeReferenceImageReference imageLibrary;

        protected override void HookEvents()
        {
            this.imageLibrary.ChangedValueEvent += OnRuntimeImageLibChanged;
            this.addedImageEvent?.AddListener(this.OnImageAdded);
            this.updatedImageEvent?.AddListener(this.OnImageUpdated);
        }

        protected override void UnHookEvents()
        {
            this.imageLibrary.ChangedValueEvent -= OnRuntimeImageLibChanged;
            this.addedImageEvent?.RemoveListener(this.OnImageAdded);
            this.updatedImageEvent?.RemoveListener(this.OnImageUpdated);
        }

        private void OnRuntimeImageLibChanged(MutableRuntimeReferenceImageLibrary imageLibrary)
        {
            if(imageLibrary == null)
            {
                return;
            }
            this.clueSet?.InitClueSet(imageLibrary);
        }

        private void OnImageAdded(ARTrackedImage image)
        {
            this.clueSet?.UpdateClueFromImage(image, RuntimeClueImageState.STATE_ADDED);
        }

        private void OnImageUpdated(ARTrackedImage image)
        {
            this.clueSet?.UpdateClueFromImage(image, RuntimeClueImageState.STATE_UPDATED);
        }
    }
}


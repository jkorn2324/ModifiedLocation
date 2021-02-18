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

    /// <summary>
    /// The riddle manager.
    /// </summary>
    public class RiddleManager : Utils.EventsListener
    {
        [SerializeField]
        private GameRiddleSet riddleSet;
        [SerializeField]
        private Utils.ARTrackedImageEvent addedImageEvent;
        [SerializeField]
        private Utils.ARTrackedImageEvent updatedImageEvent;
        [SerializeField]
        private Utils.ARTrackedImageEvent removedImageEvent;
        [SerializeField]
        private RuntimeReferenceImageReference imageLibrary;

        private static bool _initialized = false;

        protected override void OnStart()
        {
            if(_initialized)
            {
                Destroy(this.gameObject);
                return;
            }
            _initialized = true;
            this.riddleSet?.InitRiddleSet();
            DontDestroyOnLoad(this.gameObject);
        }

        protected override void HookEvents()
        {
            this.imageLibrary.ChangedValueEvent += OnRuntimeImageLibChanged;
            this.addedImageEvent?.AddListener(this.OnImageAdded);
            this.updatedImageEvent?.AddListener(this.OnImageUpdated);
            this.removedImageEvent?.AddListener(this.OnImageRemoved);
        }

        protected override void UnHookEvents()
        {
            this.imageLibrary.ChangedValueEvent -= OnRuntimeImageLibChanged;
            this.addedImageEvent?.RemoveListener(this.OnImageAdded);
            this.updatedImageEvent?.RemoveListener(this.OnImageUpdated);
            this.removedImageEvent?.RemoveListener(this.OnImageRemoved);
        }

        private void OnRuntimeImageLibChanged(MutableRuntimeReferenceImageLibrary imageLibrary)
        {
            if(imageLibrary == null)
            {
                return;
            }
            this.riddleSet?.SetReferenceLibrary(imageLibrary);
        }

        private void OnImageAdded(ARTrackedImage image)
        {
            this.riddleSet?.UpdateRiddleFromImage(image, RuntimeRiddleClueState.STATE_ADDED);
        }

        private void OnImageUpdated(ARTrackedImage image)
        {
            this.riddleSet?.UpdateRiddleFromImage(image, RuntimeRiddleClueState.STATE_UPDATED);
        }

        private void OnImageRemoved(ARTrackedImage image)
        {
            this.riddleSet?.UpdateRiddleFromImage(image, RuntimeRiddleClueState.STATE_REMOVED);
        }
    }
}


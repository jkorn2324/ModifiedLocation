using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace ModifiedLocation.Scripts.Game
{ 
    public class ClueManager : Utils.EventsListener
    {
        [SerializeField]
        private GameClueSet gameClueSet;
        [SerializeField]
        private Utils.ARTrackedImageEvent addedImageEvent;
        [SerializeField]
        private RuntimeReferenceImageReference imageLibrary;
        [SerializeField]
        private Utils.BoolReference imageTrackingSupported;

        protected override void OnStart()
        {
            this.OnSupportedChanged(this.imageTrackingSupported.Value);
        }

        protected override void HookEvents()
        {
            this.imageTrackingSupported.ChangedValueEvent += this.OnSupportedChanged;
            this.addedImageEvent?.AddListener(this.OnImageAdded);
        }

        protected override void UnHookEvents()
        {
            this.imageTrackingSupported.ChangedValueEvent -= this.OnSupportedChanged;
        }

        private void OnSupportedChanged(bool supported)
        {
            if(supported && imageLibrary.Value != null)
            {
                this.gameClueSet?.AddReferenceClues(imageLibrary.Value);
            }
        }

        private void OnImageAdded(ARTrackedImage image)
        {
            GameClue clueAsset = this.gameClueSet.GetClueFromImageName(image.name);
            if(clueAsset != null)
            {
                RuntimeClue runtimeClue = new RuntimeClue(clueAsset, image);
                runtimeClue.SpawnPrefab();

                Debug.Log("Found a clue that exists.");
            }
        }
    }
}


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


        protected override void HookEvents()
        {
            this.imageLibrary.ChangedValueEvent += OnRuntimeImageLibChanged;
            this.addedImageEvent?.AddListener(this.OnImageAdded);
        }

        protected override void UnHookEvents()
        {
            this.imageLibrary.ChangedValueEvent -= OnRuntimeImageLibChanged;
            this.addedImageEvent?.RemoveListener(this.OnImageAdded);
        }

        private void OnRuntimeImageLibChanged(MutableRuntimeReferenceImageLibrary imageLibrary)
        {
            if(imageLibrary == null)
            {
                return;
            }
            this.gameClueSet?.AddReferenceClues(imageLibrary);
        }

        private void OnImageAdded(ARTrackedImage image)
        {
            GameClue clueAsset = this.gameClueSet.GetClueFromImageName(image.name);
            if(clueAsset != null)
            {
                Debug.Log("Image has been added..." + image.name);
                RuntimeClue runtimeClue = new RuntimeClue(clueAsset, image);
                runtimeClue.SpawnPrefab();

                Debug.Log("Found a clue that exists.");
            }
        }
    }
}


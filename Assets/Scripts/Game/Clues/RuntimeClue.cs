using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ModifiedLocation.Scripts.Game
{
    /// <summary>
    /// The runtime clue image state used for updating.
    /// </summary>
    public enum RuntimeClueImageState
    {
        STATE_REMOVED,
        STATE_UPDATED,
        STATE_ADDED
    }

    /// <summary>
    /// The Clue based on how it is at runtime.
    /// </summary>
    public class RuntimeClue
    {
        private GameClue _gameClue;
        private ClueScannedCompnent _preScannedObject;

        public GameClue GameClueData
            => this._gameClue;

        public RuntimeClue(GameClue clue)
        {
            this._gameClue = clue;
            this._preScannedObject = null;
        }

        /// <summary>
        /// Spawned the prefab.
        /// </summary>
        /// <param name="image">The ar tracked image itself.</param>
        /// <param name="state">The state of the image.</param>
        public void UpdatePrefab(ARTrackedImage image, RuntimeClueImageState state)
        {
            if(this._preScannedObject == null && !this._gameClue.Found)
            {
                GameObject prefab = this._gameClue.ReferenceImageData.Prefab.gameObject;
                GameObject spawnedObject = GameObject.Instantiate(prefab);
                this._preScannedObject = spawnedObject.GetComponent<ClueScannedCompnent>();
                this._preScannedObject.ParentClue = this;
            }
            GameObject gameObject = this._preScannedObject.gameObject;
            gameObject.transform.position = image.transform.position;
            gameObject.transform.rotation = image.transform.rotation;
        }
    }

    /// <summary>
    /// Manages a set of clues at runtime.
    /// </summary>
    public class RuntimeClueManager
    {
        private List<RuntimeClue> _clues;
        private GameClueSet _parentSet;

        public RuntimeClueManager(GameClueSet parentSet)
        {
            this._parentSet = parentSet;
            this._clues = new List<RuntimeClue>();
        }

        /// <summary>
        /// Updates the clue from the image.
        /// </summary>
        /// <param name="image">The tracked image.</param>
        /// <param name="state">The image state.</param>
        public void UpdateClueFromImage(ARTrackedImage image, RuntimeClueImageState state)
        {
            string imageName = image.referenceImage.name;
            RuntimeClue clue = this.GetClueFromImage(imageName);
            if(clue != null)
            {
                clue.UpdatePrefab(image, state);
                return;
            }

            GameClue gameClueData = this._parentSet.GetClueFromImage(imageName);
            if(gameClueData != null)
            {
                clue = new RuntimeClue(gameClueData);
                this._clues.Add(clue);
                clue.UpdatePrefab(image, state);
                return;
            }
        }

        public bool ContainsClueFromImage(string imageName)
        {
            RuntimeClue clue = this.GetClueFromImage(imageName);
            return clue != null;
        }

        public RuntimeClue GetClueFromImage(string imageName)
        {
            return this._clues.Find((RuntimeClue clue) =>
            {
                return clue.GameClueData.ReferenceImageData.ImageName == imageName;
            });
        }
    }
}



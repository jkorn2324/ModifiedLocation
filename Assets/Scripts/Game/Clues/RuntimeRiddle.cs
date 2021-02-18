using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ModifiedLocation.Scripts.Game
{
    /// <summary>
    /// The runtime clue image state used for updating.
    /// </summary>
    public enum RuntimeRiddleClueState
    {
        STATE_REMOVED,
        STATE_UPDATED,
        STATE_ADDED
    }

    /// <summary>
    /// The Clue based on how it is at runtime.
    /// </summary>
    public class RuntimeRiddle
    {
        private GameRiddle _gameClue;
        private RiddleScannedComponent _preScannedObject;
        private RuntimeRiddleManager _runtimeRiddleManager;
        private bool _riddleFound = false;

        public bool RiddleFound
            => this._riddleFound;

        public GameRiddle GameRiddleData
            => this._gameClue;

        public RuntimeRiddle(RuntimeRiddleManager runtimeRiddleManager, GameRiddle clue)
        {
            this._gameClue = clue;
            this._preScannedObject = null;
            this._runtimeRiddleManager = runtimeRiddleManager;
        }

        /// <summary>
        /// Spawned the prefab.
        /// </summary>
        /// <param name="image">The ar tracked image itself.</param>
        /// <param name="state">The state of the image.</param>
        public void UpdatePrefab(ARTrackedImage image, RuntimeRiddleClueState state)
        {
            if(this.RiddleFound)
            {
                this._preScannedObject.transform.position = image.transform.position;
                this._preScannedObject.transform.rotation = image.transform.rotation;
                return;
            }

            if(this._preScannedObject == null)
            {
                GameObject prefab = this._gameClue.ReferenceImageData.Prefab.gameObject;
                GameObject spawnedObject = GameObject.Instantiate(prefab);
                this._preScannedObject = spawnedObject.GetComponent<RiddleScannedComponent>();
                this._preScannedObject.ParentRiddle = this;
            }
            GameObject gameObject = this._preScannedObject.gameObject;
            gameObject.transform.position = image.transform.position;
            gameObject.transform.rotation = image.transform.rotation;
        }

        public void SetRiddleFound(ref PlayerScanResult result)
        {
            if(this.RiddleFound)
            {
                result.scanResult = PlayerScanResultType.RESULT_ALREADY_FOUND;
                return;
            }
            this._riddleFound = this._runtimeRiddleManager.OnRiddleFound(this, ref result);
        }
    }

    /// <summary>
    /// Manages a set of clues at runtime.
    /// </summary>
    public class RuntimeRiddleManager
    {
        private List<RuntimeRiddle> _riddles;
        private GameRiddleSet _parentSet;

        public RuntimeRiddleManager(GameRiddleSet parentSet)
        {
            this._parentSet = parentSet;

            this._riddles = new List<RuntimeRiddle>();
            foreach(GameRiddle riddle in parentSet)
            {
                RuntimeRiddle runtimeRiddle = new RuntimeRiddle(this, riddle);
                this._riddles.Add(runtimeRiddle);
            }
        }

        /// <summary>
        /// Updates the clue from the image.
        /// </summary>
        /// <param name="image">The tracked image.</param>
        /// <param name="state">The image state.</param>
        public void UpdateRiddleFromImage(ARTrackedImage image, RuntimeRiddleClueState state)
        {
            string imageName = image.referenceImage.name;
            RuntimeRiddle riddle = this.GetRiddleFromImage(imageName);
            if(riddle != null)
            {
                riddle.UpdatePrefab(image, state);
            }
        }

        public bool OnRiddleFound(RuntimeRiddle riddle, ref PlayerScanResult result)
        {
            bool? output = this._parentSet?.OnRiddleFound(riddle.GameRiddleData, ref result);
            return output.HasValue && output.Value;
        }

        public bool ContainsRiddleFromImage(string imageName)
        {
            RuntimeRiddle clue = this.GetRiddleFromImage(imageName);
            return clue != null;
        }

        public RuntimeRiddle GetRiddleFromImage(string imageName)
        {
            return this._riddles.Find((RuntimeRiddle clue) =>
            {
                return clue.GameRiddleData.ReferenceImageData.ImageName == imageName;
            });
        }

        public RuntimeRiddle GetRiddle(int index)
        {
            if(index < 0 || index >= this._riddles.Count)
            {
                return null;
            }
            return this._riddles[index];
        }

        public IEnumerator<RuntimeRiddle> GetEnumerator()
        {
            return this._riddles.GetEnumerator();
        }
    }
}



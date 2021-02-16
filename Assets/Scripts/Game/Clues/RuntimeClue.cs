using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ModifiedLocation.Scripts.Game
{
    /// <summary>
    /// The Clue based on how it is at runtime.
    /// </summary>
    public class RuntimeClue
    {
        private GameClue _gameClue;
        private ARTrackedImage _trackedImage;

        public RuntimeClue(GameClue clue, ARTrackedImage trackedImage)
        {
            this._gameClue = clue;
            this._trackedImage = trackedImage;
        }

        /// <summary>
        /// Spawned the prefab.
        /// </summary>
        public void SpawnPrefab()
        {
            if(this._trackedImage != null)
            {
                GameObject prefab = this._gameClue.ReferenceImageData.Prefab;
                GameObject spawnedObject = GameObject.Instantiate(prefab);
                spawnedObject.transform.position = this._trackedImage.transform.position;
                spawnedObject.transform.rotation = this._trackedImage.transform.rotation;
            }
        }
    }
}



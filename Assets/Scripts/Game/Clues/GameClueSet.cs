using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace ModifiedLocation.Scripts.Game
{

    [CreateAssetMenu(fileName = "Game Clue Set", menuName = "Clue/Game Clue Set")]
    public class GameClueSet : ScriptableObject
    {
        [SerializeField]
        private List<GameClue> clues;
        private RuntimeClueManager _runtimeClueManager = null;

        /// <summary>
        /// Adds the reference clues.
        /// </summary>
        /// <param name="library">The library.</param>
        public void InitClueSet(MutableRuntimeReferenceImageLibrary library)
        {
            // Updates the clues amount.
            Debug.Log("Adding reference clues");
            foreach(GameClue clue in this.clues)
            {
                clue.ReferenceImageData.AddReferenceImage(library);
            }

            if (this._runtimeClueManager == null)
            {
                this._runtimeClueManager = new RuntimeClueManager(this);
            }
        }

        public void UpdateClueFromImage(ARTrackedImage image, RuntimeClueImageState state)
        {
            this._runtimeClueManager?.UpdateClueFromImage(image, state);
        }

        public RuntimeClue GetRuntimeClueFromImage(string imageName)
        {
            return this._runtimeClueManager?.GetClueFromImage(imageName);
        }

        public GameClue GetClueFromImage(string imageName)
        {
            return this.clues.Find((GameClue clue) =>
            {
                return clue.ReferenceImageData.ImageName == imageName;
            });
        }
    }
}

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

        /// <summary>
        /// Adds the reference clues.
        /// </summary>
        /// <param name="library">The library.</param>
        public void AddReferenceClues(MutableRuntimeReferenceImageLibrary library)
        {
            // Updates the clues amount.
            Debug.Log("Adding reference clues");
            foreach(GameClue clue in this.clues)
            {
                clue.ReferenceImageData.AddReferenceImage(library);
            }
        }

        /// <summary>
        /// Gets the clue at the given index.
        /// </summary>
        /// <param name="index">The index of the clue.</param>
        /// <returns>The Game Clue.</returns>
        public GameClue GetClue(int index)
        {
            if(index >= clues.Count || index < 0)
            {
                return null;
            }
            return this.clues[index];
        }

        /// <summary>
        /// Gets the clue from an image name.
        /// </summary>
        /// <param name="imageName">The image name.</param>
        /// <returns>The Game Clue.</returns>
        public GameClue GetClueFromImageName(string imageName)
        {
            return this.clues.Find((GameClue clue) =>
            {
                return clue.ReferenceImageData.ImageName == imageName;
            });
        }
    }

    [System.Serializable]
    public class GameClueSetReference : Utils.GenericReference<GameClueSet>
    {
        [SerializeField]
        private GameClueSet value;
        private RuntimeClueManager _clueManager = null;

        public override bool HasVariable 
            => this.value != null;

        protected override GameClueSet ReferenceValue 
        { 
            get => this.value;
            set
            {
                this.value = value;
            }
        }

        public void Init(MutableRuntimeReferenceImageLibrary imageLibrary)
        {
            this.Value?.AddReferenceClues(imageLibrary);
            this._clueManager = new RuntimeClueManager(this.Value);
        }

        public void UpdateClueFromImage(ARTrackedImage image, RuntimeClueImageState state)
        {
            this._clueManager?.UpdateClueFromImage(image, state);
        }

        public override void Reset() { }
    }
}

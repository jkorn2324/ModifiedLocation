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

        public int NumberOfClues
            => this.clues.Count;

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
}

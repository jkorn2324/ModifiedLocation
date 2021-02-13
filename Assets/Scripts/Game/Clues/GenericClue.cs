using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

namespace ModifiedLocation.Scripts.Game
{
    // TODO: Edit the clue

    [CreateAssetMenu(fileName = "Generic Clue", menuName = "Generic Clue")]
    public class GenericClue : ScriptableObject
    {
        [SerializeField]
        private string clueTitle;
        [SerializeField]
        private string clueDescription;
        [SerializeField]
        private List<string> clueHints;

        [SerializeField]
        private Texture2D imageSearched;

        public string ClueTitle 
            => this.clueTitle;

        public string ClueDescription
            => this.clueDescription;
    }
}


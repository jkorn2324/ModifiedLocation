using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using System.Threading.Tasks;

namespace ModifiedLocation.Scripts.Game
{
    [System.Serializable]
    public class ClueReferenceImage
    {
        [SerializeField]
        private string imageName;
        [SerializeField]
        private Texture2D textureImage;
        [SerializeField]
        private GameObject prefab;

        public GameObject Prefab
            => this.prefab;

        public string ImageName
            => this.imageName;

        // Adds a reference image to the library.
        public async void AddReferenceImage(MutableRuntimeReferenceImageLibrary referenceImageLibrary)
        {
            AddReferenceImageJobState job = referenceImageLibrary.ScheduleAddImageWithValidationJob(
                this.textureImage, this.imageName, 1.0f);
            await Task.Run(() => {
                while (!job.jobHandle.IsCompleted); 
            });
        }
    }

    [System.Serializable]
    public class ClueReferenceData
    {
        [SerializeField]
        private string clueTitle;
        [SerializeField, TextArea(2, 30)]
        private string clueDescription;
        [SerializeField]
        private List<string> clueHints;

        public string ClueTitle
            => this.clueTitle;

        public string ClueDescription
            => this.clueDescription;

        public string GetHint(int hintIndex)
        {
            if (clueHints.Count <= hintIndex || hintIndex < 0)
            {
                return null;
            }
            return this.clueHints[hintIndex];
        }
    }

    [CreateAssetMenu(fileName = "Game Clue", menuName = "Clue/Game Clue")]
    public class GameClue : ScriptableObject
    {
        [SerializeField]
        private string clueLocalizedName;
        [SerializeField]
        private ClueReferenceData clueData;
        [SerializeField]
        private ClueReferenceImage referenceImageData;
        [SerializeField, HideInInspector]
        private bool found = false;

        public ClueReferenceData ClueData
            => this.clueData;

        public ClueReferenceImage ReferenceImageData
            => this.referenceImageData;

        public bool Found
        {
            get => this.found;
            set => this.found = value;
        }
    }
}


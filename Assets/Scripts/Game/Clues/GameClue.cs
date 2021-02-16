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
        private ClueScannedCompnent prefab;

        public ClueScannedCompnent Prefab
            => this.prefab;

        public string ImageName
            => this.imageName;

        // Adds a reference image to the library.
        public async void AddReferenceImage(MutableRuntimeReferenceImageLibrary referenceImageLibrary)
        {
            //Debug.Log("Adding the reference texture: " + imageName);
            Texture2D readableTexture = this.CreateReadableTexture(this.textureImage);
            AddReferenceImageJobState job = referenceImageLibrary.ScheduleAddImageWithValidationJob(
                readableTexture, this.imageName, 1.0f);
            await Task.Run(() => {
                while (!job.jobHandle.IsCompleted);
                Debug.Log("Finished adding reference image: " + this.imageName);
                Debug.Log("Number Of Reference Images: " + referenceImageLibrary.count);
            });
        }

        /// <summary>
        /// Used to create a readable texture for the image.
        /// 
        /// Taken from this thread:
        /// https://stackoverflow.com/questions/44733841/how-to-make-texture2d-readable-via-scripts
        /// </summary>
        /// <param name="image">The texture image.</param>
        /// <returns>A new readable image.</returns>
        private Texture2D CreateReadableTexture(Texture2D image)
        {
            RenderTexture renderTexture = RenderTexture.GetTemporary(
                image.width, image.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
            Graphics.Blit(image, renderTexture);
            RenderTexture prevTexture = RenderTexture.active;
            Texture2D readableTexture = new Texture2D(image.width, image.height);
            readableTexture.ReadPixels(new Rect(0.0f, 0.0f, image.width, image.height), 0, 0);
            readableTexture.Apply();
            RenderTexture.active = prevTexture;
            RenderTexture.ReleaseTemporary(renderTexture);
            return readableTexture;
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


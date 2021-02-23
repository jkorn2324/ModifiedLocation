using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using System.Threading.Tasks;

namespace ModifiedLocation.Scripts.Game
{
    [System.Serializable]
    public class RiddleReferenceImage
    {
        [SerializeField]
        private string imageName;
        [SerializeField]
        private Texture2D textureImage;
        [SerializeField]
        private RiddleScannedComponent prefab;

        public RiddleScannedComponent Prefab
            => this.prefab;

        public string ImageName
            => this.imageName;

        // Adds a reference image to the library.
        public async void AddReferenceImage(MutableRuntimeReferenceImageLibrary referenceImageLibrary)
        {
            //Debug.Log("Adding the reference texture: " + imageName);
            Texture2D readableTexture = Utils.GameUtils.CreateReadableTexture(this.textureImage);
            AddReferenceImageJobState job = referenceImageLibrary.ScheduleAddImageWithValidationJob(
                readableTexture, this.imageName, 1.0f);
            await Task.Run(() => {
                while (!job.jobHandle.IsCompleted);
                Debug.Log("Finished adding reference image: " + this.imageName);
                Debug.Log("Number Of Reference Images: " + referenceImageLibrary.count);
            });
        }

        public static RiddleReferenceImage CreateImage(string imageName, Texture2D texture, RiddleScannedComponent prefab)
        {
            RiddleReferenceImage referenceImage = new RiddleReferenceImage();
            referenceImage.imageName = imageName;
            referenceImage.textureImage = texture;
            referenceImage.prefab = prefab;
            return referenceImage;
        }
    }

    [System.Serializable]
    public class RiddleReferenceData
    {
        [SerializeField]
        private string riddleTitle;
        [SerializeField, TextArea(2, 30)]
        private string riddleDescription;
        [SerializeField]
        private List<string> clueHints;

        public string ClueTitle
            => this.riddleTitle;

        public string ClueDescription
            => this.riddleDescription;

        public string GetHint(int hintIndex)
        {
            if (clueHints.Count <= hintIndex || hintIndex < 0)
            {
                return null;
            }
            return this.clueHints[hintIndex];
        }


        public static RiddleReferenceData GenerateReferenceData(string title, string desc)
        {
            RiddleReferenceData data = new RiddleReferenceData();
            data.riddleTitle = title;
            data.riddleDescription = desc;
            return data;
        }
    }

    [CreateAssetMenu(fileName = "Game Riddle", menuName = "Riddle/Game Riddle")]
    public class GameRiddle : ScriptableObject
    {
        [SerializeField]
        private string clueLocalizedName;
        [SerializeField]
        private RiddleReferenceData clueData;
        [SerializeField]
        private RiddleReferenceImage referenceImageData;

        public RiddleReferenceData ClueData
            => this.clueData;

        public RiddleReferenceImage ReferenceImageData
            => this.referenceImageData;
        
        /// <summary>
        /// Creates a new game riddle based off of these parameters.
        /// </summary>
        /// <param name="riddleTitle">The riddle title.</param>
        /// <param name="riddleDescription">The riddle description</param>
        /// <param name="texture">The texture.</param>
        /// <param name="prefab">The prefab.</param>
        /// <returns>The game riddle output.</returns>
        public static GameRiddle Create(string riddleTitle, string riddleDescription, Texture2D texture, RiddleScannedComponent prefab)
        {
            GameRiddle newRiddle = GameRiddle.CreateInstance<GameRiddle>();
            if(newRiddle != null)
            {
                Debug.Log(riddleTitle);
                string localizedName = "";
                RiddleReferenceData referenceData = RiddleReferenceData.GenerateReferenceData(
                    riddleTitle, riddleDescription);
                newRiddle.clueData = referenceData;

                RiddleReferenceImage imageData = RiddleReferenceImage.CreateImage(
                    localizedName, texture, prefab);
                newRiddle.referenceImageData = imageData;
                newRiddle.clueLocalizedName = localizedName;
                newRiddle.name = localizedName;
            }
            return newRiddle;
        }
    }
}


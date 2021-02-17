using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace ModifiedLocation.Scripts.Game
{

    [CreateAssetMenu(fileName = "Game Riddle Set", menuName = "Clue/Game Riddle Set")]
    public class GameRiddleSet : ScriptableObject
    {
        [SerializeField]
        private List<GameRiddle> riddles;

        private List<GameRiddle> _riddlesLeft = new List<GameRiddle>();
        private int _currentActiveRiddleIndex = 0;

        public int GetNumberOfRiddles
            => this.riddles != null ? this.riddles.Count : 0;

        public int GetRiddlesLeft
            => this._riddlesLeft.Count;

        public GameRiddle ActiveRiddle
            => this._riddlesLeft[this._currentActiveRiddleIndex];

        private RuntimeRiddleManager _runtimeRiddleManager = null;

        /// <summary>
        /// Initializes the riddle set.
        /// </summary>
        public void InitRiddleSet()
        {
            foreach(GameRiddle riddle in this.riddles)
            {
                this._riddlesLeft.Add(riddle);
            }

            if(this._runtimeRiddleManager == null)
            {
                this._runtimeRiddleManager = new RuntimeRiddleManager(this);
            }
            this._currentActiveRiddleIndex = Random.Range(0, this._riddlesLeft.Count);
        }

        public void SetReferenceLibrary(MutableRuntimeReferenceImageLibrary library)
        {
            foreach (GameRiddle riddle in this.riddles)
            {
                riddle.ReferenceImageData.AddReferenceImage(library);
            }
        }

        public void UpdateRiddleFromImage(ARTrackedImage image, RuntimeRiddleClueState state)
        {
            this._runtimeRiddleManager?.UpdateRiddleFromImage(image, state);
        }

        public bool OnRiddleFound(GameRiddle riddle, ref PlayerScanResult scanResult)
        {
            if(this.ActiveRiddle != riddle)
            {
                scanResult.scanResult = PlayerScanResultType.RESULT_FAILED;
                return false;
            }

            scanResult.scanResult = PlayerScanResultType.RESULT_LINE_UP;
            scanResult.riddleFound = riddle;

            if(this._riddlesLeft.Contains(riddle))
            {
                this._riddlesLeft.RemoveAt(this._currentActiveRiddleIndex);
            }

            if(this._currentActiveRiddleIndex <= 0)
            {
                // TODO: Completed treasure hunt...
                return true;
            }
            this._currentActiveRiddleIndex = Random.Range(0, this._riddlesLeft.Count);
            scanResult.nextRiddle = this.ActiveRiddle;
            return true;
        }

        public RuntimeRiddle GetRuntimeRiddleFromImage(string imageName)
        {
            return this._runtimeRiddleManager?.GetRiddleFromImage(imageName);
        }

        public GameRiddle GetRiddleFromImage(string imageName)
        {
            return this.riddles.Find((GameRiddle clue) =>
            {
                return clue.ReferenceImageData.ImageName == imageName;
            });
        }

        public IEnumerator<GameRiddle> GetEnumerator()
        {
            return this.riddles.GetEnumerator();
        }
    }
}

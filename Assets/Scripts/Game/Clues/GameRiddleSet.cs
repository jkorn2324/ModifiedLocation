using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace ModifiedLocation.Scripts.Game
{

    [CreateAssetMenu(fileName = "Game Riddle Set", menuName = "Riddle/Game Riddle Set")]
    public class GameRiddleSet : ScriptableObject
    {

        #region fields

        [SerializeField]
        private string riddleSetName;
        [SerializeField]
        private List<GameRiddle> riddles;

        private List<GameRiddle> _riddlesLeft
            = new List<GameRiddle>();
        private RuntimeRiddleManager _runtimeRiddleManager = null;

        #endregion

        #region properties

        public int TotalRiddles
            => this.riddles != null ? this.riddles.Count : 0;
        public int NumRiddlesLeft
            => this._riddlesLeft.Count;

        #endregion

        #region methods

        public void AddRiddle(GameRiddle riddle)
        {
            if(this.riddles.Contains(riddle))
            {
                return;
            }
            this.riddles.Add(riddle);
        }

        /// <summary>
        /// Initializes the riddle set.
        /// </summary>
        public void InitRiddleSet()
        {
            if(this._riddlesLeft.Count > 0)
            {
                this._riddlesLeft.Clear();
            }

            foreach(GameRiddle riddle in this.riddles)
            {
                if(riddle != null)
                {
                    this._riddlesLeft.Add(riddle);
                }
            }

            if(this._runtimeRiddleManager == null)
            {
                this._runtimeRiddleManager = new RuntimeRiddleManager(this);
            }
        }

        public void SetReferenceLibrary(MutableRuntimeReferenceImageLibrary library)
        {
            foreach (GameRiddle riddle in this.riddles)
            {
                if(riddle != null)
                {
                    riddle.ReferenceImageData.AddReferenceImage(library);
                }
            }
        }

        public void UpdateRiddleFromImage(ARTrackedImage image, RuntimeRiddleClueState state)
        {
            this._runtimeRiddleManager?.UpdateRiddleFromImage(image, state);
        }

        public bool OnRiddleFound(GameRiddle riddle, ref PlayerScanResult scanResult)
        {
            scanResult.scanResult = PlayerScanResultType.RESULT_SUCCESS;
            scanResult.riddleFound = riddle;

            if(this._riddlesLeft.Contains(riddle))
            {
                this._riddlesLeft.Remove(riddle);
            }

            if(this._riddlesLeft.Count <= 0)
            {
                // TODO: Completed treasure hunt...
                return true;
            }
            return true;
        }

        public List<RuntimeRiddle> GetRuntimeRiddles()
        {
            List<RuntimeRiddle> riddles = new List<RuntimeRiddle>();
            foreach(RuntimeRiddle riddle in this._runtimeRiddleManager)
            {
                riddles.Add(riddle);
            }
            return riddles;
        }

        public RuntimeRiddle GetRuntimeRiddleFromImage(string imageName)
        {
            return this._runtimeRiddleManager?.GetRiddleFromImage(imageName);
        }

        public RuntimeRiddle GetRuntimeRiddle(int index)
        {
            return this._runtimeRiddleManager?.GetRiddle(index);
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

        #endregion
    }
}

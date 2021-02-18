using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModifiedLocation.Scripts.Game
{

    [System.Serializable]
    public struct RiddleUIElements
    {
        [SerializeField]
        public Text riddleText;
        [SerializeField]
        public Text riddleName;
        [SerializeField]
        public Canvas riddleCanvas;
        [SerializeField]
        public Canvas playerUICanvas;
        [SerializeField]
        public Canvas riddleScannedCanvas;
    }

    public class RiddleUI : MonoBehaviour
    {
        [SerializeField]
        private GameRiddleSet riddleSet;
        [SerializeField]
        private RiddleUIElements uiElements;

        private int _currentViewIndex = 0;
        private RuntimeRiddle _viewableRiddle = null;

        /// <summary>
        /// Called when the riddle ui displayed.
        /// </summary>
        public void OnRiddleUIDisplayed()
        {
            this._viewableRiddle = this.riddleSet?.GetRuntimeRiddle(this._currentViewIndex);
            this.UpdateUIElements(this._viewableRiddle);

            // TODO: Animation
            this.uiElements.riddleCanvas.enabled = true;
            this.uiElements.playerUICanvas.enabled = false;
            this.uiElements.riddleScannedCanvas.enabled = false;
        }

        /// <summary>
        /// Called when the riddle ui exited.
        /// </summary>
        public void OnRiddleUIExited()
        {
            // TODO: Animation
            this.uiElements.riddleCanvas.enabled = false;
            this.uiElements.playerUICanvas.enabled = true;
            this.uiElements.riddleScannedCanvas.enabled = true;
        }

        /// <summary>
        /// Switches the riddle tot he next riddle.
        /// </summary>
        /// <param name="nextRiddle">The next riddle.</param>
        public void SwitchToRiddle(bool nextRiddle)
        {
            // TODO: Switch animation.
            if (nextRiddle)
            {
                this._currentViewIndex++;
            }
            else
            {
                this._currentViewIndex--;
                if(this._currentViewIndex < 0)
                {
                    this._currentViewIndex = this.riddleSet.TotalRiddles - 1;
                }
            }
            this._currentViewIndex %= this.riddleSet.TotalRiddles;
            RuntimeRiddle riddle = this.riddleSet.GetRuntimeRiddle(this._currentViewIndex);
            this.UpdateUIElements(riddle);
            this._viewableRiddle = riddle;
        }

        private void UpdateUIElements(RuntimeRiddle riddle)
        {
            if(riddle == null)
            {
                this.uiElements.riddleName.text = "";
                this.uiElements.riddleText.text = "";
                return;
            }
            
            GameRiddle gameRiddle = riddle.GameRiddleData;
            RiddleReferenceData referenceData = gameRiddle.ClueData;

            this.uiElements.riddleName.text = referenceData.ClueTitle;
            this.uiElements.riddleText.text = referenceData.ClueDescription;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModifiedLocation.Scripts.Game
{
    [System.Serializable]
    public struct RiddleScannedAnimations
    {
        [SerializeField]
        public string inAnimation;
    }

    [System.Serializable]
    public struct RiddleScannedReferences
    {
        [SerializeField]
        public ScanResultEvent scanResultEvent;
        [SerializeField]
        public Utils.BoolReference canPlayerScan;
        [SerializeField]
        public Utils.FloatReference delayBetweenScans;
    }

    [System.Serializable]
    public struct RiddleScannedUIVariables
    {
        [SerializeField]
        public Canvas uiCanvas;
        [SerializeField]
        public Text riddleFoundStatus;
        [SerializeField]
        public Text riddleNameText;
        [SerializeField]
        public Text riddleDescriptionText;
    }

    /// <summary>
    /// The riddle scanned ui.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class RiddleScannedUI : Utils.EventsListener
    {
        [SerializeField]
        private RiddleScannedAnimations animations;
        [SerializeField]
        private RiddleScannedReferences references;
        [SerializeField]
        private RiddleScannedUIVariables uiVariables;

        private Animator _animator;

        protected override void OnStart()
        {
            this._animator = this.GetComponent<Animator>();
            this.uiVariables.uiCanvas.enabled = false;
        }

        protected override void HookEvents()
        {
            this.references.scanResultEvent?.AddListener(this.OnScanned);
        }

        protected override void UnHookEvents()
        {
            this.references.scanResultEvent?.RemoveListener(this.OnScanned);
        }

        private void OnScanned(PlayerScanResult scanResult)
        {
            if(scanResult.scanResult == PlayerScanResultType.RESULT_SUCCESS)
            {
                RiddleReferenceData referenceData = scanResult.riddleFound.ClueData;
                this.uiVariables.riddleFoundStatus.text = "Riddle Found:";
                this.uiVariables.riddleNameText.text = referenceData.ClueTitle;
                this.uiVariables.riddleDescriptionText.text = referenceData.ClueDescription;
            }
            else if(scanResult.scanResult == PlayerScanResultType.RESULT_LINE_UP)
            {
                this.uiVariables.riddleFoundStatus.text = "Partial Object Scan";
                this.uiVariables.riddleNameText.text = "";
                this.uiVariables.riddleDescriptionText.text = "Unable to fully scan the object in this environment, line up the object with the camera.";
            }
            else if (scanResult.scanResult == PlayerScanResultType.RESULT_ALREADY_FOUND)
            {
                this.uiVariables.riddleFoundStatus.text = "Riddle Already Found!";
                this.uiVariables.riddleNameText.text = "";
                this.uiVariables.riddleDescriptionText.text = "";
            }
            else if (scanResult.scanResult == PlayerScanResultType.RESULT_FAILED)
            {
                this.uiVariables.riddleFoundStatus.text = "No Objects Found";
                this.uiVariables.riddleDescriptionText.text = "No objects were found in this environment.";
                this.uiVariables.riddleNameText.text = "";
            }
            this._animator.Play(this.animations.inAnimation);
        }

        private void OnScannedComplete()
        {
            StartCoroutine(WaitEnumerator(
                () => this.references.canPlayerScan.Value = true,
                this.references.delayBetweenScans.Value));
        }

        private IEnumerator WaitEnumerator(System.Action action, float time)
        {
            yield return new WaitForSeconds(time);
            action.Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedLocation.Scripts.Game
{
    
    public interface IScannable
    {

        /// <summary>
        /// Called when the interface is scannable by a player.
        /// </summary>
        /// <param name="scanner">The clue scanner.</param>
        /// <param name="raycast">The raycast that the scanner did.</param>
        /// <param name="scanResult">The scan result.</param>
        void OnScanned(RiddleScanner scanner, RaycastHit raycast, ref PlayerScanResult scanResult);
    }

    [System.Serializable]
    public struct RiddleScannerReferences
    {
        [SerializeField]
        public Utils.BoolReference canPlayerScan;
        [SerializeField]
        public Utils.Vector3Reference cameraPosition;
        [SerializeField]
        public Utils.QuaternionReference cameraRotation;
    }

    [System.Serializable]
    public struct RiddleEvents
    {
        [SerializeField]
        public Utils.GameEvent playerScanEvent;
        [SerializeField]
        public ScanResultEvent scanResultEvent;
    }


    /// <summary>
    /// The clue scanner class.
    /// </summary>
    public class RiddleScanner : Utils.EventsListener
    {
        [SerializeField, Range(1.0f, 500.0f)]
        private float maxDistance = 5;
        [SerializeField]
        public RiddleScannerReferences references;
        [SerializeField]
        public RiddleEvents events;

        protected override void OnStart()
        {
            this.references.canPlayerScan.Reset();
        }

        protected override void HookEvents()
        {
            this.events.playerScanEvent?.AddListener(this.TryScanForClues);
        }

        protected override void UnHookEvents()
        {
            this.events.playerScanEvent?.RemoveListener(this.TryScanForClues);
        }
        
        private void Update()
        {
            this.transform.position = this.references.cameraPosition.Value;
            this.transform.rotation = this.references.cameraRotation.Value;
        }

        /// <summary>
        /// Called to try and scan for clues in the areea.
        /// </summary>
        private void TryScanForClues()
        {
            if(!this.references.canPlayerScan.Value)
            {
                return;
            }

            PlayerScanResult scanResult = new PlayerScanResult();
            scanResult.scanResult = PlayerScanResultType.RESULT_FAILED;

            RaycastHit raycast;
            if (Physics.Raycast(
                this.transform.position, this.transform.forward, out raycast))
            {
                IScannable scannable = raycast.rigidbody.GetComponent<IScannable>();
                if (scannable != null)
                {
                    if (raycast.distance > this.maxDistance)
                    {
                        scanResult.scanResult = PlayerScanResultType.RESULT_LINE_UP;
                    }
                    else
                    {
                        scannable.OnScanned(this, raycast, ref scanResult);
                    }
                }
            }

            this.references.canPlayerScan.Value = false;
            this.events.scanResultEvent?.CallEvent(scanResult);
        }
    }
}


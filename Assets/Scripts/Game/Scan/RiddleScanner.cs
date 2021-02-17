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
        /// <param name="scanResult">The scan result.</param>
        void OnScanned(RiddleScanner scanner, ref PlayerScanResult scanResult);
    }


    /// <summary>
    /// The clue scanner class.
    /// </summary>
    public class RiddleScanner : Utils.EventsListener
    {
        [SerializeField, Range(1.0f, 500.0f)]
        private float maxDistance = 5;
        [SerializeField]
        private Utils.Vector3Reference cameraPosition;
        [SerializeField]
        private Utils.QuaternionReference cameraRotation;
        [SerializeField]
        private Utils.GameEvent playerScanEvent;
        [SerializeField]
        private ScanResultEvent resultScanEvent;

        protected override void HookEvents()
        {
            this.playerScanEvent?.AddListener(this.TryScanForClues);
        }

        protected override void UnHookEvents()
        {
            this.playerScanEvent?.RemoveListener(this.TryScanForClues);
        }
        
        private void Update()
        {
            this.transform.position = this.cameraPosition.Value;
            this.transform.rotation = this.cameraRotation.Value;
        }

        /// <summary>
        /// Called to try and scan for clues in the areea.
        /// </summary>
        private void TryScanForClues()
        {
            RaycastHit raycast;
            if (Physics.Raycast(
                this.transform.position, this.transform.forward, out raycast))
            {
                Debug.Log(raycast.distance);

                PlayerScanResult scanResult = new PlayerScanResult();
                scanResult.scanResult = PlayerScanResultType.RESULT_FAILED;

                // TODO: Add a distance checker
                // TODO: Add an event that triggers graphics
                IScannable scannable = raycast.rigidbody.GetComponent<IScannable>();
                if (scannable != null)
                {
                    scannable.OnScanned(this, ref scanResult);
                }
                this.resultScanEvent?.CallEvent(scanResult);
            }
        }
    }
}


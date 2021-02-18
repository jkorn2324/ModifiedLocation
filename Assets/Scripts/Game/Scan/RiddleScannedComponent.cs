using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedLocation.Scripts.Game
{
    /// <summary>
    /// The physical representation of a clue.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class RiddleScannedComponent : MonoBehaviour, IScannable
    {
        private RuntimeRiddle _parentRiddle;

        public RuntimeRiddle ParentRiddle
        {
            get => this._parentRiddle;
            set => this._parentRiddle = value;
        }

        public void OnScanned(RiddleScanner scanner, RaycastHit ray, ref PlayerScanResult scanResult)
        {
            if(this._parentRiddle == null)
            {
                scanResult.scanResult = PlayerScanResultType.RESULT_FAILED;
                return;
            }
            this._parentRiddle.SetRiddleFound(ref scanResult);
        }
    }
}


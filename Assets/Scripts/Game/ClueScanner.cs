using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedLocation.Scripts.Game
{
    /// <summary>
    /// The clue scanner class.
    /// </summary>
    public class ClueScanner : MonoBehaviour
    {
        [SerializeField]
        private Utils.Vector3Reference cameraPosition;
        [SerializeField]
        private Utils.QuaternionReference cameraRotation;

        private void Update()
        {
            this.transform.position = this.cameraPosition.Value;
            this.transform.rotation = this.cameraRotation.Value;
        }

        /// <summary>
        /// Called to try and scan for clues in the areea.
        /// </summary>
        public void TryScanForClues()
        {

        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedLocation.Scripts.Game
{
    /// <summary>
    /// Tracks the camera position, etc...
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class ARCameraTracker : MonoBehaviour
    {
        [SerializeField]
        private Utils.Vector3Reference cameraPosition;
        [SerializeField]
        private Utils.QuaternionReference cameraRotation;

        private void Update()
        {
            this.cameraPosition.Value = this.transform.position;
            this.cameraRotation.Value = this.transform.rotation;
        }
    }
}


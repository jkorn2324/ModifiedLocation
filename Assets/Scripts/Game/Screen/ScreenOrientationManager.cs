using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedLocation.Scripts.Game
{

    /// <summary>
    /// The screen orientation manager.
    /// </summary>
    public class ScreenOrientationManager : MonoBehaviour
    {
        [SerializeField]
        private ScreenOrientationEvent @event;
        private ScreenOrientation _prevOrientation = ScreenOrientation.AutoRotation;

        private static bool _initialized = false;

        private void Start()
        {
            if(_initialized)
            {
                Destroy(this.gameObject);
                return;
            }

            _initialized = true;
            DontDestroyOnLoad(this.gameObject);
        }

        private void LateUpdate()
        {
            if(Screen.orientation != this._prevOrientation)
            {
                this.@event?.CallEvent(Screen.orientation);
            }
            this._prevOrientation = Screen.orientation;
        }
    }
}

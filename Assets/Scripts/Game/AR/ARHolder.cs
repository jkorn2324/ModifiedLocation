using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedLocation.Scripts.Game
{
    /// <summary>
    /// Used to contain all of the AR elements that need
    /// to be used throughout each of the classes.
    /// </summary>
    public class ARHolder : MonoBehaviour
    {
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
    }
}


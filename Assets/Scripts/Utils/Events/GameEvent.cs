using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedLocation.Scripts.Utils
{

    [CreateAssetMenu(fileName = "Game Event", menuName = "Events/Game Event")]
    public class GameEvent : ScriptableObject
    {
        [SerializeField, Inspector.InspectorButton("CallEvent")]
        private bool callEvent;

        private event System.Action _event
            = delegate { };

        public void AddListener(System.Action @function)
        {
            this._event += @function;
        }

        public void RemoveListener(System.Action @function)
        {
            this._event -= @function;
        }

        public void CallEvent()
        {
            this._event.Invoke();
        }
    }
}


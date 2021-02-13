using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedLocation.Scripts.Utils
{

    /// <summary>
    /// The Generic event scriptable object.
    /// </summary>
    /// <typeparam name="T">The parameter of the event.</typeparam>
    public abstract class GenericEvent<T> : ScriptableObject
    {
        protected event System.Action<T> _event
            = delegate { };

        public void AddListener(System.Action<T> @function)
        {
            this._event += @function;
        }

        public void RemoveListener(System.Action<T> @function)
        {
            this._event -= @function;
        }

        public void CallEvent(T parameter)
        {
            this._event.Invoke(parameter);
        }
    }
}


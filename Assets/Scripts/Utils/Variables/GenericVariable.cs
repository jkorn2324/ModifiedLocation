using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedLocation.Scripts.Utils
{

    /// <summary>
    /// The generic variable definition.
    /// </summary>
    /// <typeparam name="T">The generic variable.</typeparam>
    public abstract class GenericVariable<T> : ScriptableObject
    {
        [SerializeField]
        protected T originalValue;
        [SerializeField]
        protected T value;

        public event System.Action<T> ChangedValueEvent
            = delegate { };

        public T Value
        {
            get => this.value;
            set
            {
                if (this.value == null || !this.value.Equals(value))
                {
                    this.ChangedValueEvent(value);
                }
                this.value = value;
            }
        }

        public void Reset()
        {
            this.value = this.originalValue;
        }
    }

    /// <summary>
    /// The Generic reference abstact class.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public abstract class GenericReference<T>
    {
        [SerializeField]
        protected bool isConstant = true;
        [SerializeField]
        protected T constantValue;

        protected abstract T ReferenceValue
        {
            get;
            set;
        }

        public abstract bool HasVariable
        {
            get;
        }

        public T Value
        {
            set
            {
                if (this.isConstant)
                {
                    return;
                }
                this.ReferenceValue = value;
            }
            get => this.isConstant ? this.constantValue : this.ReferenceValue;
        }

        abstract public void Reset();
    }
}

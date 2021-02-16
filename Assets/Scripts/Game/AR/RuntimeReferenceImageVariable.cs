using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace ModifiedLocation.Scripts.Game
{
    /// <summary>
    /// The Runtime reference image variable.
    /// </summary>
    [CreateAssetMenu(fileName = "Runtime Reference Image Library", menuName = "Variables/Runtime Reference Image Library")]
    public class RuntimeReferenceImageVariable : Utils.GenericVariable<MutableRuntimeReferenceImageLibrary> 
    {
        [SerializeField, HideInInspector]
        protected new MutableRuntimeReferenceImageLibrary originalValue;
        [SerializeField, HideInInspector]
        protected new MutableRuntimeReferenceImageLibrary value;
    }

    [System.Serializable]
    public class RuntimeReferenceImageReference : Utils.GenericReference<MutableRuntimeReferenceImageLibrary>
    {
        [SerializeField]
        private RuntimeReferenceImageVariable variable;
        [SerializeField, HideInInspector]
        protected new MutableRuntimeReferenceImageLibrary constantValue;

        public event System.Action<MutableRuntimeReferenceImageLibrary> ChangedValueEvent
        {
            add
            {
                if (this.variable == null) return;
                this.variable.ChangedValueEvent += value;
            }
            remove
            {
                if (this.variable == null) return;
                this.variable.ChangedValueEvent -= value;
            }
        }

        protected override MutableRuntimeReferenceImageLibrary ReferenceValue
        {
            get => this.variable.Value;
            set => this.variable.Value = value;
        }

        public override bool HasVariable
            => this.variable != null;

        public override void Reset()
        {
            this.variable?.Reset();
        }
    }
}



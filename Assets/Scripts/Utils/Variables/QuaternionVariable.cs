using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedLocation.Scripts.Utils
{
    [CreateAssetMenu(fileName = "Quaternion Variable", menuName = "Variables/Quaternion Variable")]
    public class QuaternionVariable : GenericVariable<Quaternion> { }

    [System.Serializable]
    public class QuaternionReference : GenericReference<Quaternion>
    {
        [SerializeField]
        private QuaternionVariable variable;

        public event System.Action<Quaternion> ChangedValueEvent
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

        protected override Quaternion ReferenceValue
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


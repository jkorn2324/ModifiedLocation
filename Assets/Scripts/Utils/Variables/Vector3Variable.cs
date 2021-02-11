using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedLocation.Scripts.Utils
{
    [CreateAssetMenu(fileName = "Vector3 Variable", menuName = "Variables/Vector3 Variable")]
    public class Vector3Variable : GenericVariable<Vector3> { }

    [System.Serializable]
    public class Vector3Reference : GenericReference<Vector3>
    {
        [SerializeField]
        private Vector3Variable variable;

        public event System.Action<Vector3> ChangedValueEvent
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

        protected override Vector3 ReferenceValue
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



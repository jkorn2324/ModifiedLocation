using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedLocation.Scripts.Utils
{

    [CreateAssetMenu(fileName = "Integer Variable", menuName = "Variables/Integer Variable")]
    public class IntegerVariable : GenericVariable<int> { }

    [System.Serializable]
    public class IntegerReference : GenericReference<int>
    {
        [SerializeField]
        private IntegerVariable variable;

        public event System.Action<int> ChangedValueEvent
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

        protected override int ReferenceValue
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
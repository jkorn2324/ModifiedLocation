using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ModifiedLocation.Scripts.Utils
{

    [CreateAssetMenu(fileName = "Bool Variable", menuName = "Variables/Bool Variable")]
    public class BoolVariable : GenericVariable<bool> { }

    [System.Serializable]
    public class BoolReference : GenericReference<bool>
    {
        [SerializeField]
        private BoolVariable variable;

        public event System.Action<bool> ChangedValueEvent
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

        protected override bool ReferenceValue
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


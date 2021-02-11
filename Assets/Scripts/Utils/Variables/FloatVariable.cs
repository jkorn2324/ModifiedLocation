using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedLocation.Scripts.Utils
{ 

    [CreateAssetMenu(fileName = "Float Variable", menuName = "Variables/Float Variable")]
    public class FloatVariable : GenericVariable<float> { }

    [System.Serializable]
    public class FloatReference : GenericReference<float>
    {
        [SerializeField]
        private FloatVariable variable;

        public event System.Action<float> ChangedValueEvent
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

        protected override float ReferenceValue
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


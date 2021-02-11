using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedLocation.Scripts.Utils
{

    [CreateAssetMenu(fileName = "String Variable", menuName = "Variables/String Variable")]
    public class StringVariable : GenericVariable<string> { }

    [System.Serializable]
    public class StringReference : GenericReference<string>
    {
        [SerializeField]
        private StringVariable variable;

        public event System.Action<string> ChangedValueEvent
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

        protected override string ReferenceValue
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

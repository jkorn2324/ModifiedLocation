using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedLocation.Scripts.Game
{

    [CreateAssetMenu(fileName = "Game Riddle Set Variable", menuName = "Riddle/Game Riddle Set Variable")]
    public class GameRiddleSetVariable : Utils.GenericVariable<GameRiddleSet> { }

    [System.Serializable]
    public class GameRiddleSetReference : Utils.GenericReference<GameRiddleSetVariable>
    {
        [SerializeField]
        private GameRiddleSetVariable variable; 

        public override bool HasVariable
            => variable != null;

        protected override GameRiddleSetVariable ReferenceValue 
        {
            get => this.variable;
            set => this.variable = value;
        }

        public override void Reset()
        {
            this.variable?.Reset();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedLocation.Scripts.Game
{

    [CreateAssetMenu(fileName = "Game Riddle Set Variable", menuName = "Riddle/Game Riddle Set Variable")]
    public class GameRiddleSetVariable : Utils.GenericVariable<GameRiddleSet> { }

    [System.Serializable]
    public class GameRiddleSetReference
    {
        [SerializeField]
        private GameRiddleSetVariable variable; 

        public bool HasRiddleSet
            => this.variable != null;

        public GameRiddleSet RiddleSet
        {
            get => this.variable.Value;
            set => this.variable.Value = value;
        }

        public void AddRiddle(GameRiddle riddle)
        {
            if(this.variable != null && this.variable.Value != null)
            {
                this.variable.Value.AddRiddle(riddle);
            }
        }
    }
}
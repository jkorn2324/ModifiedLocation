using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedLocation.Scripts.Game
{
    [CreateAssetMenu(fileName = "Treasure Hunt Set", menuName = "Treasure Hunt Set")]
    public class TreasureHuntSet : ScriptableObject
    {
        [SerializeField]
        private GameClueSetReference activeClueSet;
        [SerializeField]
        private List<GameClueSet> treasureHuntClues;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedLocation.Scripts.Game
{
    /// <summary>
    /// The physical representation of a clue.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class ClueScannedCompnent : MonoBehaviour, IScannable
    {
        private RuntimeClue _parentClue;

        public RuntimeClue ParentClue
        {
            get => this._parentClue;
            set => this._parentClue = value;
        }

        public void OnScanned(ClueScanner scanner)
        {
            // TODO: Implementation
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModifiedLocation.Scripts.Game
{
    [System.Serializable]
    public struct PlayerUIReferences
    {
        [SerializeField]
        public ScanResultEvent scanResultEvent;
        [SerializeField]
        public GameRiddleSet riddleSet;
    }

    [System.Serializable]
    public struct PlayerUIObjects
    {
        [SerializeField]
        public Text numRiddlesText;
    }

    /// <summary>
    /// The player ui.
    /// </summary>
    public class PlayerUI : Utils.EventsListener
    {
        [SerializeField]
        private PlayerUIReferences references;
        [SerializeField]
        private PlayerUIObjects uiObjects;

        protected override void OnStart()
        {
            this.UpdateNumberOfRiddles();
        }

        protected override void HookEvents()
        {
            this.references.scanResultEvent?.AddListener(this.OnPlayerScanned);
        }

        protected override void UnHookEvents()
        {
            this.references.scanResultEvent?.RemoveListener(this.OnPlayerScanned);
        }

        private void OnPlayerScanned(PlayerScanResult scanResult)
        {
            this.UpdateNumberOfRiddles();
        }

        private void UpdateNumberOfRiddles()
        {
            Debug.Log("Update the number of riddles");
            int numRiddles = this.references.riddleSet.GetRiddlesLeft;
            this.uiObjects.numRiddlesText.text = numRiddles + "/" + this.references.riddleSet.GetNumberOfRiddles;
        }
    }
}

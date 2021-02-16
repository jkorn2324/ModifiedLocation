using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ModifiedLocation.Scripts.Game
{
    [RequireComponent(typeof(ARSession))]
    public class ARSupportedChecker : Utils.EventsListener
    {
        [SerializeField]
        private Utils.BoolReference arSupported;
        [SerializeField]
        private Utils.GameEvent createRuntimeLibrary;

        private ARSession _session;

        private void Awake()
        {
            this._session = this.GetComponent<ARSession>();
        }

        protected override void OnStart()
        {
            this.arSupported.Reset();
        }

        protected override void HookEvents()
        {
            ARSession.stateChanged += this.OnSessionStateChanged;
        }

        protected override void UnHookEvents()
        {
            ARSession.stateChanged -= this.OnSessionStateChanged;
        }

        private void OnSessionStateChanged(ARSessionStateChangedEventArgs changedEvent)
        {
            if(changedEvent.state == ARSessionState.Unsupported)
            {
                this.arSupported.Value = false;
                return;
            }

            if(this.arSupported.Value && changedEvent.state == ARSessionState.SessionInitializing)
            {
                this.createRuntimeLibrary?.CallEvent();
                return;
            }

            if(!this.arSupported.Value)
            {
                this.arSupported.Value = true;
            }
        }
    }
}
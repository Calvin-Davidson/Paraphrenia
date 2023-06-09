using Runtime.Interaction;
using Runtime.Networking.NetworkEvent;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Puzzels
{
    public class ElectricRoomPuzzle : Puzzle
    {
        [SerializeField, Tooltip("Should be filled in the order in which the should be fixed")]
        private NetworkedInteractable[] interactableCables;

        private int _repairedCables;
        private bool _isValid;

        public NetworkEvent onBecameValid = new(NetworkEventPermission.Everyone);
        public NetworkEvent onBecameInvalid = new(NetworkEventPermission.Everyone);
        public NetworkEvent onInvalidCableRepaired = new(NetworkEventPermission.Everyone);

        private bool IsValid
        {
            get => _isValid;
            set
            {
                if (value) onBecameValid?.Invoke();
                if (!value) onBecameInvalid?.Invoke();
                _isValid = value;
            }
        }

        public override void OnNetworkSpawn()
        {
            onBecameValid.Initialize(this);
            onBecameInvalid.Initialize(this);
            onInvalidCableRepaired.Initialize(this);

            foreach (var networkedInteractable in interactableCables)
            {
                networkedInteractable.onInteract.AddListener(() => { HandleCableInteract(networkedInteractable); });
            }

            base.OnNetworkSpawn();
        }

        public override void OnNetworkDespawn()
        {
            onBecameValid.Dispose();
            onBecameInvalid.Dispose();
            onInvalidCableRepaired.Dispose();
        }
        

        private void HandleCableInteract(NetworkedInteractable interactable)
        {
            if (interactableCables[_repairedCables] != interactable)
            {
                DoFail();
                onInvalidCableRepaired?.Invoke();
                return;
            }

            interactable.IsActive = false;
            _repairedCables += 1;
            
            if (_repairedCables != interactableCables.Length) return;
            
            onCompleteNetworked?.Invoke();
            IsValid = true;
        }

        /// <summary>
        /// Can be invoked when part of the puzzle fails will reset the interactable cables and set repaired cables to 0
        /// </summary>
        private void DoFail()
        {
            _repairedCables = 0;
            foreach (var networkedInteractable in interactableCables)
            {
                networkedInteractable.IsActive = true;
            }
        }
    }
}
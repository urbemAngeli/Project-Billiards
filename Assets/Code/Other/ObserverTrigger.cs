using System;
using UnityEngine;

namespace Code.Other
{
    [RequireComponent(typeof(Collider))]
    public class ObserverTrigger : MonoBehaviour
    {
        public event Action<Collider> onEnter;
        public event Action<Collider> onExit;

        private void OnTriggerEnter(Collider other) => 
            onEnter?.Invoke(other);

        private void OnTriggerExit(Collider other) => 
            onExit?.Invoke(other);
    }
}
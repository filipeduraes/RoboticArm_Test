using System;
using UnityEngine;
using UnityEngine.Events;

namespace Miscellaneous
{
    public class DoActionsOnTrigger : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private UnityEvent onTriggerEnter;
        [SerializeField] private UnityEvent onTriggerExit;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareLayers(layerMask))
                return;
            
            onTriggerEnter.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareLayers(layerMask))
                return;
            
            onTriggerExit.Invoke();
        }
    }
}
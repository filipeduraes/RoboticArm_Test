using System;
using UnityEngine;

namespace RoboticArm
{
    [RequireComponent(typeof(Rigidbody))]
    public class Item : MonoBehaviour
    {
        public Rigidbody ItemRigidbody { get; private set; }
        public Transform InitialParent { get; private set; }

        private void Start()
        {
            ItemRigidbody = GetComponentInChildren<Rigidbody>();
            InitialParent = transform.parent;
        }
    }
}
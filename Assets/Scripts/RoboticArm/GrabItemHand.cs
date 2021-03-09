using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RoboticArm
{
    public class GrabItemHand : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private LayerMask itemsLayer;
        [SerializeField] private Transform itemChecker;
        [SerializeField] private float checkerRadius = 0.5f;
        
        [Header("Events")]
        [SerializeField] private UnityEvent onItemGrabbed;
        [SerializeField] private UnityEvent onItemReleased;
        
        private readonly List<Item> selectedItems = new List<Item>();
        private bool itemIsBeingHeld;

        public void CheckAndGrabItem()
        {
            Collider item = CheckForItem();
            if (item != null)
                GrabItem(item);
        }

        private void GrabItem(Collider itemCollider)
        {
            if (itemIsBeingHeld)
                return;

            Item item = itemCollider.GetComponent<Item>();
            if (item == null) return;

            selectedItems.Add(item);
            item.transform.parent = transform;
            item.ItemRigidbody.isKinematic = true;
            
            onItemGrabbed.Invoke();
            itemIsBeingHeld = true;
        }

        private Collider CheckForItem()
        {
            Collider[] items = new Collider[1];
            Physics.OverlapSphereNonAlloc(itemChecker.position, checkerRadius, items, itemsLayer);
            return items[0];
        }

        public void ReleaseItem()
        {
            if (!itemIsBeingHeld)
                return;
            
            for (int index = 0; index < selectedItems.Count; index++)
            {
                Item item = selectedItems[index];
                item.transform.parent = item.InitialParent;
                item.ItemRigidbody.isKinematic = false;
                selectedItems.Remove(item);
            }
            
            onItemReleased.Invoke();
            itemIsBeingHeld = false;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(itemChecker.position, checkerRadius);
        }
#endif
    }
}

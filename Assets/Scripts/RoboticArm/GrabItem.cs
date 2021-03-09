using System.Collections.Generic;
using UnityEngine;

namespace RoboticArm
{
    public class GrabItem : MonoBehaviour
    {
        [SerializeField] private LayerMask itemsLayer;
        [SerializeField] private Transform itemChecker;
        [SerializeField] private float checkerRadius = 0.5f;
        
        private readonly List<Item> selectedItems = new List<Item>();
        
        public void GrabSelectedItem()
        {
            Collider[] items = Physics.OverlapSphere(itemChecker.position, checkerRadius, itemsLayer);

            foreach (Collider itemCollider in items)
            {
                Item item = itemCollider.GetComponent<Item>();
                if (item == null) return;

                selectedItems.Add(item);
                item.transform.parent = transform;
                item.ItemRigidbody.isKinematic = true;
            }
        }
        
        public void ReleaseSelectedItem()
        {
            for (int index = 0; index < selectedItems.Count; index++)
            {
                Item item = selectedItems[index];
                item.transform.parent = item.InitialParent;
                item.ItemRigidbody.isKinematic = false;
                selectedItems.Remove(item);
            }
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

using UnityEngine;

namespace RoboticArm.Automation
{
    public class GrabItemAutomatically : MonoBehaviour
    {
        [SerializeField] private GrabItemHand grabItemHand;
        public bool AutoIsEnabled { private get; set; }

        private void FixedUpdate()
        {
            if (!AutoIsEnabled)
                return;
            
            grabItemHand.CheckAndGrabItem();
        }
    }
}
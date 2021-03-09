using UnityEngine;

namespace RoboticArm
{
    public class ControlRotation : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private Axis axisToRotate;
        [SerializeField] private float speed = 10f;
        
        [Header("Clamping")]
        [SerializeField] private float clampedAngleMin;
        [SerializeField] private float clampedAngleMax;
        [SerializeField] private bool enableClamp;

        private float movedAngle;
        private Vector3 axis;
        
        public void RotateClockwise() => RotateAroundAxis(-speed);
        public void RotateCounterClockwise() => RotateAroundAxis(speed);

        private void Start() => axis = ReturnAxis();

        private void RotateAroundAxis(float angle)
        {
            if (enableClamp)
            {
                float nextAngle = movedAngle + angle;
                if (!(clampedAngleMin < nextAngle) || !(nextAngle < clampedAngleMax))
                    return;
                
                movedAngle += angle;

                transform.Rotate(axis);
                return;
            }
            
            transform.Rotate(axis, angle);
        }

        private Vector3 ReturnAxis()
        {
            switch (axisToRotate)
            {
                case Axis.X: return transform.right.normalized;
                case Axis.Y: return transform.up.normalized;
                case Axis.Z: return transform.forward.normalized;
                default: return Vector3.zero;
            }
        }
    }
}
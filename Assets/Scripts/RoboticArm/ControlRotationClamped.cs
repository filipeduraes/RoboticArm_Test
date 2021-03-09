using UnityEngine;

namespace RoboticArm
{
    public class ControlRotationClamped : ControlRotation
    {
        [SerializeField] private float clampedAngleMin;
        [SerializeField] private float clampedAngleMax;
        
        private float movedAngle;

        public override void RotateAroundAxis(float angle)
        {
            float nextAngle = movedAngle + angle;
            float angleToMove = ClampAngle(angle, nextAngle);

            movedAngle += angleToMove;
            base.RotateAroundAxis(angleToMove);
        }
        
        private float ClampAngle(float angle, float nextAngle)
        {
            float angleToMove;

            if (nextAngle < clampedAngleMin)
                angleToMove = movedAngle - clampedAngleMin;
            else if (nextAngle > clampedAngleMax)
                angleToMove = clampedAngleMax - movedAngle;
            else
                angleToMove = angle;
            
            return angleToMove;
        }
    }
}
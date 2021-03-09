using System.Collections;
using UnityEngine;

namespace RoboticArm
{
    public class ControlRotation : MonoBehaviour
    {
        public bool IsMoving { get; private set; }
        public bool HasMoved { get; private set; }

        [SerializeField] private Axis axisToRotate;
        
        private Vector3 axis;
        private Quaternion startRotation;

        private void Awake()
        {
            axis = GetAxis();
            startRotation = transform.localRotation;
        }

        public virtual void RotateAroundAxis(float angle) => RotateAroundAxis(angle, 0.2f);
        
        public virtual void RotateAroundAxis(float angle, float speed)
        {
            Vector3 newRotation = transform.localRotation.eulerAngles + axis * angle;
            StartCoroutine(LerpRotation(newRotation, speed));
            HasMoved = true;
        }

        public void ResetRotation()
        {
            StartCoroutine(LerpRotation(startRotation.eulerAngles, 1f));
            HasMoved = false;
        }
        
        private IEnumerator LerpRotation(Vector3 newRotation, float timeInSeconds)
        {
            float timer = 0f;
            Vector3 initialRotation = transform.localRotation.eulerAngles;
            IsMoving = true;
            
            while (timer <= timeInSeconds)
            {
                timer += Time.deltaTime;
                float t = timer / timeInSeconds;
                Vector3 lerpRotation = Vector3.Lerp(initialRotation, newRotation, t);
                transform.localRotation = Quaternion.Euler(lerpRotation);
                yield return null;
            }

            IsMoving = false;
        }

        private Vector3 GetAxis()
        {
            switch (axisToRotate)
            {
                case Axis.X: return Vector3.right;
                case Axis.Y: return Vector3.up;
                case Axis.Z: return Vector3.forward;
                default: return Vector3.zero;
            }
        }
    }
}
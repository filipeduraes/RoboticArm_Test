using System.Collections;
using UnityEngine;

namespace Miscellaneous
{
    public class LerpPosition : MonoBehaviour
    {
        [SerializeField] private Vector3 endPosition;
        [SerializeField] private float time = 0.2f;

        [Header("Axis Affected")]
        [SerializeField] private bool x;
        [SerializeField] private bool y;
        [SerializeField] private bool z;
        
        private Vector3 startPosition;
        private bool isMoving;
        
        private void Awake()
        {
            startPosition = transform.position;

            if (!x)
                endPosition.x = startPosition.x;
            if (!y)
                endPosition.y = startPosition.y;
            if (!z)
                endPosition.z = startPosition.z;
        }

        public void LerpToEnd()
        {
            if(isMoving)
                return;
            
            StopAllCoroutines();
            StartCoroutine(LerpTransformToPosition(transform.position, endPosition, time));
        }
        
        public void LerpToStart()
        {
            if(isMoving)
                return;
            
            StopAllCoroutines();
            StartCoroutine(LerpTransformToPosition(transform.position, startPosition, time));
        }
        
        private IEnumerator LerpTransformToPosition(Vector3 initialPosition, Vector3 finalPosition, float timeInSeconds)
        {
            float timer = 0f;
            isMoving = true;
            
            while (timer < timeInSeconds)
            {
                timer += Time.deltaTime;
                float t = timer / timeInSeconds;

                transform.position = Vector3.Lerp(initialPosition, finalPosition, t);
                yield return null;
            }
            
            isMoving = false;
        }
    }
}
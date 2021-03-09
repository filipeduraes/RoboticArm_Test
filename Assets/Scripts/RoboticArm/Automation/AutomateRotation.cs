using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace RoboticArm.Automation
{
    public class AutomateRotation : MonoBehaviour
    {
        [SerializeField] private float speed = 0.8f;
        
        [Header("Dictionary")]
        [SerializeField] private List<ControlRotation> roboticParts;
        [SerializeField] private List<float> angles;

        [Header("Events")] 
        [SerializeField] private UnityEvent onSequenceStarted;
        [SerializeField] private UnityEvent onSequenceEnded;

        private List<ControlRotation> allControllers = new List<ControlRotation>();
        private int maxIndex;
        private bool sequenceIsBeingExecuted;

        private void Awake()
        {
            maxIndex = roboticParts.Count > angles.Count ? angles.Count : roboticParts.Count;
            allControllers = FindObjectsOfType<ControlRotation>().ToList();
        }

        public void StartSequence()
        {
            if(sequenceIsBeingExecuted)
                return;
            
            onSequenceStarted.Invoke();
            StartCoroutine(MoveSequence());
        }

        private IEnumerator MoveSequence()
        {            
            sequenceIsBeingExecuted = true;
            yield return ResetAllRotations();
            
            for (int index = 0; index < maxIndex; index++)
            {
                ControlRotation controlRotation = roboticParts[index];
                if (controlRotation == null) continue;
                
                controlRotation.RotateAroundAxis(angles[index], speed);
                yield return new WaitUntil(() => !controlRotation.IsMoving);
            }
            
            onSequenceEnded.Invoke();
            yield return ResetAllRotations();

            sequenceIsBeingExecuted = false;
        }
        
        private IEnumerator ResetAllRotations()
        {
            foreach (ControlRotation controlRotation in allControllers)
            {
                if (!controlRotation.HasMoved) continue;
                controlRotation.ResetRotation();
                ControlRotation rotation = controlRotation;
                yield return new WaitUntil(() => !controlRotation.HasMoved);
            }
        }
    }
}
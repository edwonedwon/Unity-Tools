using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using Edwon.Tools;

namespace Edwon.Tools
{
    // if this class is paired with a LeanSelectable it will be dragged when selected
    // otherwise the OnDrag events must be called manually
    public class DraggableRigidbody : MonoBehaviour, IDraggable
    {
        public enum MoveType{Kinematic, MovePositionLerp}
        public MoveType moveType;
        [SerializeField]
        [ReadOnly]
        bool isDragged;
        public bool IsDragged {get{ return isDragged;}}
        new Camera camera;
        Transform throwVectorTF;
        public Rigidbody mainRigidbody;
        public float distanceFromCamera = 1f;
        Vector3 velocity = Vector3.zero;
        Vector2 screenVelocity;
        Vector2 screenVelocitySmoothed;
        Vector2 screenPosLast;
        LeanSelectable selectable;

        [Header("Throw")]
        public float throwForce = 0.1f;
        public float throwForceMin = 3f;
        public float throwForceClamp = 30;
        public float screenVelocityLerp = 0.9f;
        public bool randomRotation = false;

        [Header("MovePositionLerp")]
        public float moveTime;  
        public float moveMaxSpeed;
        public float rotateTime;

        [Header("Debug")]
        public bool debugLog;
        public bool debugDraw;

        void Awake()
        {
            GetPlayerComponents();
            selectable = GetComponent<LeanSelectable>();
            throwVectorTF = camera.transform.Find("Throw Vector");
            if (mainRigidbody == null)
                mainRigidbody = GetComponent<Rigidbody>();
        }

        void GetPlayerComponents()
        {
            camera = Camera.main;
        }

        public void OnDragBegin(LeanFinger finger)
        {
            OnDragBegin(finger.ScreenPosition);
        }

        public void OnDragBegin(Vector2 screenPos)
        {   
            if (debugLog)
                Debug.Log("OnDragBegin: " + screenPos);

            if (mainRigidbody == null)
                return;

            isDragged = true;

            if (moveType == MoveType.Kinematic)
                mainRigidbody.isKinematic = true;

            screenPosLast = Vector2.zero;
            
            OnDragUpdate(screenPos);
        }

        public void OnDragUpdate(LeanFinger finger)
        {
            OnDragUpdate(finger.ScreenPosition);
        }

        public void OnDragUpdate(Vector2 screenPos)
        {
            if (debugLog)
                Debug.Log("OnDragUpdate: " + screenPos);

            if (mainRigidbody == null)
                return;
            
            Quaternion targetRotation = Quaternion.identity;
            Vector3 targetPosition = GetTargetPosition(screenPos);

            if (randomRotation)
                targetRotation = Random.rotation;
            else
                targetRotation = camera.transform.rotation;

            switch(moveType)
            {
                case MoveType.Kinematic:
                {
                    mainRigidbody.MovePosition(targetPosition);
                }
                break;
                case MoveType.MovePositionLerp:
                {
                    Vector3 targetPositionSmooth = Vector3.SmoothDamp(mainRigidbody.transform.position, targetPosition, ref velocity, moveTime, moveMaxSpeed);
                    Quaternion targetRotationSmooth = Quaternion.RotateTowards(mainRigidbody.transform.rotation, targetRotation, rotateTime);
                    mainRigidbody.MovePosition(targetPositionSmooth);
                    mainRigidbody.MoveRotation(targetRotationSmooth);
                }
                break;
            }

            screenVelocity = screenPos - screenPosLast;
            screenVelocitySmoothed = Vector3.Lerp(
                screenVelocitySmoothed, 
                new Vector3(screenVelocity.x, screenVelocity.y, screenVelocity.magnitude),
                screenVelocityLerp);
            screenPosLast = screenPos;
        }

        public void OnDragEnd(LeanFinger finger)
        {
            OnDragEnd(finger.ScreenPosition);
        }

        public void OnDragEnd(Vector2 screenPos)
        {
            if (debugLog)
                Debug.Log("OnDragEnd: " + screenPos);

            if (mainRigidbody == null)
                return;

            isDragged = false;
                
            if (moveType == MoveType.Kinematic)
                mainRigidbody.isKinematic = false;

            Vector3 throwForceVector = new Vector3(
                Mathf.Clamp(screenVelocitySmoothed.x, -throwForceClamp, throwForceClamp), 
                0, 
                screenVelocitySmoothed.y);
            throwForceVector = throwVectorTF.TransformDirection(throwForceVector);
            throwForceVector *= throwForce;
            throwForceVector = Vector3.ClampMagnitude(throwForceVector, throwForceClamp);
            if (debugDraw)
                Debug.DrawRay(camera.transform.position, throwForceVector, Color.red, 1f);
            mainRigidbody.AddForce(throwForceVector);
        }

		void OnSelect(LeanFinger finger)
		{
            OnDragBegin(finger);
		}

		void OnSelectUpdate(LeanFinger finger)
		{
            OnDragUpdate(finger);
		}

		void OnSelectUp(LeanFinger finger)
		{
            OnDragEnd(finger);
		}

        void OnDeselect()
		{

		}

        public Vector3 GetTargetPosition(Vector2 screenPos)
        {
            if (camera == null)
                GetPlayerComponents();

            return camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, distanceFromCamera));
        }

        void OnEnable()
        {
            if (selectable != null)
            {
                selectable.OnSelect.AddListener(OnSelect);
                selectable.OnSelectUpdate.AddListener(OnSelectUpdate);
                selectable.OnSelectUp.AddListener(OnSelectUp);
                selectable.OnDeselect.AddListener(OnDeselect);
            }
        }

        void OnDisable()
        {
            if (selectable != null)
            {
                selectable.OnSelect.RemoveListener(OnSelect);
                selectable.OnSelectUp.RemoveListener(OnSelectUp);
                selectable.OnSelectUpdate.RemoveListener(OnSelectUpdate);
                selectable.OnDeselect.RemoveListener(OnDeselect);
            }
        }
    }
}
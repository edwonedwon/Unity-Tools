using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Edwon
{
    [System.Serializable]
	public struct TransformLocalSettings
	{
		public Vector3 localPosition;
		public Quaternion localRotation;
		public Vector3 localScale;
	}

	[System.Serializable]
	public struct SpringSettings
	{
		public Vector3 anchor;
		public bool autoAnchor;
		public Vector3 connectedAnchor;
		public float power;
		public float damper;
		public float tolerance;
	}

	[System.Serializable]
	public struct RigidBodySettings
	{
		public float mass;
		public float drag;
		public float angularDrag;
		public bool gravity;
		public bool kinematic;
		public float sleepThreshold;
		public RigidbodyInterpolation interpolation;
		public CollisionDetectionMode collisionDetection;
		public RigidbodyConstraints constraints;
	}
}
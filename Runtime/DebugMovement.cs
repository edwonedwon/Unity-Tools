using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMovement : MonoBehaviour
{
    [Header("Position")]
    public bool position = true;
    public float timeScalePosition = 1;
    public float scalePosition = 1;
    public AnimationCurve xPosCurve;
    public AnimationCurve yPosCurve;
    public AnimationCurve zPosCurve;
    [Header("Rotation")]
    public bool rotation = true;
    public float timeScaleRotation = 1;
    public float scaleRotation = 1;
    public AnimationCurve xRotCurve;
    public AnimationCurve yRotCurve;
    public AnimationCurve zRotCurve;

    Vector3 startPosition;
    Vector3 startRotation;

    public float randomTimeScaleRange = 1f;
    Vector3 randomTimeScalePosition;
    Vector3 randomTimeScaleRotation;

    void Awake()
    {
        startPosition = transform.localPosition;
        startRotation = transform.localRotation.eulerAngles;
        RandomizeTimeOffsets();
    }

    [InspectorButton("RandomizeTimeOffsets")]
    public bool randomizeTimeOffsets;
    public void RandomizeTimeOffsets()
    {
        randomTimeScalePosition = RandomVector3(randomTimeScaleRange);
        randomTimeScaleRotation = RandomVector3(randomTimeScaleRange);
    }

    Vector3 RandomVector3(float range)
    {
        return new Vector3(
            Random.Range(-range, range),
            Random.Range(-range, range),
            Random.Range(-range, range)
        );
    }

    void Update()
    {
        if (position)
        {
            float x = startPosition.x + (xPosCurve.Evaluate((Time.time * randomTimeScalePosition.x) * timeScalePosition) * scalePosition);
            float y = startPosition.y + (yPosCurve.Evaluate((Time.time * randomTimeScalePosition.y) * timeScalePosition) * scalePosition);
            float z = startPosition.z + (zPosCurve.Evaluate((Time.time * randomTimeScalePosition.z) * timeScalePosition) * scalePosition);
            transform.localPosition = new Vector3(x,y,z);
        }
        if (rotation)
        {
            float x = startRotation.x + (xRotCurve.Evaluate((Time.time * randomTimeScaleRotation.x) * timeScaleRotation) * scaleRotation);
            float y = startRotation.y + (yRotCurve.Evaluate((Time.time * randomTimeScaleRotation.y) * timeScaleRotation) * scaleRotation);
            float z = startRotation.z + (zRotCurve.Evaluate((Time.time * randomTimeScaleRotation.z) * timeScaleRotation) * scaleRotation);
            transform.localRotation = Quaternion.Euler(x,y,z);
        }
    }
}

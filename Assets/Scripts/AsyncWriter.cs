using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsyncWriter : MonoBehaviour
{
    public float recordingInterval;
    public AsyncRecording recording;

    public bool isRecording;
    private float recordingTimer;
    private Vector3 lastPos;
    private Quaternion lastRot;

    private void LateUpdate()
    {
        if (isRecording)
        {
            if (recordingTimer >= recordingInterval)
            {
                recording.deltaTimes.Add(recordingTimer);
                if (recording.deltaTimes.Count == 0)
                {
                    
                    recording.positionDeltas.Add(transform.position - recording.startPosition);
                    recording.rotationDeltas.Add(transform.rotation * Quaternion.Inverse(recording.startRotation));
                }
                else
                {
                    recording.positionDeltas.Add(transform.position - lastPos);
                    recording.rotationDeltas.Add(transform.rotation * Quaternion.Inverse(lastRot));
                }

                lastPos = transform.position;
                lastRot = transform.rotation;
                recordingTimer = 0f;
            }

            recordingTimer += Time.deltaTime;
        }
    }
    
    public AsyncRecording StopAndGetRecording()
    {
        recording.deltaTimes.Add(recordingTimer);
        recording.positionDeltas.Add(transform.position - lastPos);
        recording.rotationDeltas.Add(transform.rotation * Quaternion.Inverse(lastRot));
        isRecording = false;
        return recording;
    }

    [ContextMenu("Start Recording")]
    private void StartRecording()
    {
        recording = new AsyncRecording();
        recording.startPosition = transform.position;
        recording.startRotation = transform.rotation;
        recording.positionDeltas = new List<Vector3>();
        recording.rotationDeltas = new List<Quaternion>();
        recording.deltaTimes = new List<float>();

        isRecording = true;
    }
}

[Serializable]
public struct AsyncRecording
{
    public Vector3 startPosition;
    public Quaternion startRotation;

    public List<Vector3> positionDeltas;
    public List<Quaternion> rotationDeltas;

    public List<float> deltaTimes;
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AsyncRecorder : MonoBehaviour
{
    public float recordingInterval;
    [HideInInspector] public AsyncRecording recording;

    private bool _isRecording;
    private float _recordingTimer;

    private void LateUpdate()
    {
        if (_isRecording)
        {
            if (_recordingTimer >= recordingInterval)
            {
                recording.timeStamps.Add(_recordingTimer);
                recording.positionDeltas.Add(transform.position);
                recording.rotationDeltas.Add(transform.rotation);
            }

            _recordingTimer += Time.deltaTime;
        }
    }

    public void StopAndSaveRecording()
    {
        recording.timeStamps.Add(_recordingTimer);
        recording.positionDeltas.Add(transform.position);
        recording.rotationDeltas.Add(transform.rotation);
        _isRecording = false;

        JsonUtility.ToJson(recording).SaveInPLayerPrefs();
    }

    [ContextMenu("Start Recording")]
    private void StartRecording()
    {
        recording = new AsyncRecording();
        recording.positionDeltas = new List<Vector3>();
        recording.rotationDeltas = new List<Quaternion>();
        recording.timeStamps = new List<float>();

        _isRecording = true;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 40, 150, 30), "Start Record"))
        {
            StartRecording();
        }

        if (GUI.Button(new Rect(20, 80, 150, 30), "Save Recording"))
        {
            StopAndSaveRecording();
        }
    }
}

[Serializable]
public struct AsyncRecording
{
    public List<Vector3> positionDeltas;
    public List<Quaternion> rotationDeltas;

    public List<float> timeStamps;
}
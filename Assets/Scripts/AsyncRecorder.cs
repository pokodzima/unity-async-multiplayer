using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AsyncRecorder : MonoBehaviour
{
    [SerializeField] private float recordingInterval;
    private AsyncRecording _recording;

    private bool _isRecording;
    private float _recordingTimer;

    private void LateUpdate()
    {
        if (_isRecording)
        {
            if (_recordingTimer >= recordingInterval)
            {
                _recording.timeStamps.Add(_recordingTimer);
                _recording.positionDeltas.Add(transform.position);
                _recording.rotationDeltas.Add(transform.rotation);
            }

            _recordingTimer += Time.deltaTime;
        }
    }

    public void StopAndSaveRecording()
    {
        _recording.timeStamps.Add(_recordingTimer);
        _recording.positionDeltas.Add(transform.position);
        _recording.rotationDeltas.Add(transform.rotation);
        _isRecording = false;

        JsonUtility.ToJson(_recording).SaveInPLayerPrefs();
    }

    [ContextMenu("Start Recording")]
    private void StartRecording()
    {
        _recording = new AsyncRecording();
        _recording.positionDeltas = new List<Vector3>();
        _recording.rotationDeltas = new List<Quaternion>();
        _recording.timeStamps = new List<float>();

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
public struct AsyncRecording : IEquatable<AsyncRecording>
{
    public List<Vector3> positionDeltas;
    
    public List<Quaternion> rotationDeltas;

    public List<float> timeStamps;

    public bool Equals(AsyncRecording other)
    {
        return Equals(positionDeltas, other.positionDeltas) && Equals(rotationDeltas, other.rotationDeltas) &&
               Equals(timeStamps, other.timeStamps);
    }
}
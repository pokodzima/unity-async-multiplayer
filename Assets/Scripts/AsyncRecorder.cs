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
                _recording.PositionDeltas.Add(transform.position);
                _recording.RotationDeltas.Add(transform.rotation);
            }

            _recordingTimer += Time.deltaTime;
        }
    }

    public void StopAndSaveRecording()
    {
        _recording.timeStamps.Add(_recordingTimer);
        _recording.PositionDeltas.Add(transform.position);
        _recording.RotationDeltas.Add(transform.rotation);
        _isRecording = false;

        JsonUtility.ToJson(_recording).SaveInPLayerPrefs();
    }

    [ContextMenu("Start Recording")]
    private void StartRecording()
    {
        _recording = new AsyncRecording();
        _recording.PositionDeltas = new List<Vector3>();
        _recording.RotationDeltas = new List<Quaternion>();
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
public struct AsyncRecording: IEquatable<AsyncRecording>
{
    private List<Vector3> _positionDeltas;

    public List<Vector3> PositionDeltas
    {
        set => _positionDeltas = value;
        get => _positionDeltas;
    }

    private List<Quaternion> _rotationDeltas;

    public List<Quaternion> RotationDeltas
    {
        set => _rotationDeltas = value;
        get => _rotationDeltas;
    }

    public List<float> timeStamps;

    public bool Equals(AsyncRecording other)
    {
        return Equals(_positionDeltas, other._positionDeltas) && Equals(_rotationDeltas, other._rotationDeltas) && Equals(timeStamps, other.timeStamps);
    }

    public override bool Equals(object obj)
    {
        return obj is AsyncRecording other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_positionDeltas, _rotationDeltas, timeStamps);
    }
}
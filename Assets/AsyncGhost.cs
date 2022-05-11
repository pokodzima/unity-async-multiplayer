using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsyncGhost : MonoBehaviour
{
    private float _timeOfRecording;

    private bool _isPlaying;

    private AnimationCurve _posXCurve;
    private AnimationCurve _posYCurve;
    private AnimationCurve _posZCurve;

    private AnimationCurve _rotXCurve;
    private AnimationCurve _rotYCurve;
    private AnimationCurve _rotZCurve;
    private AnimationCurve _rotWCurve;
    
    private float _playingTime;
    
    void Update()
    {
        if (_isPlaying)
        {
            var pos = new Vector3();
            pos.x = _posXCurve.Evaluate(_playingTime);
            pos.y = _posYCurve.Evaluate(_playingTime);
            pos.z = _posZCurve.Evaluate(_playingTime);

            var rot = new Quaternion();
            rot.x = _rotXCurve.Evaluate(_playingTime);
            rot.y = _rotYCurve.Evaluate(_playingTime);
            rot.z = _rotZCurve.Evaluate(_playingTime);
            rot.w = _rotWCurve.Evaluate(_playingTime);

            transform.position = pos;
            transform.rotation = rot;
            _playingTime += Time.deltaTime;

            if (_playingTime >= _timeOfRecording)
            {
                _isPlaying = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void InitializeGhost(AsyncRecording recording)
    {
        _posXCurve = new AnimationCurve();
        _posYCurve = new AnimationCurve();
        _posZCurve = new AnimationCurve();

        _rotXCurve = new AnimationCurve();
        _rotYCurve = new AnimationCurve();
        _rotZCurve = new AnimationCurve();
        _rotWCurve = new AnimationCurve();
        
        for (int i = 0; i < recording.timeStamps.Count; i++)
        {
            AddKeys(recording.timeStamps[i], recording.PositionDeltas[i], recording.RotationDeltas[i]);
        }

        _timeOfRecording = recording.timeStamps.Last();
        _playingTime = 0f;
        
        _isPlaying = true;
    }

    private void AddKeys(float time, Vector3 pos, Quaternion rot)
    {
        _posXCurve.AddKey(time, pos.x);
        _posYCurve.AddKey(time, pos.y);
        _posZCurve.AddKey(time, pos.z);

        _rotXCurve.AddKey(time, rot.x);
        _rotYCurve.AddKey(time, rot.y);
        _rotZCurve.AddKey(time, rot.z);
        _rotWCurve.AddKey(time, rot.w);
    }
}
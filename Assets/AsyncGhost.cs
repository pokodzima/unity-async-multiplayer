using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsyncGhost : MonoBehaviour
{
    private float timeOfRecording;

    private bool isPlaying;

    private AnimationCurve posXCurve;
    private AnimationCurve posYCurve;
    private AnimationCurve posZCurve;

    private AnimationCurve rotXCurve;
    private AnimationCurve rotYCurve;
    private AnimationCurve rotZCurve;
    private AnimationCurve rotWCurve;
    
    private float playingTime;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            var pos = new Vector3();
            pos.x = posXCurve.Evaluate(playingTime);
            pos.y = posYCurve.Evaluate(playingTime);
            pos.z = posZCurve.Evaluate(playingTime);

            var rot = new Quaternion();
            rot.x = rotXCurve.Evaluate(playingTime);
            rot.y = rotYCurve.Evaluate(playingTime);
            rot.z = rotZCurve.Evaluate(playingTime);
            rot.w = rotWCurve.Evaluate(playingTime);

            transform.position = pos;
            transform.rotation = rot;
            playingTime += Time.deltaTime;

            if (playingTime >= timeOfRecording)
            {
                isPlaying = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void InitializeGhost(AsyncRecording recording)
    {
        posXCurve = new AnimationCurve();
        posYCurve = new AnimationCurve();
        posZCurve = new AnimationCurve();

        rotXCurve = new AnimationCurve();
        rotYCurve = new AnimationCurve();
        rotZCurve = new AnimationCurve();
        rotWCurve = new AnimationCurve();
        
        for (int i = 0; i < recording.timeStamps.Count; i++)
        {
            AddKeys(recording.timeStamps[i], recording.positionDeltas[i], recording.rotationDeltas[i]);
        }

        timeOfRecording = recording.timeStamps.Last();
        playingTime = 0f;
        
        isPlaying = true;
    }

    private void AddKeys(float time, Vector3 pos, Quaternion rot)
    {
        posXCurve.AddKey(time, pos.x);
        posYCurve.AddKey(time, pos.y);
        posZCurve.AddKey(time, pos.z);

        rotXCurve.AddKey(time, rot.x);
        rotYCurve.AddKey(time, rot.y);
        rotZCurve.AddKey(time, rot.z);
        rotWCurve.AddKey(time, rot.w);
    }
}
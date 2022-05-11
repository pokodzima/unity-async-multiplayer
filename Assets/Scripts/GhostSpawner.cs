using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ghostCube;
    private const string ARKey = "AsyncRecording";

    private void StartGhost()
    {
        ghostCube.SetActive(true);
        var rec = PlayerPrefs.GetString(ARKey);
        ghostCube.GetComponent<AsyncGhost>().InitializeGhost(JsonUtility.FromJson<AsyncRecording>(rec));
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 120, 150, 30), "Play Recording"))
        {
            if (!PlayerPrefs.HasKey(ARKey))
            {
                Debug.LogError("No Recordings!");
                return;
            }

            StartGhost();
        }
    }
}
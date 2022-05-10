using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using UnityEngine;

public static class AsyncRecordingExtensions
{
    private const string ARKey = "AsyncRecording";
    public static void SaveInPLayerPrefs(this string inputString)
    {
        PlayerPrefs.SetString(ARKey, inputString);
    }
    
    
}
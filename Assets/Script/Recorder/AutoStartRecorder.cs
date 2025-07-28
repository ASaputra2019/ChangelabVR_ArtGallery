#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Reflection;

public static class AutoStartRecorder
{
    private static object controller;

    public static void StartRecording()
    {
        var recorderWindowType = typeof(EditorWindow).Assembly.GetType("UnityEditor.Recorder.RecorderWindow");
        var window = EditorWindow.GetWindow(recorderWindowType);

        var controllerField = recorderWindowType.GetField("m_RecorderController", BindingFlags.NonPublic | BindingFlags.Instance);
        controller = controllerField?.GetValue(window);

        if (controller == null)
        {
            Debug.LogError("RecorderController not found. Make sure the Recorder is configured.");
            return;
        }

        var method = controller.GetType().GetMethod("StartRecording", BindingFlags.Public | BindingFlags.Instance);
        method?.Invoke(controller, null);
        Debug.Log("▶️ Started recording.");
    }

    public static void StopRecording()
    {
        if (controller == null)
        {
            Debug.LogWarning("No active RecorderController. Did you start it?");
            return;
        }

        var method = controller.GetType().GetMethod("StopRecording", BindingFlags.Public | BindingFlags.Instance);
        method?.Invoke(controller, null);
        Debug.Log("⏹️ Stopped recording.");
    }
}
#endif

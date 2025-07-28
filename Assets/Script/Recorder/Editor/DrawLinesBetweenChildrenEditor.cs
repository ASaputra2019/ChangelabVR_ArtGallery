using ChangeLab.ArtGallery.Recorder;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Recorder))]
public class DrawLinesBetweenChildrenEditor : Editor
{
    void OnSceneGUI()
    {
        Recorder script = (Recorder)target;
        Transform parent = script.transform;

        Handles.color = Color.green;

        for (int i = 0; i < parent.childCount - 1; i++)
        {
            Transform a = parent.GetChild(i);
            Transform b = parent.GetChild(i + 1);

            if (a != null && b != null)
            {
                Handles.DrawLine(a.position, b.position);
            }
        }
    }
}

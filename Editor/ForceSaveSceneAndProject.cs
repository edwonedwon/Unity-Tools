#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

public class ForceSaveSceneAndProject : MonoBehaviour
{
    [MenuItem("File/Save project %&s")]
    static void FunctionForceSaveProyect()
    {
        EditorApplication.ExecuteMenuItem("File/Save Project");
        // Debug.Log("Saved project");
    }

    [MenuItem("File/Save Scene And Project %#&s")]
    static void FunctionForceSaveSceneAndProyect()
    {
        EditorApplication.ExecuteMenuItem("File/Save");
        EditorApplication.ExecuteMenuItem("File/Save Project");
        // Debug.Log("Saved scene and project");
    }
}

#endif
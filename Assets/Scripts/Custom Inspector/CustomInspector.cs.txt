using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CreateGrid))]
public class CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CreateGrid createGrid = (CreateGrid) target;
        if (GUILayout.Button("Generate Grid"))
        {
            createGrid.GenerateGrid();
        }

        if (GUILayout.Button("Clear Grid"))
        {
            createGrid.ClearGrid();
        }

        if (GUILayout.Button("Switch Coords"))
        {
            createGrid.SwitchCoords();
        }
    }
}

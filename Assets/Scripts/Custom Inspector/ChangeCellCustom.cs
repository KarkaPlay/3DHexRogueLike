using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChangeCellType))]
[CanEditMultipleObjects]
public class ChangeCellCustom : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        ChangeCellType changeCellType = (ChangeCellType) target;
        if (GUILayout.Button("Grass", GUILayout.Height(60)))
        {
            changeCellType.ToGrass();
        }
        
        if (GUILayout.Button("Sand", GUILayout.Height(60)))
        {
            changeCellType.ToSand();
        }
        
        if (GUILayout.Button("Ground", GUILayout.Height(60)))
        {
            changeCellType.ToGround();
        }
        
        if (GUILayout.Button("Water", GUILayout.Height(60)))
        {
            changeCellType.ToWater();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEditor;

[CustomEditor(typeof(ImportDB))]
public class CIImportDB : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        ImportDB importDB = (ImportDB) target;
        if (GUILayout.Button("Импортировать из Базы Данных", GUILayout.Height(60)))
        {
            importDB.Import();
        }

        if (GUILayout.Button("Закрыть соединение", GUILayout.Height(60)))
        {
            importDB.CloseConnection();
        }
    }
}

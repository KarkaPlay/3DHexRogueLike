using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor;
using Object = UnityEngine.Object;

public class ImportDB : MonoBehaviour
{
    private GameObject golem, wolf, goblin, goblinArcher, dragon, ogr, destroyer, otrodie, centaur, zombie;
    private GameObject Knight, Archer, Wizard, Barbarian, Leader, Viking;
    private GameObject double_sword, long_bow, dubinka, dagger, spear, bow, sword, hammer, sekira, axe, fistbul;
    private GameObject[] characters;
    private GameObject[] weapons;

    public void GetPrefabs()
    {
        golem = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemies/Golem.prefab", typeof(Object)) as GameObject;
        wolf = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemies/Wolf.prefab", typeof(Object)) as GameObject;
        goblin = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemies/Goblin.prefab", typeof(Object)) as GameObject;
        goblinArcher = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemies/Goblin-archer.prefab", typeof(Object)) as GameObject;
        //dragon = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemies/Dragon.prefab", typeof(Object)) as GameObject;
        //ogr = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemies/Ogr.prefab", typeof(Object)) as GameObject;
        destroyer = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemies/Destroyer.prefab", typeof(Object)) as GameObject;
        otrodie = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemies/Otrodie.prefab", typeof(Object)) as GameObject;
        centaur = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemies/Centaur.prefab", typeof(Object)) as GameObject;
        //zombie = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemies/Zombie.prefab", typeof(Object)) as GameObject;
        
        Knight = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Allies/Knight.prefab", typeof(Object)) as GameObject;
        Archer = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Allies/Archer.prefab", typeof(Object)) as GameObject;
        Wizard = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Allies/Wizard.prefab", typeof(Object)) as GameObject;
        Barbarian = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Allies/Barbarian.prefab", typeof(Object)) as GameObject;
        Leader = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Allies/Knight_Golden_Male.prefab", typeof(Object)) as GameObject;
        Viking = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Allies/Viking.prefab", typeof(Object)) as GameObject;
        
        //double_sword = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Double_Sword.prefab", typeof(Object)) as GameObject;
        //long_bow = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Long_Bow.prefab", typeof(Object)) as GameObject;
        //dubinka = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Dubinka.prefab", typeof(Object)) as GameObject;
        //dagger = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Dagger.prefab", typeof(Object)) as GameObject;
        //spear = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Spear.prefab", typeof(Object)) as GameObject;
        //bow = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Bow.prefab", typeof(Object)) as GameObject;
        //sword = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Sword.prefab", typeof(Object)) as GameObject;
        //hammer = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Hammer.prefab", typeof(Object)) as GameObject;
        //sekira = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Sekira.prefab", typeof(Object)) as GameObject;
        //axe = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Axe.prefab", typeof(Object)) as GameObject;
        //fistbul = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Fistbul.prefab", typeof(Object)) as GameObject;
    }

    public void CloseConnection()
    {
        string databaseName =
            "C:/Users/c1e2r/Desktop/Unity Projects/TPKI/3DHexRogueLike/Assets/Database/database.bytes";
        SqliteConnection connection = new SqliteConnection(string.Format("Data Source={0};", databaseName));
        connection.Close();
    }

    public void Import()
    {
        GetPrefabs();
        characters = new GameObject[] { golem, wolf, goblin, goblinArcher, destroyer, otrodie, centaur, Knight, Archer, Wizard, Barbarian, Leader, Viking};
        weapons = new GameObject[] { double_sword, long_bow, dubinka, dagger, spear, bow, sword, hammer, sekira, axe, fistbul };

        string databaseName =
            "C:/Users/c1e2r/Desktop/Unity Projects/TPKI/3DHexRogueLike/Assets/Database/database.bytes";
        SqliteConnection connection = new SqliteConnection(string.Format("Data Source={0};", databaseName));

        connection.Open();

        SqliteCommand cmd_db = new SqliteCommand("SELECT * FROM enemies", connection);
        var reader = cmd_db.ExecuteReader();
        while (reader.Read())
        {
            //golem.GetComponent<Character>().HP = int.Parse(reader[0].ToString());
            GameObject newObject = new GameObject();
            Character newCharacter = newObject.AddComponent<Character>();
            
            newCharacter.gameObject.name = reader[1].ToString();
            newCharacter.HP = int.Parse(reader[2].ToString());
            newCharacter.movementRange = int.Parse(reader[3].ToString());
            newCharacter.initiative = int.Parse(reader[4].ToString());
            
            //Debug.Log(newCharacter.gameObject.name+" имеет "+newCharacter.HP+" здоровья, "+newCharacter.movementRange+" дистанции и "+newCharacter.initiative+" инициативы");

            foreach (var character in characters)
            {
                if (character.name == newCharacter.gameObject.name)
                {
                    character.name = newCharacter.name;
                    character.GetComponent<Character>().HP = newCharacter.HP;
                    character.GetComponent<Character>().movementRange = newCharacter.movementRange;
                    character.GetComponent<Character>().initiative = newCharacter.initiative;
                    //PrefabUtility.SaveAsPrefabAsset(character, "Assets/Prefabs/Enemies/" + character.name + ".prefab");
                    PrefabUtility.SavePrefabAsset(character);
                    Debug.Log(newCharacter.gameObject.name + " - ХП: " + newCharacter.HP + " Дистанция: " + newCharacter.movementRange + " Инициатива: " + newCharacter.initiative);
                    break;
                }
            }
            
            
        }
        
        connection.Close();
    }
}

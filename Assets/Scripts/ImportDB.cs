using UnityEngine;
using Mono.Data.Sqlite;
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
        destroyer = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemies/Destroyer.prefab", typeof(Object)) as GameObject;
        otrodie = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemies/Otrodie.prefab", typeof(Object)) as GameObject;
        centaur = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemies/Centaur.prefab", typeof(Object)) as GameObject;

        Knight = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Allies/Knight.prefab", typeof(Object)) as GameObject;
        Archer = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Allies/Archer.prefab", typeof(Object)) as GameObject;
        Wizard = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Allies/Wizard.prefab", typeof(Object)) as GameObject;
        Barbarian = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Allies/Barbarian.prefab", typeof(Object)) as GameObject;
        Leader = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Allies/Knight_Golden_Male.prefab", typeof(Object)) as GameObject;
        Viking = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Allies/Viking.prefab", typeof(Object)) as GameObject;
        
        /*double_sword = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Double_Sword.prefab", typeof(Object)) as GameObject;
        long_bow = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Long_Bow.prefab", typeof(Object)) as GameObject;
        dubinka = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Dubinka.prefab", typeof(Object)) as GameObject;
        dagger = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Dagger.prefab", typeof(Object)) as GameObject;
        spear = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Spear.prefab", typeof(Object)) as GameObject;
        bow = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Bow.prefab", typeof(Object)) as GameObject;
        sword = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Sword.prefab", typeof(Object)) as GameObject;
        hammer = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Hammer.prefab", typeof(Object)) as GameObject;
        sekira = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Sekira.prefab", typeof(Object)) as GameObject;
        axe = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Axe.prefab", typeof(Object)) as GameObject;
        fistbul = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Fistbul.prefab", typeof(Object)) as GameObject;*/
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
        //weapons = new GameObject[] { double_sword, long_bow, dubinka, dagger, spear, bow, sword, hammer, sekira, axe, fistbul };
        
        GetDataFromDB("enemies");
        GetDataFromDB("allies");
        GetDataFromDB("weapons");
        GetDataFromDB("armor");
    }

    public void GetDataFromDB(string tableName)
    {
        string databaseName =
            "C:/Users/c1e2r/Desktop/Unity Projects/TPKI/3DHexRogueLike/Assets/Database/database.bytes";
        SqliteConnection connection = new SqliteConnection(string.Format("Data Source={0};", databaseName));
        connection.Open();
        SqliteCommand cmd_db = new SqliteCommand("SELECT * FROM "+tableName, connection);
        var reader = cmd_db.ExecuteReader();
        while (reader.Read())
        {
            GameObject newObject = new GameObject();
            Character newCharacter = newObject.AddComponent<Character>();

            if (tableName is "enemies" or "allies")
            {
                newCharacter.gameObject.name = reader[1].ToString();
                newCharacter.HP = int.Parse(reader[2].ToString());
                newCharacter.movementRange = int.Parse(reader[3].ToString());
                newCharacter.initiative = int.Parse(reader[4].ToString());
                newCharacter.weaponName = reader[5].ToString();
                newCharacter.armorName = reader[6].ToString();
            }
            else if (tableName == "weapons")
            {
                newCharacter.weaponName = reader[1].ToString();
                newCharacter.weaponDamage = int.Parse(reader[2].ToString());
                newCharacter.weaponMinRange = int.Parse(reader[3].ToString());
                newCharacter.weaponMaxRange = int.Parse(reader[4].ToString());
            }
            else if (tableName == "armor")
            {
                newCharacter.armorName = reader[1].ToString();
                newCharacter.defense = int.Parse(reader[2].ToString());
            }

            foreach (var character in characters)
            {
                Character characterComponent = character.GetComponent<Character>();
                if (tableName is "enemies" or "allies")
                {
                    if (character.name == newCharacter.gameObject.name)
                    {
                        character.name = newCharacter.name;
                        characterComponent.HP = newCharacter.HP;
                        characterComponent.movementRange = newCharacter.movementRange;
                        characterComponent.initiative = newCharacter.initiative;
                        characterComponent.weaponName = newCharacter.weaponName;
                        characterComponent.armorName = newCharacter.armorName;
                    
                        Debug.Log("У персонажа "+character.name+" имеется "+characterComponent.HP+" здоровья, "+characterComponent.movementRange+" дистанции и "+characterComponent.initiative+" инициативы");
                        PrefabUtility.SavePrefabAsset(character);
                        break;
                    }
                }

                if (tableName == "weapons")
                {
                    if (characterComponent.weaponName == newCharacter.weaponName)
                    {
                        characterComponent.weaponDamage = newCharacter.weaponDamage;
                        characterComponent.weaponMinRange = newCharacter.weaponMinRange;
                        characterComponent.weaponMaxRange = newCharacter.weaponMaxRange;

                        Debug.Log("У персонажа "+character.name+" обновлены данные об оружии "+newCharacter.weaponName);
                        PrefabUtility.SavePrefabAsset(character);
                    }
                }

                if (tableName == "armor")
                {
                    if (characterComponent.armorName == newCharacter.armorName)
                    {
                        characterComponent.defense = newCharacter.defense;
                        
                        Debug.Log("У персонажа "+character.name+" обновлены данные об доспехе "+newCharacter.armorName);
                        PrefabUtility.SavePrefabAsset(character);
                    }
                }
            }
            DestroyImmediate(newObject);
        }
        connection.Close();
    }
}

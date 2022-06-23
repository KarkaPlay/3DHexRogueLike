using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Character character;
    public int damage;
    public int defence;
    public int attackRange;

    public List<DBCell> dbtable;

    public struct DBCell
    {
        public int x;
        public int y;
        public int value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpInterface : MonoBehaviour
{
    private Character character;
    private int healthpoint;
    private int attack;
  
    private Text HpText;
    private GameObject hp;
    private void Start()
    {
        hp = GameObject.FindGameObjectWithTag("HPText");
      
        HpText = hp.GetComponent<Text>();
        HpText.enabled = false;
      
    }
    private void OnMouseEnter()
    {
        character = GetComponent<Character>();
        healthpoint = character.HP;
        attack = character.attack;
        hp.transform.position = Input.mousePosition;
        HpText.text = "HP:"+healthpoint.ToString();
        HpText.enabled = true;

    }
    private void OnMouseExit()
    {
        HpText.enabled = false;

    }
}

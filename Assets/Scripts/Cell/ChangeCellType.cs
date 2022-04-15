using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChangeCellType : MonoBehaviour
{
    public GameObject grassCell, rockCell, sandCell, waterCell;

    public void ToGrass()
    {
        GetComponent<Renderer>().material = grassCell.GetComponent<Renderer>().sharedMaterial;
        GetComponent<Cell>().isPlayable = true;
        transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
    }
    
    public void ToSand()
    {
        GetComponent<Renderer>().material = sandCell.GetComponent<Renderer>().sharedMaterial;
        GetComponent<Cell>().isPlayable = true;
        transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
    }
    
    public void ToRock()
    {
        GetComponent<Renderer>().material = rockCell.GetComponent<Renderer>().sharedMaterial;
        GetComponent<Cell>().isPlayable = false;
        transform.position = new Vector3(transform.position.x, -0.25f, transform.position.z);
    }
    
    public void ToWater()
    {
        GetComponent<Renderer>().material = waterCell.GetComponent<Renderer>().sharedMaterial;
        GetComponent<Cell>().isPlayable = false;
        transform.position = new Vector3(transform.position.x, -0.75f, transform.position.z);
    }
}

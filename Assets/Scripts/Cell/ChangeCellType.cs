using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChangeCellType : MonoBehaviour
{
    public GameObject grassCell, groundCell, sandCell, waterCell;

    public void ToGrass()
    {
        GetComponent<Renderer>().material = grassCell.GetComponent<Renderer>().sharedMaterial;
        GetComponent<Cell>().isPlayable = true;
        GetComponent<MeshFilter>().mesh = grassCell.GetComponent<MeshFilter>().sharedMesh;
        transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
    }
    
    public void ToSand()
    {
        GetComponent<Renderer>().material = sandCell.GetComponent<Renderer>().sharedMaterial;
        GetComponent<Cell>().isPlayable = true;
        GetComponent<MeshFilter>().mesh = sandCell.GetComponent<MeshFilter>().sharedMesh;
        transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
    }
    
    public void ToGround()
    {
        GetComponent<Renderer>().material = groundCell.GetComponent<Renderer>().sharedMaterial;
        GetComponent<Cell>().isPlayable = true;
        GetComponent<MeshFilter>().mesh = groundCell.GetComponent<MeshFilter>().sharedMesh;
        transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
    }
    
    public void ToWater()
    {
        GetComponent<Renderer>().material = waterCell.GetComponent<Renderer>().sharedMaterial;
        GetComponent<Cell>().isPlayable = false;
        GetComponent<MeshFilter>().mesh = waterCell.GetComponent<MeshFilter>().sharedMesh;
        transform.position = new Vector3(transform.position.x, -0.75f, transform.position.z);
    }
}

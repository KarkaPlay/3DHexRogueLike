using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class CreateGrid : MonoBehaviour
{
    public GridManager gridManager;
    public GameObject cellPrefab;
    public GameObject emptyCellPrefab;
    public int width, height;
    public bool coordsIsOn;

    private Vector3 gridStartPos;
    private float YGridOffset, XGridOffset;
    
    public void GenerateGrid()
    {
        width = 34; // Клеток в ширину
        height = 16; // Клеток в высоту

        YGridOffset = 1.5f; // Расстояние между ячейками по вертикали
        XGridOffset = 1f; // Расстояние между ячейками по горизонтали
        gridStartPos = new Vector3(0f, -0.5f, 0f);
        
        gridManager = gameObject.GetComponent<GridManager>();
        
        GameObject prefab = cellPrefab; // Убрать "= cellPrefab если вернуть пустые ячейки"
        for (int j = 0; j < height / 2; j++)
        {
            for (int i = 0; i < width / 2; i++)
            {
                // Если ячейки на границе, то они не пустые
                //if (j*2 == 0 || j*2 >= height-2 || i*2 <= 1 || i*2 >= width-2) prefab = cellPrefab;
                //else prefab = emptyCellPrefab;
                
                // Текущий отступ
                Vector3 currentOffset = new Vector3(XGridOffset * i, 0, YGridOffset * j);
                // Теукщая ячейка
                GameObject currentCell = Instantiate(prefab, gridStartPos + currentOffset, Quaternion.identity, gameObject.transform);
                currentCell.GetComponent<Cell>().cellCoords = new Vector2Int(i * 2, j * 2);
                currentCell.transform.position = gridStartPos + currentOffset;

                TextMeshPro hexText = currentCell.GetComponentInChildren<TextMeshPro>();
                hexText.text = currentCell.GetComponent<Cell>().cellCoords.x + "." + currentCell.GetComponent<Cell>().cellCoords.y;
                currentCell.name = "cell " + hexText.text;
            }

            for (int i = 0; i < width / 2; i++)
            {
                // Если ячейки на границе
                //if (j*2 == 0 || j*2 >= height-2 || i*2 <= 1 || i*2 >= width-2) prefab = cellPrefab;
                //else prefab = emptyCellPrefab;
                
                Vector3 currentOffset = new Vector3(XGridOffset / 2 + XGridOffset * i, 0, YGridOffset / 2 + YGridOffset * j);
                GameObject currentCell = Instantiate(prefab, gridStartPos + currentOffset, Quaternion.identity, gameObject.transform);
                currentCell.GetComponent<Cell>().cellCoords = new Vector2Int(i * 2 + 1, j * 2 + 1);
                
                TextMeshPro hexText = currentCell.GetComponentInChildren<TextMeshPro>();
                hexText.text = currentCell.GetComponent<Cell>().cellCoords.x + "." + currentCell.GetComponent<Cell>().cellCoords.y;
                currentCell.name = "cell " + hexText.text;
            }
        }
    }

    public void ClearGrid()
    {
        var children = new List<GameObject>();
        foreach (Transform child in transform) children.Add(child.gameObject);
        children.ForEach(child => DestroyImmediate(child));
    }

    public void SwitchCoords()
    {
        MeshRenderer textMesh;
        for (int i = 0; i < transform.childCount; i++)
        {
            textMesh = transform.GetChild(i).GetChild(0).GetComponent<MeshRenderer>();
            textMesh.enabled = !textMesh.enabled;
        }
    }
}

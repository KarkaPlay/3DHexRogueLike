using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] public Vector2Int cellCoords;
    public List<Cell> neighbourCells;
    public Color originalColor;
    
    public bool isPlayable = true;
    public bool isChosen = false;
    public bool isHighlighted = false;
    
    public GridManager manager;
    public Character character;

    private void Start()
    {
        manager = transform.parent.GetComponent<GridManager>();
        originalColor = GetComponent<Renderer>().material.color;
        FindNeighbours();
        SetCharacter();
    }

    public void FindNeighbours()
    {
        neighbourCells.Add(manager.FindCell(new Vector2Int(cellCoords.x-2, cellCoords.y)));
        neighbourCells.Add(manager.FindCell(new Vector2Int(cellCoords.x-1, cellCoords.y+1)));
        neighbourCells.Add(manager.FindCell(new Vector2Int(cellCoords.x+1, cellCoords.y+1)));
        neighbourCells.Add(manager.FindCell(new Vector2Int(cellCoords.x+2, cellCoords.y)));
        neighbourCells.Add(manager.FindCell(new Vector2Int(cellCoords.x+1, cellCoords.y-1)));
        neighbourCells.Add(manager.FindCell(new Vector2Int(cellCoords.x-1, cellCoords.y-1)));
        neighbourCells.RemoveAll(x => x == null);
    }

    public void SetCharacter()
    {
        character = transform.GetComponentInChildren<Character>();
    }

    public void OnMouseEnter()
    {
        if (isPlayable && !isChosen)
        {
            ChangeColor(Color.white);
        }
    }

    private void OnMouseExit()
    {
        if (!isChosen && !isHighlighted)
        {
            ChangeColor(originalColor);
        }
    }

    public void ChangeColor(Color newColor)
    {
        GetComponent<Renderer>().material.color = newColor;
    }

    public void OnMouseDown()
    {
        if (isPlayable)
        {
            manager.ChooseCell(this);
            isChosen = true;
        }
    }
}
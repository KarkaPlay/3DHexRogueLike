using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] public Vector2Int cellCoords;
    public List<Cell> neighbourCells;
    public Color originalColor;
    
    public bool isPlayable = true;
    public bool isChosen;
    public bool isHighlighted;
    
    public Character character;

    private void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
        FindNeighbours();
        SetCharacter();
    }
    
    public void FindNeighbours()
    {
        var manager = transform.parent.GetComponent<GridManager>();
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

    // TODO - Если на клетке стоит враг, то при наведении курсора она становится красной
    public void OnMouseEnter()
    {
        if (isPlayable && !isChosen)
        {
            ChangeColor(Color.white);
        }
    }
    
    public void OnMouseExit()
    {
        if (character)
        {
            if (!character.isChosen)
            {
                ChangeColor(originalColor);
            }
        }
        else
        {
            if (!isHighlighted)
            {
                ChangeColor(originalColor);
            }
        }
    }

    public void ChangeColor(Color newColor)
    {
        GetComponent<Renderer>().material.color = newColor;
    }

    public void UnHighlight(bool unHighlightNeighbours)
    {
        if (!isPlayable) return;
        
        isHighlighted = false;
        ChangeColor(originalColor);
        
        if (unHighlightNeighbours)
        {
            foreach (var cell in neighbourCells)
            {
                cell.UnHighlight(false);
            }
        }
    }

    public void Highlight(bool highlightNeighbours)
    {
        if (!isPlayable) return;
        
        isHighlighted = true;
        ChangeColor(Color.white);

        if (highlightNeighbours)
        {
            foreach (var cell in neighbourCells)
            {
                cell.Highlight(false);
            }
        }
    }
    
    public void OnMouseOver()
    {
        // При нажатии правой кнопки мыши
        if (isHighlighted && Input.GetMouseButtonDown(1))
        {
            ChangeColor(Color.red);
            transform.parent.GetComponent<GridManager>().currentChosenCharacter.MoveTo(this);
        }
    }
}
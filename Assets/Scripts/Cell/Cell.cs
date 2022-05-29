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

    
    public void OnMouseEnter()
    {
        if (!isPlayable) return;
        if (!character)
            return;
        if (character.manager.currentState != GridManager.State.nothingChosen)
            return;
        
        ChangeColor(Color.white);
    }
    
    public void OnMouseExit()
    {
        if (!isHighlighted)
        {
            ChangeColor(originalColor);
        }
    }
    
    public void OnMouseOver()
    {
        // При нажатии правой кнопки мыши
        if (isHighlighted && !character && Input.GetMouseButtonDown(1))
        {
            transform.parent.GetComponent<GridManager>().currentChosenCharacter.MoveTo(this);
        }

        if (character)
        {
            if (character.isEnemy)
            {
                // Атаковать врага
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
        
        if (character) { ChangeColor(character.isEnemy ? Color.red : Color.green); }
        else { ChangeColor(Color.white); }

        if (highlightNeighbours)
        {
            foreach (var cell in neighbourCells)
            {
                cell.Highlight(false);
            }
        }
    }
    
}
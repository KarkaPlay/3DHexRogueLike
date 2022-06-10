using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] public Vector2Int cellCoords;
    public List<Cell> neighbourCells;
    public List<Cell> movementRangeCells;
    public List<Cell> attackRangeCells;
    public Color originalColor;
    
    public bool isPlayable = true;
    public bool isChosen;
    public bool isHighlighted;
    
    public Character character;

    private void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
        SetCharacter();
        AddNeighboursInRange(movementRangeCells, character ? character.movementRange : 1);
    }

    public void AddNeighboursInRange(List<Cell> list, int range = 1)
    {
        list = new List<Cell>();
        foreach (var cell in FindNeighboursInRange(range))
        {
            list.Add(cell);
        }
    }

    public List<Cell> FindNeighboursInRange(int range)
    {
        List<Cell> neighbours = new List<Cell>();

        var manager = transform.parent.GetComponent<GridManager>();

        foreach (var other in manager.cells)
        {
            if (Mathf.Abs(other.cellCoords.x - cellCoords.x)/2 <= range && Mathf.Abs(other.cellCoords.y - cellCoords.y) <= range 
                                                                        && (Mathf.Abs(other.cellCoords.x - cellCoords.x) + Mathf.Abs(other.cellCoords.y - cellCoords.y))/2 <= range)
            {
                neighbourCells.Add(other);
                movementRangeCells.Add(other);
            }
        }
        return neighbours;
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
        if (isHighlighted && Input.GetMouseButtonDown(1))
        {
            if (!character) // Если клетка свободна
            {
                transform.parent.GetComponent<GridManager>().currentChosenCharacter.MoveTo(this);
            }
            
            if (character) // Если на клетке враг
            {
                if (character.isEnemy)
                {
                    character.HP -= character.manager.currentChosenCharacter.attack;
                    if (character.HP < 1)
                    {
                        Destroy(character.gameObject);
                    }
                }
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

        foreach (var cell in movementRangeCells)
        {
            if (!character)
            {
                if (!isPlayable) return;
                isHighlighted = true;
                ChangeColor(Color.white);
            }
        }
        
        foreach (var cell in attackRangeCells)
        {
            if (character)
            {
                ChangeColor(character.isEnemy ? Color.red : Color.green);
                isHighlighted = true;
            }
        }
        
        /*if (character) { ChangeColor(character.isEnemy ? Color.red : Color.green); }
        else { ChangeColor(Color.white); }

        if (highlightNeighbours)
        {
            foreach (var cell in movementRangeCells)
            {
                //cell.Highlight(false);
                if (!character)
                {
                    ChangeColor(Color.white);
                }
            }

            foreach (var cell in attackRangeCells)
            {
                //cell.Highlight(false);
                if (character)
                {
                    ChangeColor(character.isEnemy ? Color.red : Color.green);
                }
            }
        }*/
    }
    
}
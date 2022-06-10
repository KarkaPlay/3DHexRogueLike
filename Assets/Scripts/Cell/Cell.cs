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
        AddNeighboursInRange(1);
        SetCharacter();
    }

    public void AddNeighboursInRange(int range = 1)
    {
        foreach (var cell in FindNeighboursInRange(range))
        {
            neighbourCells.Add(cell);
        }
    }

    public List<Cell> FindNeighboursInRange(int range = 1)
    {
        List<Cell> neighboursInRange = new List<Cell>();
        
        while (range > 0)
        {
            foreach (var cell in FindNeighbours())
            {
                foreach (var cell2 in cell.FindNeighbours())
                {
                    neighboursInRange.Add(cell2);
                }
            }
            range--;
        }

        //neighboursInRange = neighboursInRange.Distinct().ToList();
        return neighboursInRange;
    }
    
    public List<Cell> FindNeighbours()
    {
        List<Cell> neighbours = new List<Cell>();

        var manager = transform.parent.GetComponent<GridManager>();

        // при range = 1
        // x-2, x-1, x+1, x+2
        // y-1, y, y+1

        int range = 2;

        for (int i = 0; i < range; i++)
        {
            
        }
        neighbours.Add(manager.FindCell(new Vector2Int(cellCoords.x - 2, cellCoords.y)));
        neighbours.Add(manager.FindCell(new Vector2Int(cellCoords.x - 1, cellCoords.y + 1)));
        neighbours.Add(manager.FindCell(new Vector2Int(cellCoords.x + 1, cellCoords.y + 1)));
        neighbours.Add(manager.FindCell(new Vector2Int(cellCoords.x + 2, cellCoords.y)));
        neighbours.Add(manager.FindCell(new Vector2Int(cellCoords.x + 1, cellCoords.y - 1)));
        neighbours.Add(manager.FindCell(new Vector2Int(cellCoords.x - 1, cellCoords.y - 1)));
        neighbours.RemoveAll(x => x == null);
        
        // при range = 2
        neighbours.Add(manager.FindCell(new Vector2Int(cellCoords.x - 4, cellCoords.y)));
        neighbours.Add(manager.FindCell(new Vector2Int(cellCoords.x - 1, cellCoords.y + 1)));
        neighbours.Add(manager.FindCell(new Vector2Int(cellCoords.x + 1, cellCoords.y + 1)));
        neighbours.Add(manager.FindCell(new Vector2Int(cellCoords.x + 2, cellCoords.y)));
        neighbours.Add(manager.FindCell(new Vector2Int(cellCoords.x + 1, cellCoords.y - 1)));
        neighbours.Add(manager.FindCell(new Vector2Int(cellCoords.x - 1, cellCoords.y - 1)));
        neighbours.RemoveAll(x => x == null);
        
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
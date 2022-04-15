using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public List<Cell> cells;

    public Cell currentChosen;
    public AudioSource audioSource;

    private void Awake()
    {
        foreach (var cell in GetComponentsInChildren<Cell>())
        {
            cells.Add(cell);
        }
    }

    public Cell FindCell(Vector2Int coords)
    {
        foreach (var cell in cells)
        {
            if (cell.cellCoords == coords)
            {
                return cell;
            }
        }
        return null;
    }

    public void ChooseCell(Cell cell)
    {
        // Отменяем предыдущее выделение
        if (currentChosen)
        {
            currentChosen.ChangeColor(currentChosen.originalColor);
            currentChosen.isChosen = false;
            
            foreach (var neighbourCell in currentChosen.neighbourCells)
            {
                neighbourCell.ChangeColor(neighbourCell.originalColor);
                neighbourCell.isHighlighted = false;
            }
        }

        // Выделяем ячейку
        currentChosen = cell;
        cell.ChangeColor(Color.blue);
        foreach (var neighbourCell in cell.neighbourCells)
        {
            if (neighbourCell.isPlayable)
            {
                neighbourCell.ChangeColor(Color.white);
                neighbourCell.isHighlighted = true;
            }
        }
        
        // Звук выбора ячейки
        audioSource.Play();
    }
}

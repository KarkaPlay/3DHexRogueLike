using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public List<Cell> cells;
    
    public Character currentChosenCharacter;
    public AudioSource audioSource;

    public enum State
    {
        nothingChosen,
        characterChosen,
        inAction
    };

    public State currentState;

    private void Awake()
    {
        foreach (var cell in GetComponentsInChildren<Cell>())
        {
            cells.Add(cell);
        }

        currentState = State.nothingChosen;
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

    public void ChooseCharacter(Character chosenCharacter)
    {
        // Отменяем предыдущее выделение
        if (currentChosenCharacter)
        {
            var currentChosenCell = currentChosenCharacter.cell;
            currentChosenCell.ChangeColor(currentChosenCell.originalColor);
            currentChosenCharacter.isChosen = false;
            
            currentChosenCell.UnHighlight(true);
        }

        // Выделяем ячейку
        currentChosenCharacter = chosenCharacter;
        currentChosenCharacter.cell.Highlight(true);
        
        currentChosenCharacter.isChosen = true;
        currentChosenCharacter.cell.ChangeColor(Color.blue);

        // Звук выбора ячейки
        audioSource.Play();
        
        // Состояние меняется на "выбран персонаж"
        currentState = State.characterChosen;
    }
}

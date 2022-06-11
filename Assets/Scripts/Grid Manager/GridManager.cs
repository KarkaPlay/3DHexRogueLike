using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public List<Cell> cells;
    public List<Character> characters;
    public AudioClip clip;

    public Character currentChosenCharacter;
    public AudioSource audioSource;
    
    public int enemyCount, allyCount;
    public GameManager gameManager;

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

        foreach (var cell in cells)
        {
            characters.Add(cell.GetComponentInChildren<Character>());
        }

        characters.RemoveAll(item => item == null);

        currentState = State.nothingChosen;

        /*audioSource.clip = clips[gameManager.scene.buildIndex - 1];*/
    }

    private void Start()
    {
        audioSource.Play();
    }

    public int i = 0;

    public void UpdateStats()
    {
        enemyCount = 0;
        allyCount = 0;
        foreach (var character in characters)
        {
            if (character.isEnemy)
            {
                enemyCount++;
            }
            else
            {
                allyCount++;
            }
        }
        if (enemyCount == 0)
        {
            if (gameManager.scene.buildIndex == 3)
            {
                gameManager.finaleImage.GetComponent<Image>().enabled = true;
            }
            else
            {
                gameManager.winImage.GetComponent<Image>().enabled = true;
            }
        }

        if (allyCount == 0)
        {
            gameManager.looseImage.GetComponent<Image>().enabled = true;
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
        currentChosenCharacter.cell.HighlightMovement(true);
        currentChosenCharacter.cell.HighlightAttack(true);
        
        currentChosenCharacter.isChosen = true;
        currentChosenCharacter.cell.ChangeColor(Color.blue);

        // Звук выбора ячейки
        //audioSource.Play();
        
        // Состояние меняется на "выбран персонаж"
        currentState = State.characterChosen;
    }
}

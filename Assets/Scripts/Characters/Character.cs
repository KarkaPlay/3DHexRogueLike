using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    public bool isEnemy;
    public bool isChosen;
    
    public Cell cell;
    public Weapon weapon;
    public GridManager manager;
    public Vector3 gridOffset;

    public int HP;
    public int attack;
    public int movementRange;
    public int attackRange;
    public int initiative;

    private void Start()
    {
        gridOffset = new Vector3(0f, .65f, 0f);
        cell = transform.parent.GetComponent<Cell>();
        manager = cell.transform.parent.GetComponent<GridManager>();
        manager.UpdateStats();
    }

    // TODO - сделать плавный переход между позициями
    private void Update()
    {
        //Move();
    }

    public List<Cell> randomCells;
    public void RandomMove()
    {
        bool attacking = false;
        Character toAttack = new Character();
        foreach (var attackCell in cell.attackRangeCells)
        {
            if (attackCell.character)
            {
                if (!attackCell.character.isEnemy)
                {
                    attacking = true;
                    toAttack = attackCell.character;
                }
            }
        }

        if (attacking)
        {
            toAttack.HP -= attack;
            if (toAttack.HP < 1)
            {
                manager.characters.Remove(toAttack);
                Destroy(toAttack.gameObject);
            }
        }
        else
        {
            randomCells = cell.movementRangeCells;
            randomCells.RemoveAll(x => !x.isPlayable || x.character);
            int randNum = Random.Range(0, randomCells.Count);
            EnemyMoveTo(randomCells[randNum]);
        }
    }

    /*static System.Random rnd = new System.Random();
    public IEnumerator RandomMoveRoutine()
    {
        yield return new WaitForSeconds(1f);

        MoveTo(cell.movementRangeCells[rnd.Next(cell.movementRangeCells.Count)]);
    }*/
    
    public void MoveTo(Cell toCell)
    {
        Debug.Log("Перемещаемся");
        manager.currentState = GridManager.State.inAction;
        
        cell.character = null;
        toCell.character = this;
        
        cell.UnHighlight(true);
        cell.SetNeighbours(1, 0);
        cell = toCell;
        cell.SetNeighbours(movementRange, attackRange);
        
        Debug.Log("Соседи выставлены");
        
        transform.position = toCell.transform.position + gridOffset;
        transform.parent = toCell.gameObject.transform;
        isChosen = false;

        manager.currentState = GridManager.State.nothingChosen;

        Debug.Log("Состояние изменено");
        //Character enemyToMove = null;
        StartEnemyAction();
        manager.UpdateStats();
    }

    

    public void StartEnemyAction()
    {
        List<Character> enemies = new List<Character>();
        foreach (var character in manager.characters)
        {
            if (character.isEnemy)
            {
                enemies.Add(character);
            }
        }
        Debug.Log("Создан список врагов, Количество - "+enemies.Count);

        if (manager.i > enemies.Count-1)
        {
            manager.i = 0;
        }
        else
        {
            enemies[manager.i].RandomMove();
            manager.i++;
        }
    }

    void EnemyMoveTo(Cell toCell)
    {
        Debug.Log("Враг начинает ходить");
        manager.currentState = GridManager.State.inAction;
        
        cell.character = null;
        toCell.character = this;
        
        cell.SetNeighbours(1, 0);
        cell = toCell;
        cell.SetNeighbours(movementRange, attackRange);
        
        transform.position = toCell.transform.position + gridOffset;
        transform.parent = toCell.gameObject.transform;
        isChosen = false;

        manager.currentState = GridManager.State.nothingChosen;
        Debug.Log("Враг закончил ходить");
    }

    void Move()
    {
        //transform.position = Vector3.LerpUnclamped();
    }

    private void OnMouseDown()
    {
        if (!isEnemy)
        {
            manager.ChooseCharacter(this);
        }
    }
    
    private void OnMouseEnter()
    {
        // TODO: изменять курсор на курсор атаки, если на ячейке враг
        if (manager.currentState == GridManager.State.characterChosen)
            return;

        cell.OnMouseEnter();
    }

    private void OnMouseExit()
    {
        if (manager.currentState == GridManager.State.characterChosen)
            return;
        
        cell.OnMouseExit();
    }

    // TODO: Запустить атаку при нажатии ПКМ
    private void OnMouseOver()
    {
        //if (manager.currentState == GridManager.State.characterChosen)
            //return;
        
        cell.OnMouseOver();
    }
}

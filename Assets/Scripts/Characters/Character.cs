using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    // TODO - сделать префаб врага и функцию атаки
    public bool isEnemy;
    public bool isChosen;
    
    public Cell cell;
    public GridManager manager;
    public Vector3 gridOffset;

    public int HP;
    public int attack;

    private void Start()
    {
        gridOffset = new Vector3(-0.5f, 1.25f, 0.25f);
        cell = transform.parent.GetComponent<Cell>();
        manager = cell.transform.parent.GetComponent<GridManager>();
    }

    // TODO - сделать плавный переход между позициями
    private void Update()
    {
        Move();
    }
    
    // BUG - можно перемещаться на клетку с персонажем
    public void MoveTo(Cell toCell)
    {
        cell.character = null;
        toCell.character = this;
        
        cell.UnHighlight(true);

        cell = toCell;
        
        transform.position = toCell.transform.position + gridOffset;
        transform.parent = toCell.gameObject.transform;
        isChosen = false;
    }

    void Move()
    {
        //transform.position = Vector3.LerpUnclamped();
    }

    private void OnMouseDown() { manager.ChooseCharacter(this); }

    private void OnMouseEnter() { cell.OnMouseEnter(); }

    private void OnMouseExit() { cell.OnMouseExit(); }

    private void OnMouseOver() { cell.OnMouseOver(); }
}

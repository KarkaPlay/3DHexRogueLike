using UnityEngine;

public class Character : MonoBehaviour
{
    public bool isEnemy;
    public Cell cell;

    public int HP;
    public int attack;

    private void Start()
    {
        SetCell();
    }

    // TODO - сделать механику перемещения персонажа по нажатию ПКМ
    void Move()
    {
        
    }

    public void SetCell()
    {
        cell = transform.parent.GetComponent<Cell>();
    }

    private void OnMouseDown()  { cell.OnMouseDown(); }

    private void OnMouseEnter() { cell.OnMouseEnter(); }

    private void OnMouseExit() { cell.OnMouseExit(); }

    private void OnMouseOver() { cell.OnMouseOver(); }
}

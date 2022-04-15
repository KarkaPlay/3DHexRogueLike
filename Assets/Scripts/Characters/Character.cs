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

    public void SetCell()
    {
        cell = transform.parent.GetComponent<Cell>();
    }

    private void OnMouseDown()
    {
        cell.OnMouseDown();
    }

    private void OnMouseEnter()
    {
        cell.OnMouseEnter();
    }
}

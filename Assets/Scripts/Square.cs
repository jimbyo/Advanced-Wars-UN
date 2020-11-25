using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public bool moveable;
    public GameObject moveSquare;
    public GameObject attackSquare;
    public int playerNum;
    private GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    // move unit to a specific square
    {
        if (moveable)
        {
            GM.selected.GetComponent<Unit>().Move(transform.position.x, transform.position.y);
        }
    }

    public void SetAttackable(bool attackable)
    // if a square is not block and is in unit's attack range, set this square attackable
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position,Vector2.zero);
        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Unit"))
            {
                hit.collider.GetComponent<Unit>().attackable = attackable;
            }
        }
    }
}

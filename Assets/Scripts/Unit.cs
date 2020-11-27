using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int damage;
    public int maxHP;
    public int HP;
    public int moveRange;
    public int attackRange;
    public bool hasMoved;
    public bool hasAttacked;
    public int playerNum;
    public bool attackable;

    public GameObject Square;

    private GameManager GM;
    private UIManager UM;

    // Start is called before the first frame update
    void Start()
    {
        SetStandSquare(true);
        HP = maxHP;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        UM = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseDown()
    {
        if (playerNum == GM.currentplayer)
        {
            GM.selected = gameObject;
            if (!hasMoved)
            {
                GM.TurnOnMoveRange();
                GM.TurnOffAttackRange();
            }
            else if (!hasAttacked)
            {
                GM.TurnOnAttackRange();
            }
        }
        else if (attackable)
        {
            Attack(GM.selected.GetComponent<Unit>().damage);
            GM.selected.GetComponent<Unit>().hasAttacked = true;
            GM.TurnOffAllRanges();
            attackable = false;
        }
        UM.InfoShow(gameObject.GetComponent<Unit>());
    }

    public void Move(float x, float y)
    // move unit to a new square
    {
        SetStandSquare(false);
        transform.position = new Vector3(x,y,transform.position.z);
        GM.TurnOffAllRanges();
        SetStandSquare(true);
        hasMoved = true;
        if (!hasAttacked)
        {
            GM.TurnOnAttackRange();
        }
    }


    public void Attack(int damage)
    // deal damage to a selected unit
    {
        HP = HP - damage;
        print("Damage: "+damage +" HP: "+HP);
        UM.InfoShow(gameObject.GetComponent<Unit>());
        if (HP <= 0)
        {
            GM.Kill(gameObject);
        }
    }

    public void reset()
    // reset the unit
    {
        hasMoved = false;
        hasAttacked = false;
        attackable = false;
    }

    public void SetStandSquare(bool stand)
    // set playerNum of the square under the unit to that unit's playerNum
    // set playerNum of other square to 0
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position,Vector2.zero);
        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Square"))
            {
                if (stand)
                {
                    hit.collider.GetComponent<Square>().playerNum = playerNum;
                    Square = hit.collider.gameObject;
                }
                else
                {
                    hit.collider.GetComponent<Square>().playerNum = 0;
                }

            }
        }
    }

}

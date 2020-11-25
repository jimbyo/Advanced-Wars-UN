using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] squares;
    public GameObject[] units;
    public GameObject selected;
    public int currentplayer;
    public int nextplayer;
    private int[] unitCount;

    private List<GameObject> moveRangeList;
    private List<GameObject> attackRangeList;

    // Start is called before the first frame update
    void Start()
    {
        squares = GameObject.FindGameObjectsWithTag("Square");
        units = GameObject.FindGameObjectsWithTag("Unit");
        // reset the position of all units
        foreach (var i in units)
        {
            i.transform.position = new Vector3(i.transform.position.x,
            i.transform.position.y,i.transform.position.z);
        }
        moveRangeList = new List<GameObject>();
        attackRangeList = new List<GameObject>();
        GetUnitNum();
    }

    // Update is called once per frame
    void Update()
    // right click will turn off all ranges
    {
        if (Input.GetMouseButtonDown(1))
        {
            TurnOffAllRanges();
        }
    }

    public void TurnOnMoveRange()
    // search all squares(playerNum == 0) within unit's move range and turn on move range
    {
        TurnOffMoveRange();
        foreach (var i in squares)
        {
            int moveRange = selected.GetComponent<Unit>().moveRange;
            if (Mathf.Abs(i.transform.position.x - selected.transform.position.x) +
            Mathf.Abs(i.transform.position.y - selected.transform.position.y) <= moveRange
            && i.GetComponent<Square>().playerNum == 0)
            {
                i.GetComponent<Square>().moveable = true;
                i.GetComponent<Square>().moveSquare.SetActive(true);
                moveRangeList.Add(i);
            }
        }
    }

    public void TurnOffMoveRange()
    // turn off move range and clear move range list
    {
        foreach (var i in moveRangeList)
        {
            i.GetComponent<Square>().moveable = false;
            i.GetComponent<Square>().moveSquare.SetActive(false);
        }
        moveRangeList.Clear();
    }

    public void TurnOnAttackRange()
    // search all squares(playerNum != unit's playNum) within unit's attack range and turn on attack range
    {
            TurnOffAllRanges();
            foreach (var i in squares)
            {
                int attackRange = selected.GetComponent<Unit>().attackRange;
                if (Mathf.Abs(i.transform.position.x - selected.transform.position.x) +
                Mathf.Abs(i.transform.position.y - selected.transform.position.y) <= attackRange
                && i.GetComponent<Square>().playerNum != selected.GetComponent<Unit>().playerNum)
                {
                    i.GetComponent<Square>().attackSquare.SetActive(true);
                    attackRangeList.Add(i);
                    if (i.GetComponent<Square>().playerNum != 0)
                    {
                        i.GetComponent<Square>().SetAttackable(true);
                    }
                }
            }
    }

    public void TurnOffAttackRange()
    // turn off attack range and clear attack range list
    {
        foreach (var i in attackRangeList)
        {
            i.GetComponent<Square>().attackSquare.SetActive(false);
        }
        attackRangeList.Clear();
    }

    public void TurnOffAllRanges()
    // turn off move range and attack range
    {
        foreach (var i in moveRangeList)
        {
            i.GetComponent<Square>().moveable = false;
            i.GetComponent<Square>().moveSquare.SetActive(false);
        }
        moveRangeList.Clear();
        foreach (var i in attackRangeList)
        {
            i.GetComponent<Square>().attackSquare.SetActive(false);
            i.GetComponent<Square>().SetAttackable(false);
        }
        attackRangeList.Clear();
    }

    public void EndTurn()
    // reset all property of units of current players
    {
        TurnOffAllRanges();
        selected = null;
        int tmp = currentplayer;
        currentplayer = nextplayer;
        nextplayer = tmp;
        units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (var i in units)
        {
            Unit u = i.GetComponent<Unit>();
            if (u.playerNum == currentplayer)
            {
                u.reset();
            }
        }
        print("Current Player: " + currentplayer);

    }

    public void Kill(GameObject obj)
    // destory a unit
    {
        int pn = obj.GetComponent<Unit>().playerNum;
        obj.GetComponent<Unit>().SetStandSquare(false);
        Destroy(obj);
        unitCount[pn]--;
        if (unitCount[pn] <= 0)
        {
            print("Player "+ pn + " lose");
        }
    }

    public void GetUnitNum()
    // get unit number of player's and enemy's
    {
        unitCount = new int[3];
        units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (var i in units)
        {
            Unit u = i.GetComponent<Unit>();
            unitCount[u.playerNum]++;
        }
    }

}

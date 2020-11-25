using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameManager GM;

    public Text gold;
    public Text HP;
    public Text damage;
    public Text speed;
    public Text gold2;
    public Text HP2;
    public Text damage2;
    public Text speed2;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void EndTurn()
    {
        GM.EndTurn();
    }

    public void InfoShow(Unit p)
    // show info of selected unit
    {
        if (p.playerNum == 1)
        {
            HP.text = p.HP.ToString();
            damage.text = p.damage.ToString();
            speed.text = p.moveRange.ToString();
        }
        else if (p.playerNum == 2)
        {
            HP2.text = p.HP.ToString();
            damage2.text = p.damage.ToString();
            speed2.text = p.moveRange.ToString();
        }
    }

    public void InfoClear()
    {

    }

    public void onClickReturn()
    // return to start menu
    {
        SceneManager.LoadScene(0);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map_generator : MonoBehaviour
{
    private float width;
    private float height;
    private int x;
    private int y;

    private int treenum;
    private int wallnum;
    public  int num;
    private HashSet<Vector2> allpos;
    private HashSet<Vector2> treepos;
    private HashSet<Vector2> treepos2;
    private HashSet<Vector2> wallpos;
    private HashSet<Vector2> playerpos;
    private HashSet<Vector2> enemypos;

    private GameObject ToDestory;

    public GameObject blocks;
    public GameObject squares;
    public GameObject ground;
    public GameObject tree1;
    public GameObject tree2;
    public GameObject wall;
    public GameObject water;
    public GameObject bridge;
    public GameObject enemy;
    public GameObject player;
    public GameObject obj;


    static void randomPos(int num,  ref HashSet<Vector2> arr, ref HashSet<Vector2> all, int lmin, int lmax, int rmin, int rmax)
    {
        System.Random rand = new System.Random();
        for (int i=0; i<num; i++)
        {
            while ( arr.Count < num)
            {
                Vector2 tmp = new Vector2(rand.Next(lmin, lmax), rand.Next(rmin, rmax));
                if (all.Contains(tmp) == false)
                {
                    arr.Add(tmp);
                    all.Add(tmp);
                }
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        height = Camera.main.orthographicSize *2f;
        width = height / (float)Screen.height * (float)Screen.width ;
        height = height - 3;
        width = width - 4;

        System.Random rand = new System.Random();
        wallnum = rand.Next(10, 20);
        treenum = rand.Next(7, 13);
        allpos = new HashSet<Vector2>();
        treepos = new HashSet<Vector2>();
        treepos2 = new HashSet<Vector2>();
        wallpos = new HashSet<Vector2>();
        playerpos = new HashSet<Vector2>();
        enemypos = new HashSet<Vector2>();

        randomPos(treenum, ref treepos, ref allpos, -(int)(width/2-1), -1, -(int)(height/2-3), (int)(height/2-1));
        randomPos(treenum, ref treepos2, ref allpos, 1, (int)(width/2-1), -(int)(height/2-3), (int)(height/2-1));
        randomPos(wallnum/2, ref wallpos, ref allpos, -(int)(width/2-1), -1, -(int)(height/2-3), (int)(height/2-1));
        randomPos(wallnum, ref wallpos, ref allpos, 1, (int)(width/2-1), -(int)(height/2-3), (int)(height/2-1));
        randomPos(num, ref playerpos, ref allpos, -(int)(width/2-1), -1, -(int)(height/2-3), (int)(height/2-1));
        randomPos(num, ref enemypos, ref allpos, 1, (int)(width/2-1), -(int)(height/2-3), (int)(height/2-1));

        for (int i=0; i<(int)width+1; i++)
        {
            for (int j=2; j<(int)height+1; j++)
            {
                x = i-(int)width/2;
                y = j-(int)height/2;
                Vector2 tmp = new Vector2(x, y);
                if (x == 0)
                {
                    if (y == 0)
                    {
                        obj = Instantiate(bridge, new Vector3(x,y,0), Quaternion.identity);
                        obj.transform.parent = squares.transform;
                    }
                    else
                    {
                        obj = Instantiate(water, new Vector3(x,y,0), Quaternion.identity);
                        obj.transform.parent = blocks.transform;
                    }
                }
                else if (treepos.Contains(tmp))
                {
                    obj = Instantiate(tree1, new Vector3(x,y,0), Quaternion.identity);
                    obj.transform.parent = blocks.transform;
                }
                else if (treepos2.Contains(tmp))
                {
                    obj = Instantiate(tree2, new Vector3(x,y,0), Quaternion.identity);
                    obj.transform.parent = blocks.transform;
                }
                else if (wallpos.Contains(tmp))
                {
                    obj = Instantiate(wall, new Vector3(x,y,0), Quaternion.identity);
                    obj.transform.parent = blocks.transform;
                }
                else
                {
                    obj = Instantiate(ground, new Vector3(x,y,0), Quaternion.identity);
                    obj.transform.parent = squares.transform;
                }
            }
        }

        foreach (Vector2 i in playerpos)
        {
            obj = Instantiate(player, new Vector3(i.x,i.y,0), Quaternion.identity);
            obj.transform.parent = blocks.transform;
        }
        foreach (Vector2 i in enemypos)
        {
            obj = Instantiate(enemy, new Vector3(i.x,i.y,0), Quaternion.identity);
            obj.transform.parent = blocks.transform;
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}

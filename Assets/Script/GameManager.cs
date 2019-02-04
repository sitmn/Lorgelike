using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool Playerturn;
    public bool PlayerMoving;
    public bool Enemymoving;
    public bool Menu;
    public bool coroutine;

    public int emoveY;
    public int emoveX;
    public int i,a;
    public int level;

    public LayerMask blockinglayer;

    private map_creat mapscript;
    

    public static GameManager instance = null;

    public GameObject Enemy;
    public GameObject Player;

    public List<Enemy_script> enemies;
    public List<Vector3> roomlist;
    public List<Vector3> entrancelist_0;
    public List<Vector3> entrancelist_1;
    public List<Vector3> entrancelist_2;
    public List<Vector3> entrancelist_3;
    public List<Vector3> entrancelist_4;
    public List<Vector3> entrancelist_5;
    public List<Vector3> entrancelist_6;
    public List<Vector3> entrancelist_7;
    public List<Vector3> entrancelist_8;

    public List<map_item> possessionitemlist;


    void Awake()
    {
        //シングルトン
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        //EnemyをListで管理
        enemies = new List<Enemy_script>();

        //itemをListで管理
        possessionitemlist = new List<map_item>();

        roomlist = new List<Vector3>();
        entrancelist_0 = new List<Vector3>();
        entrancelist_1 = new List<Vector3>();
        entrancelist_2 = new List<Vector3>();
        entrancelist_3 = new List<Vector3>();
        entrancelist_4 = new List<Vector3>();
        entrancelist_5 = new List<Vector3>();
        entrancelist_6 = new List<Vector3>();
        entrancelist_7 = new List<Vector3>();
        entrancelist_8 = new List<Vector3>();

    //コンポーネントを取得
    mapscript = GetComponent<map_creat>();
        
        //マップ生成
        mapscript.Mapcreat();


        instance.Playerturn = true;
        instance.Menu = false;
        instance.PlayerMoving = false;
        instance.Enemymoving = false;
        coroutine = false;

        emoveX = 0;
        emoveY = 0;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update () {
        if (instance.Playerturn == true || instance.Enemymoving == true|| instance.Menu ==true)
        {
            return;
        }
        

        if (coroutine == false)
        {
            coroutine = true;
            StartCoroutine(MoveEnemies());
        }
    }

    //Enemy全体の行動
    IEnumerator MoveEnemies()
    {
        //1ターンの最小時間を0.1秒に、Space押しながらで加速
        if (Input.GetKey(KeyCode.Space) == false /*&& map_creat.map[(int)Player.transform.position.x , (int)Player.transform.position.z] != 3*/ )
        {
            yield return new WaitForSeconds(0.1f);
        }
        
        
        //Enemyを1体ずつ移動
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Emove();
        }


        instance.Playerturn = true;
        instance.Enemymoving = false;
        coroutine = false;
    }
    

    //Enemyをリストに追加
    public void AddListenemy(Enemy_script script)
    {
        enemies.Add(script);
    }

    public void AddListItem(map_item item)
    {
        possessionitemlist.Add(item);
    }
    
    
}

public class map_state
{
    public int number;
    public int room_No;
    public Vector3 entrance_pos;
}
public class map_exist
{
    public int number;

    public int hp;
    public int attack;
    public int defence;

    public GameObject obj;
    public Player_script player_script;
    public Enemy_script enemy_script;
}

public class map_item
{
    public string name;
    public int number;
    public bool exist;
    public GameObject obj;
}

public class wall : map_state
{
    public wall()
    {
        number = 0;
    }
}
public class room : map_state
{
    public room()
    {
        number = 1;
    }
}
public class intersection : map_state
{
    public intersection()
    {
        number = 2;
    }
}
public class entrance : map_state
{
    public entrance()
    {
        number = 3;
    }
}

public class kaidan : map_state
{
    public kaidan()
    {
        number = 5;
    }
}

public class item1 : map_item
{
    public item1()
    {
        name = "薬草";
        number = 0;
        exist = true;
    }

    public void healuse(int listnum)
    {
        GameManager.instance.possessionitemlist.RemoveAt(listnum);
        //〇アイテム効果
    }
}
public class item2 : map_item
{
    public item2()
    {
        name = "爆弾";
        number = 1;
        exist = true;
    }
    public void attackuse(int listnum)
    {
        GameManager.instance.possessionitemlist.RemoveAt(listnum);
        //〇アイテム効果
    }
}
public class item3 : map_item
{
    public item3()
    {
        name = "場所替え";
        number = 2;
        exist = true;
    }
    public void changeuse(int listnum)
    {
        GameManager.instance.possessionitemlist.RemoveAt(listnum);
        //〇アイテム効果
    }
}
public class clean : map_item
{
    public clean()
    {
        number = 20;
        exist = false;
    }
}
public class test : map_state
{
    public test()
    {
        number = 99;
    }
}
public class aisle : map_state
{
    public aisle()
    {
        number = 10;
    }
}


public class player : map_exist
{
    public int test = 3;
    public player()
    {
        number = 5;
        hp = 10;
        attack = 1;
    }
}
public class enemy1 : map_exist
{

    public enemy1()
    {
        number = 6;
        hp = 1;
        attack = 1;
    }
}
public class enemy2 : map_exist
{

    public enemy2()
    {
        number = 6;
        hp = 2;
        attack = 1;
    }
}
public class enemy3 : map_exist
{

    public enemy3()
    {
        number = 6;
        hp = 3;
        attack = 1;
    }
}
public class clear : map_exist
{
    public clear()
    {
        number = 10;
    }
}
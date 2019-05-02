using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    

    public int floorlevel;
    public static GameManager instance = null;

    //移動用の1フレーム分の時間
    public float inverseMoveTime;
    public float moveTime = 0.1f;


    public bool Playerturn;
    public bool PlayerMoving;
    public int EnemyMoving;
    public bool Menu;
    public bool coroutine;
    public bool Pose;
    public bool one;
    public bool space;

    public int emoveY;
    public int emoveX;
    public int i, a;
    public int enemy_probability;

    public LayerMask blockinglayer;

    private Player_script player_script;
    private map_creat mapscript;
    private MenuController menuscript;


    public GameObject Enemy;
    private GameObject Player;
    public GameObject Menuobj;

    public List<Enemy_script> enemies;
    public List<Vector3> roomlist;
    public List<Vector3> roomlist_0;
    public List<Vector3> roomlist_1;
    public List<Vector3> roomlist_2;
    public List<Vector3> roomlist_3;
    public List<Vector3> roomlist_4;
    public List<Vector3> roomlist_5;
    public List<Vector3> roomlist_6;
    public List<Vector3> roomlist_7;
    public List<Vector3> roomlist_8;
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
    public List<map_item> possessionweaponlist;

    public List<Text> MainTexts;
    public Text[] MainText = new Text[3];
    
    public int MAX_TEXT = 50;
    
    public int MAX_ITEM = 20;
    public int MAX_WEAPON = 20;

    private int q = 0;
    public Text T;

    void Awake()
    {
        //シングルトン
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);

        MainTexts = new List<Text>();

        //EnemyをListで管理
        enemies = new List<Enemy_script>();
        

        //itemをListで管理
        possessionitemlist = new List<map_item>();
        possessionweaponlist = new List<map_item>();

        roomlist = new List<Vector3>();
        roomlist_0 = new List<Vector3>();
        roomlist_1 = new List<Vector3>();
        roomlist_2 = new List<Vector3>();
        roomlist_3 = new List<Vector3>();
        roomlist_4 = new List<Vector3>();
        roomlist_5 = new List<Vector3>();
        roomlist_6 = new List<Vector3>();
        roomlist_7 = new List<Vector3>();
        roomlist_8 = new List<Vector3>();

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
        menuscript = Menuobj.GetComponent<MenuController>();
        
            //マップ生成
            mapscript.Mapcreat();

        instance.PlayerMoving = false;
        instance.Playerturn = true;
        instance.Menu = false;
        instance.EnemyMoving = 0;
        coroutine = false;
        space = false;

        one = true;
        enemy_probability = 2;

        instance.Pose = false;
    }

    // Use this for initialization
    void Start()
    {

        inverseMoveTime = 1f / moveTime;
    }
    

    // Update is called once per frame
    void Update ()
    {

        if (instance.Playerturn == true || instance.Menu ==true || instance.Pose == true || coroutine == true || GameManager.instance.PlayerMoving == true)
        {
            return;
        }

        if(one == true)
        {
            //Awake、Startの代わり
            
            one = false;
            Player = GameObject.Find("Player");
            player_script = Player.GetComponent<Player_script>();
        }

        if (coroutine == false)
        {
            if (map_creat.map[(int)Player.transform.position.x, (int)Player.transform.position.z].number == 2 || map_creat.map[(int)Player.transform.position.x, (int)Player.transform.position.z].number == 10)
        {
            player.exist_room_no = 10;
        }
        else
        {
            player.exist_room_no = map_creat.map[(int)Player.transform.position.x, (int)Player.transform.position.z].room_No;
        }

        

        
            coroutine = true;
            StartCoroutine(MoveEnemies());
        }
    }
    
    //Enemy全体の行動
    IEnumerator MoveEnemies()
    {
        //1ターンの最小時間を0.1秒に、Space押しながらで加速
        /*if (GameManager.instance.space == false /*&& map_creat.map[(int)Player.transform.position.x , (int)Player.transform.position.z] != 3 )
        {
            yield return new WaitForSeconds(0.1f);
        }*/
        
        //敵がいない際、動きが早くなるのを防ぐ
        if(GameManager.instance.enemies.Count == 0)
        {
            yield return null;
            yield return null;
            yield return null;
            yield return null;
                
        }

        //Enemyを1体ずつ移動
        for (int i = 0; i < enemies.Count; i++)
            {
            //Debug.Log("A");
            enemies[i].Emove();
            //Debug.Log("F");
            /*if (Input.GetKey(KeyCode.Space)== false /*&& map_creat.map[(int)Player.transform.position.x , (int)Player.transform.position.z] != 3 )
                {
                    yield return null;
                    
                }
        */
                
            }

        //Playerが動ききってからプレイヤーのターンにする
        while (instance.EnemyMoving != GameManager.instance.enemies.Count)
        {
            yield return null;

        }

        //Debug.Log("G");

        //ランダムで敵を生成
        int random_enemy = Random.Range(0, 100);
        if (random_enemy <= enemy_probability)
        {
            int random = Random.Range(1, 3);
            mapscript.Random_Enemy_Instantiate(random);
        }

        GameManager.instance.EnemyMoving = 0;


        instance.Playerturn = true;
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

    public void AddListWeapon(map_item weapon)
    {
        possessionweaponlist.Add(weapon);
    }

    //文章の表示
    public void AddMainText(string sentence)
    {
        CancelInvoke("Text_Chancel");

        Text TEXT = Instantiate(T);
        TEXT.text = sentence;

        MainTexts.Add(TEXT);

        Debug.Log(MainText[0].text);

        if(MainText[0].text == "")
        {Debug.Log("A");
            MainText[0].text = MainTexts[MainTexts.Count - 1].text;
        }else if(MainText[1].text == ""){
            Debug.Log("B");
            MainText[0].text = MainTexts[MainTexts.Count - 2].text;
            MainText[1].text = MainTexts[MainTexts.Count - 1].text;
        }
        else
        {
            Debug.Log("C");
            if (MainTexts.Count > 2)
            {
                MainText[0].text = MainTexts[MainTexts.Count - 3].text;
                MainText[1].text = MainTexts[MainTexts.Count - 2].text;
                MainText[2].text = MainTexts[MainTexts.Count - 1].text;
            }
            else if (MainTexts.Count == 2)
            {
                MainText[0].text = MainTexts[MainTexts.Count - 2].text;
                MainText[1].text = MainTexts[MainTexts.Count - 1].text;
            }
            else if (MainTexts.Count == 1)
            {
                MainText[0].text = MainTexts[MainTexts.Count - 1].text;
            }
        }
        //保存した文章を古いものから削除
        if (MainTexts.Count > MAX_TEXT)
        {
            MainTexts.RemoveAt(0);
        }

        Invoke("Text_Chancel", 3);
    }
    //文章の表示を削除
    void Text_Chancel()
    {
        MainText[0].text = "";
        MainText[1].text = "";
        MainText[2].text = "";
    }

    public void itemuse(int listnum)
    {
        GameManager.instance.menuscript.BackButton();
        GameManager.instance.menuscript.BackButton();
        GameManager.instance.possessionitemlist.RemoveAt(listnum);
        

        GameManager.instance.Playerturn = false;
    }
    public void weaponuse(int listnum)
    {
        GameManager.instance.menuscript.BackButton();
        GameManager.instance.menuscript.BackButton();

        GameManager.instance.Playerturn = false;
    }
    
    public void Attack(int attack_range, int attack_type, bool slanting_wall, bool attack_through, int attack_damage)
    {
        player_script.Attack(attack_range, attack_type, slanting_wall, attack_through, attack_damage);
    }

}

public class map_state
{
    public int number;
    public int room_No;
    public Vector3 entrance_pos;
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

public class map_item
{
    public string name;
    public int number;
    public bool exist;
    public GameObject obj;
    public GameObject minimap_item;
    
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
        //〇アイテム効果
        GameManager.instance.AddMainText("薬草を使用した");

        int healpoint = 30;
        player.player_hp += healpoint;
        if(player.player_hp > player.player_MAX_hp)
        {
            player.player_hp = player.player_MAX_hp;
        }
        GameManager.instance.AddMainText("ＨＰが" + healpoint + "回復した");

        GameManager.instance.itemuse(listnum);
    }
}
public class item2 : map_item
{

    void Awake()
    {
    }
    
    public item2()
    {
        
        name = "爆弾";
        number = 0;
        exist = true;
    }
    public void attackuse(int listnum)
    {
        //〇アイテム効果
        GameManager.instance.AddMainText("爆弾を使用した");
        GameManager.instance.Attack(1, 1, true, true, 10);
        GameManager.instance.itemuse(listnum);
    }
}
public class item3 : map_item
{
    public item3()
    {
        name = "場所替え";
        number = 0;
        exist = true;
    }
    public void changeuse(int listnum)
    {
        //〇アイテム効果

        GameManager.instance.AddMainText("場所替えを使用した");
        GameManager.instance.itemuse(listnum);
    }
}

public class material1 : map_item
{
    public material1()
    {
        number = 1;
        exist = true;
    }
}
public class material2: map_item
{
    public material2()
    {
        number = 1;
        exist = true;
    }
}
public class material3 : map_item
{
    public material3()
    {
        number = 1;
        exist = true;
    }
}

public class weapon : map_item
{
    //↓武器用

    public int HP_W;
    public int ATTACK_W;
    public int DEFENSE_W;

    public int ATTACK_RANGE_W;
    public int ATTACK_TYPE_W;
    public bool ATTACK_THROUGH_W;
    public bool SLANTING_WALL_W;

    public void installing(int listnum)
    {
        if (GameManager.instance.possessionweaponlist[listnum].name.Contains("E:") == false)
        {
            player.player_MAX_hp = player.player_origin_hp + HP_W;
            if (player.player_MAX_hp <= player.player_hp)
            {
                player.player_hp = player.player_MAX_hp;
            }
            player.player_attack = player.player_origin_attack + ATTACK_W;
            player.player_defense = player.player_origin_defense + DEFENSE_W;

            player.player_attack_range = ATTACK_RANGE_W;
            player.player_attack_type = ATTACK_TYPE_W;
            player.player_attack_through = ATTACK_THROUGH_W;
            player.player_slanting_wall = SLANTING_WALL_W;

            for (int i = 0; i < GameManager.instance.possessionweaponlist.Count; i++)
            {
                if (GameManager.instance.possessionweaponlist[i].name.Contains("E:") == true)
                {
                    GameManager.instance.possessionweaponlist[i].name = GameManager.instance.possessionweaponlist[i].name.Replace("E:", "");
                }
            }
            GameManager.instance.AddMainText(GameManager.instance.possessionweaponlist[listnum].name + "を装備した");

            GameManager.instance.possessionweaponlist[listnum].name = GameManager.instance.possessionweaponlist[listnum].name.Insert(0, "E:");

            GameManager.instance.weaponuse(listnum);
        }else if(GameManager.instance.possessionweaponlist[listnum].name.Contains("E:") == true)
        {
            player.player_MAX_hp = player.player_origin_hp;
            if (player.player_MAX_hp <= player.player_hp)
            {
                player.player_hp = player.player_MAX_hp;
            }
            player.player_attack = player.player_origin_attack;
            player.player_defense = player.player_origin_defense;

            player.player_attack_range = 1;
            player.player_attack_type = 0;
            player.player_attack_through = false;
            player.player_slanting_wall = false;

            GameManager.instance.possessionweaponlist[listnum].name = GameManager.instance.possessionweaponlist[listnum].name.Replace("E:", "");

            GameManager.instance.AddMainText(GameManager.instance.possessionweaponlist[listnum].name + "を外した");

            GameManager.instance.weaponuse(listnum);
        }
    }
}
public class weapon1 : weapon
{
    public weapon1(string NAME, int hp_w, int attack_w , int defense_w , int attack_range_w , int attack_type_w , bool attack_through_w , bool slanting_wall_w)
    {
        exist = true;
        number = 2;
        name = NAME;

        HP_W = hp_w;
        ATTACK_W = attack_w;
        DEFENSE_W = defense_w;

        ATTACK_RANGE_W = attack_range_w;
        ATTACK_TYPE_W = attack_type_w;
        ATTACK_THROUGH_W = attack_through_w;
        SLANTING_WALL_W = slanting_wall_w;
    }   
}
public class weapon2 : weapon
{
    public weapon2(string NAME, int hp_w, int attack_w, int defense_w, int attack_range_w, int attack_type_w, bool attack_through_w, bool slanting_wall_w)
    {
        exist = true;
        number = 2;
        name = NAME;

        HP_W = hp_w;
        ATTACK_W = attack_w;
        DEFENSE_W = defense_w;

        ATTACK_RANGE_W = attack_range_w;
        ATTACK_TYPE_W = attack_type_w;
        ATTACK_THROUGH_W = attack_through_w;
        SLANTING_WALL_W = slanting_wall_w;
    }
}
public class weapon3 : weapon
{
    public weapon3(string NAME, int hp_w, int attack_w, int defense_w, int attack_range_w, int attack_type_w, bool attack_through_w, bool slanting_wall_w)
    {
        exist = true;
        number = 2;
        name = NAME; ;

        HP_W = hp_w;
        ATTACK_W = attack_w;
        DEFENSE_W = defense_w;

        ATTACK_RANGE_W = attack_range_w;
        ATTACK_TYPE_W = attack_type_w;
        ATTACK_THROUGH_W = attack_through_w;
        SLANTING_WALL_W = slanting_wall_w;
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

public class map_exist
{
    public int number;
    
    public GameObject obj;
    public Player_script player_script;
    public Enemy_script enemy_script;

    public enemystate state;
}

public class player : map_exist
{
    //初期ステータス
    public static int player_MAX_hp = 100;
    public static int player_origin_hp = 100;
    public static int player_hp = 100;
    public static int player_MAX_mp = 100;
    public static int player_origin_mp = 100;
    public static int player_mp = 100;
    public static int player_origin_attack = 3;
    public static int player_attack = 3;
    public static int player_origin_defense = 2;
    public static int player_defense = 2;

    //装備で変わる攻撃タイプ
    public static bool player_attack_through = false;
    public static int player_attack_range = 1;
    public static int player_attack_type = 0;
    public static bool player_slanting_wall = false;

    public static int player_experience = 0;
    public static int player_level = 1;
    public static int exist_room_no;

    public static weapon weapon;

    public player()
    {
        number = 5;
    }
}
public class enemy : map_exist
{
    public enemy()
    {
        number = 6;

    }
}
public class clear : map_exist
{
    public clear()
    {
        number = 10;
    }
}

public class enemystate
{
    public int MAX_HP;
    public int HP;
    public int MAX_MP;
    public int MP;
    public int MAX_ATTACK;
    public int ATTACK;
    public int MAX_DEFENSE;
    public int DEFENSE;
    public int RANGE_ATTACK;
    public int ATTACK_TYPE;
    public bool SLANTING_WALL;
    
    public enemystate(int max_hp,int hp,int max_mp,int mp,int max_attack, int attack, int max_defense, int defense, int range_attack, int attack_type , bool slanting_wall)
    {
        MAX_HP = max_hp;
        HP = hp;
        MAX_MP = max_mp;
        MP = mp;
        MAX_ATTACK = max_attack;
        ATTACK = attack;
        MAX_DEFENSE = max_defense;
        DEFENSE = defense;
        RANGE_ATTACK = range_attack;
        ATTACK_TYPE = attack_type;
        SLANTING_WALL = slanting_wall;
    }
}
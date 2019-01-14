using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool Playerturn;
    public bool Enemymoving;
    public bool Pose;
    public bool coroutine;

    public int emoveY;
    public int emoveX;
    public int i,a;
    public int level;

    public LayerMask blockinglayer;

    private map_creat mapscript;

    
    private Enemy_script enemyscript;
    private Player_script playerscript;

    public static GameManager instance = null;

    public GameObject Enemy;
    public GameObject Player;

    public List<Enemy_script> enemies;

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

        //コンポーネントを取得
        mapscript = GetComponent<map_creat>();
        enemyscript = Enemy.GetComponent<Enemy_script>();
        
        //マップ生成
        mapscript.Mapcreat();


        instance.Playerturn = true;
        instance.Pose = false;

        instance.Enemymoving = false;
        coroutine = false;

        emoveX = 0;
        emoveY = 0;
    }

    // Use this for initialization
    void Start()
    {
        playerscript = Player.GetComponent<Player_script>();
    }

    // Update is called once per frame
    void Update () {
        if (instance.Playerturn == true || instance.Enemymoving == true|| instance.Pose ==true)
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
        if (Input.GetKey(KeyCode.Space) == false /*×|| map_creat.map[(int)playerscript.transform.position.x, (int)playerscript.transform.position.z] == 3 || map_creat.map[(int)playerscript.transform.position.x, (int)playerscript.transform.position.z] == 4 || map_creat.map[(int)playerscript.transform.position.x, (int)playerscript.transform.position.z] == 5*/)
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

    private void OnLevelWasLoaded(int index)
    {
        

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool Playerturn;
    public bool Enemymoving;
    public bool Pose;
    public bool coroutine;
    public bool kasoku;
    

    public int emoveY;
    public int emoveX;
    public int i,a;
    public int level;

    public LayerMask blockinglayer;

    map_creat mapscript;

    
    Enemy_script enemyscript;

    public static GameManager instance = null;

    public GameObject Enemy;
    public GameObject Player;

    public List<Enemy_script> enemies;

    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        enemies = new List<Enemy_script>();

        mapscript = GetComponent<map_creat>();
        //playerscript = Player.GetComponent<Player_script>();
        enemyscript = Enemy.GetComponent<Enemy_script>();
        
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
        
        
    }

    // Update is called once per frame
    void Update () {
        if (instance.Playerturn == true || instance.Enemymoving == true|| instance.Pose ==true)
        {
            
            return;
        }
        /*
        for (i = 0; i < enemies.Count; i++) {
            enemies[i].Emove();
            
            for (a = 0; a < 100; a++)
            {
                Debug.Log(a);
            }
            
        }


        
        */
        if (coroutine == false)
        {
            coroutine = true;
            StartCoroutine(MoveEnemies());
            
        }
    }

   IEnumerator MoveEnemies()
    {
        kasoku = Input.GetKey(KeyCode.Space);
        if (kasoku == false)
        {
            yield return new WaitForSeconds(0.2f);
        }
        if (enemies.Count == 0)
        {
            //yield return new WaitForSeconds(0.05f);
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Emove();
            //yield return new WaitForSeconds(0.05f);
        }
        instance.Playerturn = true;
        instance.Enemymoving = false;
        coroutine = false;
    }


    //階段
    /*private void OnLevelWasLoaded(int index)
    {
        level++;
        mapscript.Mapcreat();
    }*/


    public void AddListenemy(Enemy_script script)
    {
        enemies.Add(script);
    }

    private void OnLevelWasLoaded(int index)
    {
        

    }


}

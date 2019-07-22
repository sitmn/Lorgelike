using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_script : MonoBehaviour
{

    private int i;
    private int emoveX, emoveY;
    
    private bool enotmove,playerfind, enemyattack;
    private bool smoothmove;
    
    private Vector3 enemypos;
    private Vector3 destination;

    private GameObject Player;
    private Player_script player_script;
    public GameObject minimap_enemy;

    private map_creat mapscript;

    private Animator myAnimator;
    private Animator playerAnimator;
    
    void Start()
    {
        GameObject obj2 = Instantiate(minimap_enemy, new Vector3((int)transform.position.x + map_creat.minimapdistance, 1, (int)transform.position.z + map_creat.minimapdistance), Quaternion.identity);
        obj2.transform.parent = transform;
        obj2.transform.SetSiblingIndex(0);
        //このEnemyが生成された時、このスクリプトをListに追加
        GameManager.instance.AddListenemy(this);

        Player = GameObject.Find("Player");
        player_script = Player.GetComponent<Player_script>();
        this.enotmove = false;
        this.playerfind = false;
        destination = new Vector3(0,0,0);
        this.smoothmove = false;

        myAnimator = GetComponent<Animator>();
        playerAnimator = Player.GetComponent<Animator>();
    }

    void Update()
    {
        transform.GetChild(0).transform.position = new Vector3(transform.position.x + map_creat.minimapdistance, 1, transform.position.z + map_creat.minimapdistance);

        //マッピング外の場合オブジェクトを消しておく
        if (map_creat.mini_map[(int)transform.position.x , (int)transform.position.z].activeSelf == true)
        {
            if(transform.GetChild(0).gameObject.activeSelf == false)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }else if (map_creat.mini_map[(int)transform.position.x, (int)transform.position.z].activeSelf == false)
        {
            if (transform.GetChild(0).gameObject.activeSelf == true)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }


    //敵の行動
    public void Emove(){
        StartCoroutine(EmoveC());
    }
    IEnumerator EmoveC()
    {
        while (GameManager.instance.enemyattack)
        {
            yield return null;
        }

        FindPlayer();
        enemypos = transform.position;
        
        if (this.playerfind == true)
        {
            FindPlayerMove();
            
        }
        else
        {
            if (map_creat.map[(int)transform.position.x, (int)transform.position.z].number == 2 ||map_creat.map[(int)transform.position.x, (int)transform.position.z].number == 10)
            {
                AisleMove();
                

                if (map_creat.map[(int)transform.position.x , (int)transform.position.z].number == 3)
                {
                    destination = new Vector3(0, 0, 0);
                }
            }else if (map_creat.map[(int)transform.position.x, (int)transform.position.z].number == 1 || map_creat.map[(int)transform.position.x, (int)transform.position.z].number == 3 || map_creat.map[(int)transform.position.x, (int)transform.position.z].number == 5)
            {
                RoomMove();
                
            }
        }

        if(smoothmove == false)
        {
            GameManager.instance.EnemyMoving++;
        }

        this.enemyattack = false;
        this.enotmove = false;
        this.emoveX = 0;
        this.emoveY = 0;

        //完全ランダム移動
        /*while (this.enotmove == true)
        { 
            i = Random.Range(0, 4);
            switch (i)
            {
                case 0:
                    emoveX = 1;
                    emoveY = 0;
                    transform.Rotate(0, 0, 0,Space.World);
                
                break;

                case 1:
                    emoveX = 0;
                    emoveY = 1;
                    transform.Rotate(0, 90, 0, Space.World);

                    break;

                case 2:
                    emoveX = -1;
                    emoveY = 0;
                    transform.Rotate(0, 180, 0, Space.World);

                    break;

                case 3:
                    emoveX = 0;
                    emoveY = -1;
                    transform.Rotate(0, 270, 0, Space.World);

                    break;

            }

　　　　//動けるかどうかの判定
        this.enotmove = Physics.Linecast(transform.position, transform.position + new Vector3(emoveX, 0, emoveY), blockinglayer);
            if (this.enotmove == false)
            {
        　　　　transform.position += new Vector3(emoveX, 0, emoveY);
                GameManager.instance.Enemymoving = false;
            } 
        }*/

    }


    private void FindPlayer()
    {
        if(Mathf.Abs(transform.position.x - Player.transform.position.x) + Mathf.Abs(transform.position.z - Player.transform.position.z) <= 5)
        {
            playerfind = true;
        }
        else
        {
            playerfind = false;
        }
    }

    
    //部屋に入ったときの移動
    private void RoomMove()
    {
        int randam_entrance;
        if (destination == new Vector3(0, 0, 0))//目的地（入口）がない場合、目的地を設定
        {
            if (map_creat.map[(int)transform.position.x, (int)transform.position.z].room_No == 0)
            {

                if (GameManager.instance.entrancelist_0.Count == 1)
                {
                    destination = GameManager.instance.entrancelist_0[0];

                }
                else if (GameManager.instance.entrancelist_0.Count != 1)
                {
                    randam_entrance = Random.Range(0, GameManager.instance.entrancelist_0.Count);
                    destination = GameManager.instance.entrancelist_0[randam_entrance];
                    while (destination == enemypos)
                    {
                        randam_entrance = Random.Range(0, GameManager.instance.entrancelist_0.Count);
                        destination = GameManager.instance.entrancelist_0[randam_entrance];
                    }
                }
            }

            else if (map_creat.map[(int)transform.position.x, (int)transform.position.z].room_No == 1)
            {
                if (GameManager.instance.entrancelist_1.Count == 1)
                {
                    destination = GameManager.instance.entrancelist_1[0];
                }
                else if (GameManager.instance.entrancelist_1.Count != 1)
                {
                    randam_entrance = Random.Range(0, GameManager.instance.entrancelist_1.Count);
                    destination = GameManager.instance.entrancelist_1[randam_entrance];
                    while (destination == enemypos)
                    {
                        randam_entrance = Random.Range(0, GameManager.instance.entrancelist_1.Count);
                        destination = GameManager.instance.entrancelist_1[randam_entrance];
                    }
                }
            }
            else if (map_creat.map[(int)transform.position.x, (int)transform.position.z].room_No == 2)
            {
                if (GameManager.instance.entrancelist_2.Count == 1)
                {
                    destination = GameManager.instance.entrancelist_2[0];
                }
                else if (GameManager.instance.entrancelist_2.Count != 1)
                {
                    randam_entrance = Random.Range(0, GameManager.instance.entrancelist_2.Count);
                    destination = GameManager.instance.entrancelist_2[randam_entrance];
                    while (destination == enemypos)
                    {
                        randam_entrance = Random.Range(0, GameManager.instance.entrancelist_2.Count);
                        destination = GameManager.instance.entrancelist_2[randam_entrance];
                    }
                }
            }
            else if (map_creat.map[(int)transform.position.x, (int)transform.position.z].room_No == 3)
            {
                if (GameManager.instance.entrancelist_3.Count == 1)
                {
                    destination = GameManager.instance.entrancelist_3[0];
                }
                else if (GameManager.instance.entrancelist_3.Count != 1)
                {
                    randam_entrance = Random.Range(0, GameManager.instance.entrancelist_3.Count);
                    destination = GameManager.instance.entrancelist_3[randam_entrance];
                    while (destination == enemypos)
                    {
                        randam_entrance = Random.Range(0, GameManager.instance.entrancelist_3.Count);
                        destination = GameManager.instance.entrancelist_3[randam_entrance];
                    }
                }
            }
            else if (map_creat.map[(int)transform.position.x, (int)transform.position.z].room_No == 4)
            {
                if (GameManager.instance.entrancelist_4.Count == 1)
                {
                    destination = GameManager.instance.entrancelist_4[0];
                }
                else if (GameManager.instance.entrancelist_4.Count != 1)
                {
                    randam_entrance = Random.Range(0, GameManager.instance.entrancelist_4.Count);
                    destination = GameManager.instance.entrancelist_4[randam_entrance];
                    while (destination == enemypos)
                    {
                        randam_entrance = Random.Range(0, GameManager.instance.entrancelist_4.Count);
                        destination = GameManager.instance.entrancelist_4[randam_entrance];
                    }
                }
            }
            else if (map_creat.map[(int)transform.position.x, (int)transform.position.z].room_No == 5)
            {
                if (GameManager.instance.entrancelist_5.Count == 1)
                {
                    destination = GameManager.instance.entrancelist_5[0];
                }
                else if (GameManager.instance.entrancelist_5.Count != 1)
                {
                    randam_entrance = Random.Range(0, GameManager.instance.entrancelist_5.Count);
                    destination = GameManager.instance.entrancelist_5[randam_entrance];
                    while (destination == enemypos)
                    {
                        randam_entrance = Random.Range(0, GameManager.instance.entrancelist_5.Count);
                        destination = GameManager.instance.entrancelist_5[randam_entrance];
                    }
                }
            }
            else if (map_creat.map[(int)transform.position.x, (int)transform.position.z].room_No == 6)
            {
                if (GameManager.instance.entrancelist_6.Count == 1)
                {
                    destination = GameManager.instance.entrancelist_6[0];
                }
                else if (GameManager.instance.entrancelist_6.Count != 1)
                {
                    randam_entrance = Random.Range(0, GameManager.instance.entrancelist_6.Count);
                    destination = GameManager.instance.entrancelist_6[randam_entrance];
                    while (destination == enemypos)
                    {
                        randam_entrance = Random.Range(0, GameManager.instance.entrancelist_6.Count);
                        destination = GameManager.instance.entrancelist_6[randam_entrance];
                    }
                }
            }
            else if (map_creat.map[(int)transform.position.x, (int)transform.position.z].room_No == 7)
            {
                if (GameManager.instance.entrancelist_7.Count == 1)
                {
                    destination = GameManager.instance.entrancelist_7[0];
                }
                else if (GameManager.instance.entrancelist_7.Count != 1)
                {
                    randam_entrance = Random.Range(0, GameManager.instance.entrancelist_7.Count);
                    destination = GameManager.instance.entrancelist_7[randam_entrance];
                    while (destination == enemypos)
                    {
                        randam_entrance = Random.Range(0, GameManager.instance.entrancelist_7.Count);
                        destination = GameManager.instance.entrancelist_7[randam_entrance];
                    }
                }
            }
            else if (map_creat.map[(int)transform.position.x, (int)transform.position.z].room_No == 8)
            {
                if (GameManager.instance.entrancelist_8.Count == 1)
                {
                    
                    destination = GameManager.instance.entrancelist_8[0];
                }
                else if (GameManager.instance.entrancelist_8.Count != 1)
                {
                    randam_entrance = Random.Range(0, GameManager.instance.entrancelist_8.Count);
                    destination = GameManager.instance.entrancelist_8[randam_entrance];
                    while (destination == enemypos)
                    {
                        randam_entrance = Random.Range(0, GameManager.instance.entrancelist_8.Count);
                        destination = GameManager.instance.entrancelist_8[randam_entrance];
                    }
                }
            }
        }
        
        
        if (destination == transform.position)//目的地に着いたとき、部屋から出る
        {
            int r = Random.Range(0, 11);
            if (r <= 5)
            {//左回り
                transform.eulerAngles = new Vector3(0, 270 + Player_script.asset_rotate , 0);
                
                while ((((int)(transform.eulerAngles.y + 0.5) / 90) % 4 == 1 /*0*/ && map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number != 10 /*(map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 1 || map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 3)*/) ||
                    (((int)(transform.eulerAngles.y + 0.5) / 90) % 4 == 2 /*1*/ && map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number != 10 /*(map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 1 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 3)*/) ||
                    (((int)(transform.eulerAngles.y + 0.5) / 90) % 4 == 3 /*2*/ && map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number != 10 /*(map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 1 || map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 3)*/) ||
                    (((int)(transform.eulerAngles.y + 0.5) / 90) % 4 == 0 /*3*/ && map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number != 10 /*(map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 1 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 3)*/))
                {
                    transform.Rotate(new Vector3(0, 90, 0));
                }
                ForwardMove();
                
            }
            else if (r >= 6)
            {//右回り
                transform.eulerAngles = new Vector3(0, 90 + Player_script.asset_rotate, 0);
                
                while ((((int)(transform.eulerAngles.y + 0.5) / 90) % 4 == 1 /*0*/ && map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number != 10 /*(map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 1 || map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 3)*/) ||
                    (((int)(transform.eulerAngles.y + 0.5) / 90) % 4 == 2 /*1*/ && map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number != 10 /*(map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 1 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 3)*/) ||
                    (((int)(transform.eulerAngles.y + 0.5) / 90) % 4 == 3 /*2*/ && map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number != 10 /*(map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 1 || map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 3)*/) ||
                    (((int)(transform.eulerAngles.y + 0.5) / 90) % 4 == 0 /*3*/ && map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number != 10 /*(map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 1 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 3)*/))
                {
                    transform.Rotate(new Vector3(0, 270, 0));
                }
                ForwardMove();
            }//移動先にプレイヤーと敵がいないならば移動
            if (map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY].number == 5 || map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY].number == 6 || map_creat.map[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY].number == 0)
            {
                enotmove = true;
            }
            if (enotmove == false) { 
                map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY] = map_creat.map_ex[(int)transform.position.x, (int)transform.position.z];
                map_creat.map_ex[(int)transform.position.x, (int)transform.position.z] = new clear();
                //加速と通常時の移動
                if (GameManager.instance.space == true)
                {
                    transform.localPosition = enemypos;

                    destination = new Vector3(0, 0, 0);
                }
                else if (GameManager.instance.space == false)
                {
                    StartCoroutine(SmoothMovement(enemypos));

                    destination = new Vector3(0, 0, 0);
                }

            }
            else if (enotmove == true)//正面が移動できないものならターン
            {
                enotmove = false;
                transform.Rotate(new Vector3(0, 90, 0));
                RotateLeft();
                ForwardMove();
                if (map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY].number == 5 || map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY].number == 6 || map_creat.map[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY].number == 0)
                {
                    enotmove = true;
                }
                if (enotmove == false)
                {
                    map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY] = map_creat.map_ex[(int)transform.position.x, (int)transform.position.z];
                    map_creat.map_ex[(int)transform.position.x, (int)transform.position.z] = new clear();
                    //加速と通常時の移動
                    if (GameManager.instance.space == true)
                    {
                        transform.localPosition = enemypos;

                        destination = new Vector3(0, 0, 0);
                    }
                    else if (GameManager.instance.space == false)
                    {
                        StartCoroutine(SmoothMovement(enemypos));


                        destination = new Vector3(0, 0, 0);
                    }
                }
            }
        }
        else if (transform.position != destination)//目的地でない場合、目的地に向かう
        {
            int entrancedistance_x = (int)(transform.position.x - destination.x);
            int entrancedistance_y = (int)(transform.position.z - destination.z);

            if (entrancedistance_x == 0)
            {
                if (entrancedistance_y < 0)
                {
                    transform.eulerAngles = new Vector3(0, 270 + Player_script.asset_rotate, 0);
                    if (map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)
                    {
                        playerfind = false;
                        enotmove = true;
                    }
                }
                else if (entrancedistance_y > 0)
                {
                    transform.eulerAngles = new Vector3(0, 90 + Player_script.asset_rotate, 0);
                    if (map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)
                    {
                        playerfind = false;
                        enotmove = true;
                    }
                }
            }
            else if (entrancedistance_y == 0)
            {
                if (entrancedistance_x < 0)
                {
                    transform.eulerAngles = new Vector3(0, 0 + Player_script.asset_rotate, 0);
                    if (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0)
                    {
                        playerfind = false;
                        enotmove = true;
                    }
                }
                else if (entrancedistance_x > 0)
                {
                    transform.eulerAngles = new Vector3(0, 180 + Player_script.asset_rotate, 0);
                    if (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0)
                    {
                        playerfind = false;
                        enotmove = true;
                    }
                }
            }
            else
            {
                if (entrancedistance_x < 0 && entrancedistance_y < 0)    //斜め移動時、壁があれば十字移動で近づく、近づくための場所全てが壁なら見失う
                {
                    transform.eulerAngles = new Vector3(0, 315 + Player_script.asset_rotate, 0);
                    if (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z + 1].number == 0 && map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 && map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)
                    {
                        playerfind = false;
                        enotmove = true;
                    }
                    else if (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z + 1].number == 0 || map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)
                    {
                        int r = Random.Range(0, 2);
                        if (r == 0)
                        {
                            if (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 270 + Player_script.asset_rotate, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 0 + Player_script.asset_rotate, 0);
                            }
                        }
                        else
                        {
                            if (map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 0 + Player_script.asset_rotate, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 270 + Player_script.asset_rotate, 0);
                            }
                        }
                    }
                }
                else if (entrancedistance_x > 0 && entrancedistance_y < 0)
                {
                    transform.eulerAngles = new Vector3(0, 225 + Player_script.asset_rotate, 0);
                    if (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z + 1].number == 0 && map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 && map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)
                    {
                        playerfind = false;
                        enotmove = true;
                    }
                    else if (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z + 1].number == 0 || map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)
                    {
                        int r = Random.Range(0, 2);
                        if (r == 0)
                        {
                            if (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 270 + Player_script.asset_rotate, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 180 + Player_script.asset_rotate, 0);
                            }
                        }
                        else
                        {
                            if (map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 180 + Player_script.asset_rotate, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 270 + Player_script.asset_rotate, 0);
                            }
                        }
                    }
                }
                else if (entrancedistance_x > 0 && entrancedistance_y > 0)
                {
                    transform.eulerAngles = new Vector3(0, 135 + Player_script.asset_rotate, 0);
                    if (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z - 1].number == 0 && map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 && map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)
                    {
                        playerfind = false;
                        enotmove = true;
                    }
                    else if (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z - 1].number == 0 || map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)
                    {
                        int r = Random.Range(0, 2);
                        if (r == 0)
                        {
                            if (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 90 + Player_script.asset_rotate, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 180 + Player_script.asset_rotate, 0);
                            }
                        }
                        else
                        {
                            if (map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 180 + Player_script.asset_rotate, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 90 + Player_script.asset_rotate, 0);
                            }
                        }
                    }
                }
                else if (entrancedistance_x < 0 && entrancedistance_y > 0)
                {
                    transform.eulerAngles = new Vector3(0, 45 + Player_script.asset_rotate, 0);
                    if (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z - 1].number == 0 && map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 && map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)
                    {
                        playerfind = false;
                        enotmove = true;
                    }
                    else if (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z - 1].number == 0 || map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)
                    {
                        int r = Random.Range(0, 2);
                        if (r == 0)
                        {
                            if (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 90 + Player_script.asset_rotate, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 0 + Player_script.asset_rotate, 0);
                            }
                        }
                        else
                        {
                            if (map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 0 + Player_script.asset_rotate, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 90 + Player_script.asset_rotate, 0);
                            }
                        }
                    }
                }
            }
            ForwardMove();

            //移動先にプレイヤーと敵がいないならば移動
            if (map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY].number == 5 || map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY].number == 6)
            {
                enotmove = true;
            }
            if (enotmove == false)
            {
                map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY] = map_creat.map_ex[(int)transform.position.x, (int)transform.position.z];
                map_creat.map_ex[(int)transform.position.x, (int)transform.position.z] = new clear();
                //加速と通常時の移動
                if (GameManager.instance.space == true)
                {
                    transform.localPosition = enemypos;

                }
                else if (GameManager.instance.space == false)
                {
                    StartCoroutine(SmoothMovement(enemypos));
                }
            }
            else if (enotmove == true)//正面が移動できないものならターン
            {
                
                enotmove = false;
                transform.Rotate(new Vector3(0, 90, 0));
                RotateLeft();
                ForwardMove();
                if (map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY].number == 5 || map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY].number == 6)
                {
                    enotmove = true;
                }
                if (enotmove == false)
                {
                    map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY] = map_creat.map_ex[(int)transform.position.x, (int)transform.position.z];
                    map_creat.map_ex[(int)transform.position.x, (int)transform.position.z] = new clear();
                    //加速と通常時の移動
                    if (GameManager.instance.space == true)
                    {
                        transform.localPosition = enemypos;

                    }
                    else if (GameManager.instance.space == false)
                    {
                        StartCoroutine(SmoothMovement(enemypos));
                    }
                }
            }
        }
    }

    //プレイヤーを見つけたときの移動
    private void FindPlayerMove()
    {
        int attack_range = map_creat.map_ex[(int)transform.position.x, (int)transform.position.z].state.RANGE_ATTACK;
        int attack_type = map_creat.map_ex[(int)transform.position.x, (int)transform.position.z].state.ATTACK_TYPE;
        int distance_x = (int)(transform.position.x - Player.transform.position.x);
        int distance_y = (int)(transform.position.z - Player.transform.position.z);
        bool slanting_wall = map_creat.map_ex[(int)transform.position.x, (int)transform.position.z].state.SLANTING_WALL;

        EnemyAttack(attack_range, attack_type, distance_x, distance_y , slanting_wall);
        
        
        if(enemyattack == false) {          //プレイやーとの位置関係によって角度を変更
            if (distance_x == 0)
            {
                if (distance_y < 0)
                {
                    transform.eulerAngles = new Vector3(0, 270 + Player_script.asset_rotate, 0);
                    if(map_creat.map[(int)transform.position.x , (int)transform.position.z + 1].number == 0){
                        playerfind = false;
                        enotmove = true;
                    }
                }
                else if (distance_y > 0)
                {
                    transform.eulerAngles = new Vector3(0, 90 + Player_script.asset_rotate, 0);
                    if (map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)
                    {
                        playerfind = false;
                        enotmove = true;
                    }
                }
            }
            else if (distance_y == 0)
            {
                if (distance_x < 0)
                {
                    transform.eulerAngles = new Vector3(0, 0 + Player_script.asset_rotate, 0);
                    if (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0)
                    {
                        playerfind = false;
                        enotmove = true;
                    }
                }
                else if (distance_x > 0)
                {
                    transform.eulerAngles = new Vector3(0, 180 + Player_script.asset_rotate, 0);
                    if (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0)
                    {
                        playerfind = false;
                        enotmove = true;
                    }
                }
            }
            else
            {
                if(distance_x < 0 && distance_y < 0)    //斜め移動時、壁があれば十字移動で近づく、近づくための場所全てが壁なら見失う
                {
                    transform.eulerAngles = new Vector3(0, 315 + Player_script.asset_rotate, 0);
                    if(map_creat.map[(int)transform.position.x + 1 , (int)transform.position.z + 1].number == 0 && map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 && map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)
                    {
                        playerfind = false;
                        enotmove = true;
                    }
                    else if(map_creat.map[(int)transform.position.x + 1, (int)transform.position.z + 1].number == 0 || map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)
                    {
                        int r = Random.Range(0, 2);
                        if(r == 0)
                        {
                            if(map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 270 + Player_script.asset_rotate, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 0 + Player_script.asset_rotate, 0);
                            }
                        }
                        else
                        {
                            if (map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 0 + Player_script.asset_rotate, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 270 + Player_script.asset_rotate, 0);
                            }
                        }
                    }
                }else if(distance_x > 0 && distance_y < 0)
                {
                    transform.eulerAngles = new Vector3(0, 225 + Player_script.asset_rotate, 0);
                    if (map_creat.map[(int)transform.position.x -1 , (int)transform.position.z + 1].number == 0 && map_creat.map[(int)transform.position.x -1, (int)transform.position.z].number == 0 && map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)
                    {
                        playerfind = false;
                        enotmove = true;
                    }
                    else if (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z + 1].number == 0 || map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)
                    {
                        int r = Random.Range(0, 2);
                        if (r == 0)
                        {
                            if (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 270 + Player_script.asset_rotate, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 180 + Player_script.asset_rotate, 0);
                            }
                        }
                        else
                        {
                            if (map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 180 + Player_script.asset_rotate, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 270 + Player_script.asset_rotate, 0);
                            }
                        }
                    }
                }
                else if(distance_x > 0 && distance_y > 0)
                {
                    transform.eulerAngles = new Vector3(0, 135 + Player_script.asset_rotate, 0);
                    if (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z - 1].number == 0 && map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 && map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)
                    {
                        playerfind = false;
                        enotmove = true;
                    }
                    else if (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z - 1].number == 0 || map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)
                    {
                        int r = Random.Range(0, 2);
                        if (r == 0)
                        {
                            if (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 90 + Player_script.asset_rotate, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 180 + Player_script.asset_rotate, 0);
                            }
                        }
                        else
                        {
                            if (map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 180 + Player_script.asset_rotate, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 90 + Player_script.asset_rotate, 0);
                            }
                        }
                    }
                }
                else if(distance_x < 0 && distance_y > 0)
                {
                    transform.eulerAngles = new Vector3(0, 45 + Player_script.asset_rotate, 0);
                    if (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z - 1].number == 0 && map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 && map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)
                    {
                        playerfind = false;
                        enotmove = true;
                    }
                    else if (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z - 1].number == 0 || map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)
                    {
                        int r = Random.Range(0, 2);
                        if (r == 0)
                        {
                            if (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 90 + Player_script.asset_rotate, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 0 + Player_script.asset_rotate, 0);
                            }
                        }
                        else
                        {
                            if (map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 0 + Player_script.asset_rotate, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 90 + Player_script.asset_rotate, 0);
                            }
                        }
                    }
                }
            }
            ForwardMove();

            //移動先にプレイヤーと敵がいないならば移動
            if (map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY].number == 5 || map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY].number == 6)
            {
                enotmove = true;
            }
            if (enotmove == false)
            {
                map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY] = map_creat.map_ex[(int)transform.position.x, (int)transform.position.z];
                map_creat.map_ex[(int)transform.position.x, (int)transform.position.z] = new clear();
                //加速と通常時の移動
                if (GameManager.instance.space == true)
                {
                    transform.localPosition = enemypos;

                }
                else if (GameManager.instance.space == false)
                {
                    StartCoroutine(SmoothMovement(enemypos));
                }
            }
        }else if(enemyattack == true)
        {
            //敵攻撃中は加速停止
            GameManager.instance.space = false;

            //〇プレイヤー方向を向く

            if (distance_x == -1 && distance_y == 0)
            {
                transform.eulerAngles = new Vector3(0, 0 + Player_script.asset_rotate, 0);
            }else if(distance_x == 0 && distance_y == 1)
            {
                transform.eulerAngles = new Vector3(0, 90 + Player_script.asset_rotate, 0);
            }else if(distance_x == 1 && distance_y == 0)
            {
                transform.eulerAngles = new Vector3(0, 180 + Player_script.asset_rotate, 0);
            }else if(distance_x == 0 && distance_y == -1)
            {
                transform.eulerAngles = new Vector3(0, 270 + Player_script.asset_rotate, 0);
            }
            else if (distance_x == -1 && distance_y == 1)
            {
                transform.eulerAngles = new Vector3(0, 45 + Player_script.asset_rotate, 0);
            }
            else if (distance_x == 1 && distance_y == 1)
            {
                transform.eulerAngles = new Vector3(0, 135 + Player_script.asset_rotate, 0);
            }
            else if (distance_x == 1 && distance_y == -1)
            {
                transform.eulerAngles = new Vector3(0, 225 + Player_script.asset_rotate, 0);
            }
            else if (distance_x == -1 && distance_y == -1)
            {
                transform.eulerAngles = new Vector3(0, 315 + Player_script.asset_rotate, 0);
            }
            StartCoroutine(AttackAnimation());
            
        }
    }

    //設置物を見つけたときの移動
    private void AisleMove()
    {
        //通路の分岐点でランダムに曲がる
        if (map_creat.map[(int)transform.position.x, (int)transform.position.z].number == 2)
        {
            int r = Random.Range(0, 2);
            //左に曲がる
            if (r == 0) {
                transform.Rotate(new Vector3(0, 270, 0));

                //正面が壁ならば90度回転,正面が壁でなくなるまでループ
                RotateLeft();
            }
            //右に曲がる
            else{
                transform.Rotate(new Vector3(0, 90, 0));

                //正面が壁ならば270度回転,正面が壁でなくなるまでループ
                RotateRight();
            }

        }
        else if(map_creat.map[(int)transform.position.x, (int)transform.position.z].number == 10)
        
        {

            //正面が壁ならば90度回転,正面が壁でなくなるまでループ
            RotateLeft();
        }
        
        ForwardMove();

        //移動先にプレイヤーと敵がいないならば移動
        if(map_creat.map_ex[(int)transform.position.x + emoveX , (int)transform.position.z + emoveY].number == 5 || map_creat.map_ex[(int)transform.position.x + emoveX , (int)transform.position.z + emoveY].number == 6)
        {
            enotmove = true;
        }
        if (enotmove == false) {
            map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY] = map_creat.map_ex[(int)transform.position.x, (int)transform.position.z];
            map_creat.map_ex[(int)transform.position.x, (int)transform.position.z] = new clear();
            //加速と通常時の移動
            if (GameManager.instance.space == true)
            {
                transform.localPosition = enemypos;

            }
            else if (GameManager.instance.space == false)
            {
                StartCoroutine(SmoothMovement(enemypos));
            }
        }
        else if(enotmove == true)//正面が移動できないものならターン
        {
            enotmove = false;
            transform.Rotate(new Vector3(0, 90, 0));
            RotateLeft();
            ForwardMove();
            if (map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY].number == 5 || map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY].number == 6)
            {
                enotmove = true;
            }
            if (enotmove == false)
            {
                map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY] = map_creat.map_ex[(int)transform.position.x, (int)transform.position.z];
                map_creat.map_ex[(int)transform.position.x, (int)transform.position.z] = new clear();
                //加速と通常時の移動
                if (GameManager.instance.space == true)
                {
                    transform.localPosition = enemypos;

                }
                else if (GameManager.instance.space == false)
                {
                    StartCoroutine(SmoothMovement(enemypos));
                }
            }
        }
    }

    

    //左回りで分岐探索
    void RotateLeft()
    {
        int angle = (int)(transform.eulerAngles.y+ 0.5);
        while (((angle / 45) % 8 == 2 /*0*/ && map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0) ||
                    ((angle / 45) % 8 == 4 /*2*/ && map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0) ||
                    ((angle / 45) % 8 == 6 /*4*/ && map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0) ||
                    ((angle / 45) % 8 == 0 /*6*/ && map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0) ||
                    ((angle / 45) % 8 == 3 /*1*/ && (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z - 1].number == 0 || map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)) ||
                    ((angle / 45) % 8 == 5 /*3*/ && (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z - 1].number == 0 || map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)) ||
                    ((angle / 45) % 8 == 7 /*5*/ && (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z + 1].number == 0 || map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)) ||
                    ((angle / 45) % 8 == 1 /*7*/ && (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z + 1].number == 0 || map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)))
        {
            transform.Rotate(new Vector3(0, 45, 0));
            angle = (int)(transform.eulerAngles.y + 0.5);
        }
    }
    //右回りで分岐探索
    void RotateRight()
    {   int angle = (int)(transform.eulerAngles.y + 0.5);
        while (((angle / 45) % 8 == 2 /*0*/ && map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0) ||
                    ((angle / 45) % 8 == 4 /*2*/ && map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0) ||
                    ((angle / 45) % 8 == 6 /*4*/ && map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0) ||
                    ((angle / 45) % 8 == 0 /*6*/ && map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0) ||
                    ((angle / 45) % 8 == 3 /*1*/ && (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z - 1].number == 0 || map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)) ||
                    ((angle / 45) % 8 == 5 /*3*/ && (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z - 1].number == 0 || map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)) ||
                    ((angle / 45) % 8 == 7 /*5*/ && (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z + 1].number == 0 || map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)) ||
                    ((angle / 45) % 8 == 1 /*7*/ && (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z + 1].number == 0 || map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)))
            {
            transform.Rotate(new Vector3(0, 315, 0));
            angle = (int)(transform.eulerAngles.y + 0.5);
        }
    }
    //向いている方向に前進
    private void ForwardMove()
    {
        enemypos = transform.position;
        emoveX = 0;
        emoveY = 0;
        //intに変換した際数字が切り捨てられるのを防止
        int angle = ((int)(transform.eulerAngles.y + 0.5));
        if ((angle / 45) % 8 == 2 /*0*/)
        {
            this.emoveX = 1;
            enemypos += new Vector3(emoveX, 0, 0);
        }else if ((angle / 45) % 8 == 4 /*2*/)
        {
            this.emoveY = -1;
            enemypos += new Vector3(0, 0, emoveY);
        }
        else if ((angle / 45) % 8 == 6 /*4*/)
        {
            this.emoveX = -1;
            enemypos += new Vector3(emoveX, 0, 0);
        }
        else if((angle / 45) % 8 == 0 /*6*/)
        {
            this.emoveY = 1;
            enemypos += new Vector3(0, 0, emoveY);
        }
        else if ((angle / 45) % 8 == 3 /*1*/)
        {
            this.emoveX = 1;
            this.emoveY = -1;
            enemypos += new Vector3(emoveX, 0, emoveY);
        }
        else if ((angle / 45) % 8 == 5 /*3*/)
        {
            this.emoveX = -1;
            this.emoveY = -1;
            enemypos += new Vector3(emoveX, 0, emoveY);
        }
        else if ((angle / 45) % 8 == 7 /*5*/)
        {
            this.emoveX = -1;
            this.emoveY = 1;
            enemypos += new Vector3(emoveX, 0, emoveY);
        }
        else if ((angle / 45) % 8 == 1 /*7*/)
        {
            this.emoveX = 1;
            this.emoveY = 1;
            enemypos += new Vector3(emoveX, 0, emoveY);
        }
    }


    public int enemydamage(int hp, int attack , int defense)
    {
        int damage = attack - defense;
        hp -= damage;

        GameManager.instance.AddMainText(damage + "のダメージを与えた");
        
        return hp;
    }

    public void enemydie()
    {
            GameManager.instance.AddMainText("倒れた");
            map_creat.map_ex[(int)transform.position.x, (int)transform.position.z] = new clear();


            GameManager.instance.enemies.Remove(this);
            Destroy(this.gameObject);
        
    }

    IEnumerator SmoothMovement(Vector3 end)
    {
        smoothmove = true;
        
        //現在地から目的地を引き、2点間の距離を求める(Vector3型)
        //sqrMagnitudeはベクトルを2乗したあと2点間の距離に変換する(float型)
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        //2点間の距離が0になった時、ループを抜ける
        //Epsilon : ほとんど0に近い数値を表す
        while (sqrRemainingDistance > float.Epsilon)
        {
            myAnimator.SetInteger("AnimIndex", 1);
            //現在地と移動先の間を1秒間にinverseMoveTime分だけ移動する場合の、
            //1フレーム分の移動距離を算出する
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, GameManager.instance.inverseMoveTime * Time.deltaTime);
            //算出した移動距離分、移動する
            transform.localPosition = newPosition;
            //現在地が目的地寄りになった結果、sqrRemainDistanceが小さくなる
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            //1フレーム待ってから、while文の先頭へ戻る
            yield return null;
            //Debug.Log(transform.position.x + "X");
            //Debug.Log(transform.position.z + "Z");
        }
        smoothmove = false;
        GameManager.instance.EnemyMoving++;

        myAnimator.SetInteger("AnimIndex", 0);

        transform.localPosition = new Vector3((int)end.x, -0.5f, (int)end.z);
        
        if(map_creat.map_ex[(int)transform.position.x,(int)transform.position.z].number != 6)
        {
            Debug.Log(gameObject);
        }

    }

    IEnumerator AttackAnimation()
    {
        this.smoothmove = true;
        GameManager.instance.enemyattack = true;

        yield return new WaitForSeconds(0.1f);

        myAnimator.SetInteger("AnimIndex",2);

        //アニメーション
        yield return null;
        yield return new AnimationWait(myAnimator, 0);


        myAnimator.SetInteger("AnimIndex", 0);

        //プレイヤーダメージ
        int player_damage = player_script.playerdamage(player.player_hp, map_creat.map_ex[(int)transform.position.x, (int)transform.position.z].state.ATTACK);
        bool player_die = player_script.playerdie();
        
        if(player_damage <= 0) {

        }else if(player_damage > 0)
        {
            if (player_die == false)
            {
                playerAnimator.SetInteger("AnimIndex", 3);
            }else if (player_die == true)
            {
                playerAnimator.SetInteger("AnimIndex", 4);
                yield break;
            }
        }

        yield return null;
        yield return new AnimationWait(playerAnimator, 0);
        

        playerAnimator.SetInteger("AnimIndex", 0);

        this.smoothmove = false;

        GameManager.instance.enemyattack = false;
        GameManager.instance.EnemyMoving++;
    }
    

    //プレイヤーが攻撃範囲にいるか？壁は邪魔じゃないか？
    private void EnemyAttack(int attack_range, int attack_type ,int distance_x , int distance_z , bool slanting_wall)
    {
        enemyattack = true;

        //直線1本攻撃のとき
        if (attack_type == 0)
        {

            //斜め範囲にいるとき（横壁は貫通）
            if (Mathf.Abs(distance_x) == Mathf.Abs(distance_z) && Mathf.Abs(distance_x) <= attack_range)
            {
                if (distance_x > 0 && distance_z > 0)
                {
                    for (int i = 1; (int)transform.position.x - i >= (int)Player.transform.position.x; i++)
                    {
                        //プレイヤーと敵の直線上に壁がある場合
                        if (map_creat.map[(int)transform.position.x - i, (int)transform.position.z - i].number == 0)
                        {
                            enemyattack = false;
                        }

                        //斜め時、隣の壁があると攻撃できない場合
                        if (slanting_wall == false)
                        {
                            if ((map_creat.map[(int)transform.position.x - i + 1, (int)transform.position.z - i].number == 0) || (map_creat.map[(int)transform.position.x - i, (int)transform.position.z - i + 1].number == 0))
                            {
                                enemyattack = false;
                            }
                        }
                    }
                }
                else if (distance_x > 0 && distance_z < 0)
                {
                    for (int i = 1; (int)transform.position.x - i >= (int)Player.transform.position.x; i++)
                    {
                        if (map_creat.map[(int)transform.position.x - i, (int)transform.position.z + i].number == 0)
                        {
                            enemyattack = false;
                        }

                        //斜め時、隣の壁があると攻撃できない場合
                        if (slanting_wall == false)
                        {
                            if ((map_creat.map[(int)transform.position.x - i + 1, (int)transform.position.z + i].number == 0) || (map_creat.map[(int)transform.position.x - i, (int)transform.position.z + i - 1].number == 0))
                            {
                                enemyattack = false;
                            }
                        }
                    }
                }
                else if (distance_x < 0 && distance_z > 0)
                {
                    for (int i = 1; (int)transform.position.x + i <= (int)Player.transform.position.x; i++)
                    {
                        if (map_creat.map[(int)transform.position.x + i, (int)transform.position.z - i].number == 0)
                        {
                            enemyattack = false;
                        }

                        //斜め時、隣の壁があると攻撃できない場合
                        if (slanting_wall == false)
                        {
                            if ((map_creat.map[(int)transform.position.x + i - 1, (int)transform.position.z - i].number == 0) || (map_creat.map[(int)transform.position.x + i, (int)transform.position.z - i + 1].number == 0))
                            {
                                enemyattack = false;
                            }
                        }
                    }
                }
                else if (distance_x < 0 && distance_z < 0)
                {
                    for (int i = 1; (int)transform.position.x + i <= (int)Player.transform.position.x; i++)
                    {
                        if (map_creat.map[(int)transform.position.x + i, (int)transform.position.z + i].number == 0)
                        {
                            enemyattack = false;
                        }

                        //斜め時、隣の壁があると攻撃できない場合
                        if (slanting_wall == false)
                        {
                            if ((map_creat.map[(int)transform.position.x + i - 1, (int)transform.position.z + i].number == 0) || (map_creat.map[(int)transform.position.x + i, (int)transform.position.z + i - 1].number == 0))
                            {
                                enemyattack = false;
                            }
                        }
                    }
                }

            }

            //十字範囲にいるとき
            else if ((distance_x == 0 && Mathf.Abs(distance_z) <= attack_range) || (distance_z == 0 && Mathf.Abs(distance_x) <= attack_range))
            {
                if (distance_x == 0)
                {
                    if (distance_z < 0)
                    {
                        for (int i = 1; (int)transform.position.z + i < (int)Player.transform.position.z; i++)
                        {
                            if (map_creat.map[(int)transform.position.x, (int)transform.position.z + i].number == 0)
                            {
                                enemyattack = false;
                            }
                        }
                    }
                    else if (distance_z > 0)
                    {
                        for (int i = 1; (int)transform.position.z - i > (int)Player.transform.position.z; i++)
                        {
                            if (map_creat.map[(int)transform.position.x, (int)transform.position.z - i].number == 0)
                            {
                                enemyattack = false;
                            }
                        }
                    }
                }
                else if (distance_z == 0)
                {
                    if (distance_x < 0)
                    {
                        for (int i = 1; (int)transform.position.x + i < (int)Player.transform.position.x; i++)
                        {
                            if (map_creat.map[(int)transform.position.x + i, (int)transform.position.z].number == 0)
                            {
                                enemyattack = false;
                            }
                        }
                    }
                    else if (distance_x > 0)
                    {
                        for (int i = 1; (int)transform.position.x - i > (int)Player.transform.position.x; i++)
                        {
                            if (map_creat.map[(int)transform.position.x - i, (int)transform.position.z].number == 0)
                            {
                                enemyattack = false;
                            }
                        }
                    }
                }
            }
            else
            {
                enemyattack = false;
            }
        }


    }

    public class AnimationWait : CustomYieldInstruction
    {
        Animator m_animator;
        int m_layerNo;
        int m_lastStateHash;

        public AnimationWait(Animator animator, int layerNo)
        {
            m_animator = animator;
            m_layerNo = layerNo;
            m_lastStateHash = m_animator.GetCurrentAnimatorStateInfo(m_layerNo).nameHash;
        }

        public override bool keepWaiting
        {
            get
            {
                var currentAnimatorState = m_animator.GetCurrentAnimatorStateInfo(m_layerNo);
                return currentAnimatorState.fullPathHash == m_lastStateHash &&
                    (currentAnimatorState.normalizedTime < 1);
            }
        }

    }

    /*int distance_x = (int)(transform.position.x - Player.transform.position.x);
    int distance_y = (int)(transform.position.z - Player.transform.position.z);
        
        if(Mathf.Abs(distance_x) <= map_creat.map_ex[(int)transform.position.x, (int)transform.position.z].state.RANGE_ATTACK && Mathf.Abs(distance_y) <= map_creat.map_ex[(int)transform.position.x, (int)transform.position.z].state.RANGE_ATTACK)
        {
            if ((distance_x == -1 && distance_y == -1 &&(map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)) ||
                (distance_x == -1 && distance_y == 1 && (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)) ||
                (distance_x == 1 && distance_y == -1 && (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)) ||
                (distance_x == 1 && distance_y == 1 && (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)))
            {
                enemyattack = false;
            }
            else
            {
                enemyattack = true;
            }
        }*/
}


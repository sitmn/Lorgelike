using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_script : MonoBehaviour
{

    private int i;
    private int emoveX, emoveY;
    
    private bool enotmove,playerfind, enemyattack;
    
    private Vector3 enemypos;
    private Vector3 destination;

    private GameObject Player;
    private Player_script player_script;

    private map_creat mapscript;
    
    
    void Start()
    {
        //このEnemyが生成された時、このスクリプトをListに追加
        GameManager.instance.AddListenemy(this);

        Player = GameObject.Find("Player");
        player_script = Player.GetComponent<Player_script>();
        this.enotmove = false;
        this.playerfind = false;
        destination = new Vector3(0,0,0);

    }

    
    //敵の行動
    public void Emove()
    {
        GameManager.instance.Enemymoving = true;
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
                if(map_creat.map[(int)transform.position.x , (int)transform.position.z].number == 3)
                {
                    destination = new Vector3(0, 0, 0);
                }
            }else if (map_creat.map[(int)transform.position.x, (int)transform.position.z].number == 1 || map_creat.map[(int)transform.position.x, (int)transform.position.z].number == 3)
            {
                RoomMove();
                
            }
        }

        this.enemyattack = false;
        this.enotmove = false;
        this.emoveX = 0;
        this.emoveY = 0;

        GameManager.instance.Enemymoving = false;

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
            int r = Random.Range(0, 2);
            if (r == 0)
            {//左回り
                transform.eulerAngles = new Vector3(0, 270, 0);
                while (((transform.eulerAngles.y / 90) % 4 == 0 && (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 1 || map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 3)) ||
                    ((transform.eulerAngles.y / 90) % 4 == 1 && (map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 1 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 3)) ||
                    ((transform.eulerAngles.y / 90) % 4 == 2 && (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 1 || map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 3)) ||
                    ((transform.eulerAngles.y / 90) % 4 == 3 && (map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 1 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 3)))
                {
                    transform.Rotate(new Vector3(0, 90, 0));
                }
                ForwardMove();
                
            }
            else if (r == 1)
            {//右回り
                transform.eulerAngles = new Vector3(0, 90, 0);
                while (((transform.eulerAngles.y / 90) % 4 == 0 && (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 1 || map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 3)) ||
                    ((transform.eulerAngles.y / 90) % 4 == 1 && (map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 1 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 3)) ||
                    ((transform.eulerAngles.y / 90) % 4 == 2 && (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 1 || map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 3)) ||
                    ((transform.eulerAngles.y / 90) % 4 == 3 && (map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 1 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 3)))
                {
                    transform.Rotate(new Vector3(0, 270, 0));
                }
                ForwardMove();
            }//移動先にプレイヤーと敵がいないならば移動
            if (map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY].number == 5 || map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY].number == 6)
            {
                enotmove = true;
            }
            if (enotmove == false) { 
                map_creat.map_ex[(int)transform.position.x + emoveX, (int)transform.position.z + emoveY] = map_creat.map_ex[(int)transform.position.x, (int)transform.position.z];
                map_creat.map_ex[(int)transform.position.x, (int)transform.position.z] = new clear();
                StartCoroutine(SmoothMovement(enemypos));
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
                    StartCoroutine(SmoothMovement(enemypos));
                }
            }
            destination = new Vector3(0, 0, 0);
        }
        else if (transform.position != destination)//目的地でない場合、目的地に向かう
        {
            int entrancedistance_x = (int)(transform.position.x - destination.x);
            int entrancedistance_y = (int)(transform.position.z - destination.z);

            if (entrancedistance_x == 0)
            {
                if (entrancedistance_y < 0)
                {
                    transform.eulerAngles = new Vector3(0, 270, 0);
                    if (map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)
                    {
                        playerfind = false;
                        enotmove = true;
                    }
                }
                else if (entrancedistance_y > 0)
                {
                    transform.eulerAngles = new Vector3(0, 90, 0);
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
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    if (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0)
                    {
                        playerfind = false;
                        enotmove = true;
                    }
                }
                else if (entrancedistance_x > 0)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
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
                    transform.eulerAngles = new Vector3(0, 315, 0);
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
                                transform.eulerAngles = new Vector3(0, 270, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 0, 0);
                            }
                        }
                        else
                        {
                            if (map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 0, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 270, 0);
                            }
                        }
                    }
                }
                else if (entrancedistance_x > 0 && entrancedistance_y < 0)
                {
                    transform.eulerAngles = new Vector3(0, 225, 0);
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
                                transform.eulerAngles = new Vector3(0, 270, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 180, 0);
                            }
                        }
                        else
                        {
                            if (map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 180, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 270, 0);
                            }
                        }
                    }
                }
                else if (entrancedistance_x > 0 && entrancedistance_y > 0)
                {
                    transform.eulerAngles = new Vector3(0, 135, 0);
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
                                transform.eulerAngles = new Vector3(0, 90, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 180, 0);
                            }
                        }
                        else
                        {
                            if (map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 180, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 90, 0);
                            }
                        }
                    }
                }
                else if (entrancedistance_x < 0 && entrancedistance_y > 0)
                {
                    transform.eulerAngles = new Vector3(0, 45, 0);
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
                                transform.eulerAngles = new Vector3(0, 90, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 0, 0);
                            }
                        }
                        else
                        {
                            if (map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 0, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 90, 0);
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
                StartCoroutine(SmoothMovement(enemypos));

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
                    StartCoroutine(SmoothMovement(enemypos));

                }
            }
        }
    }

    //プレイヤーを見つけたときの移動
    private void FindPlayerMove()
    {
        int distance_x = (int)(transform.position.x - Player.transform.position.x);
        int distance_y = (int)(transform.position.z - Player.transform.position.z);
        
        if(Mathf.Abs(distance_x) <= 1 && Mathf.Abs(distance_y) <= 1)
        {
            if ((distance_x == -1 && distance_y == -1 &&(map_creat.map[(int)transform.position.x + 1 , (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)) ||
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
        }
        if(enemyattack == false) {          //プレイやーとの位置関係によって角度を変更
            if (distance_x == 0)
            {
                if (distance_y < 0)
                {
                    transform.eulerAngles = new Vector3(0, 270, 0);
                    if(map_creat.map[(int)transform.position.x , (int)transform.position.z + 1].number == 0){
                        playerfind = false;
                        enotmove = true;
                    }
                }
                else if (distance_y > 0)
                {
                    transform.eulerAngles = new Vector3(0, 90, 0);
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
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    if (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0)
                    {
                        playerfind = false;
                        enotmove = true;
                    }
                }
                else if (distance_x > 0)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
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
                    transform.eulerAngles = new Vector3(0, 315, 0);
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
                                transform.eulerAngles = new Vector3(0, 270, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 0, 0);
                            }
                        }
                        else
                        {
                            if (map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 0, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 270, 0);
                            }
                        }
                    }
                }else if(distance_x > 0 && distance_y < 0)
                {
                    transform.eulerAngles = new Vector3(0, 225, 0);
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
                                transform.eulerAngles = new Vector3(0, 270, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 180, 0);
                            }
                        }
                        else
                        {
                            if (map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 180, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 270, 0);
                            }
                        }
                    }
                }
                else if(distance_x > 0 && distance_y > 0)
                {
                    transform.eulerAngles = new Vector3(0, 135, 0);
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
                                transform.eulerAngles = new Vector3(0, 90, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 180, 0);
                            }
                        }
                        else
                        {
                            if (map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 180, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 90, 0);
                            }
                        }
                    }
                }
                else if(distance_x < 0 && distance_y > 0)
                {
                    transform.eulerAngles = new Vector3(0, 45, 0);
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
                                transform.eulerAngles = new Vector3(0, 90, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 0, 0);
                            }
                        }
                        else
                        {
                            if (map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)
                            {
                                transform.eulerAngles = new Vector3(0, 0, 0);
                            }
                            else
                            {
                                transform.eulerAngles = new Vector3(0, 90, 0);
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
                StartCoroutine(SmoothMovement(enemypos));
            }
        }else if(enemyattack == true)
        {
            player_script.playerdamage(player.player_hp, map_creat.map_ex[(int)transform.position.x , (int)transform.position.z].attack);
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
            StartCoroutine(SmoothMovement(enemypos));

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
                StartCoroutine(SmoothMovement(enemypos));
            }
        }
    }

    

    //左回りで分岐探索
    void RotateLeft()
    {
        int angle = (int)(transform.eulerAngles.y + 0.5);
        while (((angle / 45) % 8 == 0 && map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0) ||
                    ((angle / 45) % 8 == 2 && map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0) ||
                    ((angle / 45) % 8 == 4 && map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0) ||
                    ((angle / 45) % 8 == 6 && map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0) ||
                    ((angle / 45) % 8 == 1 && (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z - 1].number == 0 || map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)) ||
                    ((angle / 45) % 8 == 3 && (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z - 1].number == 0 || map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)) ||
                    ((angle / 45) % 8 == 5 && (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z + 1].number == 0 || map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)) ||
                    ((angle / 45) % 8 == 7 && (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z + 1].number == 0 || map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)))
        {
            transform.Rotate(new Vector3(0, 45, 0));
            angle = (int)(transform.eulerAngles.y + 0.5);
        }
    }
    //右回りで分岐探索
    void RotateRight()
    {   int angle = (int)(transform.eulerAngles.y + 0.5);
        while (((angle / 45) % 8 == 0 && map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0) ||
                    ((angle / 45) % 8 == 2 && map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0) ||
                    ((angle / 45) % 8 == 4 && map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0) ||
                    ((angle / 45) % 8 == 6 && map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0) ||
                    ((angle / 45) % 8 == 1 && (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z - 1].number == 0 || map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)) ||
                    ((angle / 45) % 8 == 3 && (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z - 1].number == 0 || map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)) ||
                    ((angle / 45) % 8 == 5 && (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z + 1].number == 0 || map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)) ||
                    ((angle / 45) % 8 == 7 && (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z + 1].number == 0 || map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)))
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
        if ((angle / 45) % 8 == 0)
        {
            this.emoveX = 1;
            enemypos += new Vector3(emoveX, 0, 0);
        }else if ((angle / 45) % 8 == 2)
        {
            this.emoveY = -1;
            enemypos += new Vector3(0, 0, emoveY);
        }
        else if ((angle / 45) % 8 == 4)
        {
            this.emoveX = -1;
            enemypos += new Vector3(emoveX, 0, 0);
        }
        else if((angle / 45) % 8 == 6)
        {
            this.emoveY = 1;
            enemypos += new Vector3(0, 0, emoveY);
        }
        else if ((angle / 45) % 8 == 1)
        {
            this.emoveX = 1;
            this.emoveY = -1;
            enemypos += new Vector3(emoveX, 0, emoveY);
        }
        else if ((angle / 45) % 8 == 3)
        {
            this.emoveX = -1;
            this.emoveY = -1;
            enemypos += new Vector3(emoveX, 0, emoveY);
        }
        else if ((angle / 45) % 8 == 5)
        {
            this.emoveX = -1;
            this.emoveY = 1;
            enemypos += new Vector3(emoveX, 0, emoveY);
        }
        else if ((angle / 45) % 8 == 7)
        {
            this.emoveX = 1;
            this.emoveY = 1;
            enemypos += new Vector3(emoveX, 0, emoveY);
        }
    }


    public int enemydamage(int hp, int attack)
    {
        int damage = attack;
        hp -= damage;
        if (hp <= 0)
        {
            map_creat.map_ex[(int)transform.position.x, (int)transform.position.z] = new clear();
            GameManager.instance.enemies.Remove(this);
            Destroy(this.gameObject);
        }
        return hp;
    }

    IEnumerator SmoothMovement(Vector3 end)
    {
        //現在地から目的地を引き、2点間の距離を求める(Vector3型)
        //sqrMagnitudeはベクトルを2乗したあと2点間の距離に変換する(float型)
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        //2点間の距離が0になった時、ループを抜ける
        //Epsilon : ほとんど0に近い数値を表す
        while (sqrRemainingDistance > float.Epsilon)
        {
            //現在地と移動先の間を1秒間にinverseMoveTime分だけ移動する場合の、
            //1フレーム分の移動距離を算出する
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, GameManager.instance.inverseMoveTime * Time.deltaTime);
            //算出した移動距離分、移動する
            transform.position = newPosition;
            //現在地が目的地寄りになった結果、sqrRemainDistanceが小さくなる
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            //1フレーム待ってから、while文の先頭へ戻る
            yield return null;
        }
    }

    /*クラスを移動先に移してから徐々に移動なので使用不可
    private void EnemyDebug()
    {
        if (map_creat.map_ex[(int)transform.position.x, (int)transform.position.z].number != 6)
        {
            Debug.Log("バグ、敵のクラスがなくなった");
        }
        if (map_creat.map[(int)transform.position.x, (int)transform.position.z].number == 0)
        {
            Debug.Log("バグ、壁の中");
        }
    }*/
}


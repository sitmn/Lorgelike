using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_script : MonoBehaviour
{

    private int i;
    private int emoveX, emoveY;
    
    private bool enotmove,playerfind;
    
    private Vector3 enemypos;

    private GameObject Player;
    
    
    void Start()
    {
        //このEnemyが生成された時、このスクリプトをListに追加
        GameManager.instance.AddListenemy(this);

        Player = GameObject.Find("Player");
        this.enotmove = false;
        this.playerfind = false;
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
            if (map_creat.map[(int)transform.position.x, (int)transform.position.z].number == 2 || map_creat.map[(int)transform.position.x, (int)transform.position.z].number == 3 || map_creat.map[(int)transform.position.x, (int)transform.position.z].number == 10)
            {
                AisleMove();
            }
        }

        
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

    }

    //プレイヤーを見つけたときの移動
    private void FindPlayerMove()
    {
        int distance_x = (int)(transform.position.x - Player.transform.position.x);
        int distance_y = (int)(transform.position.z - Player.transform.position.z);

        if(Mathf.Abs(distance_x) <= 1 && Mathf.Abs(distance_y) <= 1)
        {
            //attack
        }
        else {          //プレイやーとの位置関係によって角度を変更
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
                transform.position = enemypos;
                
            }
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
        else if(map_creat.map[(int)transform.position.x, (int)transform.position.z].number == 3 || map_creat.map[(int)transform.position.x, (int)transform.position.z].number == 10)
        
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
            transform.position = enemypos;
            
        }else if(enotmove == true)//正面が移動できないものならターン
        {
            
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
                transform.position = enemypos;
            }
        }
    }

    

    //左回りで分岐探索
    void RotateLeft()
    {
        while (((transform.eulerAngles.y / 90) % 4 == 0 && map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0) ||
                    ((transform.eulerAngles.y / 90) % 4 == 1 && map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0) ||
                    ((transform.eulerAngles.y / 90) % 4 == 2 && map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0) ||
                    ((transform.eulerAngles.y / 90) % 4 == 3 && map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0))
        {
            transform.Rotate(new Vector3(0, 90, 0));
        }
    }
    //右回りで分岐探索
    void RotateRight()
    {
        while (((transform.eulerAngles.y / 90) % 4 == 0 && map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0) ||
                    ((transform.eulerAngles.y / 90) % 4 == 1 && map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0) ||
                    ((transform.eulerAngles.y / 90) % 4 == 2 && map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0) ||
                    ((transform.eulerAngles.y / 90) % 4 == 3 && map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0))
        {
            transform.Rotate(new Vector3(0, 270, 0));
        }
    }
    //向いている方向に前進
    void ForwardMove()
    {
        Debug.Log(transform.eulerAngles.y);
        if (((int)transform.eulerAngles.y / 45) % 8 == 0)
        {
            this.emoveX = 1;
            enemypos += new Vector3(emoveX, 0, 0);
        }else if (((int)transform.eulerAngles.y / 45) % 8 == 2)
        {
            this.emoveY = -1;
            enemypos += new Vector3(0, 0, emoveY);
        }
        else if (((int)transform.eulerAngles.y / 45) % 8 == 4)
        {
            this.emoveX = -1;
            enemypos += new Vector3(emoveX, 0, 0);
        }
        else if(((int)transform.eulerAngles.y / 45) % 8 == 6)
        {
            this.emoveY = 1;
            enemypos += new Vector3(0, 0, emoveY);
        }
        else if (((int)transform.eulerAngles.y / 45) % 8 == 1)
        {
            this.emoveX = 1;
            this.emoveY = -1;
            enemypos += new Vector3(emoveX, 0, emoveY);
        }
        else if (((int)transform.eulerAngles.y / 45) % 8 == 3)
        {
            this.emoveX = -1;
            this.emoveY = -1;
            enemypos += new Vector3(emoveX, 0, emoveY);
        }
        else if ((transform.eulerAngles.y / 45) % 8 == 5)
        {
            this.emoveX = -1;
            this.emoveY = 1;
            enemypos += new Vector3(emoveX, 0, emoveY);
        }
        else if (((int)transform.eulerAngles.y / 45) % 8 == 7)
        {
            this.emoveX = 1;
            this.emoveY = 1;
            enemypos += new Vector3(emoveX, 0, emoveY);
        }
    }

    public int enemydamage(int hp, int attack, int defence)
    {
        int damage = attack - defence;
        hp -= damage;
        if (hp <= 0)
        {
            map_creat.map_ex[(int)transform.position.x, (int)transform.position.z] = new clear();
            GameManager.instance.enemies.Remove(this);
            Destroy(this.gameObject);
        }
        return hp;
    }
}


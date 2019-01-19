using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_script : MonoBehaviour
{

    private int i;
    public int emoveX, emoveY;
    

    public bool enotmove;

    public LayerMask blockinglayer;

    public int enemyhp;

    private Vector3 enemypos;
    
    
    void Start()
    {
        //このEnemyが生成された時、このスクリプトをListに追加
        GameManager.instance.AddListenemy(this);

        this.enotmove = false;
    }

    
    //敵の行動
    public void Emove()
    {
        GameManager.instance.Enemymoving = true;
        this.enotmove = true;

        
        AisleMove();

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

    //被ダメージ、HP0以下で削除
    public void enemydamage(int playerattack)
    {
        enemyhp -= playerattack;
        if (enemyhp <= 0)
        {
            GameManager.instance.enemies.Remove(this);
            Destroy(this.gameObject);
        }
    }

//↓敵AI（未使用）
    public void AI()
    {
        
    }

    
    //部屋に入ったときの移動
    private void RoomMove()
    {

    }

    //プレイヤーを見つけたときの移動
    private void FindPlayerMove()
    {

    }

    //設置物を見つけたときの移動
    private void AisleMove()
    {
        enemypos = transform.position;

        //通路の分岐点でランダムに曲がる
        if (map_creat.map[(int)enemypos.x, (int)enemypos.z] == 3 || map_creat.map[(int)enemypos.x, (int)enemypos.z] == 4)
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
        else {

            //正面が壁ならば90度回転,正面が壁でなくなるまでループ
            RotateLeft();
        }
        
        ForwardMove();

        //移動先にプレイヤーと敵がいないならば移動
        this.enotmove = Physics.Linecast(transform.position, enemypos, blockinglayer);
        if (enotmove == false) {
            transform.position = enemypos;
        }else if(enotmove == true)//正面が移動できないものならターン
        {
            
            transform.Rotate(new Vector3(0, 90, 0));
            RotateLeft();
            ForwardMove();
            this.enotmove = Physics.Linecast(transform.position, enemypos, blockinglayer);
            if(enotmove == false)
            {
                transform.position = enemypos;
            }
        }
    }

    //左回りで分岐探索
    void RotateLeft()
    {
        while (((transform.eulerAngles.y / 90) % 4 == 0 && map_creat.map[(int)enemypos.x + 1, (int)enemypos.z] == 0) ||
                    ((transform.eulerAngles.y / 90) % 4 == 1 && map_creat.map[(int)enemypos.x, (int)enemypos.z - 1] == 0) ||
                    ((transform.eulerAngles.y / 90) % 4 == 2 && map_creat.map[(int)enemypos.x - 1, (int)enemypos.z] == 0) ||
                    ((transform.eulerAngles.y / 90) % 4 == 3 && map_creat.map[(int)enemypos.x, (int)enemypos.z + 1] == 0))
        {
            transform.Rotate(new Vector3(0, 90, 0));
        }
    }
    //右回りで分岐探索
    void RotateRight()
    {
        while (((transform.eulerAngles.y / 90) % 4 == 0 && map_creat.map[(int)enemypos.x + 1, (int)enemypos.z] == 0) ||
                    ((transform.eulerAngles.y / 90) % 4 == 1 && map_creat.map[(int)enemypos.x, (int)enemypos.z - 1] == 0) ||
                    ((transform.eulerAngles.y / 90) % 4 == 2 && map_creat.map[(int)enemypos.x - 1, (int)enemypos.z] == 0) ||
                    ((transform.eulerAngles.y / 90) % 4 == 3 && map_creat.map[(int)enemypos.x, (int)enemypos.z + 1] == 0))
        {
            transform.Rotate(new Vector3(0, 270, 0));
        }
    }
    //向いている方向に前進
    void ForwardMove()
    {
        if ((transform.eulerAngles.y / 90) % 4 == 0)
        {
            enemypos += new Vector3(1, 0, 0);
        }else if ((transform.eulerAngles.y / 90) % 4 == 1)
        {
            enemypos += new Vector3(0, 0, -1);
        }
        else if ((transform.eulerAngles.y / 90) % 4 == 2)
        {
            enemypos += new Vector3(-1, 0, 0);
        }
        else
        {
            enemypos += new Vector3(0, 0, 1);
        }
    }
}


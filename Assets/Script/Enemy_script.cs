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

        while (this.enotmove == true)
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
        }

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

    //通路にいるときの移動
    public void RoadMove()
    {
        //通路にいるとき
        if (map_creat.map[(int)transform.position.x,(int)transform.position.z]==1)
        {
            if (transform.eulerAngles == new Vector3(0, 0, 0))
            {
                emoveX = 1;
                emoveY = 0;
            }
            if (transform.eulerAngles == new Vector3(0, 90, 0))
            {
                emoveX = 0;
                emoveY = -1;
            }
            if (transform.eulerAngles == new Vector3(0, 180, 0))
            {
                emoveX = -1;
                emoveY = 0;
            }
            if (transform.eulerAngles == new Vector3(0, 270, 0))
            {
                emoveX = 0;
                emoveY = 1;
            }
        }
        //通路の分岐点にいるときランダムで移動
        else if(map_creat.map[(int)transform.position.x, (int)transform.position.z] == 3)
        {
            i = Random.Range(0, 3);
            if(i == 0)
            {
                if (transform.eulerAngles == new Vector3(0, 0, 0))
                {
                    emoveX = 1;
                    emoveY = 0;
                }
                if (transform.eulerAngles == new Vector3(0, 90, 0))
                {
                    emoveX = 0;
                    emoveY = -1;
                }
                if (transform.eulerAngles == new Vector3(0, 180, 0))
                {
                    emoveX = -1;
                    emoveY = 0;
                }
                if (transform.eulerAngles == new Vector3(0, 270, 0))
                {
                    emoveX = 0;
                    emoveY = 1;
                }
            }
            if(i == 1)
            {
                if (transform.eulerAngles == new Vector3(0, 270, 0))
                {
                    emoveX = 1;
                    emoveY = 0;
                }
                if (transform.eulerAngles == new Vector3(0, 0, 0))
                {
                    emoveX = 0;
                    emoveY = -1;
                }
                if (transform.eulerAngles == new Vector3(0, 90, 0))
                {
                    emoveX = -1;
                    emoveY = 0;
                }
                if (transform.eulerAngles == new Vector3(0, 180, 0))
                {
                    emoveX = 0;
                    emoveY = 1;
                }
            }
            if(i == 2)
            {
                if (transform.eulerAngles == new Vector3(0, 180, 0))
                {
                    emoveX = 1;
                    emoveY = 0;
                }
                if (transform.eulerAngles == new Vector3(0, 270, 0))
                {
                    emoveX = 0;
                    emoveY = -1;
                }
                if (transform.eulerAngles == new Vector3(0, 0, 0))
                {
                    emoveX = -1;
                    emoveY = 0;
                }
                if (transform.eulerAngles == new Vector3(0, 90, 0))
                {
                    emoveX = 0;
                    emoveY = 1;
                }
            }
        }
    }

    //部屋に入ったときの移動
    public void RoomMove()
    {

    }

    //プレイヤーを見つけたときの移動
    public void FindPlayerMove()
    {

    }

    //設置物を見つけたときの移動
    public void FindShellMove()
    {

    }

}


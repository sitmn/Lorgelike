using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_script : MonoBehaviour {
    
    private int moveX;
    private int moveY;
    private bool attack;
    private int attack_x, attack_y,x,z;
    
    public bool notmove,vectorchange,menu;
    
    public GameObject Player;

    private Transform playerpos;
    
    // Use this for initialization
    void Start()
    {
        this.notmove = false;
        this.playerpos = GetComponent<Transform>();

        //プレイヤーをマップ内に移動
        do
        {
          x = Random.Range(1, 58);
          z = Random.Range(1, 58);
        } while (map_creat.map[x, z].number != 1 || map_creat.map_ex[x,z].number == 6);

        map_creat.map_ex[x, z] = new player();
        map_creat.map_ex[x, z].obj = Player;
        map_creat.map_ex[x, z].player_script = Player.GetComponent<Player_script>();
        this.playerpos.position = new Vector3( x, 0, z);
    }

    // Update is called once per frame
    public void Update()
    {
        //プレイヤーターンでないときまたはポーズ時は動かない
        if (GameManager.instance.Playerturn == false||GameManager.instance.Pose==true)
        {
            return;
        }
        //Cキーを押している間、vectorchangeを有効に
        if (Input.GetKey(KeyCode.C) && this.menu == false)
        {
            this.vectorchange = true;
            //〇マス目を表示
        }
        else
        {
            this.vectorchange = false;
            //〇マス目表示を消す
        }

        //Xキーを押している間、menuを有効に
        if (Input.GetKey(KeyCode.X) && this.menu ==false && this.vectorchange == false)
        {
            this.menu = true;
            //〇メニュー画面を表示
        }
        

        //向きのみ変更
        if (this.vectorchange == true)
        {
            VectorChange();
        }else if(this.menu == true){
            //〇メニュー処理

            /*〇メニュー終了処理
            if (Input.GetKey(KeyCode.X) && menu == true)
            {
                menu = false;
            }*/
        }
        else {
            if (Input.GetKey(KeyCode.Z))
            {
                Attack();
            } else{
                PlayerMove();
            }
        }
    }


    private void VectorChange()
    {
        if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            transform.eulerAngles = new Vector3(0, 270, 0);
        }
        else if (Input.GetKey(KeyCode.E) == true)
        {
            transform.eulerAngles = new Vector3(0, 45, 0);
        }
        else if (Input.GetKey(KeyCode.W) == true)
        {
            transform.eulerAngles = new Vector3(0, 135, 0);
        }
        else if (Input.GetKey(KeyCode.Q) == true)
        {
            transform.eulerAngles = new Vector3(0, 225, 0);
        }
        else if (Input.GetKey(KeyCode.R) == true)
        {
            transform.eulerAngles = new Vector3(0, 315, 0);
        }
    }


    private void PlayerMove()
    {
        //方向キーの向きに回転、障害物を確認
        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            this.moveY = 1;
            transform.eulerAngles = new Vector3(0, 270, 0);
            //移動先に壁と敵がいるか？
            if (map_creat.map_ex[(int)playerpos.position.x, (int)playerpos.position.z + moveY].number == 6 || map_creat.map[(int)playerpos.position.x, (int)playerpos.position.z + moveY].number == 0)
            {
                
                this.notmove = true;
            }
            if (this.notmove == false && GameManager.instance.Playerturn == true)
            {
                map_creat.map_ex[(int)transform.position.x + moveX, (int)transform.position.z + moveY] = map_creat.map_ex[(int)transform.position.x, (int)transform.position.z];
                map_creat.map_ex[(int)transform.position.x, (int)transform.position.z] = new clear();

                playerpos.position += new Vector3(this.moveX, 0, this.moveY);

                GameManager.instance.Playerturn = false;
            }
            else if (this.notmove == true)
            {
                this.notmove = false;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            this.moveX = -1;
            transform.eulerAngles = new Vector3(0, 180, 0);
            if (map_creat.map_ex[(int)playerpos.position.x　+ moveX, (int)playerpos.position.z].number == 6 || map_creat.map[(int)playerpos.position.x + moveX, (int)playerpos.position.z].number == 0)
            {
                
                this.notmove = true;
            }
            if (this.notmove == false && GameManager.instance.Playerturn == true)
            {
                map_creat.map_ex[(int)transform.position.x + moveX, (int)transform.position.z + moveY] = map_creat.map_ex[(int)transform.position.x, (int)transform.position.z];
                map_creat.map_ex[(int)transform.position.x, (int)transform.position.z] = new clear();
                playerpos.position += new Vector3(this.moveX, 0, this.moveY);

                GameManager.instance.Playerturn = false;
            }
            else if (this.notmove == true)
            {
                this.notmove = false;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            this.moveX = 1;
            transform.eulerAngles = new Vector3(0, 0, 0);
            if (map_creat.map_ex[(int)playerpos.position.x + moveX, (int)playerpos.position.z].number == 6 || map_creat.map[(int)playerpos.position.x + moveX, (int)playerpos.position.z].number == 0)
            {
               
                this.notmove = true;
            }
            if (this.notmove == false && GameManager.instance.Playerturn == true)
            {
                map_creat.map_ex[(int)transform.position.x + moveX, (int)transform.position.z + moveY] = map_creat.map_ex[(int)transform.position.x, (int)transform.position.z];
                map_creat.map_ex[(int)transform.position.x, (int)transform.position.z] = new clear();
                playerpos.position += new Vector3(this.moveX, 0, this.moveY);

                GameManager.instance.Playerturn = false;
            }
            else if (this.notmove == true)
            {
                this.notmove = false;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            this.moveY = -1;
            transform.eulerAngles = new Vector3(0, 90, 0);
            if (map_creat.map_ex[(int)playerpos.position.x, (int)playerpos.position.z + moveY].number == 6 || map_creat.map[(int)playerpos.position.x, (int)playerpos.position.z + moveY].number == 0)
            {
                
                this.notmove = true;
            }
            if (this.notmove == false && GameManager.instance.Playerturn == true)
            {
                map_creat.map_ex[(int)transform.position.x + moveX, (int)transform.position.z + moveY] = map_creat.map_ex[(int)transform.position.x, (int)transform.position.z];
                map_creat.map_ex[(int)transform.position.x, (int)transform.position.z] = new clear();
                playerpos.position += new Vector3(this.moveX, 0, this.moveY);

                GameManager.instance.Playerturn = false;
            }
            else if (this.notmove == true)
            {
                this.notmove = false;
            }
        }
        else if (Input.GetKey(KeyCode.Q) == true)
        {
            this.moveX = -1;
            this.moveY = 1;
            transform.eulerAngles = new Vector3(0, 225, 0);
            if(map_creat.map[(int)transform.position.x + moveX, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + moveY].number == 0)
            {
                this.notmove = true;
            }
            else
            {
                if (map_creat.map_ex[(int)playerpos.position.x + moveX, (int)playerpos.position.z + moveY].number == 6 || map_creat.map[(int)playerpos.position.x + moveX, (int)playerpos.position.z + moveY].number == 0)
                {
                    this.notmove = true;
                }
            }
            
            if (this.notmove == false && GameManager.instance.Playerturn == true)
            {
                map_creat.map_ex[(int)transform.position.x + moveX, (int)transform.position.z + moveY] = map_creat.map_ex[(int)transform.position.x, (int)transform.position.z];
                map_creat.map_ex[(int)transform.position.x, (int)transform.position.z] = new clear();
                playerpos.position += new Vector3(this.moveX, 0, this.moveY);

                GameManager.instance.Playerturn = false;
            }
            else if (this.notmove == true)
            {
                this.notmove = false;
            }
        }
        else if (Input.GetKey(KeyCode.W) == true)
        {
            this.moveX = -1;
            this.moveY = -1;
            transform.eulerAngles = new Vector3(0, 135, 0);
            if (map_creat.map[(int)transform.position.x + moveX, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + moveY].number == 0)
            {
                this.notmove = true;
            }
            else
            {
                if (map_creat.map_ex[(int)playerpos.position.x + moveX, (int)playerpos.position.z + moveY].number == 6 || map_creat.map[(int)playerpos.position.x + moveX, (int)playerpos.position.z + moveY].number == 0)
                {
                    this.notmove = true;
                }
            }

            if (this.notmove == false && GameManager.instance.Playerturn == true)
            {
                map_creat.map_ex[(int)transform.position.x + moveX, (int)transform.position.z + moveY] = map_creat.map_ex[(int)transform.position.x, (int)transform.position.z];
                map_creat.map_ex[(int)transform.position.x, (int)transform.position.z] = new clear();
                playerpos.position += new Vector3(this.moveX, 0, this.moveY);

                GameManager.instance.Playerturn = false;
            }
            else if (this.notmove == true)
            {
                this.notmove = false;
            }
        }
        else if (Input.GetKey(KeyCode.E) == true)
        {
            this.moveX = 1;
            this.moveY = -1;
            transform.eulerAngles = new Vector3(0, 45, 0);
            if (map_creat.map[(int)transform.position.x + moveX, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + moveY].number == 0)
            {
                this.notmove = true;
            }
            else
            {
                if (map_creat.map_ex[(int)playerpos.position.x + moveX, (int)playerpos.position.z + moveY].number == 6 || map_creat.map[(int)playerpos.position.x + moveX, (int)playerpos.position.z + moveY].number == 0)
                {
                    this.notmove = true;
                }
            }

            if (this.notmove == false && GameManager.instance.Playerturn == true)
            {
                map_creat.map_ex[(int)transform.position.x + moveX, (int)transform.position.z + moveY] = map_creat.map_ex[(int)transform.position.x, (int)transform.position.z];
                map_creat.map_ex[(int)transform.position.x, (int)transform.position.z] = new clear();
                playerpos.position += new Vector3(this.moveX, 0, this.moveY);

                GameManager.instance.Playerturn = false;
            }
            else if (this.notmove == true)
            {
                this.notmove = false;
            }
        }
        else if (Input.GetKey(KeyCode.R) == true)
        {
            this.moveX = 1;
            this.moveY = 1;
            transform.eulerAngles = new Vector3(0, 315, 0);
            if (map_creat.map[(int)transform.position.x + moveX, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + moveY].number == 0)
            {
                this.notmove = true;
            }
            else
            {
                if (map_creat.map_ex[(int)playerpos.position.x + moveX, (int)playerpos.position.z + moveY].number == 6 || map_creat.map[(int)playerpos.position.x + moveX, (int)playerpos.position.z + moveY].number == 0)
                {
                    this.notmove = true;
                }
            }

            if (this.notmove == false && GameManager.instance.Playerturn == true)
            {
                map_creat.map_ex[(int)transform.position.x + moveX, (int)transform.position.z + moveY] = map_creat.map_ex[(int)transform.position.x, (int)transform.position.z];
                map_creat.map_ex[(int)transform.position.x, (int)transform.position.z] = new clear();
                transform.position += new Vector3(moveX,0,moveY);

                GameManager.instance.Playerturn = false;
            }
            else if (this.notmove == true)
            {
                this.notmove = false;
            }
        }

        //障害物がなく、向きのみ変更でないとき移動


        //変数リセット
        this.moveX = 0;
        this.moveY = 0;
    }

    //プレイヤーの向いている方向に攻撃
    private void Attack()
    {
        if(transform.eulerAngles == new Vector3(0, 0, 0)) {
            this.attack_x = 1;
            this.attack_y = 0;
        }else if (transform.eulerAngles == new Vector3(0, 90, 0))
        {
            this.attack_x = 0;
            this.attack_y = -1;
        }else if (transform.eulerAngles == new Vector3(0, 180, 0))
        {
            this.attack_x = -1;
            this.attack_y = 0;
        }else if (transform.eulerAngles == new Vector3(0, 270, 0))
        {
            this.attack_x = 0;
            this.attack_y = 1;
        }
        else if (transform.eulerAngles == new Vector3(0, 45, 0))
        {
            this.attack_x = 1;
            this.attack_y = -1;
        }
        else if (transform.eulerAngles == new Vector3(0, 135, 0))
        {
            this.attack_x = -1;
            this.attack_y = -1;
        }
        else if (transform.eulerAngles == new Vector3(0, 225, 0))
        {
            this.attack_x = -1;
            this.attack_y = 1;
        }
        else if (transform.eulerAngles == new Vector3(0, 315, 0))
        {
            this.attack_x = 1;
            this.attack_y = 1;
        }

        //向いてる方向に攻撃
        if(transform.eulerAngles == new Vector3(0,45,0) && (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)||
            transform.eulerAngles == new Vector3(0, 135, 0) && (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z - 1].number == 0)||
            transform.eulerAngles == new Vector3(0, 225, 0) && (map_creat.map[(int)transform.position.x - 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0)||
                transform.eulerAngles == new Vector3(0, 315, 0) && (map_creat.map[(int)transform.position.x + 1, (int)transform.position.z].number == 0 || map_creat.map[(int)transform.position.x, (int)transform.position.z + 1].number == 0))
        {

        }
        else
        {
            if (map_creat.map_ex[(int)transform.position.x + this.attack_x, (int)transform.position.z + this.attack_y].number == 6)
            {
                this.attack = true;
            }
        }
        if (this.attack == true)
        {
            this.attack = false;
            map_creat.map_ex[(int)transform.position.x + this.attack_x, (int)transform.position.z + this.attack_y].hp = map_creat.map_ex[(int)transform.position.x + this.attack_x, (int)transform.position.z + this.attack_y].enemy_script.
                enemydamage(map_creat.map_ex[(int)transform.position.x + this.attack_x, (int)transform.position.z + this.attack_y].hp , map_creat.map_ex[(int)transform.position.x, (int)transform.position.z].attack , map_creat.map_ex[(int)transform.position.x + this.attack_x, (int)transform.position.z + this.attack_y].defence);
        }
        GameManager.instance.Playerturn = false;
    }

    


    //階段
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "kaidan")
        {
            GameManager.instance.Pose = true;
           
            GameManager.instance.Playerturn = false;

            
            GameManager.instance.enemies.Clear();

            SceneManager.LoadScene("Dangyon");
            
        }
    }
}

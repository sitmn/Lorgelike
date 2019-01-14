using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_script : MonoBehaviour {
    
    private int moveX;
    private int moveY;
    private bool attack;
    private int attack_x, attack_y;


    public LayerMask blockinglayer;
    public LayerMask enemylayer;

    public bool notmove,vectorchange,menu;

    public Vector3 direction;

    public RaycastHit enemyhit;

    private Transform playerpos;

    public int playerpow = 1;



    // Use this for initialization
    void Start()
    {
        this.notmove = true;
        this.playerpos = GetComponent<Transform>();
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
    }


    private void PlayerMove()
    {
        //方向キーの向きに回転、障害物を確認
        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            this.moveY = 1;
            transform.eulerAngles = new Vector3(0, 270, 0);
            this.notmove = Physics.Linecast(playerpos.position, playerpos.position + new Vector3(this.moveX, 0, this.moveY), this.blockinglayer);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            this.moveX = -1;

            transform.eulerAngles = new Vector3(0, 180, 0);
            this.notmove = Physics.Linecast(playerpos.position, playerpos.position + new Vector3(this.moveX, 0, this.moveY), this.blockinglayer);
        }
        else if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            this.moveX = 1;
            transform.eulerAngles = new Vector3(0, 0, 0);
            this.notmove = Physics.Linecast(playerpos.position, playerpos.position + new Vector3(this.moveX, 0, this.moveY), this.blockinglayer);
        }
        else if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            this.moveY = -1;
            transform.eulerAngles = new Vector3(0, 90, 0);
            this.notmove = Physics.Linecast(playerpos.position, playerpos.position + new Vector3(this.moveX, 0, this.moveY), this.blockinglayer);
        }

        //障害物がなく、向きのみ変更でないとき移動
        if (this.notmove == false && GameManager.instance.Playerturn == true)
        {
            playerpos.position += new Vector3(this.moveX, 0, this.moveY);
            this.notmove = true;
            GameManager.instance.Playerturn = false;
        }
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

        this.attack = Physics.Linecast(playerpos.position, playerpos.position + new Vector3(this.attack_x, 0, this.attack_y), out enemyhit, this.enemylayer);
        if (attack == true)
        {
            enemyhit.collider.gameObject.GetComponent<Enemy_script>().enemydamage(playerpow);
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

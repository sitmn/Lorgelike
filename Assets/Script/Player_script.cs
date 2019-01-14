using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_script : MonoBehaviour {


    public int a;
    public int moveX;
    public int moveY;

    public int attack_x, attack_y;

    public LayerMask blockinglayer;
    public LayerMask enemylayer;

    public bool notmove,vector,test,attack;

    public Vector3 direction;

    public RaycastHit enemyhit;

    Transform playerpos;

    public int playerpow = 1;



    // Use this for initialization
    void Start()
    {
        notmove = true;
    }

    // Update is called once per frame
    public void Update()
    {
        //プレイヤーターンでないときまたはポーズ時は動かない
        if (GameManager.instance.Playerturn == false||GameManager.instance.Pose==true)
        {
            
            
            return;
        }

        //移動せず向きのみ変える
        if (Input.GetKey(KeyCode.C))
        {
            vector = true;
        }

            playerpos = this.transform;
            if (Input.GetKey(KeyCode.UpArrow) == true)
            {
                moveY = 1;
            transform.eulerAngles = new Vector3(0, 270, 0);
            attack_x = 0;
            attack_y = 1;
            notmove = Physics.Linecast(playerpos.position, playerpos.position + new Vector3(moveX, 0, moveY), blockinglayer);
        }
            else if (Input.GetKey(KeyCode.LeftArrow) == true)
            {
                moveX = -1;
            
            transform.eulerAngles = new Vector3(0, 180, 0);
            attack_x = -1;
            attack_y = 0;
            notmove = Physics.Linecast(playerpos.position, playerpos.position + new Vector3(moveX, 0, moveY), blockinglayer);
        }
            else if (Input.GetKey(KeyCode.RightArrow) == true)
            {
                moveX = 1;
            transform.eulerAngles = new Vector3(0, 0, 0);
            attack_x = 1;
            attack_y = 0;
            notmove = Physics.Linecast(playerpos.position, playerpos.position + new Vector3(moveX, 0, moveY), blockinglayer);
        }
            else if (Input.GetKey(KeyCode.DownArrow) == true)
            {
                moveY = -1;
            transform.eulerAngles = new Vector3(0, 90, 0);
            attack_x = 0;
            attack_y = -1;
            notmove = Physics.Linecast(playerpos.position, playerpos.position + new Vector3(moveX, 0, moveY), blockinglayer);
        }

        if (Input.GetKey(KeyCode.Z) == true)
        {
            
            attack = Physics.Linecast(playerpos.position, playerpos.position + new Vector3(attack_x, 0, attack_y), out enemyhit, enemylayer);
            if (attack == true)
            {
                enemyhit.collider.gameObject.GetComponent<Enemy_script>().enemydamage(playerpow);
            }
            GameManager.instance.Playerturn = false;
            

        }
            
        //障害物がなく、向きのみ変更でないとき移動
            if (notmove == false&&vector==false&&GameManager.instance.Playerturn==true)
            {
            Debug.Log("a");
            playerpos.position += new Vector3(moveX, 0, moveY);
            notmove = true;
            GameManager.instance.Playerturn = false;
            
        }
            //変数リセット
            moveX = 0;
            moveY = 0;
        vector = false;
        
       
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

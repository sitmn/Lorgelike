using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public static bool menu_one;

    private bool itemscreen,weaponscreen,developscreen;

    public GameObject MenuScreen;
    public GameObject ItemScreen;
    public GameObject WeaponScreen;
    public GameObject DevelopScreen;
    public GameObject MaterialsNumbersScreen;

    public GameObject ItemDescriptionScreen;
    public GameObject WeaponDescriptionScreen;
    public GameObject DevelopDescriptionScreen;

    public GameObject item_use_button;
    public GameObject item_put_button;

    public GameObject weapon_use_button;
    public GameObject weapon_put_button;

    public GameObject develop_do_button;

    public GameObject ItemView;
    public GameObject WeaponView;
    public GameObject DevelopView;

    private GameObject Player;

    private Player_script player_script;
    private ScrollItemButton item_button_script;

    public Text ItemDescription;
    public Text WeaponDescription;
    public Text DevelopDescription;

    public Text Materials1_text;
    public Text Materials2_text;
    public Text Materials3_text;
    public Text Develop_Need_MP_text;

    public Text Weapon_HP_Text;
    public Text Weapon_Attack_Text;
    public Text Weapon_Defense_Text;
    public Text Weapon_Range_Text;
    public Text Weapon_Through_Text;
    public Text Weapon_Slanting_Text;
    public Text Weapon_MP_Cost;
    public Text Weapon_Endurance;

    public Text Develop_Weapon_HP_Text;
    public Text Develop_Weapon_Attack_Text;
    public Text Develop_Weapon_Defense_Text;
    public Text Develop_Weapon_Range_Text;
    public Text Develop_Weapon_Through_Text;
    public Text Develop_Weapon_Slanting_Text;
    public Text Develop_Weapon_MP_Cost;
    public Text Develop_Weapon_Endurance;

    void awake()
    {
        menu_one = true;
    }

    // Use this for initialization
    void Start () {
        itemscreen = false;
        weaponscreen = false;
        developscreen = false;
        MenuScreen.SetActive(false);
        ItemScreen.SetActive(false);
        WeaponScreen.SetActive(false);
        DevelopScreen.SetActive(false);
        ItemDescriptionScreen.SetActive(false);
        WeaponDescriptionScreen.SetActive(false);
        DevelopDescriptionScreen.SetActive(false);
        MaterialsNumbersScreen.SetActive(false);

        Player = GameObject.Find("Player");
        player_script = Player.GetComponent<Player_script>();
        item_button_script = ItemView.GetComponent<ScrollItemButton>();
	}
	
	// Update is called once per frame
	void Update () {

        if (menu_one == true)
        {
            //Awake、Startの代わり

            menu_one = false;
            Player = GameObject.Find("Player");
            player_script = Player.GetComponent<Player_script>();

            
        }

        if (GameManager.instance.Menu == false)
        {
            return;
        }
        

        
	}

    public void MenuButton() 
    {   if (GameManager.instance.Playerturn == true && GameManager.instance.PlayerMoving == false)
        {
            GameManager.instance.Menu = true;

            item_button_script.ListItemRegistration();
            item_button_script.ListWeaponRegistration();
            item_button_script.ListDevelopRegistration();

            MenuScreen.SetActive(true);
        }
    }

    public void ItemButton()
    {   if (weaponscreen == false && developscreen == false)
        {
            itemscreen = true;
            ItemScreen.SetActive(true);
        }
    }

    public void WeaponButton()
    {   if (itemscreen == false && developscreen == false)
        {
            weaponscreen = true;
            WeaponScreen.SetActive(true);
        }
    }

    public void DevelopButton()
    {
        if(itemscreen == false && weaponscreen == false)
        {
            developscreen = true;
            DevelopScreen.SetActive(true);


            MaterialsNumbersScreen.SetActive(true);
            
        }
    }

    public void BackButton()
    {
        if (itemscreen == true)
        {
            itemscreen = false;
            ItemScreen.SetActive(false);
            ItemDescriptionScreen.SetActive(false);
        } else if (weaponscreen == true)
        {
            weaponscreen = false;
            WeaponScreen.SetActive(false);
            WeaponDescriptionScreen.SetActive(false);
        } else if (developscreen == true)
        {
            developscreen = false;
            DevelopScreen.SetActive(false);
            DevelopDescriptionScreen.SetActive(false);
        }
        else
        {
            GameManager.instance.Menu = false;
            MenuScreen.SetActive(false);

            GameManager.instance.PlayerMoving = false;
        }
    }

    public void PickUpButton()
    {   if (itemscreen == false && weaponscreen == false && developscreen == false)
        {
                BackButton();

                player_script.PickUpItem();

                GameManager.instance.Playerturn = false;
        }
    }

    public void Item_Description(item item_use, int argument)
    {
        item_use_button.transform.GetComponent<Button>().onClick.RemoveAllListeners();
        item_use_button.transform.GetComponent<Button>().onClick.AddListener(() => item_use.use(argument));

        ItemDescriptionScreen.SetActive(true);

        ItemDescription.text = item_use.item_description;

    }

    public void Weapon_Description(weapon weapon_use, int argument)
    {
        weapon_use_button.transform.GetComponent<Button>().onClick.RemoveAllListeners();
        weapon_use_button.transform.GetComponent<Button>().onClick.AddListener(() => weapon_use.installing(argument));

        WeaponDescriptionScreen.SetActive(true);
        WeaponDescription.text = weapon_use.weapon_description;

        Weapon_HP_Text.text = weapon_use.HP_W.ToString();
        Weapon_Attack_Text.text = weapon_use.ATTACK_W.ToString();
        Weapon_Defense_Text.text = weapon_use.DEFENSE_W.ToString();
        Weapon_Range_Text.text = weapon_use.ATTACK_RANGE_W.ToString();
        if (weapon_use.ATTACK_THROUGH_W == true)
        {
            Weapon_Through_Text.text = "〇";
        }
        else if (weapon_use.ATTACK_THROUGH_W == false)
        {
            Weapon_Through_Text.text = "×";
        }
        if (weapon_use.SLANTING_WALL_W == true)
        {
            Weapon_Slanting_Text.text = "〇";
        }
        else if (weapon_use.SLANTING_WALL_W == false)
        {
            Weapon_Slanting_Text.text = "×";
        }

        Weapon_MP_Cost.text = weapon_use.MP_COST_W.ToString();
        Weapon_Endurance.text = weapon_use.ENDURANCE_W.ToString();

    }

    public void Develop_Item_Description(map_item develop_do)
    {
        develop_do_button.transform.GetComponent<Button>().onClick.RemoveAllListeners();
        develop_do_button.transform.GetComponent<Button>().onClick.AddListener(() => make_item(develop_do));

        Materials1_text.text = GameManager.instance.possession_material_1 + "/" + develop_do.develop_need_material1;
        Materials2_text.text = GameManager.instance.possession_material_2 + "/" + develop_do.develop_need_material2;
        Materials3_text.text = GameManager.instance.possession_material_3 + "/" + develop_do.develop_need_material3;
        Develop_Need_MP_text.text = player.player_mp + "/" + develop_do.develop_need_MP;

        DevelopDescriptionScreen.SetActive(true);

        DevelopDescription.text = develop_do.develop_description;

        Develop_Weapon_HP_Text.text = "-";
        Develop_Weapon_Attack_Text.text = "-";
        Develop_Weapon_Defense_Text.text = "-";
        Develop_Weapon_Range_Text.text = "-";
        Develop_Weapon_Through_Text.text = "-";
        Develop_Weapon_Slanting_Text.text = "-";
        Develop_Weapon_MP_Cost.text = "-";
        Develop_Weapon_Endurance.text = "-";

    }

    public void make_item(map_item develop_item)
    {
        GameManager.instance.PlayerMoving = true;

        map_item new_item;

        if (((develop_item.develop_need_material1 > GameManager.instance.possession_material_1) || (develop_item.develop_need_material2 > GameManager.instance.possession_material_2) || (develop_item.develop_need_material3 > GameManager.instance.possession_material_3)))
        {

            GameManager.instance.AddMainText("素材が足りない");
        }else if (develop_item.develop_need_MP > player.player_mp)
        {
            GameManager.instance.AddMainText("ＭＰが足りない");
        }else if(GameManager.instance.possessionitemlist.Count > GameManager.instance.MAX_ITEM)
        {
            GameManager.instance.AddMainText("持ち物が一杯で合成できない");
        }
        else
        {
            GameManager.instance.possession_material_1 -= develop_item.develop_need_material1;
            GameManager.instance.possession_material_2 -= develop_item.develop_need_material2;
            GameManager.instance.possession_material_3 -= develop_item.develop_need_material3;

            if (develop_item.name == "回復薬")
            {
                new_item = new item1(map_creat.NAME_I1, map_creat.HEAL_POINT_I1, map_creat.DEVELOP_I1_MATERIAL_1, map_creat.DEVELOP_I1_MATERIAL_2, map_creat.DEVELOP_I1_MATERIAL_3, map_creat.DEVELOP_NEED_MP_I1);

                //作成した薬草の回復量にばらつき
                float float_heal_point = new_item.heal_point;
                float_heal_point = float_heal_point + float_heal_point * (float)Develop_Random_State(new_item.heal_point) / 100f;
                new_item.heal_point = (int)float_heal_point;

                //テキストも変更
                new_item.item_description = "体力を" + new_item.heal_point + "回復する。";
                
            }
            else if (develop_item.name == "爆弾")
            {
                new_item = new item2(map_creat.NAME_I2, map_creat.ATTACK_POINT, map_creat.ATTACK_RANGE, map_creat.ATTACK_TYPE, map_creat.DEVELOP_I2_MATERIAL_1, map_creat.DEVELOP_I2_MATERIAL_2, map_creat.DEVELOP_I2_MATERIAL_3, map_creat.DEVELOP_NEED_MP_I2);
                develop_item.heal_point *= Develop_Random_State(develop_item.attack_point);
                develop_item.heal_point *= Develop_Random_State(develop_item.attack_range);
                develop_item.heal_point *= Develop_Random_State(develop_item.attack_type);
                
            }
            else if (develop_item.name == "場所替え")
            {
                new_item = new item3(map_creat.NAME_I3, map_creat.DEVELOP_I3_MATERIAL_1, map_creat.DEVELOP_I3_MATERIAL_2, map_creat.DEVELOP_I3_MATERIAL_3, map_creat.DEVELOP_NEED_MP_I3);

            }
            else if (develop_item.name == "回復薬（特）")
            {
                new_item = new item4(map_creat.NAME_I4, map_creat.HEAL_POINT_I4, map_creat.DEVELOP_I4_MATERIAL_1, map_creat.DEVELOP_I4_MATERIAL_2, map_creat.DEVELOP_I4_MATERIAL_3, map_creat.DEVELOP_NEED_MP_I4);

                //作成した薬草の回復量にばらつき
                float float_heal_point = new_item.heal_point;
                float_heal_point = float_heal_point + float_heal_point * (float)Develop_Random_State(new_item.heal_point) / 100f;
                new_item.heal_point = (int)float_heal_point;

                //テキストも変更
                new_item.item_description = "体力を" + new_item.heal_point + "回復する。";

            }
            else //ここには入らない
            {
                new_item = new item();
            }

            player_script.Success_Develop();

            GameManager.instance.AddMainText(develop_item.name + "を合成した");

            player.player_mp -= develop_item.develop_need_MP;

            GameManager.instance.Mp_Bar();
            GameManager.instance.MP_Text();

            GameManager.instance.AddListItem(new_item);
        }
        

        BackButton();
        BackButton();
    }
     

    public void Develop_Weapon_Description(map_item develop_do)
    {
        weapon develop_weapon = develop_do as weapon;

        develop_do_button.transform.GetComponent<Button>().onClick.RemoveAllListeners();
        develop_do_button.transform.GetComponent<Button>().onClick.AddListener(() => make_weapon(develop_do));

        Materials1_text.text = GameManager.instance.possession_material_1 + "/" + develop_do.develop_need_material1;
        Materials2_text.text = GameManager.instance.possession_material_2 + "/" + develop_do.develop_need_material2;
        Materials3_text.text = GameManager.instance.possession_material_3 + "/" + develop_do.develop_need_material3;
        Develop_Need_MP_text.text = player.player_mp + "/" + develop_do.develop_need_MP;

        DevelopDescriptionScreen.SetActive(true);

        DevelopDescription.text = develop_do.develop_description;

        Develop_Weapon_HP_Text.text = develop_weapon.HP_W.ToString();
        Develop_Weapon_Attack_Text.text = develop_weapon.ATTACK_W.ToString();
        Develop_Weapon_Defense_Text.text = develop_weapon.DEFENSE_W.ToString();
        Develop_Weapon_Range_Text.text = develop_weapon.ATTACK_RANGE_W.ToString();
        if (develop_weapon.ATTACK_THROUGH_W == true)
        {
            Develop_Weapon_Through_Text.text = "〇";
        }else if(develop_weapon.ATTACK_THROUGH_W == false)
        {
            Develop_Weapon_Through_Text.text = "×";
        }
        if (develop_weapon.SLANTING_WALL_W == true)
        {
            Develop_Weapon_Slanting_Text.text = "〇";
        }
        else if(develop_weapon.SLANTING_WALL_W == false){
            Develop_Weapon_Slanting_Text.text = "×";
        }

        Develop_Weapon_MP_Cost.text = develop_weapon.MP_COST_W.ToString();
        Develop_Weapon_Endurance.text = develop_weapon.ENDURANCE_W.ToString();

    }

    public void make_weapon(map_item develop_item)
    {
        GameManager.instance.PlayerMoving = true;

        weapon new_weapon;
        
        if (((develop_item.develop_need_material1 > GameManager.instance.possession_material_1) || (develop_item.develop_need_material2 > GameManager.instance.possession_material_2) || (develop_item.develop_need_material3 > GameManager.instance.possession_material_3)))
        {
            GameManager.instance.AddMainText("素材が足りない");
        }else if (develop_item.develop_need_MP > player.player_mp)
        {
            GameManager.instance.AddMainText("ＭＰが足りない");
        }
        else if (GameManager.instance.possessionweaponlist.Count > GameManager.instance.MAX_WEAPON)
        {
            GameManager.instance.AddMainText("持ち物が一杯で合成できない");
        }
        else
        {
            GameManager.instance.possession_material_1 -= develop_item.develop_need_material1;
            GameManager.instance.possession_material_2 -= develop_item.develop_need_material2;
            GameManager.instance.possession_material_3 -= develop_item.develop_need_material3;
            
            if (develop_item.name == map_creat.NAME_W1)
            {
                new_weapon = new weapon1(map_creat.NAME_W1, map_creat.HP_W1, map_creat.ATTACK_W1, map_creat.DEFENSE_W1, map_creat.ATTACK_RANGE_W1, map_creat.ATTACK_TYPE_W1, map_creat.ATTACK_THROUGH_W1, map_creat.SLANTING_WALL_W1, map_creat.DEVELOP_W1_MATERIAL_1, map_creat.DEVELOP_W1_MATERIAL_2, map_creat.DEVELOP_W1_MATERIAL_3 , map_creat.MP_COST_W1 , map_creat.ENDURANCE_W1, map_creat.DEVELOP_NEED_MP_W1);
            }
            else if (develop_item.name == map_creat.NAME_W2)
            {
                new_weapon = new weapon2(map_creat.NAME_W2, map_creat.HP_W2, map_creat.ATTACK_W2, map_creat.DEFENSE_W2, map_creat.ATTACK_RANGE_W2, map_creat.ATTACK_TYPE_W2, map_creat.ATTACK_THROUGH_W2, map_creat.SLANTING_WALL_W2, map_creat.DEVELOP_W2_MATERIAL_1, map_creat.DEVELOP_W2_MATERIAL_2, map_creat.DEVELOP_W2_MATERIAL_3, map_creat.MP_COST_W2, map_creat.ENDURANCE_W2, map_creat.DEVELOP_NEED_MP_W2);
            }
            else if (develop_item.name == map_creat.NAME_W3)
            {
                new_weapon = new weapon3(map_creat.NAME_W3, map_creat.HP_W3, map_creat.ATTACK_W3, map_creat.DEFENSE_W3, map_creat.ATTACK_RANGE_W3, map_creat.ATTACK_TYPE_W3, map_creat.ATTACK_THROUGH_W3, map_creat.SLANTING_WALL_W3, map_creat.DEVELOP_W3_MATERIAL_1, map_creat.DEVELOP_W3_MATERIAL_2, map_creat.DEVELOP_W3_MATERIAL_3, map_creat.MP_COST_W3, map_creat.ENDURANCE_W3, map_creat.DEVELOP_NEED_MP_W3);
            }
            else if (develop_item.name == map_creat.NAME_W4)
            {
                new_weapon = new weapon4(map_creat.NAME_W4, map_creat.HP_W4, map_creat.ATTACK_W4, map_creat.DEFENSE_W4, map_creat.ATTACK_RANGE_W4, map_creat.ATTACK_TYPE_W4, map_creat.ATTACK_THROUGH_W4, map_creat.SLANTING_WALL_W4, map_creat.DEVELOP_W4_MATERIAL_1, map_creat.DEVELOP_W4_MATERIAL_2, map_creat.DEVELOP_W4_MATERIAL_3, map_creat.MP_COST_W4, map_creat.ENDURANCE_W4, map_creat.DEVELOP_NEED_MP_W4);
            }
            else if (develop_item.name == map_creat.NAME_W5)
            {
                new_weapon = new weapon5(map_creat.NAME_W5, map_creat.HP_W5, map_creat.ATTACK_W5, map_creat.DEFENSE_W5, map_creat.ATTACK_RANGE_W5, map_creat.ATTACK_TYPE_W5, map_creat.ATTACK_THROUGH_W5, map_creat.SLANTING_WALL_W5, map_creat.DEVELOP_W5_MATERIAL_1, map_creat.DEVELOP_W5_MATERIAL_2, map_creat.DEVELOP_W5_MATERIAL_3, map_creat.MP_COST_W5, map_creat.ENDURANCE_W5, map_creat.DEVELOP_NEED_MP_W5);
            }
            else  //ここには入らない
            {
                new_weapon = new weapon();
            }

            float float_HP_W = new_weapon.HP_W;
            float_HP_W += float_HP_W * (float)Develop_Random_State(new_weapon.HP_W) / 100f;
            new_weapon.HP_W = (int)float_HP_W;

            float float_ATTACK_W = new_weapon.ATTACK_W;
            float_ATTACK_W += float_ATTACK_W * (float)Develop_Random_State(new_weapon.ATTACK_W) / 100f;
            new_weapon.ATTACK_W = (int)float_ATTACK_W;

            float float_DEFENSE_W = new_weapon.DEFENSE_W;
            float_DEFENSE_W += float_DEFENSE_W * (float)Develop_Random_State(new_weapon.DEFENSE_W) / 100f;
            new_weapon.DEFENSE_W = (int)float_DEFENSE_W;

            float float_ATTACK_RANGE_W = new_weapon.ATTACK_RANGE_W;
            float_ATTACK_RANGE_W += float_ATTACK_RANGE_W * (float)Develop_Random_State(new_weapon.ATTACK_RANGE_W) / 100f;
            new_weapon.ATTACK_RANGE_W = (int)float_ATTACK_RANGE_W;

            player_script.Success_Develop();

            GameManager.instance.AddMainText(develop_item.name +"を合成した");

            player.player_mp -= develop_item.develop_need_MP;

            GameManager.instance.Mp_Bar();
            GameManager.instance.MP_Text();

            GameManager.instance.AddListWeapon(new_weapon);
        }
        
        BackButton();
        BackButton();
    }

    private int Develop_Random_State(int state)
    {
        bool plus_minus;
        int i = Random.Range(0, 1);

        if (i == 0)
        {
            plus_minus = true;
        }
        else
        {
            plus_minus = false;
        }


        int random1, random2;

        random1 = Random.Range(0, 9);

        //10%の確率で乱数大に
        if (random1 == 0)
        {
            random2 = Random.Range(0, 100);

        }
        else
        {
            random2 = Random.Range(0, 30);
        }

        if (plus_minus == false)
        {
            random2 = -random2;
        }
        
        return random2;
    }
}

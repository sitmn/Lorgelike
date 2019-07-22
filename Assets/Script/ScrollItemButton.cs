using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollItemButton : MonoBehaviour
{

    public GameObject ButtonPrefab;

    public GameObject ItemContent;
    public GameObject WeaponContent;
    public GameObject DevelopContent;

    private RectTransform itemcontent;
    private RectTransform weaponcontent;
    private RectTransform developcontent;

    public GameObject[] scrollitembuttons;
    public GameObject[] scrollweaponbuttons;
    public GameObject[] scrolldevelopbuttons;

    public GameObject ItemDescriptionScreen;
    private string ItemText;

    public GameObject WeaponDescriptionScreen;
    private string WeaponText;

    public GameObject DevelopDescriptionScreen;
    private string DevelopText;

    public GameObject MenuController;
    private MenuController MenuController_script;

    private int developtopics = 0;

    // Use this for initialization
    void Start()
    {
        itemcontent = ItemContent.GetComponent<RectTransform>();
        weaponcontent = WeaponContent.GetComponent<RectTransform>();
        developcontent = DevelopContent.GetComponent<RectTransform>();

        float buttonitemspace = ItemContent.GetComponent<VerticalLayoutGroup>().spacing;
        float buttonitemheight = ButtonPrefab.GetComponent<LayoutElement>().preferredHeight;
        itemcontent.sizeDelta = new Vector2(0, (buttonitemheight + buttonitemspace) * GameManager.instance.possessionitemlist.Count);

        float buttonweaponspace = WeaponContent.GetComponent<VerticalLayoutGroup>().spacing;
        float buttonweaponheight = ButtonPrefab.GetComponent<ILayoutElement>().preferredHeight;
        weaponcontent.sizeDelta = new Vector2(0, (buttonweaponheight + buttonweaponspace) * GameManager.instance.possessionweaponlist.Count);

        float buttondevelopspace = DevelopContent.GetComponent<VerticalLayoutGroup>().spacing;
        float buttondevelopheight = ButtonPrefab.GetComponent<LayoutElement>().preferredHeight;
        developcontent.sizeDelta = new Vector2(0, (buttondevelopheight + buttondevelopspace) * GameManager.instance.developtopiclist.Count);

        MenuController_script = MenuController.GetComponent<MenuController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ListItemRegistration()
    {
        //メニュー開くごとに、配列のオブジェクトを全て消して、リストから全て生成
        for (int i = 0; i < GameManager.instance.MAX_ITEM; i++)
        {
            Destroy(scrollitembuttons[i]);

        }
        for (int i = 0; i < GameManager.instance.possessionitemlist.Count; i++)
        {
            scrollitembuttons[i] = ((GameObject)Instantiate(ButtonPrefab));

            Debug.Log(scrollitembuttons[i]);
            scrollitembuttons[i].transform.SetParent(itemcontent, false);
            scrollitembuttons[i].transform.GetComponentInChildren<Text>().text = GameManager.instance.possessionitemlist[i].name;

            int argument = i + 0;   //AddListenerで呼び出す関数の引数は、ガウスを使用しているため（？）一度0をプラスして正常な数字に

            if (GameManager.instance.possessionitemlist[i].name == "回復薬")
            {
                item item1 = GameManager.instance.possessionitemlist[i] as item1;
                
                scrollitembuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => MenuController_script.Item_Description(item1, argument)); //item1.healuse(argument));
            }
            else if (GameManager.instance.possessionitemlist[i].name == "爆弾")
            {
                item item2 = GameManager.instance.possessionitemlist[i] as item2;
                
                scrollitembuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => MenuController_script.Item_Description(item2, argument)); //item2.attackuse(argument));
            }
            else if (GameManager.instance.possessionitemlist[i].name == "場所替え")
            {
                item item3 = GameManager.instance.possessionitemlist[i] as item3;
                
                scrollitembuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => MenuController_script.Item_Description(item3, argument)); //item3.changeuse(argument));
            }else if (GameManager.instance.possessionitemlist[i].name == "回復薬（特）")
            {
                item item4 = GameManager.instance.possessionitemlist[i] as item4;

                scrollitembuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => MenuController_script.Item_Description(item4, argument)); //item1.healuse(argument));
            }
        }

        /*if(GameManager.instance.possessionitemlist[i].number == 0)
        {
            item1 item1 = GameManager.instance.possessionitemlist[i] as item1;
            GameManager.instance.scrollbuttonlist[i].transform.GetComponent<Button>().onClick.AddListener(() => item1.healuse(i));
        }*/

        //button.transform.GetComponent<Button>().onClick.AddListener(() => OnClick(no)); ,OnClickに関数を入れる
    }

    public void ListWeaponRegistration()
    {
        //メニュー開くごとに、配列のオブジェクトを全て消して、リストから全て生成
        for (int i = 0; i < GameManager.instance.MAX_WEAPON; i++)
        {
            Destroy(scrollweaponbuttons[i]);
        }
        for (int i = 0; i < GameManager.instance.possessionweaponlist.Count; i++)
        {
            scrollweaponbuttons[i] = ((GameObject)Instantiate(ButtonPrefab));

            Debug.Log(scrollweaponbuttons[i]);
            scrollweaponbuttons[i].transform.SetParent(weaponcontent, false);
            scrollweaponbuttons[i].transform.GetComponentInChildren<Text>().text = GameManager.instance.possessionweaponlist[i].name;

            int argument = i + 0;   //AddListenerで呼び出す関数の引数は、ガウスを使用しているため（？）一度0をプラスして正常な数字に

            if (GameManager.instance.possessionweaponlist[i].name.Contains("アクアマリン") == true)
            {
                weapon weapon1 = GameManager.instance.possessionweaponlist[i] as weapon;

                scrollweaponbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => MenuController_script.Weapon_Description(weapon1, argument)); //weapon1.installing(argument));
            }
            else if (GameManager.instance.possessionweaponlist[i].name.Contains("アメシスト") == true)
            {
                weapon weapon2 = GameManager.instance.possessionweaponlist[i] as weapon;

                scrollweaponbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => MenuController_script.Weapon_Description(weapon2, argument)); //weapon2.installing(argument));
            }
            else if (GameManager.instance.possessionweaponlist[i].name.Contains("エメラルド") == true)
            {
                weapon weapon3 = GameManager.instance.possessionweaponlist[i] as weapon;

                scrollweaponbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => MenuController_script.Weapon_Description(weapon3, argument)); //weapon3.installing(argument));
            }
            else if (GameManager.instance.possessionweaponlist[i].name.Contains("ルビー") == true)
            {
                weapon weapon4 = GameManager.instance.possessionweaponlist[i] as weapon;

                scrollweaponbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => MenuController_script.Weapon_Description(weapon4, argument)); //weapon3.installing(argument));
            }
            else if (GameManager.instance.possessionweaponlist[i].name.Contains("クリスタル") == true)
            {
                weapon weapon5 = GameManager.instance.possessionweaponlist[i] as weapon;

                scrollweaponbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => MenuController_script.Weapon_Description(weapon5, argument)); //weapon3.installing(argument));
            }
        }

        /*if(GameManager.instance.possessionitemlist[i].number == 0)
        {
            item1 item1 = GameManager.instance.possessionitemlist[i] as item1;
            GameManager.instance.scrollbuttonlist[i].transform.GetComponent<Button>().onClick.AddListener(() => item1.healuse(i));
        }*/

        //button.transform.GetComponent<Button>().onClick.AddListener(() => OnClick(no)); ,OnClickに関数を入れる
    }

    public void ListDevelopRegistration()
    {
        

        //合成アイテム種類が増えた際、ボタンをやり替える
        if (developtopics < GameManager.instance.developtopiclist.Count)
        {
            
            for (int i = 0; i < GameManager.instance.developtopiclist.Count; i++)
            {
                Destroy(scrolldevelopbuttons[i]);
            }
                
                for (int i = 0; i < GameManager.instance.developtopiclist.Count; i++)
            {
                scrolldevelopbuttons[i] = ((GameObject)Instantiate(ButtonPrefab));

                Debug.Log(scrolldevelopbuttons[i]);
                scrolldevelopbuttons[i].transform.SetParent(developcontent, false);
                scrolldevelopbuttons[i].transform.GetComponentInChildren<Text>().text = GameManager.instance.developtopiclist[i].name;

                
                item item1 = GameManager.instance.developtopiclist[i] as item1;
                int argument = i + 0;   //AddListenerで呼び出す関数の引数は、ガウスを使用しているため（？）一度0をプラスして正常な数字に


                if (GameManager.instance.developtopiclist[i].name == "回復薬")
                {
                    item item = GameManager.instance.developtopiclist[i] as item1;
                    
                    scrolldevelopbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => MenuController_script.Develop_Item_Description(item)); //item1.healuse(argument));
                }
                else if (GameManager.instance.developtopiclist[i].name == "爆弾")
                {
                    item item2 = GameManager.instance.developtopiclist[i] as item2;

                    scrolldevelopbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => MenuController_script.Develop_Item_Description(item2)); //item2.attackuse(argument));
                }
                else if (GameManager.instance.developtopiclist[i].name == "場所替え")
                {
                    item item3 = GameManager.instance.developtopiclist[i] as item3;

                    scrolldevelopbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => MenuController_script.Develop_Item_Description(item3)); //item3.changeuse(argument));
                }else if (GameManager.instance.developtopiclist[i].name == "回復薬（特）")
                {
                    item item4 = GameManager.instance.developtopiclist[i] as item4;

                    scrolldevelopbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => MenuController_script.Develop_Item_Description(item4)); //item1.healuse(argument));
                }
                else if (GameManager.instance.developtopiclist[i].name.Contains("アクアマリン") == true)
                {
                weapon weapon1 = GameManager.instance.developtopiclist[i] as weapon;

                    scrolldevelopbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => MenuController_script.Develop_Weapon_Description(weapon1)); //weapon1.installing(argument));
                }
                else if (GameManager.instance.developtopiclist[i].name.Contains("アメシスト") == true)
                {
                weapon weapon2 = GameManager.instance.developtopiclist[i] as weapon;

                    scrolldevelopbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => MenuController_script.Develop_Weapon_Description(weapon2)); //weapon2.installing(argument));
                }
                else if (GameManager.instance.developtopiclist[i].name.Contains("エメラルド") == true)
                {
                weapon weapon3 = GameManager.instance.developtopiclist[i] as weapon;

                    scrolldevelopbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => MenuController_script.Develop_Weapon_Description(weapon3)); //weapon3.installing(argument));
                }
                else if (GameManager.instance.developtopiclist[i].name.Contains("ルビー") == true)
                {
                    weapon weapon4 = GameManager.instance.developtopiclist[i] as weapon;

                    scrolldevelopbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => MenuController_script.Develop_Weapon_Description(weapon4)); //weapon3.installing(argument));
                }
                else if (GameManager.instance.developtopiclist[i].name.Contains("クリスタル") == true)
                {
                    weapon weapon5 = GameManager.instance.developtopiclist[i] as weapon;

                    scrolldevelopbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => MenuController_script.Develop_Weapon_Description(weapon5)); //weapon3.installing(argument));
                }

                DevelopText = GameManager.instance.developtopiclist[i].develop_description;


                }
                developtopics = GameManager.instance.developtopiclist.Count;
        }




    }
    

}

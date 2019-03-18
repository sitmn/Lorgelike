using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollItemButton : MonoBehaviour {

    public GameObject ButtonPrefab;

    public GameObject ItemContent;
    public GameObject WeaponContent;
    
    private RectTransform itemcontent;
    private RectTransform weaponcontent;

    public GameObject[] scrollitembuttons;
    public GameObject[] scrollweaponbuttons;

    // Use this for initialization
    void Start () {
        itemcontent = ItemContent.GetComponent<RectTransform>();
        weaponcontent = WeaponContent.GetComponent<RectTransform>();

        float buttonitemspace = ItemContent.GetComponent<VerticalLayoutGroup>().spacing;
        float buttonitemheight = ButtonPrefab.GetComponent<LayoutElement>().preferredHeight;
        itemcontent.sizeDelta = new Vector2(0, (buttonitemheight + buttonitemspace) * GameManager.instance.possessionitemlist.Count);

        float buttonweaponspace = WeaponContent.GetComponent<VerticalLayoutGroup>().spacing;
        float buttonweaponheight = ButtonPrefab.GetComponent<ILayoutElement>().preferredHeight;
        weaponcontent.sizeDelta = new Vector2(0, (buttonweaponheight + buttonweaponspace) * GameManager.instance.possessionweaponlist.Count);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ListItemRegistration()
    {   
        //メニュー開くごとに、配列のオブジェクトを全て消して、リストから全て生成
            for(int i = 0;i < GameManager.instance.MAX_ITEM ; i++)
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
                
                if (GameManager.instance.possessionitemlist[i].name == "薬草")
                {
                    item1 item1 = GameManager.instance.possessionitemlist[i] as item1;
                    scrollitembuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => item1.healuse(argument));
                }else if(GameManager.instance.possessionitemlist[i].name == "爆弾")
                {
                    item2 item2 = GameManager.instance.possessionitemlist[i] as item2;
                    scrollitembuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => item2.attackuse(argument));
                }else if(GameManager.instance.possessionitemlist[i].name == "場所替え")
                {
                item3 item3 = GameManager.instance.possessionitemlist[i] as item3;
                scrollitembuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => item3.changeuse(argument));
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

            if (GameManager.instance.possessionweaponlist[i].name.Contains("ロングソード") == true)
            {
                weapon weapon1 = GameManager.instance.possessionweaponlist[i] as weapon;
                scrollweaponbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => weapon1.installing(argument));
            }
            else if (GameManager.instance.possessionweaponlist[i].name.Contains("ショットガン") == true)
            {
                weapon weapon2 = GameManager.instance.possessionweaponlist[i] as weapon;
                scrollweaponbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => weapon2.installing(argument));
            }
            else if (GameManager.instance.possessionweaponlist[i].name.Contains("ライフル") == true)
            {
                weapon weapon3 = GameManager.instance.possessionweaponlist[i] as weapon;
                scrollweaponbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => weapon3.installing(argument));
            }
        }

        /*if(GameManager.instance.possessionitemlist[i].number == 0)
        {
            item1 item1 = GameManager.instance.possessionitemlist[i] as item1;
            GameManager.instance.scrollbuttonlist[i].transform.GetComponent<Button>().onClick.AddListener(() => item1.healuse(i));
        }*/

        //button.transform.GetComponent<Button>().onClick.AddListener(() => OnClick(no)); ,OnClickに関数を入れる
    }

}

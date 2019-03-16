using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollWeaponButton : MonoBehaviour {

    public GameObject ButtonPrefab;

    public GameObject WeaponContent;
    
    private RectTransform weaponcontent;

    public GameObject[] scrollweaponbuttons;

    // Use this for initialization
    void Start () {
        weaponcontent = WeaponContent.GetComponent<RectTransform>();

        float buttonspace = WeaponContent.GetComponent<VerticalLayoutGroup>().spacing;
        float buttonheight = ButtonPrefab.GetComponent<LayoutElement>().preferredHeight;
        weaponcontent.sizeDelta = new Vector2(0, (buttonheight + buttonspace) * GameManager.instance.possessionweaponlist.Count);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ListWeaponRegistration()
    {   
        //メニュー開くごとに、配列のオブジェクトを全て消して、リストから全て生成
            for(int i = 0;i < GameManager.instance.MAX_WEAPON; i++)
            {
            Destroy(scrollweaponbuttons[i]);
            }
            for (int i = 0; i < GameManager.instance.possessionweaponlist.Count; i++)
            {
                scrollweaponbuttons[i] = ((GameObject)Instantiate(ButtonPrefab));
                scrollweaponbuttons[i].transform.SetParent(weaponcontent, false);
                scrollweaponbuttons[i].transform.GetComponentInChildren<Text>().text = GameManager.instance.possessionweaponlist[i].name;

                int argument = i + 0;   //AddListenerで呼び出す関数の引数は、ガウスを使用しているため（？）一度0をプラスして正常な数字に
                
                if (GameManager.instance.possessionweaponlist[i].name == "ロングソード")
                {
                    weapon weapon1 = GameManager.instance.possessionweaponlist[i] as weapon;
                    scrollweaponbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => weapon1.installing(argument));
                }else if(GameManager.instance.possessionweaponlist[i].name == "ショットガン")
                {
                    weapon weapon2 = GameManager.instance.possessionweaponlist[i] as weapon;
                    scrollweaponbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => weapon2.installing(argument));
                }else if(GameManager.instance.possessionweaponlist[i].name == "ライフル")
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

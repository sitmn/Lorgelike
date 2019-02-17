using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollButton : MonoBehaviour {

    public GameObject ButtonPrefab;

    public GameObject Content;
    
    private RectTransform content;

    public GameObject[] scrollbuttons;

    // Use this for initialization
    void Start () {
        content = Content.GetComponent<RectTransform>();

        float buttonspace = Content.GetComponent<VerticalLayoutGroup>().spacing;
        float buttonheight = ButtonPrefab.GetComponent<LayoutElement>().preferredHeight;
        content.sizeDelta = new Vector2(0, (buttonheight + buttonspace) * GameManager.instance.possessionitemlist.Count);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ListRegistration()
    {   
        //メニュー開くごとに、配列のオブジェクトを全て消して、リストから全て生成
            for(int i = 0;i < GameManager.instance.MAX_ITEM; i++)
            {
            Destroy(scrollbuttons[i]);
            }
            for (int i = 0; i < GameManager.instance.possessionitemlist.Count; i++)
            {
                scrollbuttons[i] = ((GameObject)Instantiate(ButtonPrefab));
                scrollbuttons[i].transform.SetParent(content, false);
                scrollbuttons[i].transform.GetComponentInChildren<Text>().text = GameManager.instance.possessionitemlist[i].name;

                int argument = i + 0;   //AddListenerで呼び出す関数の引数は、ガウスを使用しているため（？）一度0をプラスして正常な数字に
                
                if (GameManager.instance.possessionitemlist[i].number == 0)
                {
                    item1 item1 = GameManager.instance.possessionitemlist[i] as item1;
                    scrollbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => item1.healuse(argument));
                }else if(GameManager.instance.possessionitemlist[i].number == 1)
                {
                    item2 item2 = GameManager.instance.possessionitemlist[i] as item2;
                    scrollbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => item2.attackuse(argument));
                }else if(GameManager.instance.possessionitemlist[i].number == 2)
                {
                item3 item3 = GameManager.instance.possessionitemlist[i] as item3;
                scrollbuttons[i].transform.GetComponent<Button>().onClick.AddListener(() => item3.changeuse(argument));
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

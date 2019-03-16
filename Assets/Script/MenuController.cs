using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    private bool itemscreen,weaponscreen;

    public GameObject MenuScreen;
    public GameObject ItemScreen;
    public GameObject WeaponScreen;
    public GameObject ItemView;
    public GameObject WeaponView;

    public GameObject Player;

    private Player_script player_script;
    private ScrollItemButton item_button_script;
    private ScrollWeaponButton weapon_button_script;


    

	// Use this for initialization
	void Start () {
        itemscreen = false;
        weaponscreen = false;
        MenuScreen.SetActive(false);
        ItemScreen.SetActive(false);
        WeaponScreen.SetActive(false);

        player_script = Player.GetComponent<Player_script>();
        item_button_script = ItemView.GetComponent<ScrollItemButton>();
        weapon_button_script = WeaponView.GetComponent<ScrollWeaponButton>();
	}
	
	// Update is called once per frame
	void Update () {

		if(GameManager.instance.Menu == false)
        {
            return;
        }
        

        
	}

    public void MenuButton() 
    {   if (GameManager.instance.Playerturn == true && GameManager.instance.PlayerMoving == false)
        {
            GameManager.instance.Menu = true;

            item_button_script.ListItemRegistration();
            weapon_button_script.ListWeaponRegistration();

            MenuScreen.SetActive(true);
        }
    }

    public void ItemButton()
    {   if (weaponscreen == false)
        {
            itemscreen = true;
            ItemScreen.SetActive(true);
        }
    }

    public void WeaponButton()
    {   if (itemscreen == false)
        {
            weaponscreen = true;
            WeaponScreen.SetActive(true);
        }
    }

    public void BackButton()
    {
        if(itemscreen == true)
        {
            itemscreen = false;
            ItemScreen.SetActive(false);
        }else if(weaponscreen == true)
        {
            weaponscreen = false;
            WeaponScreen.SetActive(false);
        }
        else
        {
            GameManager.instance.Menu = false;
            MenuScreen.SetActive(false);
        }
    }

    public void PickUpButton()
    {   if (itemscreen == false && weaponscreen == false)
        {
                BackButton();

                player_script.PickUpItem();

                GameManager.instance.Playerturn = false;
        }
    }
}

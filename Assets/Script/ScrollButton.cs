using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollButton : MonoBehaviour {

    private GameObject ButtonPrefab;

    public GameObject Content;

    const int BUTTON_COUNT = 20;

	// Use this for initialization
	void Start () {
        RectTransform content = Content.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private Button beginButton;
    private Button optionButton;
    private Button exitButton;
	// Use this for initialization
	void Start () {

        InitUI();
        

        
	}
	
	// Update is called once per frame
	void Update () {
        //GUI.Button.
        beginButton.onClick.AddListener(BeginButtonClick);
	}

    private void InitUI()
    {
        beginButton = GameObject.Find("Begin").GetComponent<Button>();
        optionButton = GameObject.Find("Option").GetComponent<Button>();
        exitButton = GameObject.Find("Exit").GetComponent<Button>();

        
    }

    private void BeginButtonClick()
    {
        Debug.Log("Begin button click!");
        SceneManager.LoadScene("main");

    }

    private void OnGUI()
    {
        //GUI.Button = beginButton;
    }
}

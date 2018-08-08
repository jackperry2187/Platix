using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject levelSelectMenu;
    public GameObject settingsMenu;
    
    public enum MenuStates { main, level, settings };
    public MenuStates currentState;

    // Use this for initialization
    void Start () {
        currentState = MenuStates.main;
	}

    // Update is called once per frame
    void Update() {
        switch (currentState)
        {
            case (MenuStates.main):
                mainMenu.SetActive(true);
                levelSelectMenu.SetActive(false);
                settingsMenu.SetActive(false);
                break;

            case (MenuStates.level):
                mainMenu.SetActive(false);
                levelSelectMenu.SetActive(true);
                settingsMenu.SetActive(false);
                break;

            case (MenuStates.settings):
                mainMenu.SetActive(false);
                levelSelectMenu.SetActive(false);
                settingsMenu.SetActive(true);
                break;
        }
    }

    public void OnMain()
    {
        currentState = MenuStates.main;
    }

    public void OnLevel()
    {
        currentState = MenuStates.level;
    }

    public void OnSettings()
    {
        currentState = MenuStates.settings;
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void onLoadLevel(int level)
    {
        SceneManager.LoadScene("Level" + level);
    }
}

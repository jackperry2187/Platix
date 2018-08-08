 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject startScreen;
    public GameObject gameScreen;
    public GameObject deathScreen;
    public GameObject pauseScreen;
    public GameObject finishScreen;
    public string levelName;
    public Text deathCount;
    public Text timeToBeat;
    public Text time;
    public Text previousTime;
    public Text finishTime;
    public int numDeaths = 0;

    public enum MenuStates {start, game, death, pause, finish, menu};
    public MenuStates currentState;

    public decimal startTime;
    public decimal finishedTime;
    public bool firstTime = true;

	// Use this for initialization
	void Start () {
        currentState = MenuStates.start;
        startTime = (decimal)Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        finishedTime = decimal.Round((decimal)Time.time - startTime, 3);
        time.text = "" + finishedTime;

        switch(currentState)
        {
            case (MenuStates.start):
                Time.timeScale = 0.0f;
                gameScreen.SetActive(false);
                deathScreen.SetActive(false);
                pauseScreen.SetActive(false);
                finishScreen.SetActive(false);
                startScreen.SetActive(true);
                if(PlayerPrefs.GetFloat(levelName) != 0.0f)
                {
                    timeToBeat.text = "Time To Beat: " + PlayerPrefs.GetFloat(levelName);
                }
                else
                {
                    timeToBeat.text = "";
                }
                break;

            case (MenuStates.game):
                Time.timeScale = 1.0f;
                gameScreen.SetActive(true);
                deathScreen.SetActive(false);
                pauseScreen.SetActive(false);
                finishScreen.SetActive(false);
                startScreen.SetActive(false);
                deathCount.text = "Deaths this level: " + numDeaths;
                break;

            case (MenuStates.death):
                Time.timeScale = 0.0f;
                gameScreen.SetActive(false);
                deathScreen.SetActive(true);
                pauseScreen.SetActive(false);
                finishScreen.SetActive(false);
                startScreen.SetActive(false);
                break;

            case (MenuStates.pause):
                Time.timeScale = 0.0f;
                gameScreen.SetActive(false);
                deathScreen.SetActive(false);
                pauseScreen.SetActive(true);
                finishScreen.SetActive(false);
                startScreen.SetActive(false);
                break;

            case (MenuStates.finish):
                Time.timeScale = 0.0f;
                gameScreen.SetActive(false);
                deathScreen.SetActive(false);
                pauseScreen.SetActive(false);
                finishScreen.SetActive(true);
                startScreen.SetActive(false);
                onFinishUpdate();
                break;

            case (MenuStates.menu):
                SceneManager.LoadScene("MainMenu");
                break;
        }

        if(Input.GetButtonDown("Cancel"))
        {
            if(currentState == MenuStates.pause)
            {
                currentState = MenuStates.game;
            }

            else
            {
                currentState = MenuStates.pause;
            }
            
        }

        if(currentState != MenuStates.finish)
        {
            if (firstTime == false)
            {
                firstTime = true;
            }
        }

	}

    public void onMenu()
    {
        currentState = MenuStates.menu;
    }

    public void onGame()
    {
        currentState = MenuStates.game;
    }

    public void onDeath()
    {
        currentState = MenuStates.death;
        numDeaths++;
    }

    public void onPause()
    {
        currentState = MenuStates.pause;
    }

    public void onFinish()
    {
        currentState = MenuStates.finish;
    }

    public void onRetry()
    {
        currentState = MenuStates.start;
        startTime = (decimal)Time.time;
    }

    public void onResetVars()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat(levelName, 0.0f);
    }

    public void onFinishUpdate()
    {
        if (PlayerPrefs.GetFloat(levelName) != 0.0f && firstTime == true)
        {
            firstTime = false;
            if (PlayerPrefs.GetFloat(levelName) > (float)finishedTime)
            {
                previousTime.text = "Previous High Score: " + PlayerPrefs.GetFloat(levelName);
                PlayerPrefs.SetFloat(levelName, (float)finishedTime);
                finishTime.text = "New High Score: " + PlayerPrefs.GetFloat(levelName);
                return;
            }

            else
            {
                previousTime.text = "Previous High Score: " + PlayerPrefs.GetFloat(levelName);
                finishTime.text = "Your Time: " + finishedTime;
                return;
            }
        }

        if(PlayerPrefs.GetFloat(levelName) == 0.0f && firstTime == true)
        {
            firstTime = false;
            previousTime.text = "";
            PlayerPrefs.SetFloat(levelName, (float)finishedTime);
            finishTime.text = "New High Score: " + PlayerPrefs.GetFloat(levelName);
            return;
        }
    }
}

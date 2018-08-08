using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public GameObject player;
    public GameObject spawn;
    public UIManager UIManager;

    public bool firstTime = true;

	// Use this for initialization
	void Start () {
        player.transform.position = spawn.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if(player.transform.position.y <= -11 && firstTime == true)
        {
            firstTime = false;
            UIManager.onDeath();
        }

        if (UIManager.currentState == UIManager.MenuStates.game && firstTime == false)
        {
            firstTime = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Spikes")
        {
            UIManager.onDeath();
        }

        if(collision.name == "Goal")
        {
            UIManager.onFinish();
        }
    }

    public void onRespawn()
    {
        player.transform.position = spawn.transform.position;
        UIManager.onGame();
    }

    public void onTryAgain()
    {
        player.transform.position = spawn.transform.position;
        UIManager.onRetry();
    }
}

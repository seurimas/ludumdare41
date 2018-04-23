using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.Find("Panel").Find("MainMenu").GetComponent<Button>().onClick.AddListener(MainMenuClick);
        transform.Find("Panel").Find("Replay").GetComponent<Button>().onClick.AddListener(ReplayClick);
    }

    public void MainMenuClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReplayClick()
    {
        SceneManager.LoadScene("SampleScene");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

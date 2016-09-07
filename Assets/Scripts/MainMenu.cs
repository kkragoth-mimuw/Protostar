using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

    private Android android;

    public GameObject QuitPrompt;
    public GameObject OptionsPrompt;

	// Use this for initialization
	void Start() {
        QuitPrompt.SetActive(false);
        OptionsPrompt.SetActive(false);

        android = GameObject.Find("Android").GetComponent<Android>();
	}
	
	// Update is called once per frame
	void Update() {
	
	}

    public void Exit() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void startDebugLevel() {
        Application.LoadLevel("Debug");
    }

    public void showQuitPrompt() {
        QuitPrompt.SetActive(true);
    }

    public void showOptionsPrompt() {
        OptionsPrompt.SetActive(true);
    }

    public void closeOptionsPrompt() {
        QuitPrompt.SetActive(false);
    }

    public void closeQuitPrompt() {
        QuitPrompt.SetActive(false);
    }
}

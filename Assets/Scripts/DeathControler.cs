using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathControler : MonoBehaviour {

    public Text scoreText;

    // Use this for initialization
    void Start () {
        UpdateFinalScore();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void UpdateFinalScore()
    {
        scoreText.text = "Score: " + GetComponent<Score>().score;
    }
}

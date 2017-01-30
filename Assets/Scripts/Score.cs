using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {

    public Text scoreText;
    public int score = 0;

    // Use this for initialization
    void Start () {
		
	}

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    // Update is called once per frame
    void Update () {
        scoreText.text = "Score: " + score;
    }

    void OnLevelFinishedLoading(Scene s, LoadSceneMode lsm)
    {
        if (s.name == "Death")
        {
            scoreText = GameObject.Find("Text (1)").GetComponent<Text>();
            GetComponent<SpawnEnemy>().enabled = false;
            GetComponent<ScrollingControler>().enabled = false;
        }
    }

    public void UpdateScore(int s)
    {
        score += s;
    }
}

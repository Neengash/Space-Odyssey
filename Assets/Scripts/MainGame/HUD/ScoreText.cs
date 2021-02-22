using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    ScoreManager score;
    Text text;
    
    void Start() {
        score = FindObjectOfType<ScoreManager>();
        text = GetComponent<Text>();
    }

    void Update() {
        text.text = score.getScore(0);
    }
}

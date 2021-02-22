using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreValue : MonoBehaviour
{
    ScoreManager score;
    TextMeshPro text;

    void Start() {
        score = FindObjectOfType<ScoreManager>();
        text = GetComponent<TextMeshPro>();

        text.text = score.getScore(0);
    }
}

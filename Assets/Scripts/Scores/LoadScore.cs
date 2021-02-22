using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadScore : MonoBehaviour
{
    [SerializeField] int position;

    ScoreManager score;
    TextMeshPro text;

    void Start()
    {
        score = FindObjectOfType<ScoreManager>();
        text = GetComponent<TextMeshPro>();
        loadScoreValue();
    }

    private void loadScoreValue() {
        text.text = score.getScore(position);
    }
}

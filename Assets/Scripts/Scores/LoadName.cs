using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadName : MonoBehaviour
{
    [SerializeField] int position;

    ScoreManager score;
    TextMeshPro text;

    void Start()
    {
        score = FindObjectOfType<ScoreManager>();
        text = GetComponent<TextMeshPro>();
        loadNameValue();
    }

    private void loadNameValue() {
        text.text = score.getName(position);
    }
}

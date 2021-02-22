using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackText : MonoBehaviour
{
    PlayerSecondaryAttack attack;
    Text text;

    void Start()
    {
        attack = FindObjectOfType<PlayerSecondaryAttack>();
        text = GetComponent<Text>();
    }

    void Update() {
        text.text = attack.getRemainingAttacks().ToString();
    }
}

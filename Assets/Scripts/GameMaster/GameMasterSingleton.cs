using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterSingleton : MonoBehaviour
{
    static GameMasterSingleton singleton;
    void Awake()
    {
        DontDestroyOnLoad(this);
        if (singleton == null) {
            singleton = this;
        } else {
            if (singleton != this) {
                gameObject.SetActive(false);
                Destroy(this);
            }
        }
    }
}

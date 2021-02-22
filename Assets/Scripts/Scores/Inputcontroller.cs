using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inputcontroller : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip okSound;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if  (Input.GetKeyDown(KeyCode.A)) {
            audioSource.PlayOneShot(okSound);
            StartCoroutine(backToMenu());
        }
    }

    IEnumerator backToMenu() {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(Scenes.MAIN_MENU);
    }
}

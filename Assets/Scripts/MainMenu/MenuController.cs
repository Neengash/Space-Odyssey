using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip errorSound;
    [SerializeField] AudioClip okSound;

    ScoreManager score;

    int currentOption;
    public const int
        NO_OPTION = -1,
        START_OPTION = 0,
        SCORE_OPTION = 1;

    void Start() {
        score = FindObjectOfType<ScoreManager>();
        audioSource = GetComponent<AudioSource>();
        currentOption = START_OPTION;
    }

    public void setOption(int option) {
        currentOption = option;
    }

    public void selectOption() {
        switch (currentOption) {
            case NO_OPTION:
                noOptionSelected();
                break;
            case START_OPTION:
                startOptionSelected();
                break;
            case SCORE_OPTION:
                scoreOptionSelected();
                break;
        }
    }

    private void noOptionSelected() {
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(errorSound);
        }
    }

    private void startOptionSelected() {
        audioSource.PlayOneShot(okSound);
        score.setLevel1();
        StartCoroutine(changeScene(Scenes.MAIN_GAME));
    }

    private void scoreOptionSelected() {
        audioSource.PlayOneShot(okSound);
        StartCoroutine(changeScene(Scenes.SCORES));
    }

    IEnumerator changeScene(int scene)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(scene);

    }
}

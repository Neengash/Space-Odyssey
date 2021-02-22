using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] TextMeshPro letter1, letter2, letter3;
    [SerializeField] TextMeshPro selectedLetter;
    [SerializeField] TextMeshPro newRecordText;
    ScoreManager scoreManager;
    AudioSource audioSource;
    [SerializeField] AudioClip okSound;

    bool isNewRecord;

    int currentLetter;
    int[] letterValues;

    readonly float[] letterXpos = { -3.4f, -0.4f, 2.6f };
    readonly string[] letters = {"-", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
        "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};

    void Start() {
        scoreManager = FindObjectOfType<ScoreManager>();
        audioSource = GetComponent<AudioSource>();
        if (scoreManager.currentIsRecord()) {
            isNewRecord = true;
            currentLetter = 0; // 0, 1, 2
            letterValues = new int[]{ 1, 1, 1}; // AAA
            updateTexts();
        } else {
            isNewRecord = false;
            deactivateNewRecordObjects();
        }
    }

    void Update() {
        if (isNewRecord) {
            checkArrowKeys();
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            if (isNewRecord) { saveScore(); }
            audioSource.PlayOneShot(okSound);
            StartCoroutine(toMainMenu());
        }
    }

    IEnumerator toMainMenu() {
        yield return new WaitForSeconds(1f); 
        SceneManager.LoadScene(Scenes.MAIN_MENU);
    }

    void checkArrowKeys()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            currentLetter++;
            if (currentLetter > 2) { currentLetter = 0;}
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            currentLetter--;
            if (currentLetter < 0) { currentLetter = 2;}
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            letterValues[currentLetter]--;
            if (letterValues[currentLetter] < 0) {
                letterValues[currentLetter] = letters.Length - 1;
            }
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            letterValues[currentLetter]++;
            if (letterValues[currentLetter] > letters.Length -1) {
                letterValues[currentLetter] = 0;
            }
        }
        updateTexts(); 
    }

    void updateTexts()
    {
        letter1.text = letters[letterValues[0]];
        letter2.text = letters[letterValues[1]];
        letter3.text = letters[letterValues[2]];

        selectedLetter.transform.position = new Vector3(
            letterXpos[currentLetter],
            selectedLetter.transform.position.y,
            selectedLetter.transform.position.z);
    }

    void deactivateNewRecordObjects() {
        letter1.gameObject.SetActive(false);
        letter2.gameObject.SetActive(false);
        letter3.gameObject.SetActive(false);
        selectedLetter.gameObject.SetActive(false);
        newRecordText.gameObject.SetActive(false);
    }

    void saveScore() {
        string name = letter1.text + letter2.text + letter3.text;
        scoreManager.saveScore(name);
    }
}

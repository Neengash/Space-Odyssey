using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    InfoText mainText;
    ScoreManager score;
    EnemySpawner enemySpawner;
    RockSpawner rockSpawner;
    PlayerSecondaryAttack specialAttack;

    const string LEVEL_TEXT = "LEVEL ";
    const float START_WAIT = 2f;
    const float END_WAIT = 8f;
    const float DEATH_WAIT = 4f;

    void Start() {
        loadReferences();
        StartCoroutine(firstLevel());
    }

    IEnumerator firstLevel() {
        yield return null;
        loadFirstLevel();
        startLevel();
    }

    void loadReferences() {
        mainText = FindObjectOfType<InfoText>();
        score = FindObjectOfType<ScoreManager>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        rockSpawner = FindObjectOfType<RockSpawner>();
        specialAttack = FindObjectOfType<PlayerSecondaryAttack>();
    }

    void loadFirstLevel() {
        specialAttack.setAttacksAvailable(PlayerSecondaryAttack.STARTING_ATTACKS);
    }

    void startLevel() {
        mainText.showText(LEVEL_TEXT + score.getLevel().ToString());
        StartCoroutine(doStartLevel());
    }

    IEnumerator doStartLevel() {
        yield return new WaitForSeconds(START_WAIT);
        enemySpawner.play(score.getLevel());
        rockSpawner.play();
    }

    public void levelComplete() {
        enemySpawner.stop();
        rockSpawner.stop();
        StartCoroutine(nextLevel());
    }
     
    IEnumerator nextLevel()
    {
        yield return new WaitForSeconds(END_WAIT);
        score.incrementLevel();
        score.scoreLevel();
        specialAttack.incrementAttacksAvailable();
        yield return new WaitForSeconds(START_WAIT);
        startLevel();
    }

    public void gameOver() {
        StartCoroutine(doGameOver());
    }

    IEnumerator doGameOver() {
        yield return new WaitForSeconds(DEATH_WAIT);
        SceneManager.LoadScene(Scenes.GAME_OVER);
    }
}

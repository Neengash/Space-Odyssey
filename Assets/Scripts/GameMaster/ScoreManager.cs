using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int currentLevel;
    int currentScore;

    // Scores are saved under the keys "name"/"score" + "position"
    // ej: name1, score1, name2, score2, name3, score3
    const string NAME_KEY = "name";
    const string SCORE_KEY = "score";

    const string DEFAULT_NAME = "---";
    const int DEFAULT_SCORE = 0;
    const int MAX_SCORE = 99999;

    const int
        ROCK_SCORE = 1,
        ENEMY_SCORE = 10,
        LVL_SCORE = 100;

    void Start() {
        currentLevel = 0;
        currentScore = 0;
    }

    public int getLevel() {
        return currentLevel;
    }

    public void setLevel1() {
        currentLevel = 1;
        currentScore = 0;
    }

    public void incrementLevel() {
        currentLevel++;
    }

    public string getName(int position) {
        return PlayerPrefs.GetString(NAME_KEY + position.ToString(), DEFAULT_NAME);
    }

    public void incrementScore(int value) {
        currentScore += value;
        if (currentScore > MAX_SCORE) {
            currentScore = MAX_SCORE;
        }
    }

    public string getScore(int position) {
        int score = getScoreValue(position);

        string score_string = score.ToString();
        if (score < 10000) { score_string = "0" + score_string; }
        if (score < 1000) { score_string = "0" + score_string; }
        if (score < 100) { score_string = "0" + score_string; }
        if (score < 10) { score_string = "0" + score_string; }

        return score_string;
    }

    public int getScoreValue(int position) {
        return (position == 0)
            ? currentScore
            : PlayerPrefs.GetInt(SCORE_KEY + position.ToString(), DEFAULT_SCORE);
    }

    public bool currentIsRecord() {
        return currentScore > getScoreValue(3);
    }

    public void saveScore(string name) {
        int score = currentScore; 
        int auxScore;
        string auxName;

        int idx = 1;

        while (idx <= 3 && score != DEFAULT_SCORE) {
            if (currentScore > getScoreValue(idx)) {
                auxName = getName(idx);
                auxScore = getScoreValue(idx);
                PlayerPrefs.SetString(NAME_KEY + idx, name);
                PlayerPrefs.SetInt(SCORE_KEY + idx, score);
                name = auxName;
                score = auxScore;
            }
            idx++;
        }
    }

    public void scoreRock() {
        currentScore += ROCK_SCORE;
    }

    public void scoreEnemy() {
        currentScore += ENEMY_SCORE;
    }

    public void scoreLevel() {
        currentScore += LVL_SCORE;
    }
}

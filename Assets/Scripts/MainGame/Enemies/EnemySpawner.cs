using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameManager lvlManager;
    ObjectPool enemyPool;
    bool ready;
    int waves;

    const float WAIT_BETWEEN_DEPLOYS = 3f;
    const float WAIT_INSIDE_DEPLOYS_MID = 1f;
    const float WAIT_INSIDE_DEPLOYS_LOW = 0.5f;

    const int BASE_WAVES = 5;

    const float
        MAX_Y = 4,
        MIN_Y = -MAX_Y;

    const float
        DEPLOYS_LINE = 3,
        DEPLOYS_SQUARE  = 2,
        DEPLOYS_DIAGONAL = 3;

    const float
        SQUARE_MARGINS = 1f,
        DIAGONAL_MARIGNS = 1f;

    enum deployments {
        DEPLOY_LINE,
        DEPLOY_SQUARE,
        DEPLOY_DIAGONAL_UP,
        DEPLOY_DIAGONAL_DOWN
    }

    void Start() {
        lvlManager = FindObjectOfType<GameManager>();
        enemyPool = GetComponent<ObjectPool>();
        ready = true;
    }

    void Update() {
        if (waves > 0 && ready) {
            StartCoroutine(spawnEnemies());
            waves--;
            ready = false;

            if (waves == 0) {
                lvlManager.levelComplete();
            }
        }
    }

    public void play(int level) {
        waves = BASE_WAVES + (int)((level)/2);
    }

    public void stop() {
        waves = 0;
    }

    IEnumerator spawnEnemies() {
        yield return new WaitForSeconds(WAIT_BETWEEN_DEPLOYS);

        int deploymentsSize = System.Enum.GetValues(typeof(deployments)).Length;
        int deployType = Random.Range(0, deploymentsSize);

        switch (deployType) {
            case (int)deployments.DEPLOY_LINE:
                yield return StartCoroutine(deployLine());
                break;
            case (int)deployments.DEPLOY_SQUARE:
                yield return StartCoroutine(deploySquare());
                break;
            case (int)deployments.DEPLOY_DIAGONAL_UP:
                yield return StartCoroutine(deployDiagonalUp());
                break;
            case (int)deployments.DEPLOY_DIAGONAL_DOWN:
                yield return StartCoroutine(deployDiagonalDown());
                break;
        }
        ready = true;
    }

    IEnumerator deployLine() {
        float posY = Random.Range(MIN_Y, MAX_Y);
        for (int i = 0; i < DEPLOYS_LINE; i++) {
            spawnEnemy(posY);
            yield return new WaitForSeconds(WAIT_INSIDE_DEPLOYS_MID);
        }
    }

    IEnumerator deploySquare() {
        float posY = Random.Range(MIN_Y + SQUARE_MARGINS, MAX_Y - SQUARE_MARGINS);
        for (int i = 0; i < DEPLOYS_SQUARE; i++) {
            spawnEnemy(posY - SQUARE_MARGINS);
            spawnEnemy(posY + SQUARE_MARGINS);
            yield return new WaitForSeconds(WAIT_INSIDE_DEPLOYS_MID);
        }
    }

    IEnumerator deployDiagonalUp() {
        float posY = Random.Range(MIN_Y + SQUARE_MARGINS, MAX_Y - SQUARE_MARGINS);
        float margin = - DIAGONAL_MARIGNS;
        for (int i = 0; i < DEPLOYS_DIAGONAL; i++) {
            spawnEnemy(posY + margin);
            margin += DIAGONAL_MARIGNS;
            yield return new WaitForSeconds(WAIT_INSIDE_DEPLOYS_LOW);
        }
    }

    IEnumerator deployDiagonalDown() {
        float posY = Random.Range(MIN_Y + SQUARE_MARGINS, MAX_Y - SQUARE_MARGINS);
        float margin = + DIAGONAL_MARIGNS;
        for (int i = 0; i < DEPLOYS_DIAGONAL; i++) {
            spawnEnemy(posY + margin);
            margin -= DIAGONAL_MARIGNS;
            yield return new WaitForSeconds(WAIT_INSIDE_DEPLOYS_LOW);
        }
    }

    void spawnEnemy(float posY) {
        SmallEnemy enemy = (SmallEnemy)enemyPool.getNext();
        enemy.gameObject.SetActive(true);
        enemy.getReady();
        enemy.transform.position = new Vector3(
            transform.position.x, posY, transform.position.z);
    }
}

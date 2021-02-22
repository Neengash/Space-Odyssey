using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    ObjectPool rockPool;
    bool playing;
    float timer;

    const float STARTING_TIMER = 4;
    const float TIMER_BASE = 2;
    const float TIMER_MARGIN = 1;
    static readonly float[] ROTATIONS = { RockLogic.SLOW_ROTATION, RockLogic.MID_ROTATION, RockLogic.FAST_ROTATION };

    const float BASE_POS_Y = 4.5f;
    const float BASE_SPEED_Y = 0.5f;
    const float MIN_SPEED_X = -3;
    const float MAX_SPEED_X = -8;

    void Start() {
        rockPool = GetComponent<ObjectPool>();
        timer = calculateTimerTime(STARTING_TIMER, TIMER_MARGIN);
        playing = false;
    }

    void Update() {
        if (playing) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                generateRock();
                timer = calculateTimerTime(TIMER_BASE, TIMER_MARGIN);
            }
        }
    }

    public void play() {
        playing = true;
    }

    public void stop() {
        playing = false;
    }

    float calculateTimerTime(float baseTime, float margin) {
        return Random.Range(baseTime - margin, baseTime + margin);
    }

    void generateRock() {
        RockLogic rock = (RockLogic)rockPool.getNext();
        rock.gameObject.SetActive(true);
        rock.getReady();

        float posY = Random.Range(- BASE_POS_Y, BASE_POS_Y);

        rock.transform.position = new Vector3(
            transform.position.x, posY, transform.position.z);

        float speedX = generateXSpeed();
        float speedY = generateYSpeed();
        float rotation = generateRotation();

        rock.setMovemnt(speedX, speedY, rotation);
    }

    float generateXSpeed() {
        // max and min are switched because they are negative values
        return Random.Range(MAX_SPEED_X, MIN_SPEED_X);
    }

    float generateYSpeed() {
        return Random.Range(- BASE_SPEED_Y, BASE_SPEED_Y);
    }

    float generateRotation() {
        int rotationSpeed = Random.Range(0, ROTATIONS.Length-1);
        int sign = (Random.Range(0, 1) == 0) ? 1 : -1;
        return sign * ROTATIONS[rotationSpeed];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    PlayerManager playerManager;
    Rigidbody2D rigidBody2D;

    float speed;
    const float UP_SPEED = 2.5f;
    const float DOWN_SPEED = - UP_SPEED;
    const float STOP = 0f;

    const float MOVEMENT_LIMITS = 4.2f;

    const float ROTATION_SPEED = 0.5f;
    const float MAX_ROTATION = 0.3f;
    const float MIN_ROTATION = -0.3f;
    const float NEUTRAL_ROTATIOn = 0f;

    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        speed = STOP;
    }

    void Update()
    {
        if (!playerManager.isDead()) {
            checkInputs();
            checkMovementLimits();
            updateRotation();
        }
    }

    void FixedUpdate()
    {
        if (!playerManager.isDead()) {
            updatePosition();
        }
    }

    void checkInputs()
    {
        if (Input.GetKey(KeyCode.DownArrow)) {
            speed = DOWN_SPEED;
        } else if (Input.GetKeyUp(KeyCode.DownArrow)) {
            speed = STOP;
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            speed = UP_SPEED;
        } else if (Input.GetKeyUp(KeyCode.UpArrow)) {
            speed = STOP;
        }
    }

    void checkMovementLimits() {
        if (
            (transform.position.y >= MOVEMENT_LIMITS && speed == UP_SPEED) ||
            (transform.position.y <= - MOVEMENT_LIMITS && speed == DOWN_SPEED)
        ) {
            speed = STOP;
        }
    }

    void updateRotation()
    {
        float rotationMod = getRotationModifier();
        transform.Rotate(new Vector3(rotationMod, 0, 0));

        rotationStabilizer();
    }

    float getRotationModifier()
    {
        if (
            (speed == UP_SPEED && transform.rotation.x < MAX_ROTATION) ||
            (speed == STOP && transform.rotation.x < NEUTRAL_ROTATIOn)
        ) {
            return ROTATION_SPEED;
        }

        if (
            (speed == DOWN_SPEED && transform.rotation.x > MIN_ROTATION) ||
            (speed == STOP && transform.rotation.x > NEUTRAL_ROTATIOn)
        ) {
            return -ROTATION_SPEED;
        }

        return 0;
    }

    void rotationStabilizer()
    {
        if (
            (speed == STOP) &&
            (transform.rotation.eulerAngles.x < 1 || transform.rotation.eulerAngles.x > 359)
        ) {
            transform.rotation = Quaternion.identity;
        }
    }

    void updatePosition()
    {
        rigidBody2D.velocity = new Vector2(0, speed);
    }
}

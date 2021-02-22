using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [SerializeField]
    ParallaxController layer1, layer2, layer3, layer4;

    float speed;
    const float BASE_SPEED = 0.05f;
    const float STOP = 0f;

    void Start() {
        if (speed == STOP) {
            speed = BASE_SPEED;
        }

        updateLayerSpeeds();
    }

    void updateLayerSpeeds() {
        layer4.speed = speed;
        layer3.speed = speed * 6 / 8;
        layer2.speed = speed * 3 / 8;
        layer1.speed = speed * 1 / 8;
        
    }
    public void setSpeed(float speed) {
        this.speed = speed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    MenuController menu;

    float x, y, z;
    const float START_POSITION = -0.19f;
    const float SCORE_POSITION = -1.5f;

    float speed;
    const float TOP = 1.5f;
    const float BOT = - TOP;
    const float STOP = 0f;

    void Start()
    {
        menu = FindObjectOfType<MenuController>();

        speed = STOP;
        x = transform.position.x;
        y = START_POSITION;
        z = transform.position.z;

        transform.position = new Vector3(x, y, z);
    }

    private void Update()
    {
       y += speed * Time.deltaTime;

        if (y >= START_POSITION) {
            y = START_POSITION;
            speed = STOP;
            menu.setOption(MenuController.START_OPTION);
        } else if(y <= SCORE_POSITION) {
            y = SCORE_POSITION;
            speed = STOP;
            menu.setOption(MenuController.SCORE_OPTION);
        }

        transform.position = new Vector3(x, y, z);
    }

    public void goTOP() {
        if (y < START_POSITION) {
            speed = TOP;
        }
    }

    public void goBOT() {
        if (y > SCORE_POSITION) {
            speed = BOT;
        }
    }

}

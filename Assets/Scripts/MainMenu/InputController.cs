using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    ShipMovement ship;
    MenuController menu;

    void Start()
    {
        ship = FindObjectOfType<ShipMovement>();
        menu = FindObjectOfType<MenuController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ship.goBOT();
            menu.setOption(MenuController.NO_OPTION);
        } 
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ship.goTOP();
            menu.setOption(MenuController.NO_OPTION);
        } 
        else if (Input.GetKeyDown(KeyCode.A)) 
        {
            menu.selectOption();
        }
    }
}

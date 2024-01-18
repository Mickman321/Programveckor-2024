using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPassword : MonoBehaviour
{
    public bool door;
    public InputField input;
    public GameObject Button0;
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    public GameObject Button4;
    public GameObject Button5;
    public GameObject Button6;
    public GameObject Button7;
    public GameObject Button8;
    public GameObject Button9;
    public GameObject clearButton;
    public GameObject enterButton;

    public void b1()
    {
        input.text = input.text + "1";
    }
    public void b2()
    {
        input.text = input.text + "2";
    }
    public void b3()
    {
        input.text = input.text + "3";
    }
    public void b4()
    {
        input.text = input.text + "4";
    }
    public void b5()
    {
        input.text = input.text + "5";
    }
    public void b6()
    {
        input.text = input.text + "6";
    }
    public void b7()
    {
        input.text = input.text + "7";
    }
    public void b8()
    {
        input.text = input.text + "8";
    }
    public void b9()
    {
        input.text = input.text + "9";
    }
    public void b0()
    {
        input.text = input.text + "0";
    }
    public void clearEvent()
    {
        input.text = null;
    }
    public void enterEvent()
    {
        if (input.text == "")
        {
            door = true;
            Debug.Log("Success");

        }
        else
        {
            door = false;
            Debug.Log("Failed");
        }
    }
}

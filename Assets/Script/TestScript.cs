using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestScript : MonoBehaviour
{
    public bool toggledText = false;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Toggled";
    }

    // Update is called once per frame
    void Update()
    {
        text.gameObject.SetActive(toggledText);
        //FUNCTIONING, BUT THE VR RAY AIN'T INTERACTING WITH WORLD SPACE UI.
    }

    public void ToggleText()
    {
        if (toggledText)
        {
            toggledText = false;
        }
        else
        {
            toggledText = true;
        }
    }
}

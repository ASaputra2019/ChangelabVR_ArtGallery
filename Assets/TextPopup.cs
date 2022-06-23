using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextPopup : MonoBehaviour
{
    public bool isInTrigger = false;
    public bool isLookingAtObject = false;
    public GameObject textPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInTrigger)
        {
            if (isLookingAtObject)
            {
                textPanel.SetActive(true);
            }
        }
        else
        {
            textPanel.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        print("collision happened");
        if(other.gameObject.tag == "InteractableTrigger")
        {
            isInTrigger = true;
            print("entered");
        }
        else
        {
            isInTrigger = false;
        }
    }
    
}

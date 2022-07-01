using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextPopup : MonoBehaviour
{
    public bool isInTrigger = false;
    public bool isLookingAtObject = false;
    public GameObject importantThingo;
    public Material highlightMaterial;
    public Material originalMaterial;
    public GameObject textPanel;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        originalMaterial = importantThingo.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInTrigger && isLookingAtObject)
        {
            textPanel.SetActive(true);
            text.text = "This Cube is an Ancient Handball before they invented rounded corners.";
            text.fontSize = 150;
            importantThingo.GetComponent<MeshRenderer>().material = highlightMaterial;
        }
        else
        {
            textPanel.SetActive(false);
            importantThingo.GetComponent<MeshRenderer>().material = originalMaterial;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //DISABLE OTHER COLLIDERS ON THE TELEPORT PADS, MESSING WITH THE TRIGGER ZONE
        print("collision happened");
        if (other.gameObject.tag == "InteractableTrigger")
        {
            isInTrigger = true;
            print("entered");
        }
        else
        {
            isInTrigger = false;
        }
    }

    public void ObjectLookedAt()
    {

        isLookingAtObject = true;
    }
    public void ObjectNotLookedAt()
    {

        isLookingAtObject = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextPopup : MonoBehaviour
{
    public List<bool> isLookingAtObjectList;
    public List<bool> isInTriggerList;
    public GameObject[] importantObjectsArray;
    public Material highlightMaterial;
    public List<Material> originalMaterialList;
    public GameObject[] textPanelArray;
    public Text[] ObjDescriptionTextArray;

    void Start()
    {
        
        for (int i = 0; i < importantObjectsArray.Length; i++)
        {
            print("poop");
            isLookingAtObjectList.Add(false);
            isInTriggerList.Add(false);
            originalMaterialList.Add(importantObjectsArray[i].GetComponent<MeshRenderer>().material);
            //originalMaterialsArray[i] = importantThingos[i].GetComponent<MeshRenderer>().material;
        }
    }

    void Update()
    {
        if (isInTriggerList[0] && isLookingAtObjectList[0])
        {
            //textPanel.SetActive(true);
            //importantObjectsArray[0].GetComponent<MeshRenderer>().material = highlightMaterial;
            //ObjDescriptionText.text = "This Cube is an Ancient Handball before they invented rounded corners.";
            //ObjDescriptionText.fontSize = 150;
            HighlightWithText(0, "This Cube is an Ancient Handball before they invented rounded corners.", 150);
            print("buts");
        }
        else if (isInTriggerList[1] && isLookingAtObjectList[1])
        {
            HighlightWithText(1, "This Cube is just a fancy rock that someone made.", 150);
            print("wow");
        }

        else
        {
            for (int i = 0; i < importantObjectsArray.Length; i++)
            {
                print("back to normal");
                textPanelArray[i].SetActive(false);
                importantObjectsArray[i].GetComponent<MeshRenderer>().material = originalMaterialList[i];
            }
        }
    }

    //This allows for things to be cleaning swapped in Update instead of having to change a lot of stuff manaually through multiple lines of code.
    private void HighlightWithText(int ObjNum, string ObjText, int TextSize)
    {
        textPanelArray[ObjNum].SetActive(true);
        importantObjectsArray[ObjNum].GetComponent<MeshRenderer>().material = highlightMaterial;
        ObjDescriptionTextArray[ObjNum].text = ObjText;
        ObjDescriptionTextArray[ObjNum].fontSize = TextSize;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Remember to set the TriggerZones' tags.
        if (other.gameObject.tag == "TriggerForObj0")
        {
            isInTriggerList[0] = true;
            print("entered");
        }
        else if (other.gameObject.tag == "TriggerForObj1")
        {
            isInTriggerList[1] = true;
            print("entered");
        }
        //if (other.gameObject.tag == "InteractableTrigger")
        //{
        //    isInTrigger = true;
        //    print("entered");
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < isInTriggerList.Count; i++)
        {
            isInTriggerList[i] = false;
            //isInTrigger = false;
        print("exit");
        }

    }

    // Make sure this is applied to the object's "Hover Exited" part of the XR interable script in the Unity Inspector for any new objects. Should wipe all "LookedAtObj"s that are true.
    // Don't forget that this List's length (or count) is based on the GameObject array
    public void ObjectNotLookedAt()
    {
        for (int i = 0; i < isLookingAtObjectList.Count; i++)
        {
            isLookingAtObjectList[i] = false;
        }

    }

    //To add more objects to look at, duplicate a method below and add a number in sequence.
    //Make sure to add the method in the Unity Inspector, under the object's "Hover Entered" section in its XR interactable script.
    public void LookedAtObj0()
    {

        isLookingAtObjectList[0] = true;
    }
    public void LookedAtObj1()
    {

        isLookingAtObjectList[1] = true;
    }
}
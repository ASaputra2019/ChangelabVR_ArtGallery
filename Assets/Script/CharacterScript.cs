using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour
{
    public Button button1;
    public GameObject PlayerCharacter;

    public float speed = 1.0f;
    public float DistanceThreshold = 0.3f;
    public Animator anim;
    public AudioClip[] artTracks;
    public AudioSource audioSource;
    public Button[] buttons;

    int currentButton;

    bool isWalking = false;
    bool isTalking = false;
    Vector3 targetPosition;





    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        foreach (Button button in buttons)
        {
            var number = button.name.Split('_')[1];
            int index = int.Parse(number);
            Debug.Log("Button Name: " + index);

            button.onClick.AddListener(() => ButtonPressed(button.name));            
        }
    }

    void FixedUpdate()
    {
        if(isWalking)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.targetPosition, speed * Time.deltaTime);

            var dist = Vector3.Distance(this.transform.position, targetPosition);

            if(dist < this.DistanceThreshold)
            {
                isWalking = false;
                this.anim.SetBool("IsWalking", false);


                Debug.Log("Current Button " + (currentButton));
                Debug.Log(artTracks[currentButton]);

                audioSource.clip = artTracks[currentButton-1];
                audioSource.volume = 1.0f;
                audioSource.Play();
                this.anim.SetBool("IsTalking", true);
                
            }
        }
        if(isTalking)
        {
            if(!audioSource.isPlaying)
            {
                this.anim.SetBool("IsTalking", false);
                isTalking = false;
            }

        }
    }

    void ButtonPressed(string buttonName)
    {
        var number = buttonName.Split('_')[1];
        int buttonNumber = int.Parse(number);

        Debug.Log("This comes here ");

        Debug.Log(buttonNumber);
        
        this.currentButton = buttonNumber;
        
        this.targetPosition = PlayerCharacter.transform.position;
        this.targetPosition.y = this.transform.position.y;

        this.transform.LookAt(targetPosition);
        
        this.anim.SetBool("IsWalking", true);
        isWalking = true;

    }


    void TaskOnClick()
    {
        Debug.Log("This comes here ");
        this.targetPosition = PlayerCharacter.transform.position;
        this.targetPosition.y = this.transform.position.y;
        
        this.anim.SetBool("IsWalking", true);
        isWalking = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

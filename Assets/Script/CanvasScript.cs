using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{

    public GameObject buttonObject;
    public Animator animator;
    public AudioSource audioSource;

    public GameObject Character;

    public void buttonPress()
    {
        print("Comes here");
        animator.SetBool("IsTalking", true);

        audioSource.Play(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = Character.GetComponent<Animator>();
        audioSource = Character.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            animator.SetBool("IsTalking", false);
        }
    }
}

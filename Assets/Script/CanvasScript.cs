using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CanvasScript : MonoBehaviour
{

    public GameObject buttonObject;
    public Animator animator;
    public AudioSource audioSource;

    public GameObject player;
    public NavMeshAgent agent;

    public GameObject Character;

    public void buttonPress()
    {
        //print("Comes here");
        //animator.SetBool("IsTalking", true);

        //audioSource.Play(0);

        //ReachPlayer();

    }

    void ReachPlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = Character.GetComponent<Animator>();
        audioSource = Character.GetComponent<AudioSource>();

        agent = Character.GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            animator.SetBool("IsTalking", false);
        }

        //print(player.transform.position);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    GameObject cube;
    public Transform center;
    public Vector3 axis = Vector3.up;
    public Vector3 desiredPosition;
    public float radius = 5.0f;
    public float radiusSpeed = 0.5f;
    public float rotationSpeed = 80.0f;
    [SerializeField] private GameObject[] nests;

    private int currentTick = 0;
    private int maxTick = 240;
    private int currentNestIndex = 0;

    //int idleAnimationHash;
    //int singAnimationHash;
    //int ruffleAnimationHash;
    //int preenAnimationHash;
    //int peckAnimationHash;
    //int hopForwardAnimationHash;
    //int hopBackwardAnimationHash;
    //int hopLeftAnimationHash;
    //int hopRightAnimationHash;
    //int worriedAnimationHash;
    //int landingAnimationHash;
    //int flyAnimationHash;
    //int hopIntHash;
    //int flyingBoolHash;
    ////int perchedBoolHash;
    //int peckBoolHash;
    //int ruffleBoolHash;
    //int preenBoolHash;
    ////int worriedBoolHash;
    //int landingBoolHash;
    //int singTriggerHash;
    //int flyingDirectionHash;
    //int dieTriggerHash;
    //
    //Animator anim;

    void Start()
    {
        //cube = GameObject.FindWithTag("orbit");
        //center = cube.transform;
        //transform.position = (transform.position - center.position).normalized * radius + center.position;
        //radius = 2.0f;
        //
        //idleAnimationHash = Animator.StringToHash("Base Layer.Idle");
        //flyAnimationHash = Animator.StringToHash("Base Layer.fly");
        //hopIntHash = Animator.StringToHash("hop");
        //flyingBoolHash = Animator.StringToHash("flying");
        //peckBoolHash = Animator.StringToHash("peck");
        //ruffleBoolHash = Animator.StringToHash("ruffle");
        //preenBoolHash = Animator.StringToHash("preen");
        //landingBoolHash = Animator.StringToHash("landing");
        //singTriggerHash = Animator.StringToHash("sing");
        //flyingDirectionHash = Animator.StringToHash("flyingDirectionX");
        //dieTriggerHash = Animator.StringToHash("die");
        //anim = GetComponent<Animator>();
        //anim.SetFloat("IdleAgitated", 0.5f);
        //
        //anim.SetBool( flyingBoolHash, true );
        ////anim.SetInteger(hopIntHash, -2);
    }

    void Update()
    {
        //transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);
        //desiredPosition = (transform.position - center.position).normalized * radius + center.position;
        //transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);

        if( ( ++currentTick % maxTick )  == 0 )
        {
            currentTick = 0;
            currentNestIndex = (currentNestIndex + 1) % nests.Length;
        }

        GameObject nestDestination = nests[currentNestIndex];
        Transform nestTransform = nestDestination.GetComponent<Transform>();
        if( nestTransform )
        {
            transform.position = Vector3.MoveTowards(transform.position, nestTransform.position, 0.1f);
            transform.LookAt(nestTransform);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        
        if(collision.gameObject != null && collision.gameObject.tag == "ball")
        {
            
        }
    }
}

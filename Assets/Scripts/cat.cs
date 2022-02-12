using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cat : MonoBehaviour
{

    [Header("Death")]
    [SerializeField] public bool isDed;
    private bool once = true;

    [Header("Movement")]
    [SerializeField] Transform[] positions;
    private enum catTypeNum {red,yellow,blue};
    [SerializeField] private catTypeNum catType;
    [SerializeField] private float speed;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float waitChance;
    [SerializeField] private float waitTime;
    [SerializeField] private float eatTime;
    int posIndex;
    Transform nextPos;
    bool isFinished = false;
    private float defaultWaitTime;
    private float defaultEatTime;
    public bool isRed = false;
    public bool isYellow = false;
    public bool isBlue = false;
    public bool isEating = false;
    private float random;

    [Header("Offset")]
    [SerializeField] private bool flip;
    [SerializeField] private float xOff;
    [SerializeField] private float yOff;
    private AudioManager audioManager;
    
    //animation
    private Animator animator;
    private static readonly int ANIM_faceHere = Animator.StringToHash("faceHere");
    private static readonly int ANIM_isRed = Animator.StringToHash("isRed");
    private static readonly int ANIM_isYellow = Animator.StringToHash("isYellow");
    private static readonly int ANIM_isBlue = Animator.StringToHash("isBlue");
    private static readonly int ANIM_isIdle = Animator.StringToHash("isIdle");
    private static readonly int ANIM_isDed = Animator.StringToHash("isDed");

    [Header("Warna Kucing")] public string catColor;

    private void Awake()
    {
        nextPos = positions[0];
        defaultWaitTime = waitTime;
        defaultEatTime = eatTime;
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();

        //select cat type
        switch (catType)
        {
            case catTypeNum.red:
                isRed = true;
                animator.SetBool(ANIM_isRed, isRed);
                break;
            case catTypeNum.yellow:
                isYellow = true;
                animator.SetBool(ANIM_isYellow, isYellow);
                break;
            case catTypeNum.blue:
                isBlue = true;
                animator.SetBool(ANIM_isBlue, isBlue);
                break;
        }
    }

    private void Update()
    {
        catMove();
        catDeath();
    }

    private void catMove()
    {
        if (isFinished) return;

        if (transform.position == nextPos.position)
        {
            posIndex++;
            if (posIndex == positions.Length)
            {
                isFinished = true;
                transform.position = new Vector3(nextPos.position.x+xOff, nextPos.position.y+yOff, nextPos.position.z);
                animator.SetBool(ANIM_isIdle, true);
                if (flip)
                {
                    transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                else
                {
                    transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                return;
            }
            nextPos = positions[posIndex];
            if (isBlue)
            {
                random = Random.value;
                waitTime = defaultWaitTime;
            }
            catMoveAnimation();
        }
        else
        {
            if (isEating)
            {
                catEat();
            }
            else
            {
                //behaviour is selected from dropdown list in inspector
                if (isRed) redBehaviour();
                if (isYellow) yellowBehaviour();
                if (isBlue) blueBehaviour();
                
            }    
        }
    }

    private void catDeath()
    {
        if (isDed && once)
        {
            once = false;
            Debug.Log("Mati COK");
            catFallAnimation();
            Invoke("changeFallScene",2);

            // UnityEngine.SceneManagement.SceneManager.LoadScene(
            //     UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            // // animasi mati sini
        }
    }

    private void changeFallScene()
    {
        SceneIndexManager.Instance.catColor = catColor;
        SceneManager.LoadScene(6);
    }

    private void redBehaviour()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPos.position, speed * Time.deltaTime);
    }
    private void yellowBehaviour()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPos.position, speedMultiplier * speed * Time.deltaTime);
    }

    private bool hasPlayed = false;
    private void blueBehaviour()
    {
        if (random < waitChance && waitTime > 0f)
        {
            //wait
            waitTime -= Time.deltaTime;
            animator.SetBool(ANIM_isIdle, true);
            if (!hasPlayed)
            {
                audioManager.Play("purr");
                hasPlayed = true;
            }
        }
        else
        {

            transform.position = Vector3.MoveTowards(transform.position, nextPos.position, speed * Time.deltaTime);
            animator.SetBool(ANIM_isIdle, false);
            hasPlayed = false;
        }
    }
    private void catEat()
    {
        if (eatTime > 0f)
        {
            //stop to eat
            eatTime -= Time.deltaTime;
            animator.SetBool(ANIM_isIdle, true);
        }
        else
        {
            isEating = false;
            eatTime = defaultEatTime;
            animator.SetBool(ANIM_isIdle, false);
        }
    }

    //animation
    private void catMoveAnimation()
    {
        //if nextpos position y is higher than current, use walkthere animation, otherwise if lower than current use walkhere
        if (nextPos.position.y > transform.position.y)
        {
            animator.SetBool(ANIM_faceHere, false);
        }
        else if (nextPos.position.y < transform.position.y)
        {
            animator.SetBool(ANIM_faceHere, true);
        }
        //with assumption cat scale is positive
        if (nextPos.position.x > transform.position.x)
        {
            if (animator.GetBool(ANIM_faceHere))
            {
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            else
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
        }
        else if(nextPos.position.x <= transform.position.x)
        {
            if(animator.GetBool(ANIM_faceHere))
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            else
            {
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
        }
    }

    private void catFallAnimation()
    {
        animator.SetBool(ANIM_isDed,true);
    }
}

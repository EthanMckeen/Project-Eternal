using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TotalBossAi : MonoBehaviour
{
//note use of c is for current




//For animation changes
    public Animator anime;
//FOR WALKING
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public Transform BossGFX;
    Path path;
    int cPoint = 0;
    bool reachedEnd = false;
//BOSSHEALTH
    int maxHealth = 300;
    int cBossHealth;

//STATES
    //Stage 
    bool isStage1 = true;
    bool isStage2 = false;
    bool isStage3 = false;
    //Intros
    bool isIntro1 = false;
    bool isIntro2 = false;
    bool isIntro3 = false;
    //Actions
    bool isWalking = true;
    bool isAttacking = false;



    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        cBossHealth = maxHealth;

        InvokeRepeating("UpdatePath", 0f, .5f);

    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            cPoint = 0;
        }
    }

    // Update is called once per frame
    async void Update()
    {
        if (isStage1) // STAGE 1
        {
            //check health
            if (isWalking)
                WalkingState();
            else if (isAttacking && reachedEnd)
                AttackingState();
        }
        else if (isStage2)  //STAGE 2
        {
            //stage2
        }
        else if (isStage3) //STAGE 3
        {
            //stage3
        }

    }

    



    //STATES BELOW

    void WalkingState()
    {

        if (path == null)
            return;

        if (cPoint >= path.vectorPath.Count)
        {
            //CHANGE STATES WE FOUND TARGET
            reachedEnd = true;
            isAttacking = true;
            isWalking = false;
            return;
        }
        else
        {
            reachedEnd = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[cPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[cPoint]);

        if (distance < nextWaypointDistance)
        {
            cPoint++;
        }

        //Flipping the GFX Left and right
        if (rb.velocity.x >= 0.01f && force.x > 0f)
        {
            BossGFX.localScale = new Vector3(-1.1f, 0.8f, 1f);
        }
        else if (rb.velocity.x <= -0.01f && force.x < 0f)
        {
            BossGFX.localScale = new Vector3(1.1f, 0.8f, 1f);
        }

    }

    void AttackingState()
    {
        anime.SetTrigger("Attack");
        anime.SetTrigger("AttackEnd");
        isAttacking = false;
        StartCoroutine(Waitfor(1.3f));
    }


   private IEnumerator Waitfor(float delay)
    {
        yield return new WaitForSeconds(delay);
        isWalking = true;
    }








    
}

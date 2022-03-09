using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossAI : MonoBehaviour
{
    public Animator anime;
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public Transform BossGFX;

    

    Path path;
    int cPoint = 0;
    bool reachedEnd = false;

    

    Seeker seeker;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();


        InvokeRepeating("UpdatePath", 0f, .5f);
        //seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    //hey the players location moved change goal
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
    void Update()
    {
            //if not in walk state stop

            if (path == null)
                return;

            if (cPoint >= path.vectorPath.Count)
            {
                reachedEnd = true;
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






}

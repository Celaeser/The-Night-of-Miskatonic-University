using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Transform player;
    private Vector2 targetPosition;
    private float view = 3;
    private float MoveSpeed = 1f;
    private Animator animator;
    private int damageCount = 5;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.identity;
        player = GameObject.FindGameObjectWithTag("Player").transform;
               
        targetPosition = player.position;
        Vector2 offset = player.position - transform.position;
        //if(offset.magnitude<1.1f)
        //{
        //    //Attack
        //    //transform.rotation = Quaternion.identity;
        //    //animator.SetTrigger("enemyAttack");
        //}
        //else 
        if (offset.magnitude < view )
        {
            //Catch
            //transform.rotation = Quaternion.identity;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy1Idle"))
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * MoveSpeed);
            }
            
        }
        else
        {
            //Patrol
            
        }
    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy1Idle") && view >= 0)
        {
            //碰到player
            Debug.Log("Hit the Player");
            animator.SetTrigger("enemyAttack");
            collision.collider.SendMessage("TakeDamage", damageCount);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy1Idle") && view >= 0)
        {
            //保持碰到player
            Debug.Log("Stay the Player");
            animator.SetTrigger("enemyAttack");
            collision.collider.SendMessage("TakeDamage",damageCount);
        }
    }

    public void setView(float newView)
    {
        view = newView;
    }
}

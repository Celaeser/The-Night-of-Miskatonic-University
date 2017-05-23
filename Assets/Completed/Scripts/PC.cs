using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC : MonoBehaviour {

    private float MoveSpeed = 1.5f;
    private Animator animator;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        walk();
	}


    private void walk()
    {
        transform.rotation = Quaternion.identity;

        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetTrigger("TurnUp");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("TurnDown");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("TurnLeft");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetTrigger("TurnRight");
        }

        if (Input.GetKey(KeyCode.I) && animator.GetCurrentAnimatorStateInfo(0).IsName("PCUP"))
        {              
            transform.Translate(Vector3.up * Time.deltaTime * MoveSpeed);
        }
        if (Input.GetKey(KeyCode.K) && animator.GetCurrentAnimatorStateInfo(0).IsName("PCDOWN"))
        {             
            transform.Translate(Vector3.down * Time.deltaTime * MoveSpeed);
        }
        if (Input.GetKey(KeyCode.J) && animator.GetCurrentAnimatorStateInfo(0).IsName("PCLEFT"))
        {              
            transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed);
        }
        if (Input.GetKey(KeyCode.L) && animator.GetCurrentAnimatorStateInfo(0).IsName("PCRIGHT"))
        {
            transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed);
        }
        


    }
}

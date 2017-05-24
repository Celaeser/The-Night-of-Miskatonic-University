using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    //public float speed = 3f;
    private float MoveSpeed = 1.5f;
    private Rigidbody2D rigidbody;
    public int smoothing = 1;
    private Animator animator;
    private int hp = 20;
    private int san = 5;
    private int sodaHp = 1;
    private int foodHp = 1;
    private int sodaSan = 1;
    
    //private Vector2 targetPosition = new Vector2(1, 1);

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        walk();        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //碰撞到墙
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("Hit the wall");
            if (Input.GetKeyDown(KeyCode.J) && animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle"))
            {
                animator.SetTrigger("playerChop");
                collision.collider.SendMessage("TakeDamage");
            }
        }
        


    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            //保持碰撞墙
            Debug.Log("Stay the wall");
            if (Input.GetKeyDown(KeyCode.J) && animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle"))
            {
                animator.SetTrigger("playerChop");
                collision.collider.SendMessage("TakeDamage");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("Exit the wall");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //碰撞Soda
        if (collision.gameObject.tag == "Soda")
        {
            Debug.Log("Hit the Soda");
            hp += sodaHp;
            san -= sodaSan;
            Destroy(collision.transform.gameObject);
            //collision.collider.SendMessage("TakeDamage");
        }
        //碰撞Food
        if (collision.gameObject.tag == "Food")
        {
            Debug.Log("Hit the Food");
            hp += sodaHp;
            Destroy(collision.transform.gameObject);
            //collision.collider.SendMessage("TakeDamage");
        }
        //碰撞出口Exit
        if (collision.gameObject.tag == "Exit")
        {
            Debug.Log("Hit the Exit");

            //GameManager gameManager;
            //gameManager = GetComponent<GameManager>();
            //gameManager.NextLevel();

            //gameManager.NextLevel();
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().NextLevel();
            //Application.LoadLevel(Application.loadedLevel);

            //Destroy(collision.transform.gameObject);
            //collision.collider.SendMessage("TakeDamage");
        }
    }

    private void walk()
    {
        transform.rotation = Quaternion.identity;
        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle"))
        //{
        //    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        //    {
        //        transform.Translate(Vector3.up * Time.deltaTime * MoveSpeed);
        //    }
        //    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        //    {
        //        transform.Translate(Vector3.down * Time.deltaTime * MoveSpeed);
        //    }
        //    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        //    {
        //        transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed);
        //    }
        //    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        //    {
        //        transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed);
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PCUP"))
            {
                animator.SetTrigger("TurnUp");
            }            
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PCDOWN"))
            {
                animator.SetTrigger("TurnDown");
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PCLEFT"))
            {
                animator.SetTrigger("TurnLeft");
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PCRIGHT"))
            {
                animator.SetTrigger("TurnRight");
            }
        }

        if (Input.GetKeyUp(KeyCode.W) && animator.GetCurrentAnimatorStateInfo(0).IsName("PCUP"))
        {
            animator.SetTrigger("Stop");
        }
        if (Input.GetKeyUp(KeyCode.S) && animator.GetCurrentAnimatorStateInfo(0).IsName("PCDOWN"))
        {
            animator.SetTrigger("Stop");
        }
        if (Input.GetKeyUp(KeyCode.A) && animator.GetCurrentAnimatorStateInfo(0).IsName("PCLEFT"))
        {
            animator.SetTrigger("Stop");
        }
        if (Input.GetKeyUp(KeyCode.D) && animator.GetCurrentAnimatorStateInfo(0).IsName("PCRIGHT"))
        {
            animator.SetTrigger("Stop");
        }

        if (Input.GetKey(KeyCode.W) && animator.GetCurrentAnimatorStateInfo(0).IsName("PCUP"))
        {
            transform.Translate(Vector3.up * Time.deltaTime * MoveSpeed);
        }
        if (Input.GetKey(KeyCode.S) && animator.GetCurrentAnimatorStateInfo(0).IsName("PCDOWN"))
        {
            transform.Translate(Vector3.down * Time.deltaTime * MoveSpeed);
        }
        if (Input.GetKey(KeyCode.A) && animator.GetCurrentAnimatorStateInfo(0).IsName("PCLEFT"))
        {
            transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed);
        }
        if (Input.GetKey(KeyCode.D) && animator.GetCurrentAnimatorStateInfo(0).IsName("PCRIGHT"))
        {
            transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed);
        }


        //if (Input.GetKey(KeyCode.W))
        //{
        //    if (animator.GetCurrentAnimatorStateInfo(0).IsName("PCUpIdle"))
        //    {
        //        animator.SetTrigger("Move");
        //    }
        //    if (animator.GetCurrentAnimatorStateInfo(0).IsName("PCDownIdle") ||
        //        animator.GetCurrentAnimatorStateInfo(0).IsName("PCLeftIdle") ||
        //        animator.GetCurrentAnimatorStateInfo(0).IsName("PCRightIdle"))
        //    {
        //        animator.SetTrigger("TurnUp");
        //        animator.SetTrigger("Move");
        //    }

        //    //if (animator.GetCurrentAnimatorStateInfo(0).IsName("PCUpIdle"))
        //    //{
        //    //    animator.SetTrigger("Move");
        //    //}
        //    if (animator.GetCurrentAnimatorStateInfo(0).IsName("PCUP"))
        //    {
        //        transform.Translate(Vector3.up * Time.deltaTime * MoveSpeed);
        //    }

        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    if (animator.GetCurrentAnimatorStateInfo(0).IsName("PCDownIdle"))
        //    {
        //        animator.SetTrigger("Move");
        //    }
        //    if (animator.GetCurrentAnimatorStateInfo(0).IsName("PCUpIdle") ||
        //        animator.GetCurrentAnimatorStateInfo(0).IsName("PCLeftIdle") ||
        //        animator.GetCurrentAnimatorStateInfo(0).IsName("PCRightIdle"))
        //    {
        //        animator.SetTrigger("TurnDown");
        //        animator.SetTrigger("Move");
        //    }
        //    //if (animator.GetCurrentAnimatorStateInfo(0).IsName("PCDownIdle"))
        //    //{
        //    //    animator.SetTrigger("Move");
        //    //}
        //    if (animator.GetCurrentAnimatorStateInfo(0).IsName("PCDOWN"))
        //    {
        //        transform.Translate(Vector3.down * Time.deltaTime * MoveSpeed);
        //    }

        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    if (animator.GetCurrentAnimatorStateInfo(0).IsName("PCLeftIdle"))
        //    {
        //        animator.SetTrigger("Move");
        //    }
        //    if (animator.GetCurrentAnimatorStateInfo(0).IsName("PCUpIdle") ||
        //        animator.GetCurrentAnimatorStateInfo(0).IsName("PCDownIdle") ||
        //        animator.GetCurrentAnimatorStateInfo(0).IsName("PCRightIdle"))
        //    {
        //        animator.SetTrigger("TurnLeft");
        //        animator.SetTrigger("Move");
        //    }
        //    //if (animator.GetCurrentAnimatorStateInfo(0).IsName("PCLeftIdle"))
        //    //{
        //    //    animator.SetTrigger("Move");
        //    //}
        //    if (animator.GetCurrentAnimatorStateInfo(0).IsName("PCLEFT"))
        //    {
        //        transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed);
        //    }

        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    if (animator.GetCurrentAnimatorStateInfo(0).IsName("PCRightIdle"))
        //    {
        //        animator.SetTrigger("Move");
        //    }
        //    if (animator.GetCurrentAnimatorStateInfo(0).IsName("PCUpIdle") ||
        //        animator.GetCurrentAnimatorStateInfo(0).IsName("PCDownIdle") ||
        //        animator.GetCurrentAnimatorStateInfo(0).IsName("PCLeftIdle"))
        //    {
        //        animator.SetTrigger("TurnRight");
        //        animator.SetTrigger("Move");
        //    }
        //    //if (animator.GetCurrentAnimatorStateInfo(0).IsName("PCRightIdle"))
        //    //{
        //    //    animator.SetTrigger("Move");
        //    //}
        //    if (animator.GetCurrentAnimatorStateInfo(0).IsName("PCRIGHT"))
        //    {
        //        transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed);
        //    }

        //}


        //if (Input.GetKeyDown(KeyCode.J) && animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle"))
        //if (Input.GetKeyDown(KeyCode.J) && animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle"))
        //{
        //    animator.SetTrigger("playerChop");
        //    //collision.collider.SendMessage("TakeDamage");
        //}
    }

    //受到攻击
    private void TakeDamage(int damageCount)
    {
        hp -= damageCount;
        animator.SetTrigger("playerHit");
        if(hp <= 0)
        {
            this.enabled = false;
        }
    }

    public int getHP()
    {
        return hp;
    }

    public int getSan()
    {
        return san;
    }
}

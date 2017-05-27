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
    public GameObject trueCamera = null;
    [SerializeField]
    private GameObject back = null;
    [SerializeField]
    private GameObject lightObj = null;

    private bool keyDown = false;

    //private Vector2 targetPosition = new Vector2(1, 1);

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        trueCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
	
	// Update is called once per frame
	void Update () {
        walk();
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!keyDown)
            {
                keyDown = true;
                trueCamera.GetComponent<DrawPixelTexture>().SwitchLight();
                lightObj.GetComponent<LightCtr>().ResetLit();
                lightObj.SetActive(!lightObj.activeSelf);
            }
        }
        else keyDown = false;
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
            back.transform.rotation = Quaternion.identity;
            back.transform.Rotate(new Vector3(0, 0, 180));
        }
        if (Input.GetKey(KeyCode.S) && animator.GetCurrentAnimatorStateInfo(0).IsName("PCDOWN"))
        {
            transform.Translate(Vector3.down * Time.deltaTime * MoveSpeed);
            back.transform.rotation = Quaternion.identity;
        }
        if (Input.GetKey(KeyCode.A) && animator.GetCurrentAnimatorStateInfo(0).IsName("PCLEFT"))
        {
            transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed);
            back.transform.rotation = Quaternion.identity;
            back.transform.Rotate(new Vector3(0, 0, 270));
        }
        if (Input.GetKey(KeyCode.D) && animator.GetCurrentAnimatorStateInfo(0).IsName("PCRIGHT"))
        {
            transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed);
            back.transform.rotation = Quaternion.identity;
            back.transform.Rotate(new Vector3(0, 0, 90));
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    public Transform player = null;  //摄像机要跟随的人物
    public float smoothTime = 0.01f; //摄像机平滑移动的时间
    private Vector3 cameraVelocity = Vector3.zero;
    private Camera mainCamera; //主摄像机（有时候会在工程中有多个摄像机，但是只能有一个主摄像机吧）
    public GameObject gameManager;
    // Use this for initialization

    private void Awake()
    {
        
            GameObject.Instantiate(gameManager);
       
        
    }

    void Start () {
        mainCamera = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        transform.position = Vector3.SmoothDamp(transform.position, player.position + new Vector3(0, 0, -5), ref cameraVelocity, smoothTime);
    }
}

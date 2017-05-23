using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFollowPlayer : MonoBehaviour {

    public Transform player;  //摄像机要跟随的人物
    public float smoothTime = 0.01f; //摄像机平滑移动的时间
    private Vector3 shadowVelocity = Vector3.zero;
    private int view = 7;

    // Use this for initialization
    void Start () {
        transform.localScale = new Vector3(view, view, 1);
    }
	
	// Update is called once per frame
	void Update () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = Vector3.SmoothDamp(transform.position, player.position + new Vector3(0, 0, -1), ref shadowVelocity, smoothTime);
    }
}

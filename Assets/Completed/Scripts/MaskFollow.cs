using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskFollow : MonoBehaviour
{
    public Transform player = null;  //摄像机要跟随的人物
    public float smoothTime = 0.01f; //摄像机平滑移动的时间
    private Vector3 cameraVelocity = Vector3.zero;

    void Start()
    {}

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        transform.position = Vector3.SmoothDamp(transform.position, player.position + new Vector3(0, 0, -5), ref cameraVelocity, smoothTime);
    }
}

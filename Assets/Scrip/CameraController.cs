using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] float FollowSpeed = 2f; // kecepatan update kamera
    private float yOffset = 1.4f;
    public Transform target; // target yang mau diikutin kamera

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x,target.position.y + yOffset,-10f);
        transform.position = Vector3.Slerp(transform.position,newPos,FollowSpeed*Time.deltaTime);
    }
}

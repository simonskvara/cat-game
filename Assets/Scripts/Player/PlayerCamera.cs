using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    
    void Awake()
    {
        if (virtualCamera.Follow != null)
        {
            virtualCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    
}

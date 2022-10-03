using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraLogic : MonoBehaviour
{
    [Header("Object to follow")]
    [SerializeField] GameObject _player;

    [Space]

    [Header("Camera positon")]
    [SerializeField] float cameraPostionX;
    [SerializeField] float cameraPostionY;
    [SerializeField] float cameraPostionZ;

    void Update()
    {
        transform.position = new Vector3(_player.transform.position.x + cameraPostionX, _player.transform.position.y + cameraPostionY, _player.transform.position.z + cameraPostionZ);
    }
}

using UnityEngine;
using System.Collections;
using DP.Controllers;
/// <summary>
/// Controls the behavior of the third-person shooter camera in the game.
/// This is crap tho.
/// </summary>
public class TPSCameraBehavior : MonoBehaviour
{
    /// <summary>
    /// Called every frame to update the camera's position and rotation.
    /// </summary>
    void Update()
    {
        PlayerCameraController.Instance.RotateCameraAndModel();
    }
}


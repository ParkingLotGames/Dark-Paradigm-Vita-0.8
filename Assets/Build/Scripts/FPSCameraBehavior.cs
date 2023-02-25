using UnityEngine;
using System.Collections;
using DP.Controllers;
/// <summary>
/// Updates the camera position and rotation once per frame.
/// </summary>
public class FPSCameraBehavior : MonoBehaviour
{
    /// <summary>
    /// Updates the camera position and rotation once per frame.
    /// </summary>
    void Update()
    {
        PlayerCameraController.Instance.RotateCameraAndPlayer();
    }
}

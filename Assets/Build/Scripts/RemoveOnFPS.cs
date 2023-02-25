using UnityEngine;
using System.Collections;
using DP.Management;

public class RemoveOnFPS : MonoBehaviour
{
    void Start()
    {
        //if(GameManager.Instance.gameMode == GameManager.GameMode.Arcade)
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

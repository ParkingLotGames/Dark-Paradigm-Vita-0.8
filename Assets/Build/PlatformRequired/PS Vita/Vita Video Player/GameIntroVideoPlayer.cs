#if UNITY_PSP2
using UnityEngine.PSVita;
#endif
#if UNITY_ANDROID
#endif
#if UNITY_PS4
using UnityEngine.PS4;
#endif


using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DP.Management;

public class GameIntroVideoPlayer : MonoBehaviour
{
#if UNITY_PSP2

    public enum CurrentlyPlaying { ParkingLot, Megane }
    public CurrentlyPlaying currentlyPlaying;
    [Tooltip("Write the location of the (MP4, 960x544) file, must be inside the StreamingAssets folder, for example: StreamingAssets/Movies/demo01.mp4")]
    [SerializeField] string kyuHENVideoFilePath, parkingLotVideoFilePath, meganeVideoFIlePath;
    [Tooltip("Create a Render Texture and reference it here, you also need to create a naterial (Unlit/Texture shader recommended for fullscreen video playing) and assign the Render Texture as the material's texture")]
    [SerializeField] RenderTexture renderTexture;
    [SerializeField] GameObject canvas;

    void Start()
    {
        if (Application.isEditor)
        {
            LoadNextScene();
        }
        else
        {
            PlayParkingLot();
        }

    }

    private void InitRenderTexture()
    {
        PSVitaVideoPlayer.Init(renderTexture);
    }

    private void PlayParkingLot()
    {
        currentlyPlaying = CurrentlyPlaying.ParkingLot;
        StopPlayback();
        InitRenderTexture();
        PSVitaVideoPlayer.Play(parkingLotVideoFilePath, PSVitaVideoPlayer.Looping.None, PSVitaVideoPlayer.Mode.RenderToTexture);
        Invoke("PlayMegane", 3.75f);
    }

    private static void StopPlayback()
    {
        PSVitaVideoPlayer.Stop();
    }

    private void PlayMegane()
    {
        currentlyPlaying = CurrentlyPlaying.Megane;
        StopPlayback();
        InitRenderTexture();
        PSVitaVideoPlayer.Play(meganeVideoFIlePath, PSVitaVideoPlayer.Looping.None, PSVitaVideoPlayer.Mode.RenderToTexture);
        //canvas.enabled = true;
        //specialThanks.enabled = true;
        Invoke("LoadNextScene", 3.75f);
    }

    void OnPreRender()
    {
        if (!Application.isEditor)
            PSVitaVideoPlayer.Update();
    }

    private void LoadNextScene()
    {
        if (!Application.isEditor)
            PSVitaVideoPlayer.TransferMemToHeap();
        SceneManager.LoadScene(1);
    } 
#endif
}
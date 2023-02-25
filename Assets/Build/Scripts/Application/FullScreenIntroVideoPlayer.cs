#if UNITY_PSP2
using UnityEngine.PSVita;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DP.Management;
using UnityEngine.Events;

/// <summary>
/// Controls playing fullscreen videos on the PS Vita platform.
/// </summary>
public class FullScreenIntroVideoPlayer : MonoBehaviour
{
#if UNITY_PSP2 //&& !UNITY_EDITOR
    bool isPlaying;
    [Tooltip("Write the location of the (MP4, 960x544) file(s), must be inside the StreamingAssets folder, for example: StreamingAssets/Movies/demo01.mp4")]
    [SerializeField] string PLSFilePath, meganeFilePath;

    [Tooltip("Create a Render Texture and reference it here, you also need to create a material (Unlit/Texture shader recommended for fullscreen video playing) and assign the Render Texture as the material's texture")]
    [SerializeField] RenderTexture renderTexture;
    [SerializeField] UnityEvent kyuHENSplash;

    /// <summary>
    /// Starts the video playback.
    /// </summary>
    void Start()
    {
        if (Application.isEditor)
        {
            InvokeEvent();
        }
        else
        {
            isPlaying = true;
            PlayParkingLot();
        }

    }

    /// <summary>
    /// Initializes the Render Texture for video playback.
    /// </summary>
    private void InitRenderTexture()
    {
        PSVitaVideoPlayer.Init(renderTexture);
    }

    /// <summary>
    /// Stops video playback.
    /// </summary>
    private static void StopPlayback()
    {
        PSVitaVideoPlayer.Stop();
    }

    /// <summary>
    /// Plays the Parking Lot video.
    /// </summary>
    private void PlayParkingLot()
    {
        StopPlayback();
        InitRenderTexture();
        PSVitaVideoPlayer.Play(PLSFilePath, PSVitaVideoPlayer.Looping.None, PSVitaVideoPlayer.Mode.RenderToTexture);
        Invoke("PlayMegane", 3.75f);
    }

    /// <summary>
    /// Plays the Megane video.
    /// </summary>
    private void PlayMegane()
    {
        StopPlayback();
        InitRenderTexture();
        PSVitaVideoPlayer.Play(meganeFilePath, PSVitaVideoPlayer.Looping.None, PSVitaVideoPlayer.Mode.RenderToTexture);
        Invoke("InvokeEvent", 3.75f);
        //canvas.enabled = true;
        //specialThanks.enabled = true;
    }

    /// <summary>
    /// Invokes a UnityEvent.
    /// </summary>
    void InvokeEvent()
    {
        kyuHENSplash.Invoke();
    }

    /// <summary>
    /// Updates the video playback.
    /// </summary>
    void OnPreRender()
    {
        if(isPlaying)
        PSVitaVideoPlayer.Update();
    }

    /// <summary>
    /// Disables the player.
    /// </summary>
    public void DisablePlayer()
    {
        enabled = false;
    }
#endif
}

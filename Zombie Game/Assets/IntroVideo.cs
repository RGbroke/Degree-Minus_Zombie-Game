using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideo : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += LoadMenuScene;
    }

    private void Update()
    {
        // Check for mouse click or "Jump" input (usually Spacebar) to skip the video
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump"))
        {
            LoadMenuScene(videoPlayer);
        }
    }

    void LoadMenuScene(VideoPlayer vp)
    {
        SceneManager.LoadScene("Menu");
    }
}


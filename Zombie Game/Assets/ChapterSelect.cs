using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterSelect : MonoBehaviour
{
    public void loadChapter1()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
    public void loadChapter2()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}

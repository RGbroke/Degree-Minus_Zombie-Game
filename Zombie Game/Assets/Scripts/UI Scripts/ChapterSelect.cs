using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterSelect : MonoBehaviour
{
    public void loadChapter1()
    {
        SceneManager.LoadScene("start");
    }
    public void loadChapter2()
    {
        SceneManager.LoadScene("InHospital");
    }
    public void loadChapter3()
    {
        SceneManager.LoadScene("Basement");
    }
}

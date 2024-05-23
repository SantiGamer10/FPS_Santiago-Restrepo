using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScenesController : MonoBehaviour
{
    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Close()
    {

        UnityEditor.EditorApplication.isPlaying = false;

            Application.Quit();
    }
}

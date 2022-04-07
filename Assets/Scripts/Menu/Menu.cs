using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Animator tirai;

    IEnumerator nextScene(string sceneTarget, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneTarget);
    }

    public void toTutorial()
    {
        tirai.SetTrigger("close");
        StartCoroutine(nextScene("tutorial1", 2f));
    }

    public void toUjian()
    {
        tirai.SetTrigger("close");
        StartCoroutine(nextScene("MainScene", 2f));
    }
}

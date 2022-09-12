using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Animator tirai;
    public GameObject inputPanel;
    public GameObject inputfield;
    public GameLoger loger;

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

    public void toLatihan()
    {
        tirai.SetTrigger("close");
        StartCoroutine(nextScene("LatihanScene", 2f));
    }

    public void InitNama()
    {
        string nama;
        nama = inputfield.GetComponent<Text>().text;
        if(nama != null || nama != "")
        {
            loger.pemain = nama;
            inputPanel.SetActive(false);
        }
    }
}

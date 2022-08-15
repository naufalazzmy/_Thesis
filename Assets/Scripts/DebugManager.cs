using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DebugManager : MonoBehaviour
{
    private static DebugManager instance;
    readonly bool savelog = true;

    public string filepath;
    public GameLoger gameLogerScript;
    StreamWriter sw;

    private void Awake()
    {
        gameLogerScript = GameObject.Find("GameLoger").GetComponent<GameLoger>();
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
            InitLogFile();
        }

    }

    void InitLogFile()
    {
        try
        {
            filepath = Application.persistentDataPath + Path.DirectorySeparatorChar + gameLogerScript.pemain + "-[UJIAN].txt";

            Log("attempting writing log file: " + filepath);

            File.WriteAllText(filepath, System.DateTime.Now.ToString() + "\n\n");

            Log("log file created: " + filepath);
        }
        catch (System.Exception e)
        {
            Log("failed to create log file\n" + e);
        }
    }

    public void Log(object message)
    {
        if (savelog)
        {
            sw = File.AppendText(filepath);
            sw.WriteLine(System.DateTime.Now.ToString() + " | " + message);
            sw.Close();
        }

        //Debug.Log(message);
    }
}
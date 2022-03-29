using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public List<GameObject> SelectedBilangan = new List<GameObject>();
    public List<List<GameObject>> historyBilangan = new List<List<GameObject>>();
    public Generator gen;
    public GameObject smoke;
    public List<GameObject> listTarget;

    // make it selected effect
    private void setSelectedTrue(GameObject sumber)
    {
        if (sumber.GetComponent<DataBilangan>().bilangan[0] == '+')
        {
            sumber.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        }
        else if (sumber.GetComponent<DataBilangan>().bilangan[0] == '-')
        {
            sumber.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        }
    }

    private void setSelectedFalse(GameObject sumber)
    {
        if (sumber.GetComponent<DataBilangan>().bilangan[0] == '+')
        {
            sumber.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
        else if (sumber.GetComponent<DataBilangan>().bilangan[0] == '-')
        {
            sumber.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
        }
    }

    //disni nih--------------
    private void checkTarget(GameObject obj)
    {
        foreach(string target in listTarget){
            if(obj.GetComponent<DataBilangan>().op == target)
        }
    }

    private void CalculateBilangan(GameObject bil1, GameObject bil2)
    {
        Bilangan obj1 = gen.newBilangan(bil1.GetComponent<DataBilangan>().bilangan, bil1.GetComponent<DataBilangan>().op);
        Bilangan obj2 = gen.newBilangan(bil2.GetComponent<DataBilangan>().bilangan, bil2.GetComponent<DataBilangan>().op);
        List<Bilangan> bilanganList = new List<Bilangan>();
        bilanganList.Add(obj1);
        bilanganList.Add(obj2);
        Bilangan hasil = gen.Hitung(bilanganList);

        // instantiate
        GameObject newObj = gen.generateObject(hasil, bil2.transform.position);
        checkTarget(newObj); // cek apakah ada di target
        
        // play smoke effect
        Vector3 smokepos = newObj.transform.position;
        smoke.transform.position = smokepos;
        smoke.SetActive(true);
        smoke.GetComponent<ParticleSystem>().Play();


        // tambah ke list history
        List<GameObject> curBilangan = new List<GameObject>() { bil1, bil2, newObj };
        historyBilangan.Add(curBilangan);
    }


    public void addSelected(GameObject sumber)
    {
        
        if (!(SelectedBilangan.Contains(sumber))) //cek dulu sumber ini sudah ada apa belum dalem list
        {
            if (SelectedBilangan.Count >= 1)  // terus if kalo sudah ada total 2 dalam list, kalkulasi terus kosongin
            {
                setSelectedFalse(SelectedBilangan[0]);
                SelectedBilangan.Add(sumber);
                CalculateBilangan(SelectedBilangan[0], SelectedBilangan[1]);  // kalkulasi nilainya disini

                SelectedBilangan[0].SetActive(false);
                SelectedBilangan[1].SetActive(false);
                SelectedBilangan.Clear();
            }
            else
            {
                SelectedBilangan.Add(sumber);
                setSelectedTrue(sumber);

            }

        }
    }

    public void Undo()
    {
        List<GameObject> lastHistory = new List<GameObject>();
        lastHistory = historyBilangan[historyBilangan.Count - 1];
        lastHistory[0].SetActive(true);
        lastHistory[1].SetActive(true);
        Destroy(lastHistory[2]);
        historyBilangan.RemoveAt(historyBilangan.Count - 1);
    }

    public void Restart()
    {
        foreach(List<GameObject> lastHistory in historyBilangan)
        {
            //List<GameObject> lastHistory = new List<GameObject>();
            //lastHistory = historyBilangan[historyBilangan.Count - 1];
            lastHistory[0].SetActive(true);
            lastHistory[1].SetActive(true);
            Destroy(lastHistory[2]);
        }
        historyBilangan.Clear();
    }


}

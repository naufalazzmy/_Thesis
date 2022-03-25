using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> SelectedBilangan = new List<GameObject>();
    public Generator gen;

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

    private void CalculateBilangan(GameObject bil1, GameObject bil2)
    {
        Bilangan obj1 = gen.newBilangan(bil1.GetComponent<DataBilangan>().bilangan, bil1.GetComponent<DataBilangan>().op);
        Bilangan obj2 = gen.newBilangan(bil2.GetComponent<DataBilangan>().bilangan, bil2.GetComponent<DataBilangan>().op);
        List<Bilangan> bilanganList = new List<Bilangan>();
        bilanganList.Add(obj1);
        bilanganList.Add(obj2);
        Bilangan hasil = gen.Hitung(bilanganList);

        gen.generateObject(hasil);
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
}

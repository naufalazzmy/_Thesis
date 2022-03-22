using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> SelectedBilangan = new List<GameObject>();

    public void addSelected(GameObject sumber)
    {
        //cek dulu sumber ini sudah ada apa belum dalem list
        // kalo belum. ditambah dalem list
        // terus if kalo sudah ada total 2 dalam list, kalkulasi terus kosongin
        SelectedBilangan.Add(sumber);
    }
}

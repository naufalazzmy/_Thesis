using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoalHandler : MonoBehaviour
{
    public int jumlahTambah;
    public int jumlahKurang;
    public int jumlahKali;
    public int jumlahBagi;

    //private int jumlahBlok = jumlahTambah + jumlahKurang + jumlahKali + jumlahBagi;
    //private int jumlahOperand;

    private Generator gen;

    private void Start()
    {
        gen = this.gameObject.GetComponent<Generator>();
        //    setJumlahOperand();
       // Debug.Log(gen);
        buatSoal();
    }

    //private void setJumlahOperand()
    //{
    //    if(jumlahTambah > 0)
    //    {
    //        jumlahOperand++;
    //    }
    //    if (jumlahKurang > 0)
    //    {
    //        jumlahOperand++;
    //    }
    //    if (jumlahKali > 0)
    //    {
    //        jumlahOperand++;
    //    }
    //    if (jumlahBagi > 0)
    //    {
    //        jumlahOperand++;
    //    }
    //}

    public void buatSoal()
    {
        List<Bilangan> bilangan = new List<Bilangan>();
        
        for (int i = 0; i < jumlahTambah; i++)
        {
            float randNum = Random.Range(1, 10); // disini randomnya
            bilangan.Add(gen.newBilangan("+" + randNum.ToString(), "+"));
        }
        for (int i = 0; i < jumlahKurang; i++)
        {
            float randNum = Random.Range(1, 10); // disini randomnya
            bilangan.Add(gen.newBilangan("-" + randNum.ToString(), "-"));
        }
        for (int i = 0; i < jumlahKali; i++)
        {
            float randNum = Random.Range(1, 10); // disini randomnya
            int isPos = Random.Range(1, 2); // get ini positive apa negative wkwkw
            if(isPos == 1)
            {
                bilangan.Add(gen.newBilangan("+" + randNum.ToString(), "*"));
            }
            else
            {
                bilangan.Add(gen.newBilangan("-" + randNum.ToString(), "*"));
            }
            
        }
        for (int i = 0; i < jumlahBagi; i++)
        {
            float randNum = Random.Range(1, 10); // disini randomnya
            int isPos = Random.Range(1, 2); // get ini positive apa negative wkwkw
            if (isPos == 1)
            {
                bilangan.Add(gen.newBilangan("+" + randNum.ToString(), "/"));
            }
            else
            {
                bilangan.Add(gen.newBilangan("-" + randNum.ToString(), "/"));
            }

        }

        Node root = gen.newNode(bilangan, null, null);
        gen.generateChildrenNodes(root);

        Node targetNode = root;

        int totalCombination = root.listBilangan.Count; // 4,3,2,1.

        //for (int i = 0; i < totalCombination - 2; i++)
        //{

        //    int targetchild = Random.Range(0, targetNode.child.Count);
        //    targetNode = targetNode.child[targetchild];
        //}

        ////disini buat set target!!
        gen.generateTarget(root.child[0].child[0]);
        gen.printSolution(root.child[0].child[0]);

        StartCoroutine(gen.instantiateforSec(bilangan, 0.7f));
    }
}

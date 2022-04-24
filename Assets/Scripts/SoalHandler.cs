using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SoalHandler : MonoBehaviour
{
    public int jumlahTambah;
    public int jumlahKurang;
    public int jumlahKali;
    public int jumlahBagi;

    public float sumKali;

    public int jumlahBlok;
    public int jumlahOperand;

    private Generator gen;
    public List<float> ListBilangan;
    
    private void Start()
    {
        jumlahBlok = jumlahTambah + jumlahKurang + jumlahKali + jumlahBagi;
        setJumlahOperand();

        gen = this.gameObject.GetComponent<Generator>();
        
       // Debug.Log(gen);
        buatSoal();
    }

    private void setJumlahOperand()
    {
        if (jumlahTambah > 0)
        {
            jumlahOperand++;
        }
        if (jumlahKurang > 0)
        {
            jumlahOperand++;
        }
        if (jumlahKali > 0)
        {
            jumlahOperand++;
        }
        if (jumlahBagi > 0)
        {
            jumlahOperand++;
        }
    }

    public void buatSoal()
    {
        List<Bilangan> bilangan = new List<Bilangan>();
        
        for (int i = 0; i < jumlahTambah; i++)
        {
            float randNum = Random.Range(1, 10); // disini randomnya TODO mu
            bilangan.Add(gen.newBilangan("+" + randNum.ToString(), "+"));
            ListBilangan.Add(randNum);
        }
        for (int i = 0; i < jumlahKurang; i++)
        {
            float randNum = Random.Range(1, 10); // disini randomnya
            bilangan.Add(gen.newBilangan("-" + randNum.ToString(), "-"));
            ListBilangan.Add(randNum);
        }
        for (int i = 0; i < jumlahKali; i++)
        {
            float randNum = Random.Range(1, 10); // disini randomnya
            int isPos = Random.Range(1, 2); // get ini positive apa negative wkwkw
            sumKali += randNum;
            ListBilangan.Add(randNum);
            if (isPos == 1)
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
            ListBilangan.Add(randNum);
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

        generateTarget(root);
        
        StartCoroutine(gen.instantiateforSec(bilangan, 0.7f));
        getDifficulty();
    }

    public void generateTarget(Node root)
    {
        Node targetNode = root;
        int totalCombination = root.listBilangan.Count; // 4,3,2,1.

        // TODO MU: Sebaiknya km atur depth dan check apa bener semuanya ga contain root bilangan?
        for (int i = 0; i < totalCombination - 1; i++)
        {

            int targetchild = Random.Range(0, targetNode.child.Count);
            targetNode = targetNode.child[targetchild];
        }


        gen.generateTarget(targetNode);
        gen.printSolution(targetNode);
    }

    private int factorial(int a)
    {
        int fact = 1;
        for(int x=1; x <= a; x++)
        {
            fact *= x;
        }

        return fact;
    }

    public void getDifficulty()
    {
        float totalSum = 0;
        
        foreach (float c in ListBilangan)
        {
            totalSum += c;
        }

        float z1 = (factorial(ListBilangan.Count) / factorial(2) * factorial((ListBilangan.Count - 2)));
        float z2 = ListBilangan.Count * totalSum / 72;
        float maxi = ListBilangan.Max();
        float mini = ListBilangan.Min();
        float z3 = 1 - ((maxi - mini) / 9);


        //float be = Mathf.Log10(totalSum) / ListBilangan.Count;
        //float es = 3 / 8;//searchdepth
        //float oh = 3 / 4;//operator



        //float je = (Mathf.Log10(jumlahOperand) * totalSum) / jumlahBlok;
        //float ka = (jumlahKali/ jumlahBlok) * (sumKali/totalSum); //ini baru untuk kali saja
        //float detphSearch = 3 / 8;
        Debug.Log((z1+z2+z3)/3); //KOK BISA SALAH COK?

      //  Debug.Log(be + es + oh);
    }
}

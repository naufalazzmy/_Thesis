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
    public List<float> ListBilangan;

<<<<<<< Updated upstream
    
    public List<float> ListBilangan;



    public float curDifficulty = 0f;

    private Generator gen;
=======
    private Generator gen;
    
>>>>>>> Stashed changes
    
    private void Start()
    {
        jumlahBlok = jumlahTambah + jumlahKurang + jumlahKali + jumlahBagi;
        setJumlahOperand();

        gen = this.gameObject.GetComponent<Generator>();

        // Debug.Log(gen);
        List<Bilangan> bilanganObj =  BuatSoal();
        float difficultyIndex = getDifficulty();
        Debug.Log(difficultyIndex);
        GenerateSoal(bilanganObj);
        // TODO: Check difficultynya
        // kalo bener yaudah generate soalnya
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

    public List<Bilangan> BuatSoal()
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
        for (int i = 0; i < jumlahBagi; i++) // PEMBAGIAN INI TRICKY SEKALI, jadi 0.00002 bisa
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

<<<<<<< Updated upstream
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
        // dan check operatornya ga ada yang * di depan
        // check kalo bisa targetnya ga ada komaan
        // dan kayaknya depth itu berpengaruh si
        
        for (int i = 0; i < totalCombination - 1; i++)
        {

            int targetchild = Random.Range(0, targetNode.child.Count);
            targetNode = targetNode.child[targetchild];
        }


        gen.generateTarget(targetNode);
        gen.printSolution(targetNode);
=======
        return bilangan;
>>>>>>> Stashed changes
    }

    private float getCombination(int jumlahBlok)
    {
        return (factorial(jumlahBlok) / (factorial(2) * factorial((jumlahBlok - 2))));
    }

    private int factorial(int a)
    {
        int fact = 1;
        for (int x = 1; x <= a; x++)
        {
            fact *= x;
        }

        return fact;
    }

    private float getDifficulty()
    {
        float totalSum = 0;

        foreach (float c in ListBilangan)
        {
            totalSum += c;
        }
        Debug.Log(totalSum);
        Debug.Log(jumlahBlok);

        float z1 = getCombination(jumlahBlok)/28f;
        float z2 = (jumlahBlok * totalSum) / 72f; // ini masih lebih dari 1
        float maxi = ListBilangan.Max();
        float mini = ListBilangan.Min();
        float z3 = 1 - ((maxi - mini) / 9f);
        float z4 = jumlahOperand / 4f;

<<<<<<< Updated upstream
        
        Debug.Log((z1)+"-"+z2+"-"+z3+"-"+z4);
        Debug.Log(((z1)+z2+z3+z4)/4);

        curDifficulty = ((z1) + z2 + z3 + z4)/ 4;
    }

    public void getScore()
    {
        // sum bilangan * 10 + second waktu (sisa) + 500 per operator * jumlah depth <= kalo dia bener
=======

        //float be = Mathf.Log10(totalSum) / ListBilangan.Count;
        //float es = 3 / 8;//searchdepth
        //float oh = 3 / 4;//operator



        //float je = (Mathf.Log10(jumlahOperand) * totalSum) / jumlahBlok;
        //float ka = (jumlahKali/ jumlahBlok) * (sumKali/totalSum); //ini baru untuk kali saja
        //float detphSearch = 3 / 8;

        Debug.Log((z1 / 28) + "-" + z2 + "-" + z3 + "-" + z4);
        return (((z1 / 28) + z2 + z3 + z4) / 4); //KOK BISA SALAH COK?

        //  Debug.Log(be + es + oh);
    }

    public void GenerateSoal(List<Bilangan> bilangan)
    {
        
        Node root = gen.newNode(bilangan, null, null);
        gen.generateChildrenNodes(root);

        generateTarget(root);
        
        StartCoroutine(gen.instantiateforSec(bilangan, 0.7f));
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
>>>>>>> Stashed changes
    }

    
}

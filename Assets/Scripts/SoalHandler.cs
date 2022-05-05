using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SoalHandler : MonoBehaviour
{

    public float sumKali;

    public int jumlahBlok;
    public int jumlahOperand;
    public List<float> ListBilangan;


    public float curDifficulty = 0f;

    private Generator gen;

    public int jumlahTambah;
    public int jumlahKurang;
    public int jumlahKali;
    public int jumlahBagi;



    private void Start()
    {
        jumlahBlok = jumlahTambah + jumlahKurang + jumlahKali + jumlahBagi;
        

        gen = this.gameObject.GetComponent<Generator>();

        generateMainBlok();
        
        List<Bilangan> bilanganObj =  BuatSoal();
        float difficultyIndex = getDifficulty();
        GenerateSoal(bilanganObj);
        // TODO: Check difficultynya
        // kalo bener yaudah generate soalnya
    }

    private void generateMainBlok()
    {
        jumlahBlok = Random.Range(3, 6);
        Debug.Log("jumlah blok: " + jumlahBlok);
        int maxOperatorCount;
        // TODO: lanjutin ....

        // check dulu jumlahblok dapet berapa,
        // jumlah balok biasa harus setngah atau lebih daripada jumlah operator, contoh 3. balok 2 op 1| contoh 4 balok 3 op 1, atau balok 2 op 2
        if(jumlahBlok == 3)
        {
            maxOperatorCount = 1;
            Debug.Log("jumlah operator: " + maxOperatorCount);
            jumlahKali = maxOperatorCount;

            jumlahTambah = 1;
            jumlahKurang = 1;
        }
        else if(jumlahBlok == 4)
        {
            maxOperatorCount = Random.Range(1, 2);
            Debug.Log("jumlah operator: " + maxOperatorCount);
            jumlahKali = maxOperatorCount;

            int jumlahblokkeluar = jumlahBlok - maxOperatorCount;
            jumlahTambah = 1;
            jumlahKurang = 1;
            jumlahblokkeluar = jumlahblokkeluar - 2;
            while (jumlahblokkeluar >= 1)
            {
                Debug.Log("Current jumlah blok: " + jumlahblokkeluar);
                int rand = Random.Range(1, 2);
                if(rand == 1)
                {
                    jumlahTambah++;
                    jumlahblokkeluar--;
                }
                else
                {
                    jumlahKurang++;
                    jumlahblokkeluar--;
                }
            }
        }
        else if(jumlahBlok == 5 || jumlahBlok == 6)
        {
            maxOperatorCount = Random.Range(1, 3);
            Debug.Log("jumlah operator: " + maxOperatorCount);
            jumlahKali = maxOperatorCount;

            int jumlahblokkeluar = jumlahBlok - maxOperatorCount;
            jumlahTambah = 1;
            jumlahKurang = 1;
            jumlahblokkeluar = jumlahblokkeluar - 2;
            while (jumlahblokkeluar >= 1)
            {
                Debug.Log("Current jumlah blok: " + jumlahblokkeluar);
                int rand = Random.Range(1, 2);
                if (rand == 1)
                {
                    jumlahTambah++;
                    jumlahblokkeluar--;
                }
                else
                {
                    jumlahKurang++;
                    jumlahblokkeluar--;
                }
            }
        }
        else if(jumlahBlok == 7 || jumlahBlok == 8)
        {
            maxOperatorCount = Random.Range(1, 4);
            Debug.Log("jumlah operator: " + maxOperatorCount);
            jumlahKali = maxOperatorCount;

            int jumlahblokkeluar = jumlahBlok - maxOperatorCount;
            jumlahTambah = 1;
            jumlahKurang = 1;
            jumlahblokkeluar = jumlahblokkeluar - 2;
            while (jumlahblokkeluar >= 1)
            {
                Debug.Log("Current jumlah blok: " + jumlahblokkeluar);
                int rand = Random.Range(1, 2);
                if (rand == 1)
                {
                    jumlahTambah++;
                    jumlahblokkeluar--;
                }
                else
                {
                    jumlahKurang++;
                    jumlahblokkeluar--;
                }
            }
        }

        setJumlahOperand();

        //ATAU MAX OP masing masing 1

        // if 3 op max 1
        // if 4 op max 2
        // if op 6 max 3
        // if op 8 max 4


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

        return bilangan;
    }

    public void generateTarget(Node root)
    {
        Node targetNode = root;
        int totalCombination = root.listBilangan.Count; // 4,3,2,1.

        // TODO MU: Sebaiknya km atur depth dan check apa bener semuanya ga contain root bilangan?
        // dan check operatornya ga ada yang * di depan
        // check kalo bisa targetnya ga ada komaan
        // dan kayaknya depth itu berpengaruh si
        // todo, kalo pembagian, lihat nilai pembagiannya, blaklfdsjjoekjr ... salah
        
        for (int i = 0; i < totalCombination - 1; i++)
        {

            int targetchild = Random.Range(0, targetNode.child.Count);
            targetNode = targetNode.child[targetchild];
        }


        gen.generateTarget(targetNode);
        gen.printSolution(targetNode);


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
        float z2 = ((totalSum) / 72f) * 0.25f; // ini masih lebih dari 1
        float maxi = ListBilangan.Max();
        float mini = ListBilangan.Min();
        float z3 = (1 - ((maxi - mini) / 9f)) * 0.2f; // range cari
        float z4 = (jumlahOperand / 4f) * 0.35f;


        
        Debug.Log((z1)+"-"+z2+"-"+z3+"-"+z4);
        Debug.Log((z1+z2+z3+z4)/1.85f);
        Debug.Log("----------------");
        curDifficulty = (z1 + z2 + z3 + z4)/ 1.85f;
        return curDifficulty;
    }

    public void getScore()
    {

    }

    public void GenerateSoal(List<Bilangan> bilangan)
    {
        
        Node root = gen.newNode(bilangan, null, null);
        gen.generateChildrenNodes(root);

        generateTarget(root);
        
        StartCoroutine(gen.instantiateforSec(bilangan, 0.7f));
    }



    
}

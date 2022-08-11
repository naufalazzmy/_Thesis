using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SoalHandler : MonoBehaviour
{
    private GameLoger gl;


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
    public int lifeCount = 0;



    private void Start()
    {
        gl = GameObject.Find("GameLoger").GetComponent<GameLoger>();
        gl.indexSoal = gl.indexSoal + 1; // init index soal

        jumlahBlok = jumlahTambah + jumlahKurang + jumlahKali + jumlahBagi;

        gen = this.gameObject.GetComponent<Generator>();

        bool founded = false;
        int cobaCount = 0;

        if (gl.prevStatus == "SUCCESS")
        {
            while (!founded)
            {
                cobaCount++;
                generateMainBlok();
                List<Bilangan> bilanganObj = BuatSoal();
                float difficultyIndex = getDifficulty();

                float minTreshold = gl.prevDifficulty + 0.015f; //0.515 
                float maxTreshold = gl.prevDifficulty + 0.1f; // 0.6

                if (difficultyIndex >= minTreshold && difficultyIndex <= maxTreshold)
                {
                    Debug.Log("COBA COUNT: " + cobaCount);
                    GenerateSoal(bilanganObj);
                    founded = true;
                }
                else if (cobaCount >= 1000)
                {
                    GenerateSoal(bilanganObj);
                    Debug.LogWarning("CAPEK NYARI SUMPAH");
                    break;
                }
            }
        }
        else if(gl.prevStatus == "SKIPPED")
        {
            while (!founded)
            {
                cobaCount++;
                generateMainBlok();
                List<Bilangan> bilanganObj = BuatSoal();
                float difficultyIndex = getDifficulty();

                float minTreshold = gl.prevDifficulty - 0.015f; //0.447 || 0.015
                float maxTreshold = gl.prevDifficulty - 0.1f; // 0.362  -   0.462 || 0.1

                if (difficultyIndex <= minTreshold && difficultyIndex >= maxTreshold)
                {
                    Debug.Log("COBA COUNT: " + cobaCount);
                    GenerateSoal(bilanganObj);
                    founded = true;
                }
                else if (cobaCount >= 1000)
                {
                    GenerateSoal(bilanganObj);
                    Debug.LogWarning("CAPEK NYARI SUMPAH");
                    break;
                }
            }
        }
        else
        {
            //Debug.LogWarning("INIT START?");

            generateMainBlok();
            List<Bilangan> bilanganObj = BuatSoal();
            float difficultyIndex = getDifficulty();
            GenerateSoal(bilanganObj);
        }

        //generateMainBlok();
        //List<Bilangan> bilanganObj = BuatSoal();
        //float difficultyIndex = getDifficulty();


        //GenerateSoal(bilanganObj);
        // TODO: Check difficultynya
        // kalo bener yaudah generate soalnya
    }

    private void generateMainBlok()
    {
        

        if (gl.prevDifficulty == 0f)
        {
            //Debug.Log("Init jumlah balok?");
            lifeCount = 3;
            jumlahBlok = Random.Range(3, 6);
        }
        else if(gl.prevDifficulty < 0.56f) //46
        {
            lifeCount = 3;
            jumlahBlok = 3;
        }else if(gl.prevDifficulty >= 0.56f && gl.prevDifficulty <= 0.75f) //0.46 - 0.65
        {
            lifeCount = 3;
            jumlahBlok = 4;
        }else if(gl.prevDifficulty > 0.75f) //0.65
        {
            lifeCount = 3;
            jumlahBlok = 5;
        }

        gen.GenerateLife(lifeCount);
        
        //Debug.Log("jumlah blok: " + jumlahBlok);
        int maxOperatorCount;
        // TODO: lanjutin ....

        // check dulu jumlahblok dapet berapa,
        // jumlah balok biasa harus setngah atau lebih daripada jumlah operator, contoh 3. balok 2 op 1| contoh 4 balok 3 op 1, atau balok 2 op 2
        if(jumlahBlok == 3)
        {
            if(gl.prevDifficulty <= 0.3)
            {
                maxOperatorCount = 0;
                jumlahTambah = 2;
                jumlahKurang = 1;
            }
            else
            {
                maxOperatorCount = 1;
                jumlahTambah = 1;
                jumlahKurang = 1;
            }
            
            //Debug.Log("jumlah operator: " + maxOperatorCount);
            jumlahKali = maxOperatorCount;


        }
        else if(jumlahBlok == 4)
        {
            maxOperatorCount = Random.Range(1, 2);
            //Debug.Log("jumlah operator: " + maxOperatorCount);
            jumlahKali = maxOperatorCount;

            int jumlahblokkeluar = jumlahBlok - maxOperatorCount;
            jumlahTambah = 1;
            jumlahKurang = 1;
            jumlahblokkeluar = jumlahblokkeluar - 2;
            while (jumlahblokkeluar >= 1)
            {
                //Debug.Log("Current jumlah blok: " + jumlahblokkeluar);
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
           // Debug.Log("jumlah operator: " + maxOperatorCount);
            jumlahKali = maxOperatorCount;

            int jumlahblokkeluar = jumlahBlok - maxOperatorCount;
            jumlahTambah = 1;
            jumlahKurang = 1;
            jumlahblokkeluar = jumlahblokkeluar - 2;
            while (jumlahblokkeluar >= 1)
            {
                //Debug.Log("Current jumlah blok: " + jumlahblokkeluar);
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
            //Debug.Log("jumlah operator: " + maxOperatorCount);
            jumlahKali = maxOperatorCount;

            int jumlahblokkeluar = jumlahBlok - maxOperatorCount;
            jumlahTambah = 1;
            jumlahKurang = 1;
            jumlahblokkeluar = jumlahblokkeluar - 2;
            while (jumlahblokkeluar >= 1)
            {
               // Debug.Log("Current jumlah blok: " + jumlahblokkeluar);
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
        jumlahOperand = 0;
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

    private List<Bilangan> generateSoalBilangan(List<Bilangan> bilangan)
    {
        ListBilangan.Clear();
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
            int isPos = Random.Range(1, 3); // get ini positive apa negative wkwkw
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
            int isPos = Random.Range(1, 3); // get ini positive apa negative wkwkw
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

    public List<Bilangan> BuatSoal()
    {
        List<Bilangan> bilangan = new List<Bilangan>();
        bilangan = generateSoalBilangan(bilangan);
        //return bilangan;

        //bool founded = false;
        //int cobaCount = 0;
        //if(gl.prevSum == 0)
        //{
        //    bilangan = generateSoalBilangan(bilangan);
        //    return bilangan;
        //}
        //else if(gl.prevStatus == "SUCCESS")
        //{
        //    while (!founded)
        //    {
        //        cobaCount++;


        //        bilangan = generateSoalBilangan(bilangan);

        //        float bilSum = 0;
        //        foreach (float bil in ListBilangan)
        //        {
        //            bilSum = bilSum + bil;
        //        }

        //        if (bilSum >= gl.prevSum && bilSum <= gl.prevSum * 2)
        //        {
        //            return bilangan;
        //        }
        //        else if (cobaCount >= 1000)
        //        {
        //            Debug.LogWarning("CAPEK SUM");
        //            return bilangan;
        //        }
        //    }

        //}
        //else if(gl.prevStatus == "SKIPPED")
        //{
        //    while (!founded)
        //    {
        //        cobaCount++;
        //        float bilSum = 0;

        //        bilangan = generateSoalBilangan(bilangan);

        //        foreach (float bil in ListBilangan)
        //        {
        //            bilSum = bilSum + bil;
        //        }

        //        if (bilSum <= gl.prevSum)
        //        { 
        //            return bilangan;
        //        }
        //        else if (cobaCount >= 1000)
        //        {
        //            Debug.LogWarning("CAPEK SUM");
        //            return bilangan;
        //        }
        //    }
        //}

        return bilangan;

        //for (int i = 0; i < jumlahTambah; i++)
        //{
        //    float randNum = Random.Range(1, 20); // disini randomnya TODO mu
        //    bilangan.Add(gen.newBilangan("+" + randNum.ToString(), "+"));
        //    ListBilangan.Add(randNum);
        //}
        //for (int i = 0; i < jumlahKurang; i++)
        //{
        //    float randNum = Random.Range(1, 20); // disini randomnya
        //    bilangan.Add(gen.newBilangan("-" + randNum.ToString(), "-"));
        //    ListBilangan.Add(randNum);
        //}
        //for (int i = 0; i < jumlahKali; i++)
        //{
        //    float randNum = Random.Range(1, 20); // disini randomnya
        //    int isPos = Random.Range(1, 2); // get ini positive apa negative wkwkw
        //    sumKali += randNum;
        //    ListBilangan.Add(randNum);
        //    if (isPos == 1)
        //    {
        //        bilangan.Add(gen.newBilangan("+" + randNum.ToString(), "*"));

        //    }
        //    else
        //    {
        //        bilangan.Add(gen.newBilangan("-" + randNum.ToString(), "*"));
        //    }

        //}
        //for (int i = 0; i < jumlahBagi; i++) // PEMBAGIAN INI TRICKY SEKALI, jadi 0.00002 bisa
        //{
        //    float randNum = Random.Range(1, 20); // disini randomnya
        //    int isPos = Random.Range(1, 2); // get ini positive apa negative wkwkw
        //    ListBilangan.Add(randNum);
        //    if (isPos == 1)
        //    {
        //        bilangan.Add(gen.newBilangan("+" + randNum.ToString(), "/"));
        //    }
        //    else
        //    {
        //        bilangan.Add(gen.newBilangan("-" + randNum.ToString(), "/"));
        //    }

        //}

        //return bilangan;
    }

    public List<Node> getLastNodes(Node root, int desiredDeep, List<Node> nodes, int deep)
    {
        desiredDeep -= 1;
        foreach (var nd in root.child)
        {
            if (deep < desiredDeep && nd.child.Count > 0)
            {
                getLastNodes(nd, desiredDeep, nodes, deep + 1);
            }
            else
            {
                nodes.Add(nd);
            }
        }

        return nodes.Where(n => n.listBilangan.Any(b => b.op == "*" || b.op == "/") == false).ToList();
    }

    public Node generateTarget(Node root)
    {

        var lastNodes = getLastNodes(root, jumlahBlok, new List<Node>(), 0); // jumlah balok itu total depth yang bakal dicari

        int rnd = Random.Range(0, lastNodes.Count);
        var targetNode = lastNodes[rnd];

        return targetNode;
    }

    private bool notIncludeOp(Node node)
    {
        int opCount = 0;
        foreach(Bilangan bil in node.listBilangan)
        {
            if(bil.op == "*" || bil.op == "/")
            {
                opCount++;
            }
        }

        if(opCount>= 1)
        {
            Debug.LogWarning("ada operator di target...");
            return false;
        }
        else
        {
            return true;
        }
    }

    private void ShuffleIndex(List<int> arrayList)
    {
        for (int i = 0; i < arrayList.Count; i++)
        {
            int temp = arrayList[i];
            int randomIndex = Random.Range(i, arrayList.Count);
            arrayList[i] = arrayList[randomIndex];
            arrayList[randomIndex] = temp;
        }
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
        float z1 = 0;
        float z2 = 0;
        float z3 = 0;
        float z4 = 0;
        float totalSum = 0;

        foreach (float c in ListBilangan)
        {
            totalSum += c;
        }
        //Debug.Log(totalSum);
        //Debug.Log(jumlahBlok);
        
        // last weight(20) = z4[0.3] z3 [0.2] z1,z2 [1]

         z1 = getCombination(jumlahBlok) / 10; // 10=>5 | 28 => 8
         z2 = ((totalSum) / 45f) * 1f; // ini masih lebih dari 1 |72=>8|45=>5(9max)
        float maxi = ListBilangan.Max();
        float mini = ListBilangan.Min();
         z3 = (1 - ((maxi - mini) / 9f)) * 0.5f; // range cari INI KOK BISA MINUS BGST [pembaginya itu maximal dari nilai 1 balok yang bisa dihasilkan]
         z4 = (jumlahOperand / 4f) * 0.3f;



        //Debug.Log((z1) + "-" + z2 + "-" + z3 + "-" + z4); //z1, z4, z2, z3
        //Debug.Log((z1 + z2 + z3 + z4) / 2.5f);
        //Debug.Log("----------------");
        curDifficulty = (z1 + z2 + z3 + z4) / 2.8f;


        gl.z1 = z1;
        gl.z2 = z2;
        gl.z3 = z3;
        gl.z4 = z4;
        gl.currentSum = totalSum;
        gl.difficulty = curDifficulty; // LOG


        return curDifficulty;
    }


    public void getScore()
    {

    }

    public void GenerateSoal(List<Bilangan> bilangan)
    {
        
        Node root = gen.newNode(bilangan, null, null);
        gen.generateChildrenNodes(root);

        Node target = generateTarget(root);

        gen.generateTarget(target);
        //gen.printSolution(target);

        StartCoroutine(gen.instantiateforSec(bilangan, 0.7f));
    }



    
}

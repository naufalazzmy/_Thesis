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


    private int jumlahTambah;
    private int jumlahKurang;
    private int jumlahKali;
    private int jumlahBagi;
    public int lifeCount = 0;

    public string solusi = "";



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
                Debug.LogError("-RESTART-");
                cobaCount++;
                generateMainBlok();
                List<Bilangan> bilanganObj = BuatSoal(); // disni ancang ancang difficulty dihitung
                float difficultyIndex = getDifficulty();
                Node target = getTarget(bilanganObj);

                if(target == null)
                {
                    Debug.LogError("TARGET NULL");
                }

                float minTreshold = gl.prevDifficulty + 0.015f; //0.515 
                float maxTreshold = gl.prevDifficulty + 0.1f; // 0.6

                if ((difficultyIndex >= minTreshold && difficultyIndex <= maxTreshold) && target != null)
                {
                    Debug.Log("COBA COUNT: " + cobaCount);
                    GenerateSoal(bilanganObj, target);
                    founded = true;
                }
                else if (cobaCount >= 1000)
                {
                    GenerateSoal(bilanganObj, target);
                    Debug.LogWarning("CAPEK NYARI SUMPAH");
                    break;
                }
            }
        }
        else if(gl.prevStatus == "SKIPPED")
        {
            
            while (!founded)
            {
                Debug.LogError("-RESTART-");
                cobaCount++;
                generateMainBlok();
                List<Bilangan> bilanganObj = BuatSoal();
                float difficultyIndex = getDifficulty();
                Node target = getTarget(bilanganObj);

                float minTreshold = gl.prevDifficulty - 0.015f; //0.447 || 0.015
                float maxTreshold = gl.prevDifficulty - 0.1f; // 0.362  -   0.462 || 0.1

                if (difficultyIndex <= minTreshold && difficultyIndex >= maxTreshold && target != null)
                {
                    Debug.Log("COBA COUNT: " + cobaCount);
                    GenerateSoal(bilanganObj, target);
                    founded = true;
                }
                else if (cobaCount >= 1000)
                {
                    GenerateSoal(bilanganObj, target);
                    Debug.LogWarning("CAPEK NYARI SUMPAH");
                    break;
                }
            }
        }
        else
        {
            Debug.LogError("-RESTART-");
            //Debug.LogWarning("INIT START?");

            generateMainBlok();
            
            List<Bilangan> bilanganObj = BuatSoal();
            Node target = getTarget(bilanganObj);
            float difficultyIndex = getDifficulty();
            GenerateSoal(bilanganObj, target);
        }
    }

    private void generateMainBlok()
    {
        
        if (gl.prevDifficulty == 0f)
        {
            //Debug.Log("Init jumlah balok?");
            lifeCount = 3; // life countnya km sesuuaikan jumlah bloknya
            jumlahBlok = Random.Range(3, 6);
        }
        else if(gl.prevDifficulty < 0.56f) //46 || EASY
        {
            lifeCount = 3;
            jumlahBlok = Random.Range(3, 6);
        }
        else if(gl.prevDifficulty >= 0.56f && gl.prevDifficulty <= 0.75f) //0.46 - 0.65 || MED
        {
            lifeCount = 3;
            jumlahBlok = Random.Range(3, 6);
        }
        else if(gl.prevDifficulty > 0.75f) //0.65 || HARD
        {
            lifeCount = 3;
            jumlahBlok = Random.Range(3, 6);
        }

       
        
        //Debug.Log("jumlah blok: " + jumlahBlok);
        int maxOperatorCount;

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
            int rand = Random.Range(1, 3);
            if(rand == 1)
            {
                jumlahKali = maxOperatorCount;
            }
            else
            {
                jumlahBagi = maxOperatorCount;
            }
            


        }
        else if(jumlahBlok == 4)
        {
            maxOperatorCount = Random.Range(1, 2);
            bool isBagiMax = false;
            int rand = 0;
            for(int i=0; i<maxOperatorCount; i++)
            {
                if (!isBagiMax)
                {
                    rand = Random.Range(1, 2); // asign apakah operator bagi/kali
                }
                else
                {
                    rand = 1;
                }
                
                if (rand == 1)
                {
                    jumlahKali = maxOperatorCount;
                }
                else
                {
                    Debug.LogError("bagi maxed");
                    jumlahBagi = maxOperatorCount;
                    isBagiMax = true;
                }
            }

            int jumlahblokkeluar = jumlahBlok - maxOperatorCount; // ini buat tentuin jumlah operator yang diproses apakah kali/bagi nanti termasuk positive apa negative
            jumlahTambah = 1;
            jumlahKurang = 1;
            jumlahblokkeluar = jumlahblokkeluar - 2;
            while (jumlahblokkeluar >= 1) //
            {
                //Debug.Log("Current jumlah blok: " + jumlahblokkeluar);
                int rand1 = Random.Range(1, 3);
                if(rand1 == 1)
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
            bool isBagiMax = false;
            int rand = 0;
            for (int i = 0; i < maxOperatorCount; i++)
            {
                if (!isBagiMax)
                {
                    rand = Random.Range(1, 2); // asign apakah operator bagi/kali
                }
                else
                {
                    rand = 1;
                }

                if (rand == 1)
                {
                    jumlahKali = maxOperatorCount;
                }
                else
                {
                    Debug.LogError("bagi maxed");
                    jumlahBagi = maxOperatorCount;
                    isBagiMax = true;
                }
            }

            int jumlahblokkeluar = jumlahBlok - maxOperatorCount;
            jumlahTambah = 1;
            jumlahKurang = 1;
            jumlahblokkeluar = jumlahblokkeluar - 2;
            while (jumlahblokkeluar >= 1)
            {
                //Debug.Log("Current jumlah blok: " + jumlahblokkeluar);
                rand = Random.Range(1, 3);
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
            bool isBagiMax = false;
            int rand = 0;
            for (int i = 0; i < maxOperatorCount; i++)
            {
                if (!isBagiMax)
                {
                    rand = Random.Range(1, 3); // asign apakah operator bagi/kali
                }
                else
                {
                    rand = 1;
                }

                if (rand == 1)
                {
                    jumlahKali = maxOperatorCount;
                }
                else
                {
                    Debug.LogError("bagi maxed");
                    jumlahBagi = maxOperatorCount;
                    isBagiMax = true;
                }
            }

            int jumlahblokkeluar = jumlahBlok - maxOperatorCount;
            jumlahTambah = 1;
            jumlahKurang = 1;
            jumlahblokkeluar = jumlahblokkeluar - 2;
            while (jumlahblokkeluar >= 1)
            {
               // Debug.Log("Current jumlah blok: " + jumlahblokkeluar);
                rand = Random.Range(1, 2);
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

        setJumlahOperand(); //cuman sekedar indikator

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
 

        return bilangan;
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

        var lastNodes = getLastNodes(root, jumlahBlok, new List<Node>(), 0); // jumlah balok = total depth yang bakal dicari

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

    // cuman buat check remove multiple
    private static bool ShouldRemove(int i)
    {
        return ((i % 2) == 0);
    }


    public Node getTarget(List<Bilangan> bilangan)
    {
        bool isPembagian = false;
        List<int> pembagianIndexes = new List<int>();
        int mainPembagianIndex = 0;
        //check pembagian
        for (int i = 0; i < bilangan.Count; i++)
        {
            string s = bilangan[i].bilangan;
            s.Remove(0, 1);
            int x = int.Parse(s);
            if (bilangan[i].op == "/" && !isPembagian) // buat ambil index pembagian pertama
            {
                isPembagian = true;
                mainPembagianIndex = i;
            }
            else if (x % 2 == 0 && bilangan[i].op != "*") // buat ambil semua bilangna genap yang bukan operator *
            {
                pembagianIndexes.Add(i);
            }
            if (x % 2 != 0 && bilangan[i].op == "/") //buat larang ada pembagian yang ganjil.
            {
                return null;
            }
        }

        string bilGenap = "";
        for (int i = 0; i < pembagianIndexes.Count; i++)
        {
            bilGenap += bilangan[pembagianIndexes[i]].bilangan.ToString() + bilangan[pembagianIndexes[i]].op.ToString() + " | ";
        }



        if (isPembagian) // ini belum di check dia harusnya sama sama genap, pastiin juga operator bagi itu maximal 1 [done saat generate jumlah balok]
        {

            //validate genap > 1
            if (pembagianIndexes.Count <= 1)
            {

                solusi = "";
                return null;
            }

            Debug.Log("Bilangan Genap: " + bilGenap);
            Debug.Log("pembagi: " + bilangan[pembagianIndexes[0]].bilangan + bilangan[pembagianIndexes[0]].op);
            Debug.Log("Main pembagi: " + bilangan[mainPembagianIndex].bilangan + bilangan[mainPembagianIndex].op);


            Debug.Log("ADA BAGI " + pembagianIndexes.Count);
            List<Bilangan> bilanganTemp = new List<Bilangan>(bilangan);
            List<Bilangan> tempSumbagi = new List<Bilangan>();

            solusi += "(" + bilanganTemp[pembagianIndexes[0]].bilangan + bilanganTemp[pembagianIndexes[0]].op;
            Debug.Log("solusi added 1");
            tempSumbagi.Add(bilanganTemp[pembagianIndexes[0]]);
            Debug.Log("sumbagi added 1");


            //dibawah sini sih salahnya
            solusi += " " + bilanganTemp[mainPembagianIndex].bilangan + bilanganTemp[mainPembagianIndex].op + ") ";
            Debug.Log("solusi added 2");
            tempSumbagi.Add(bilanganTemp[mainPembagianIndex]);
            Debug.Log("sumbagi added 2");



            Debug.Log("Remving all index");
            bilanganTemp.RemoveAll(t => (t.bilangan == bilanganTemp[pembagianIndexes[0]].bilangan && t.op == bilanganTemp[pembagianIndexes[0]].op) || (t.bilangan == bilanganTemp[mainPembagianIndex].bilangan && t.op == bilanganTemp[mainPembagianIndex].op));
            //bilanganTemp.RemoveAt(pembagianIndexes[0]);
            //bilanganTemp.Remove(bilanganTemp[pembagianIndexes[0]]);
            //Debug.Log("temp removed 1");
            //bilanganTemp.RemoveAt(mainPembagianIndex);
           // bilanganTemp.Remove(bilanganTemp[mainPembagianIndex]);
            //Debug.Log("temp removed 2");


            //tempSumbagi.Reverse(0, 2);
            Debug.Log(tempSumbagi[0].bilangan + " | " + tempSumbagi[1].bilangan);
            Bilangan newBilBagi = gen.Hitung(tempSumbagi); 
            bilanganTemp.Add(newBilBagi);
           // Debug.LogWarning(bilanganTemp.Count);

            while (bilanganTemp.Count > 1)
            {
                List<Bilangan> tempSum = new List<Bilangan>();
                //Debug.LogWarning("random 1");
                int rand = Random.Range(0, bilanganTemp.Count); //get random index untuk bilangan kedua dihitung
                solusi += "(" + bilanganTemp[rand].bilangan + bilanganTemp[rand].op;
                tempSum.Add(bilanganTemp[rand]);
                bilanganTemp.RemoveAt(rand);
              // Debug.LogWarning("END random 1");
              // Debug.LogWarning("random 2");
                rand = Random.Range(0, bilanganTemp.Count);
                solusi += " " + bilanganTemp[rand].bilangan + bilanganTemp[rand].op + ") ";
                tempSum.Add(bilanganTemp[rand]);
                bilanganTemp.RemoveAt(rand);
               //Debug.LogWarning("END random 2");


                Bilangan newBil = gen.Hitung(tempSum);
                bilanganTemp.Add(newBil);
            }
            if(bilanganTemp[0].op == "/" || bilanganTemp[0].op == "*") // biar ga ada bilangan akhir itu operator
            {
                return null;
            }
            return gen.newNode(bilanganTemp, null, null);
        }
        else
        {
            Debug.Log("NONE BAGI");
            solusi = "";
            List<Bilangan> bilanganTemp = new List<Bilangan>(bilangan);
           // Debug.LogWarning(bilanganTemp.Count);
            while (bilanganTemp.Count > 1)
            {
                List<Bilangan> tempSum = new List<Bilangan>();
               //Debug.LogWarning("random 1");
                int rand = Random.Range(0, bilanganTemp.Count); //get random index untuk bilangan kedua dihitung
                solusi += "(" + bilanganTemp[rand].bilangan + bilanganTemp[rand].op;
                tempSum.Add(bilanganTemp[rand]);
                bilanganTemp.RemoveAt(rand);
              // Debug.LogWarning("END random 1");
               //Debug.LogWarning("random 2");
                rand = Random.Range(0, bilanganTemp.Count);
                solusi += " " + bilanganTemp[rand].bilangan + bilanganTemp[rand].op + ") ";
                tempSum.Add(bilanganTemp[rand]);
                bilanganTemp.RemoveAt(rand);
                //Debug.LogWarning("END random 2");
               //Debug.Log(tempSum[0].bilangan + " | " + tempSum[1].bilangan);
                Bilangan newBil = gen.Hitung(tempSum);
                bilanganTemp.Add(newBil);
               //Debug.LogWarning(bilanganTemp.Count);
            }
            return gen.newNode(bilanganTemp, null, null);
        }
    
    }

    public void GenerateSoal(List<Bilangan> bilangan, Node target)
    {
       
        Debug.LogWarning(solusi);
        gen.generateTarget(target);
        gen.GenerateLife(lifeCount); 
        StartCoroutine(gen.instantiateforSec(bilangan, 0.7f));
    }

}

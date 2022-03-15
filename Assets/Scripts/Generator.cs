using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Generator : MonoBehaviour
{

    static Bilangan newBilangan(string bilangan, string op)
    {
        Bilangan temp = new Bilangan();
        temp.bilangan = bilangan;
        temp.op = op;
        return temp;
    }


    // Utility function to create a new tree node
    static Node newNode(List<Bilangan> listBilangan, string sumber, Node parentNode)
    {
        Node temp = new Node();
        temp.listBilangan = listBilangan;
        temp.sumber = sumber;
        temp.parentNode = parentNode;
        return temp;
    }

    private Bilangan Hitung(List<Bilangan> bilangan)
    {
        float a = float.Parse(bilangan[0].bilangan);
        float b = float.Parse(bilangan[1].bilangan);

        string aOp = bilangan[0].op;
        string bOp = bilangan[1].op;

        if(a.ToString() == "0" || a.ToString() == "0.0" || b.ToString() == "0" || b.ToString() == "0.0")
        {
            return newBilangan("invalid", "invalid");
        }
        else if(bOp == "+")
        {
            if(aOp == "*")
            {
                float hasil = a + b;
                if (hasil >= 0)
                {
                    return newBilangan("+" + hasil.ToString(), "*");
                }
                else
                {
                    return newBilangan(hasil.ToString(), "*");
                }
            }
            else if (aOp == "/")
            {
                float hasil = a + b;
                if (hasil >= 0)
                {
                    return newBilangan("+" + hasil.ToString(), "/");
                }
                else
                {
                    return newBilangan(hasil.ToString(), "/");
                }
            }
            else
            {
                float hasil = a + b;
                if (hasil >= 0)
                {
                    return newBilangan("+" + hasil.ToString(), "+");
                }
                else
                {
                    return newBilangan(hasil.ToString(), "-");
                }
            }
        }
        else if (bOp == "-")
        {
            if (aOp == "*")
            {
                float hasil = a - b;
                if (hasil >= 0)
                {
                    return newBilangan("+" + hasil.ToString(), "*");
                }
                else
                {
                    return newBilangan( hasil.ToString(), "*");
                }
            }
            else if (aOp == "/")
            {
                float hasil = a - b;
                if (hasil >= 0)
                {
                    return newBilangan("+" + hasil.ToString(), "/");
                }
                else
                {
                    return newBilangan(hasil.ToString(), "/");
                }
            }
            else
            {
                float hasil = a - b;
                if (hasil >= 0)
                {
                    return newBilangan("+" + hasil.ToString(), "+");
                }
                else
                {
                    return newBilangan( hasil.ToString(), "-");
                }
            }
        }
        else if (bOp == "*")
        {
            if (aOp == "*")
            {
                float hasil = a * b;
                if (hasil >= 0)
                {
                    return newBilangan("+" + hasil.ToString(), "*");
                }
                else
                {
                    return newBilangan(hasil.ToString(), "*");
                }
            }
            else if (aOp == "/")
            {
                float hasil = a * b;
                if (hasil >= 0)
                {
                    return newBilangan("+" + hasil.ToString(), "/");
                }
                else
                {
                    return newBilangan(hasil.ToString(), "/");
                }
            }
            else
            {
                float hasil = a * b;
                if (hasil >= 0)
                {
                    return newBilangan("+" + hasil.ToString(), "+");
                }
                else
                {
                    return newBilangan(hasil.ToString(), "-");
                }
            }
        }
        else if (bOp == "/")
        {
            if (aOp == "*")
            {
                float hasil = a / b;
                if (hasil >= 0)
                {
                    return newBilangan("+" + hasil.ToString(), "*");
                }
                else
                {
                    return newBilangan( hasil.ToString(), "*");
                }
            }
            else if (aOp == "/")
            {
                float hasil = a / b;
                if (hasil >= 0)
                {
                    return newBilangan("+" + hasil.ToString(), "/");
                }
                else
                {
                    return newBilangan(hasil.ToString(), "/");
                }
            }
            else
            {
                float hasil = a / b;
                if (hasil >= 0)
                {
                    return newBilangan("+" + hasil.ToString(), "+");
                }
                else
                {
                    return newBilangan( hasil.ToString(), "-");
                }
            }
        }
        else
        {
            return newBilangan("invalid", "invalid");
        }

    }

    private bool checkList(List<Bilangan> target, List<List<Bilangan>> num_list)
    {
        string op1 = target[0].op;
        string op2 = target[1].op;
        int sama = 0;

        if (op1 == "/" || op1 == "*" || op2 == "/" || op2 == "*")
        {
            Debug.Log("operator skip- "+target[0].bilangan.ToString() + " . " + target[1].bilangan.ToString());
            return false;
        }
        else
        {
            foreach (List<Bilangan> subList in num_list)
            {
                foreach (Bilangan item in subList)
                {
                    if (item.bilangan == target[0].bilangan || item.bilangan == target[1].bilangan)
                    {
                        Debug.Log(item.bilangan.ToString() + " == " + target[0].bilangan.ToString() + " atau " + item.bilangan.ToString() + " == " + target[1].bilangan.ToString());
                        sama++;
                    }

                }
            }

            Debug.Log("Target count = " + target.Count);
            if (sama == target.Count)
            {
                Debug.Log("COUNT SAMA");
                return true;
            }
            else
            {
                Debug.Log("count tidak pas/belum ada sama");
                return false;
            }
        }
        
        
    }

    private List<List<Bilangan>> getCombination(List<Bilangan> bilangan)
    {
        List<List<Bilangan>> sudah_list = new List<List<Bilangan>>();

        //Debug.Log(bilangan.Count);
        for (int i=0; i<bilangan.Count(); i++)
        {
            for (int j=0; j < bilangan.Count(); j++)
            {
                List<Bilangan> currentList = new List<Bilangan>();
                currentList.Add(bilangan[i]);
                currentList.Add(bilangan[j]);
                Debug.Log(currentList[0].bilangan.ToString() + " . " + currentList[1].bilangan.ToString());
                if (bilangan[i].bilangan == bilangan[j].bilangan)
                {
                    Debug.LogError("SKIPPED SAMA- " + currentList[0].bilangan.ToString() + " . " + currentList[1].bilangan.ToString());
                    continue;
                }
                else if (checkList(currentList, sudah_list))
                {
                    Debug.LogError("SKIPPED- "+currentList[0].bilangan.ToString() + " . " + currentList[1].bilangan.ToString());
                    continue;
                }
                else
                {
                    Debug.LogWarning("ADDED- " + currentList[0].bilangan.ToString() + " . " + currentList[1].bilangan.ToString());
                    sudah_list.Add(currentList);
                    //Debug.Log("SUDAH_LIST VALUE:");
                    //foreach (List<Bilangan> subList in sudah_list)
                    //{
                    //    foreach (Bilangan item in subList)
                    //    {
                    //        Debug.Log(item.bilangan);

                    //    }
                    //}
                    //Debug.Log(sudah_list[0][0].bilangan);
                }
            }
        }

        return sudah_list;
    }

    private void Start()
    {
        List<Bilangan> bilangan = new List<Bilangan>();
        bilangan.Add(newBilangan("+5", "+"));
        bilangan.Add(newBilangan("-3", "-"));
        bilangan.Add(newBilangan("+9", "+"));
        Node root = newNode(bilangan, null, null);

        //bilangan.Clear();
        //bilangan.Add(newBilangan("+5", "+"));
        //bilangan.Add(newBilangan("+6", "+"));
        //(root.child).Add(newNode(bilangan, "a", root));

        //bilangan.Clear();
        //bilangan.Add(newBilangan("+2", "+"));
        //bilangan.Add(newBilangan("+9", "+"));
        //(root.child).Add(newNode(bilangan, "b", root));

        //bilangan.Clear();
        //bilangan.Add(newBilangan("+14", "+"));
        //bilangan.Add(newBilangan("-3", "-"));
        //(root.child).Add(newNode(bilangan, "c", root));

        //List<List<Bilangan>> bilangan = new List<List<Bilangan>>();

        //bilangan.Add(new List<Bilangan> {
        //    newBilangan("+1", "+"),
        //    newBilangan("-2", "+")
        //});
        //bilangan.Add(new List<Bilangan> {
        //    newBilangan("+3", "+"),
        //    newBilangan("-4", "+")
        //});

        //List<Bilangan> bilangan2 = new List<Bilangan>();
        //bilangan2.Add(newBilangan("-2", "+"));
        //bilangan2.Add(newBilangan("-2", "+"));


        //bool hasil = false;

        //foreach (List<Bilangan> subList in bilangan)
        //{
        //    if (bilangan2.Intersect(subList).Any())
        //    {
        //        hasil = true;
        //        break;
        //    }
        //    else
        //    {
        //        hasil = false;
        //    }
        //}

        //Debug.Log(hasil);

        List<List<Bilangan>> kombinasi = new List<List<Bilangan>>();

        kombinasi = getCombination(root.listBilangan);
        Debug.Log(kombinasi.Count);
        for (int i = 0; i < kombinasi.Count; i++)
        {
            Debug.Log(kombinasi[i][0].bilangan);
            Debug.Log(kombinasi[i][1].bilangan);
            Debug.Log("------");
        }
    }
}
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

    private bool Periksa(List<Bilangan> target, List<List<Bilangan>> num_list)
    {
        foreach (List<Bilangan> subList in num_list)
        {
            foreach (Bilangan item in subList)
            {
                Debug.Log(item.bilangan);
            }
        }
        return false;
    }


    private bool checkList(List<Bilangan> target, List<List<Bilangan>> num_list)
    {
        string op1 = target[0].op;
        string op2 = target[1].op;
        if(op1 == "/" || op1 == "*" || op2 == "/" || op2 == "*")
        {
            return false;
        }else if (Periksa(target, num_list))
        {
            return true;
        }
        else
        {
            //Debug.Log(target);
            target.Reverse();
            //Debug.Log(target);
            if (Periksa(target, num_list))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private List<List<Bilangan>> getCombination(List<Bilangan> bilangan)
    {
        List<List<Bilangan>> sudah_list = new List<List<Bilangan>>();
        List<Bilangan> currentList = new List<Bilangan>();

        for (int i=0; i<bilangan.Count(); i++)
        {
            for (int j=0; j < bilangan.Count(); j++)
            {

                currentList.Clear();
                currentList.Add(bilangan[i]);
                currentList.Add(bilangan[j]);

                if (checkList(currentList, sudah_list) || bilangan[i] == bilangan[j])
                {
                    continue;
                }
                else
                {
                    sudah_list.Add(currentList);
                }
            }
        }
        return sudah_list;
    }

    private void Start()
    {
        //List<Bilangan> bilangan = new List<Bilangan>();
        //bilangan.Add(newBilangan("+5", "+"));
        //bilangan.Add(newBilangan("-3", "-"));
        //bilangan.Add(newBilangan("+9", "+"));
        //Node root = newNode(bilangan, null, null);

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
        List<List<Bilangan>> bilangan = new List<List<Bilangan>>();
        List<Bilangan> subbil = new List<Bilangan>();

        subbil.Add(newBilangan("+1", "+"));
        subbil.Add(newBilangan("-2", "+"));

        bilangan.Add(subbil);
        Debug.Log(subbil.Count);

        subbil.Clear();
        Debug.Log(subbil.Count);
        subbil.Add(newBilangan("+3", "+"));
        subbil.Add(newBilangan("+4", "+"));
        bilangan.Add(subbil);
        Debug.Log(bilangan[0][0].bilangan);
        Debug.Log("?????");
        for (int i=0; i<bilangan.Count; i++)
        {
            Debug.Log(bilangan[i][0].bilangan);
            Debug.Log(bilangan[i][1].bilangan);
            Debug.Log("__________");
        }

        //Debug.Log(bilangan[0][0].bilangan);


        //bool hasil;
        //if (checkList(bilangan2, bilangan))
        //{
        //    hasil = true;
        //}
        //else
        //{
        //    hasil = false;
        //}
        //Debug.Log(hasil);

        //List<List<Bilangan>> kombinasi = new List<List<Bilangan>>();

        //kombinasi = getCombination(root.listBilangan);
        //for(int i=0; i<kombinasi.Count; i++)
        //{
        //    Debug.Log(kombinasi[i][0].bilangan);
        //    Debug.Log(kombinasi[i][1].bilangan);
        //}
    }
}
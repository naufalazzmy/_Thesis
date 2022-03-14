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

    public Bilangan Hitung(List<Bilangan> bilangan)
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

    public bool Terdapat(List<Bilangan> target, List<Bilangan> num_list)
    {

        int targetnum = 0;

        for(int i=0; i<num_list.Count; i++)
        {
            if (target.Contains(num_list[i]))
            {
                targetnum++;
            }
        }
        if(targetnum == target.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool checkList(List<Bilangan> target, List<Bilangan> num_list)
    {
        string op1 = target[0].op;
        string op2 = target[1].op;
        if(op1 == "/" || op1 == "*" || op2 == "/" || op2 == "*")
        {
            return false;
        }else if (target.Any(item => num_list.Contains(item)))
        {
            return true;
        }
        else
        {
            Debug.Log(target);
            target.Reverse();
            Debug.Log(target);
            if (target.Any(item => num_list.Contains(item)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private void Start()
    {
        List<Bilangan> bilangan = new List<Bilangan>();
        //bilangan.Add(newBilangan("+5", "+"));
        //bilangan.Add(newBilangan("-3", "-"));
        //bilangan.Add(newBilangan("+9", "+"));
        //Node root = newNode(bilangan, null,null);

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

        bilangan.Add(newBilangan("+1", "+"));
        bilangan.Add(newBilangan("-2", "+"));
        bilangan.Add(newBilangan("+3", "+"));
        bilangan.Add(newBilangan("+4", "+"));

        List<Bilangan> bilangan2 = new List<Bilangan>();
        bilangan2.Add(bilangan[2]);
        bilangan2.Add(bilangan[3]);

        bool hasil;
        if (bilangan.Intersect(bilangan2).Any())
        {
            hasil = true;
        }
        else
        {
            hasil = false;
        }

        Debug.Log(hasil);
    }
}
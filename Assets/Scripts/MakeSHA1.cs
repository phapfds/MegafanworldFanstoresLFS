using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;

public class MakeSHA1 : MonoBehaviour
{
    public static string Make(string originalText)
    {
        SHA1 sha = new SHA1CryptoServiceProvider();
        UTF8Encoding ue = new UTF8Encoding();
        byte[] planeBytes = ue.GetBytes(originalText);
        byte[] hashBytes = sha.ComputeHash(planeBytes);
        return ByteArrayToString(hashBytes);
    }


    public static string ByteArrayToString(byte[] ba)
    {
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
            hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }
}

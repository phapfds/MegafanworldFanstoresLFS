//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using Newtonsoft.Json;
//using FirebaseWebGL.Scripts.FirebaseBridge;
using System;
using System.Text;
using System.Security.Cryptography;

public class TestJson : MonoBehaviour
{

    #region "DRAFT"
    //private void Start()
    //{

    //    //byte[] bytesToEncode = Encoding.UTF8.GetBytes("phap2.ho@fdssoft.com");
    //    //string encodedText = Convert.ToBase64String(bytesToEncode);
    //    //Debug.Log(encodedText);

    //    byte[] decodedBytes = Convert.FromBase64String("cGhhcC5obzJAZmRzc29mdC5jb20-".Replace("-","="));
    //    string decodedText = Encoding.UTF8.GetString(decodedBytes);
    //    Debug.Log(decodedText);


    //}




    //readonly string x = "{\"itemList\":[{\"id\":0,\"itemType\":5,\"nameItem\":\"Shirt\",\"description\":\"This Eintracht Frankfurt Home Shirt for the 2021 season has been developed with Nike Breathe fabric and Dri-Fit technology which delivers fast drying moisture management and cooling breathability. The jersey features the teams iconic crest so you can showcase your support for The Eagles with pride and the Nike Swoosh branding completes the look.\",\"price\":10,\"number\":1},{\"id\":1,\"itemType\":0,\"nameItem\":\"Hat\",\"description\":\"Our casually comfortable baseball caps have all your bases covered. Bad hair day? Early morning coffee run? No problem. You’ll hit a fashion home run with our winning designs. \",\"price\":20,\"number\":1},{\"id\":12,\"itemType\":8,\"nameItem\":\"Shoes\",\"description\":\"Football, as a game, requires an awful lot of running on a specialist field. In order to not damage the field or your feet it's important to have the right shoes.\",\"price\":125,\"number\":1},{\"id\":15,\"itemType\":11,\"nameItem\":\"Couch\",\"description\":\"Unwind on a new living room couch! Shop our sofa and sectional collection by material, colour or function. Discover great prices and fast delivery.\",\"price\":750,\"number\":1},{\"id\":14,\"itemType\":10,\"nameItem\":\"Table\",\"description\":\"Spring has arrived! Let's give our homes a refresh to welcome this new season. Shop our online sale and discover hundreds of items from chic dining furniture to statement sofas to brighten any space. \",\"price\":500,\"number\":1},{\"id\":6,\"itemType\":6,\"nameItem\":\"Wall Painting\",\"description\":\"A mural is any piece of artwork painted or applied directly on a wall, ceiling or other permanent surfaces. A distinguishing characteristic of mural painting is that the architectural elements of the given space are harmoniously incorporated into the picture.\",\"price\":50,\"number\":1},{\"id\":7,\"itemType\":6,\"nameItem\":\"Wall Painting\",\"description\":\"A mural is any piece of artwork painted or applied directly on a wall, ceiling or other permanent surfaces. A distinguishing characteristic of mural painting is that the architectural elements of the given space are harmoniously incorporated into the picture.\",\"price\":50,\"number\":1},{\"id\":8,\"itemType\":6,\"nameItem\":\"Wall Painting\",\"description\":\"A mural is any piece of artwork painted or applied directly on a wall, ceiling or other permanent surfaces. A distinguishing characteristic of mural painting is that the architectural elements of the given space are harmoniously incorporated into the picture.\",\"price\":50,\"number\":1},{\"id\":10,\"itemType\":6,\"nameItem\":\"Wall Painting\",\"description\":\"A mural is any piece of artwork painted or applied directly on a wall, ceiling or other permanent surfaces. A distinguishing characteristic of mural painting is that the architectural elements of the given space are harmoniously incorporated into the picture.\",\"price\":50,\"number\":1},{\"id\":11,\"itemType\":6,\"nameItem\":\"Wall Painting\",\"description\":\"A mural is any piece of artwork painted or applied directly on a wall, ceiling or other permanent surfaces. A distinguishing characteristic of mural painting is that the architectural elements of the given space are harmoniously incorporated into the picture.\",\"price\":50,\"number\":1},{\"id\":16,\"itemType\":6,\"nameItem\":\"Wall Painting\",\"description\":\"A mural is any piece of artwork painted or applied directly on a wall, ceiling or other permanent surfaces. A distinguishing characteristic of mural painting is that the architectural elements of the given space are harmoniously incorporated into the picture.\",\"price\":50,\"number\":1},{\"id\":16,\"itemType\":6,\"nameItem\":\"Wall Painting\",\"description\":\"A mural is any piece of artwork painted or applied directly on a wall, ceiling or other permanent surfaces. A distinguishing characteristic of mural painting is that the architectural elements of the given space are harmoniously incorporated into the picture.\",\"price\":50,\"number\":1},{\"id\":6,\"itemType\":6,\"nameItem\":\"Wall Painting\",\"description\":\"A mural is any piece of artwork painted or applied directly on a wall, ceiling or other permanent surfaces. A distinguishing characteristic of mural painting is that the architectural elements of the given space are harmoniously incorporated into the picture.\",\"price\":50,\"number\":1}]}";

    //private void Awake()
    //{
    //    Debug.Log(x);
    //    FanstoreItemList test = JsonConvert.DeserializeObject<FanstoreItemList>(x);
    //    Debug.Log(test.itemList[0].description);

    //    FirebaseDatabase.GetFanroomData("tKVvcfX5YRbukUZGMxnVtal4QMj1", gameObject.name, "Callback", null);

    //}
    //public void Callback(string data)
    //{
    //    Debug.Log(data);
    //    FanstoreItemList test = JsonConvert.DeserializeObject<FanstoreItemList>(data);
    //    Debug.Log(test.itemList[1].description);

    //    FanstoreItemList test_1 = JsonUtility.FromJson<FanstoreItemList>(data);
    //    Debug.Log(test_1.itemList[2].description);
    //}
    #endregion

    private void Start()
    {
        Debug.Log(MakeSHA1("fdssoft1626254202cGhhcC5ob0BmZHNzb2Z0LmNvbQ--"));
    }

    private void Update()
    {
        long TimesTAMPE = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
        Debug.Log(TimesTAMPE);
    }

    public string MakeSHA1(string originalText)
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

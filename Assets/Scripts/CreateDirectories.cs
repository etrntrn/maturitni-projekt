using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CreateDirectories : MonoBehaviour
{ //VÝJIMKY
    void Start() //vytvoří složky pro úschovu parametrizovaných i výsledných střihů
    {
        string pathAppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Editor_strihu");
        if (!Directory.Exists(pathAppData))
        {
            Debug.Log("1:creating..");
            Directory.CreateDirectory(pathAppData);
        }
        if (Directory.Exists(pathAppData))
            Debug.Log("1:exists = " + pathAppData);

        string pathMyDocuments = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Editor střihů");
        if (!Directory.Exists(pathMyDocuments))
        {
            Debug.Log("2:creating");
            Directory.CreateDirectory(pathMyDocuments);
        }
        if (Directory.Exists(pathMyDocuments))
            Debug.Log("2:exists = " + pathMyDocuments);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class CreateDirectories : MonoBehaviour
{ //VÝJIMKY
    public TMP_Text error;
    public Button novyStrih;
    public Button existujiciStrih;
    void Start() //vytvoří složky pro úschovu parametrizovaných i výsledných střihů
    {
        ErrorOn(false);
        string pathAppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Editor_strihu");
        if (!Directory.Exists(pathAppData))
        {
            try
            {
                Directory.CreateDirectory(pathAppData);
            }
            catch
            {
                ErrorOn(true);
            }
        }
        string database = Path.Combine(pathAppData, "pattern_database.csv");
        if(!File.Exists(database))
            try
            {
                File.Create(database);
            }
            catch
            {
                ErrorOn(true);
            }

        string pathMyDocuments = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Editor střihů");
        if (!Directory.Exists(pathMyDocuments))
        {
            try
            {
                Directory.CreateDirectory(pathMyDocuments);
            }
            catch
            {
                ErrorOn(true);
            }
        }
    }
    public void ErrorOn(bool errorActive)
    {
        if (errorActive == true)
        {
            novyStrih.gameObject.SetActive(false);
            existujiciStrih.gameObject.SetActive(false);
            error.gameObject.SetActive(true);
        }
        else
        {
            novyStrih.gameObject.SetActive(true);
            existujiciStrih.gameObject.SetActive(true);
            error.gameObject.SetActive(false);
        }
    }
}

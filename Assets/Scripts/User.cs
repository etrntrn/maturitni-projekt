using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class User : MonoBehaviour
{
    public int obvodPasu;
    public int obvodHrudniku;
    public int obvodSedu;
    public int delkaZad;
    public int delkaOdevu;
    public int sirkaZad;
    public int sirkaRamene;

    public void AssignData(int[] array)
    {
        obvodPasu = array[0];
        obvodHrudniku = array[1];
        obvodSedu = array[2];
        delkaZad = array[3];
        delkaOdevu = array[4];
        sirkaZad = array[5];
        sirkaRamene = array[6];
    }

    public int[] DataToArray()
    {
        int[] array = new int[7];
        array[0] = obvodPasu;
        array[1] = obvodHrudniku;
        array[2] = obvodSedu;
        array[3] = delkaZad;
        array[4] = delkaOdevu;
        array[5] = sirkaZad;
        array[6] = sirkaRamene;
        return array;
    }

    public string GetFileName()
    {
        string s = "";
        string tmpFilePath = CompleteFilePath(false, "txt", "tmp_currentName");
        if (File.Exists(tmpFilePath))
        {
            using (StreamReader sr = new StreamReader(tmpFilePath))
            {
                s = sr.ReadLine();
            }
            if (s == null)
            {
                s = "error";
                Debug.Log("jméno je prázdné - set error");
            }
        }
        else
        {
            s = "error";
            Debug.Log("soubor neexistuje - set error");
        }
        return s;
    }

    public string CompleteFilePath(bool inDocuments, string fileType, string fileName)
    {
        string folderPath = "";
        if (!inDocuments)
        {
            folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Editor_strihu");
        }
        else
        {
            folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Editor střihů");
        }
        switch (fileType)
        {
            case "csv":
                {
                    fileName += ".csv";
                    break;
                }
            case "txt":
                {
                    fileName += ".txt";
                    break;
                }
            case "svg":
                {
                    fileName += ".svg";
                    break;
                }
        }
        string completePath = Path.Combine(folderPath, fileName);
        return completePath;
    }

    public void SaveData()
    {
        string patName = GetFileName();
        if (patName == "error")
        {
            SceneManager.LoadScene("GeneralError");
        }
        else
        {
            string userMeasuresFile = CompleteFilePath(false, "csv", patName);
            int[] array = DataToArray();
            using (StreamWriter sw = new StreamWriter(userMeasuresFile, false))
            {
                foreach (int i in array)
                {
                    sw.Write(i + ";");
                }
                sw.Flush();
            }
        }
    }

    public int[] NumberTesting(string[] s)
    {
        int[] numbers = new int[7];
        for (int i = 0; i < numbers.Length; i++)
        {
            int.TryParse(s[i], out numbers[i]);
        }
        return numbers;
    }

    public void ReadData()
    {
        string patName = GetFileName();
        if (patName == "error")
        {
            SceneManager.LoadScene("GeneralError");
        }
        else
        {
            string path = CompleteFilePath(false, "csv", patName);
            string s = "";
            using (StreamReader sr = new StreamReader(path))
            {
                s = sr.ReadLine();
                string[] split = s.Split(';');
                int[] numbers = NumberTesting(split);
                AssignData(numbers);
            }
        }
    }
}

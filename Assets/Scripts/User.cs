using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
        //ošetřit field.Lenght != 9
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

    public string GetFileName() // VÝJIMKY
    {
        string s = "";
        string pathAppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Editor_strihu"); //try catch
        string AppDataFile = Path.Combine(pathAppDataFolder, @"tmp_currentName.txt");
        string tmpFilePath = CompleteFilePath(false, "txt", "tmp_currentName");

        using (StreamReader sr = new StreamReader(tmpFilePath))
        {
            s = sr.ReadLine();
        }
        if (s == null)
        {
            s = " ";
            Debug.Log("jméno je prázdné");
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
        string userMeasuresFile = CompleteFilePath(false, "csv", GetFileName());
        int[] field = DataToArray();

        using (StreamWriter sw = new StreamWriter(userMeasuresFile, false))
        {
            foreach (int i in field)
            {
                sw.Write(i + ";");
            }
            sw.Flush();
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
        string path = CompleteFilePath(false, "csv", GetFileName()); 
        string s = "";
        using (StreamReader sr = new StreamReader(path))
        {
            s = sr.ReadLine();
            //while ((s = sr.ReadLine())) != null) //bacha
            //{
                string[] split = s.Split(';');
            //}
            int[] numbers = NumberTesting(split);
            AssignData(numbers);
        }
    }
}

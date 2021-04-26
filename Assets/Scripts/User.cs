using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class User : MonoBehaviour
{
    public int obvodPasu;
    public int vyskaPostavy;
    public int obvodHrudniku;
    public int obvodSedu;
    public int delkaZad;
    public int delkaOdevu;
    public int sirkaZad;
    public int sirkaRamene;
    public int delkaRukavu;
    public string umisteni;

    public void AssignData(int[] field)
    {
        //ošetřit field.Lenght != 9
        obvodPasu = field[0];
        vyskaPostavy = field[1];
        obvodHrudniku = field[2];
        obvodSedu = field[3];
        delkaZad = field[4];
        delkaOdevu = field[5];
        sirkaZad = field[6];
        sirkaRamene = field[7];
        delkaRukavu = field[8];
    }

    public int[] DataToField()
    {
        int[] field = new int[9];
        field[0] = obvodPasu;
        field[1] = vyskaPostavy;
        field[2] = obvodHrudniku;
        field[3] = obvodSedu;
        field[4] = delkaZad;
        field[5] = delkaOdevu;
        field[6] = sirkaZad;
        field[7] = sirkaRamene;
        field[8] = delkaRukavu;
        return field;
    }

    public string GetFileName() // VÝJIMKY
    {
        string s = "";
        string pathAppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Editor_strihu"); //try catch
        string AppDataFile = Path.Combine(pathAppDataFolder, @"tmp_currentName.txt");
        Debug.Log(AppDataFile);
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
        int[] field = DataToField();

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
        int[] numbers = new int[9];
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

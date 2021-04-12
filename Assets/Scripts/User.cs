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

    public void SaveData()
    {
        string umisteni = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Editor_strihu");
        if (!Directory.Exists(umisteni))
            Directory.CreateDirectory(umisteni);

        int[] field = DataToField();
        //using (StreamWriter sw = new StreamWriter(Path.Combine(umisteni, @"user_tmp.csv"), false))
        using (StreamWriter sw = new StreamWriter(@"user_tmp.csv", false))
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
        string s = "";
        using (StreamReader sr = new StreamReader(@"user_tmp.csv"))
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System;

public class DataTreatment : MonoBehaviour
{
    User user;

    public TMP_InputField inOp;
    //public TMP_InputField inVp;
    public TMP_InputField inOh;
    public TMP_InputField inOs;
    public TMP_InputField inDz;
    public TMP_InputField inDo;
    public TMP_InputField inSz;
    public TMP_InputField inSr;
    //public TMP_InputField inDr;
    public string tmp;
    public int tmp2;
    public string textFromInput;

    
    void Start()
    {
        user = gameObject.AddComponent<User>();
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Measures")
        {
            TMP_InputField[] inpFields = { inOp, inOh, inOs, inDz, inDo, inSz, inSr };
            DisplayMeasures();
            TurnRed(inpFields);
        }

    }
    public void TurnRed(TMP_InputField[] inpFields) //pole, do niž uživatel zadal nevhodné hodnoty, zčervenají
    {
        
        Image[] backgrounds = new Image[inpFields.Length];
        int i = 0;
        foreach (TMP_InputField inp in inpFields)
        {
            backgrounds[i] = inp.image;
            i++;
        }
        foreach (TMP_InputField inp in inpFields)
        {
            if (inp.text == "0")
            {
                Image inpColor = inp.gameObject.GetComponent<Image>();
                inpColor.color = Color.red;
            }
        }
    }

    public void DisplayMeasures() //zobrazí hodnoty ze souboru v inputFields
    {
        User user = new User();
        string currentFileName = user.GetFileName();
        if (currentFileName == "error")
        {
            SceneManager.LoadScene("GeneralError");
        }
        else
        {
            string path = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Editor_strihu"));
            string fileCSV = currentFileName + ".csv";
            string file = Path.Combine(path, fileCSV);
            string[] stringMeasures = new string[7];
            for (int i = 0; i < stringMeasures.Length; i++)
                stringMeasures[i] = "0";
            if (File.Exists(file))
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string s = sr.ReadLine();
                    if (s != null)
                    {
                        stringMeasures = s.Split(';');
                        Array.Resize(ref stringMeasures, stringMeasures.Length - 1);
                    }
                }
                if (stringMeasures.Length == 7)
                {
                    inOp.text = stringMeasures[0];
                    inOh.text = stringMeasures[1];
                    inOs.text = stringMeasures[2];
                    inDz.text = stringMeasures[3];
                    inDo.text = stringMeasures[4];
                    inSz.text = stringMeasures[5];
                    inSr.text = stringMeasures[6];
                }
                else
                {
                    PatternDatabase patDat = new PatternDatabase();
                    patDat.RemovePatternByName(currentFileName);
                    SceneManager.LoadScene("GeneralError");
                }
            }
        }
    }

    public void NoteUserMeasures() //kontroluje a následně zapisuje míry
    {
        TMP_InputField[] inpFields = { inOp, inOh, inOs, inDz, inDo, inSz, inSr }; 
        int[] limits = { 110, 110, 110, 45, 110, 45, 20 };
        int[] measurements = new int[7];
        for (int i = 0; i < measurements.Length; i++)
        {
            measurements[i] = TestingManager(inpFields[i]);
            measurements[i] = TooLongTooShort(measurements[i], limits[i]);
        }
        user.AssignData(measurements);

        /*user.obvodPasu = TestingManager(inOp);
        user.obvodHrudniku = TestingManager(inOh);
        user.obvodSedu = TestingManager(inOs);
        user.delkaZad = TestingManager(inDz);
        user.delkaOdevu = TestingManager(inDo);
        user.sirkaZad = TestingManager(inSz);
        user.sirkaRamene = TestingManager(inSr);*/

        bool suitableInput = (user.obvodPasu > 0 & user.obvodHrudniku > 0 & user.obvodSedu > 0 & user.delkaZad > 0 & user.delkaOdevu > 0 & user.sirkaZad > 0 & user.sirkaRamene > 0);
        //& user.obvodPasu <= 110 & user.obvodHrudniku <= 110 & user.obvodSedu <= 110 & user.delkaZad <= 45 & user.delkaOdevu <= 110 & user.sirkaZad <= 45 & user.sirkaRamene <= 20);

        if (!suitableInput)
        {
            user.SaveData();
            SceneManager.LoadScene("InputErrorScene");
        }
        else
        {
            user.SaveData();
            SceneManager.LoadScene("DrawingScene");
        }
    }

    public int TooLongTooShort(int number, int limit)
    {
        if (number > limit)
            number = 0;
        if (number < 0)
            number = 0;
        return number;
    }
    
    public int TestingManager(TMP_InputField inpField)
    {
        tmp = GetText(inpField);
        tmp2 = InputNumberTesting(tmp);
        return tmp2;
    }

    public string GetText(TMP_InputField inputField)
    {
        textFromInput = inputField.text;
        return textFromInput;
    }

    public int InputNumberTesting(string textFromInput)
    {
        int.TryParse(textFromInput, out int number);
        return number;
    }
}

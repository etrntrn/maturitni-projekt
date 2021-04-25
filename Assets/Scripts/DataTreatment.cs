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
    public TMP_InputField inVp;
    public TMP_InputField inOh;
    public TMP_InputField inOs;
    public TMP_InputField inDz;
    public TMP_InputField inDo;
    public TMP_InputField inSz;
    public TMP_InputField inSr;
    public TMP_InputField inDr;

    public string tmp;
    public int tmp2;
    public string textFromInput;

    // Start is called before the first frame update
    void Start()
    {
         user = gameObject.AddComponent<User>(); //*
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Measures")
            DisplayMeasures();
    }

    public void DisplayMeasures()
    {
        PatternDrawing drawing = new PatternDrawing();
        User user = new User();
        string currentFileName = drawing.GetFileName(user);
        CurrentName patternName = new CurrentName();
        string path = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Editor_strihu"));
        string fileCSV = currentFileName + ".csv";
        string file = Path.Combine(path, fileCSV);
        string[] stringMeasures = new string[9];
        for (int i = 0; i < stringMeasures.Length; i++)
            stringMeasures[i]  = "0";
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
            inOp.text = stringMeasures[0];
            inVp.text = stringMeasures[1];
            inOh.text = stringMeasures[2];
            inOs.text = stringMeasures[3];
            inDz.text = stringMeasures[4];
            inDo.text = stringMeasures[5];
            inSz.text = stringMeasures[6];
            inSr.text = stringMeasures[7];
            inDr.text = stringMeasures[8];
        }
    }

    public void NoteUserMeasures()
    {
        user.obvodPasu = TestingManager(inOp);
        user.vyskaPostavy = TestingManager(inVp);
        user.obvodHrudniku = TestingManager(inOh);
        user.obvodSedu = TestingManager(inOs);
        user.delkaZad = TestingManager(inDz);
        user.delkaOdevu = TestingManager(inDo);
        user.sirkaZad = TestingManager(inSz);
        user.sirkaRamene = TestingManager(inSr);
        user.delkaRukavu = TestingManager(inDr);
        
        if (user.obvodPasu <= 0 || user.vyskaPostavy <= 0 || user.obvodHrudniku <= 0 || user.obvodSedu <= 0 || user.delkaZad <= 0 || user.delkaOdevu <= 0 || user.sirkaZad <= 0 || user.sirkaRamene <= 0 || user.delkaRukavu <= 0)
        {
            SceneManager.LoadScene("InputErrorScene");
        }
        else
        {
            user.SaveData();
            SceneManager.LoadScene("DrawingScene");
        }
    }

    public void ZkusitZnovuButton()
    {
        SceneManager.LoadScene("Measures");
    }
    
    public int TestingManager(TMP_InputField inpField)
    {
        tmp = GetText(inpField);
        tmp2 = InputNumberTesting(tmp);
        return tmp2;
    }

    public string GetText(TMP_InputField inputField)
    {
        //if (inputField.GetComponent<Text>().text != null)
        //{
        //textFromInput = inputField.GetComponent<Text>().text;
        textFromInput = inputField.text;
        return textFromInput;
        //}
        //else return "0";
    }

    public int InputNumberTesting(string textFromInput)
    {
        int.TryParse(textFromInput, out int number);
        return number;
    }
}

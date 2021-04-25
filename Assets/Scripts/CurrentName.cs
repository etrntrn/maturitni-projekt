using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class CurrentName : MonoBehaviour

{
    public string currentPatternName;
    public TMP_InputField inpField;
    private string pathAppData;
    public TMP_Text errorText;
    public TMP_Text errorText2;
    public PatternDatabase patDat;

   public void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "PatternName")
        {
            errorText.gameObject.SetActive(false);
            errorText2.gameObject.SetActive(false);
        }
        User user = new User();
        pathAppData = user.CompleteFilePath(false, "txt", "tmp_currentName");
    }

    public void SaveName()
    {
        patDat = new PatternDatabase();
        currentPatternName = inpField.text;
        bool nameTaken = NameExists(currentPatternName);
        bool isSuitableName = currentPatternName.All(Char.IsLetterOrDigit) && (currentPatternName.Length > 5) && (currentPatternName.Length < 41);
        switch (isSuitableName)
        {
            case false :
                {
                    inpField.text = "";
                    errorText.gameObject.SetActive(true);
                    break;
                }
            case true:
                {
                    switch(nameTaken)
                    {
                        case true:
                            {
                                inpField.text = "";
                                errorText2.gameObject.SetActive(true);
                                break;
                            }
                        case false:
                            {
                                errorText.gameObject.SetActive(false);
                                errorText2.gameObject.SetActive(false);

                                using (StreamWriter sw = new StreamWriter(pathAppData, false))
                                {
                                    sw.Write(currentPatternName);
                                    sw.Flush();
                                }
                                patDat.AddPatternToFile(currentPatternName);
                                SceneManager.LoadScene("Measures");
                                break;
                            }
                    }
                    break; 
                }
        }
        
    }
    public void PatternNameToFile(string currentPatternName)
    {
        User user = new User();
        pathAppData = user.CompleteFilePath(false, "txt", "tmp_currentName");
        using (StreamWriter sw = new StreamWriter(pathAppData, false))
        {
            sw.Write(currentPatternName);
            sw.Flush();
        }
    }

    public bool NameExists(string namePattern)
    {
        PatternDatabase patDat = new PatternDatabase();
        patDat.PatternNames = patDat.PatternNamesToList();
        string[] array = patDat.PatternNames.ToArray();
        bool errorActive = false;
        foreach (string s in array)
        {
            if (s == namePattern)
                errorActive = true;
        }
        return errorActive;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class PatternDatabase : MonoBehaviour
{
    public string pathPatternNamesList;
    public List<string> PatternNames;
    public TMP_Dropdown dropdown;
    public string databaseFileName;
    public TMP_Text error;
    public bool fileAccessible;
    User user;
    public TMP_Text sceneTitle;
    public Button continueButton;

    public List<string> PatternNamesToList() ///načte data (názvy vytvořených střihů) z csv souboru do listu
    {
        List<string> list1 = new List<string>();
        user = new User();
        using (StreamReader sr = new StreamReader(user.CompleteFilePath(false, "csv", "pattern_database")))
        {
            string s = sr.ReadLine();
            if (s != null) //
            {
                string[] split = s.Split(';');
                Array.Resize(ref split, split.Length - 1);
                foreach (string a in split)
                {
                    list1.Add(a);
                }
            }
        }
        return list1;
    }

    public void AddPatternToFile(string patternName) //připíše do databáze střihů název nového střihu
    {
        PatternNames = PatternNamesToList();
        PatternNames.Add(patternName);
        string[] array = PatternNames.ToArray();
        user = new User();
        //user = gameObject.AddComponent<User>();
        using (StreamWriter sw = new StreamWriter(user.CompleteFilePath(false, "csv", "pattern_database"), false))
        {
            foreach (string name in array)
                sw.Write(name + ';');
            sw.Flush();
        }
    }
    public void MoveScene() //zjišťuje, zda soubor je soubor s daným názvem opravdu k dipozici
    {
        string option = PatternNames[dropdown.value];
        CurrentName patternName = new CurrentName();
        string path = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Editor_strihu"));
        string optionCSV = option + ".csv";
        string file = Path.Combine(path, optionCSV);
        if (File.Exists(file))
        {
            error.gameObject.SetActive(false);
            patternName.PatternNameToFile(option);
            fileAccessible = true;
        }
        else
        {
            error.gameObject.SetActive(true);
            RemovePattern(dropdown.value);
            fileAccessible = false;
        }
    }
    public void RemovePattern(int patIndex) //odstraní z databáze střih, jehož soubor nelze dohledat
    {
        PatternNames = PatternNamesToList();
        PatternNames.RemoveAt(patIndex);
        string[] array = PatternNames.ToArray();
        user = new User();
        using (StreamWriter sw = new StreamWriter(user.CompleteFilePath(false, "csv", "pattern_database"), false))
        {
            foreach (string name in array)
                sw.Write(name + ';');
            sw.Flush();
        }
        PatternNames = PatternNamesToList();
        dropdown.ClearOptions();
        dropdown.AddOptions(PatternNames);
    }

    public void DropdownSelected() //pokud metoda MoveScene potvrdí existenci souboru střihu, načte jeho míry ve scéně Measures
    {
        if (dropdown.value == 0) //pokud uživatel nezmění výběr v dropdownu (chce načíst první střih v seznamu), neuplatní se metoda MoveScene (je na onValueChanged)
        {
            string option0 = dropdown.options[0].text;
            string path = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Editor_strihu"));
            string optionCSV = option0 + ".csv";
            string file = Path.Combine(path, optionCSV);
            if (File.Exists(file))
            {
                error.gameObject.SetActive(false);
                CurrentName patternName = new CurrentName();
                patternName.PatternNameToFile(option0);
                SceneManager.LoadScene("Measures");
            }
            else
            {
                error.gameObject.SetActive(true);
                RemovePattern(dropdown.value);
            }
        }
        else
        {
            if (fileAccessible)
                SceneManager.LoadScene("Measures");
        }
    }

    void Start()
    {
        error.gameObject.SetActive(false);
        user = new User();
        databaseFileName = "pattern_database";
        pathPatternNamesList = user.CompleteFilePath(false, "csv", databaseFileName);

        PatternNames = PatternNamesToList();
        if (PatternNames.Count == 0)
        {
            sceneTitle.text = "Vypadá to, že ještě nemáte žádné střihy.";
            dropdown.gameObject.SetActive(false);
            continueButton.gameObject.SetActive(false);
        }
        else
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(PatternNames);
        }
    }
}

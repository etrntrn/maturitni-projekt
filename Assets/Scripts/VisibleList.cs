using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class VisibleList : MonoBehaviour
{
    public TMP_Dropdown dropOdebrat;
    public TMP_Dropdown dropPridat;
    List<string> lineNames;
    List<string> visibleLineNames;
    List<string> invisibleLineNames;

    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Checkbox")
        {
            dropOdebrat.ClearOptions();
            dropPridat.ClearOptions();
            visibleLineNames = CreateVisibleList();
            invisibleLineNames = new List<string>();
            dropOdebrat.AddOptions(visibleLineNames);
        }
    }

    public List<string> CreateVisibleList()
    {
        Instructions instr = new Instructions();
        List<Line> completeList = instr.BasicInstructions();
        List<string> lineNames = new List<string>();
        foreach (Line line in completeList)
        {
            string tmp = line.pointsList[0] + "-" + line.pointsList[1];
            lineNames.Add(tmp);
        }
        return lineNames;
    }

    public void ButtonPridat()
    {
        string selected = invisibleLineNames[dropPridat.value];
        invisibleLineNames.Remove(selected);
        visibleLineNames.Add(selected);
        dropOdebrat.ClearOptions();
        dropPridat.ClearOptions();
        dropOdebrat.AddOptions(visibleLineNames);
        dropPridat.AddOptions(invisibleLineNames);
    }

    public void ButtonOdebrat()
    {
        string selected = visibleLineNames[dropOdebrat.value];
        visibleLineNames.Remove(selected);
        invisibleLineNames.Add(selected);
        dropOdebrat.ClearOptions();
        dropPridat.ClearOptions();
        dropOdebrat.AddOptions(visibleLineNames);
        dropPridat.AddOptions(invisibleLineNames);
    }

    public void VisibleListToFile()
    { 
        string[] array = visibleLineNames.ToArray();
        User user = new User();
        using (StreamWriter sw = new StreamWriter(user.CompleteFilePath(false, "txt", "visible"), false))
        {
            foreach (string name in array)
                sw.WriteLine(name);
            sw.Flush();
        }
    }

    public List<Line> ReadList()
    {
        Instructions instr = new Instructions();
        List<Line> lines = instr.BasicInstructions();
        User user = new User();
        foreach (Line line in lines)
        {
            line.visible = false;
        }
        using (StreamReader sr = new StreamReader(user.CompleteFilePath(false, "txt", "visible")))
        {
            string[] alldata = File.ReadAllLines(user.CompleteFilePath(false, "txt", "visible"));

            foreach (string data in alldata)
            {
                string s = sr.ReadLine();
                if (s != null)
                {
                    string[] split = s.Split('-');
                    foreach (Line line in lines)
                    {
                        Debug.Log("working");
                        if (line.pointsList[0] == split[0] && line.pointsList[1] == split[1])
                            line.visible = true;
                    }
                }
            }
        }
        return lines;
    }
}

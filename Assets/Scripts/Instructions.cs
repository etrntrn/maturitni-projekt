using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    public List<Line> BasicInstructions()
    {
        Line lSettings = new Line();
        List<Line> basicList = new List<Line>();

        string[,] linearBezierList = new string[,] { { "bodA", "bodD" }, { "bodD", "bodX" }, { "bodB", "bodC" }, { "bodD", "bodE" },
            { "bodF", "bodG" }, { "bodC", "bodE" }, { "bodE", "bodG" }, { "bodG", "bodI" }, {"bodH", "bodI" }, { "bodH", "bodX" },
            {"bodJ", "bodK" }, {"bodN", "bodO" },
            {"bodZ", "bodR" }, {"bodQ", "bodT"}, { "bodR", "bodS2" }, {"bodS2", "bodS" }, {"bodS", "bodT" }, {"bodP", "bodU"},
            {"bodT", "bodU1" }, {"bodU1", "bodN" }, {"bodY", "bodXXX" },  {"bodW", "bodV" }, {"bodV", "bodPR" },
            {"zasevekA1", "zasevekA3" }, {"zasevekA3", "zasevekA2" }, {"zasevekA2", "zasevekA4"}, {"zasevekA4", "zasevekA1"},
            {"bodL1", "bodM" }, {"bodM", "bodL2"},
            {"bodXXX", "bodNO" }, {"bodNO", "bodW"},
            {"zasevekB1", "zasevekB3" }, {"zasevekB3", "zasevekB2"}, {"zasevekB2", "zasevekB4"}, {"zasevekB4", "zasevekB1"}
        };
        AddArrayToInstructionsList(basicList, linearBezierList, 2, "linearBezier");

        string[,] quadraticBezierList = new string[,] { { "bodC", "bodK", "pomocneC" }, { "bodA", "bodJ", "pomocneJ"}, { "bodZ", "bodY", "bodPomocneYZ"}, {"bodPR", "bodN", "bodHH" } };
        AddArrayToInstructionsList(basicList, quadraticBezierList, 3, "quadraticBezier");
        return basicList;

    }

    public void AddArrayToInstructionsList(List<Line> list, string[,] array, int linePointsNum, string lineType)
    {
        Line lSettings = new Line();
        int y = 0;
        for (int x = 0; x < (array.Length / linePointsNum); x++)
        {
            if (linePointsNum == 2)
                list.Add(lSettings.CreateLine(lineType, new List<string> { array[x, y], array[x, (y + 1)] }, true));
            if (linePointsNum == 3)
            {
                list.Add(lSettings.CreateLine(lineType, new List<string> { array[x, y], array[x, (y + 1)], array[x, (y + 2)] }, true));
            }
        }
    }

    public List<Line> CreateInstructions()
    {
        List<string> pNames = new List<string>{ "bodA", "bodD" };
        string lType = "linearBezier";
        bool vis = true;
        List<string> pNames2 = new List<string> { "bodA", "bodC" };
        Line line = new Line();
        Line line2 = line.CreateLine(lType, pNames, vis);
        Line line3 = line.CreateLine(lType, pNames2, vis);
        List<Line> lines = new List<Line>();
        lines.Add(line2);
        lines.Add(line3);
        return lines;
    }
}

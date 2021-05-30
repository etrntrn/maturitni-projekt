using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class PatternDrawing : MonoBehaviour
{
    LineRenderer lr;
    static int numberPoints = 200;
    Vector3[] bezierPositions = new Vector3[numberPoints];

    void Start()
    {
        Instructions instr = new Instructions();
        List<Line> toDrawList = instr.BasicInstructions();
        foreach(Line line in toDrawList)
        {
            DrawVector(line);
        }
        CreateSVGPattern(toDrawList);
    }

    
    public Dictionary<string, int> Split (char XY)
    {
        PointsCalculation pCal = new PointsCalculation();
        User user = new User();
        Dictionary<string, Vector3> vectors = pCal.Calculation(user);
        ICollection<string> vectorStrings = vectors.Keys;
        string[] vectorStringArray = new string[vectorStrings.Count];
        vectorStrings.CopyTo(vectorStringArray, 0);
        ICollection<Vector3> vectorValues = vectors.Values;
        Dictionary<string, int> newDict = new Dictionary<string, int>();
        int value = -1;
        int tmp = 0;
        foreach (Vector3 vectorValue in vectorValues)
        {
            if (XY == 'x')
                value = (int)vectorValue.x;
            if (XY == 'y')
            {
                value = (int)vectorValue.y;
                value = (int)vectors["rozmeryPlatna"].y - value;
                if (value == 0) // rozmeryPlatna.y - rozmeryPlatna.y = 0 -> v SVG by pak byla vyska obrazku 0
                {
                    value = (int)vectors["rozmeryPlatna"].y;
                }
            }
            newDict.Add(vectorStringArray[tmp], value);
            tmp++;
        }
        tmp = 0;
        return newDict;
    }

    void DrawVector(Line line)
    {
        if (line.lineType == "linearBezier" && line.pointsList.Count == 2)
        {
            List<Vector3> vectors = line.pointsValues(line.pointsList);
            DrawLinearBezierCurve(vectors[0], vectors[1]);
        }
        if (line.lineType == "quadraticBezier" && line.pointsList.Count == 3)
        {
            List<Vector3> vectors = line.pointsValues(line.pointsList);
            DrawQuadraticBezierCurve(vectors[0], vectors[2], vectors[1]);
        }
    }

    void SVGLinearLine(StreamWriter sw, Line line)
    {
        Dictionary<string, int> valuesX = Split('x');
        Dictionary<string, int> valuesY = Split('y');
        string bod1 = line.pointsList[0];
        string bod2 = line.pointsList[1];
        string lineName = bod1 + "-" + bod2;
        string write = "<path id=\"{0}\" d=\"M {1} {2} L {3} {4}\" stroke=\"black\" stroke-width=\"1\" stroke-dasharray=\"0\"  fill =\"none\" />";
        sw.WriteLine(write, lineName, valuesX[bod1], valuesY[bod1], valuesX[bod2], valuesY[bod2]);
    }

    void SVGBezierLine(StreamWriter sw, Line line)
    {
        Dictionary<string, int> valuesX = Split('x');
        Dictionary<string, int> valuesY = Split('y');
        string bod1 = line.pointsList[0];
        string bod2 = line.pointsList[1];
        string bod3 = line.pointsList[2];
        string lineName = bod1 + "-" + bod2;
        string write = "<path id=\"{0}\" d=\"M {1} {2} Q {3} {4} {5} {6}\" stroke=\"black\" stroke-width=\"1\"  fill =\"none\" />";
        sw.WriteLine(write, lineName, valuesX[bod1], valuesY[bod1], valuesX[bod3], valuesY[bod3], valuesX[bod2], valuesY[bod2]);
    }

    void CreateSVGPattern(List<Line> toDrawList)
    {
        Dictionary<string, int> valuesX = Split('x');
        Dictionary<string, int> valuesY = Split('y');
        
        User user = new User();
        string patName = user.GetFileName();
        if (patName != "error")
        {
            string pathSavePattern = user.CompleteFilePath(true, "svg", patName);
            using (StreamWriter sw = new StreamWriter(pathSavePattern, false))
            {
                sw.WriteLine("<svg height=\"{0}\" width=\"{1}\" xmlns=\"http://www.w3.org/2000/svg\">", valuesY["rozmeryPlatna"] * 38, valuesX["rozmeryPlatna"] * 38);
                sw.WriteLine("<g transform =\"scale(37.8)\">");
                sw.WriteLine("<rect x=\"5\" y=\"5\" width=\"10\" height=\"10\" stroke=\"grey\" fill =\"none\"/>"); //čtverec 10x10

                foreach (Line line in toDrawList)
                {
                    if (line.lineType == "linearBezier" && line.pointsList.Count == 2 && line.visible == true)
                    {
                        SVGLinearLine(sw, line);
                    }
                    if (line.lineType == "quadraticBezier" && line.pointsList.Count == 3 && line.visible == true)
                    {
                        SVGBezierLine(sw, line);
                    }
                }
                sw.WriteLine("</g>");
                sw.WriteLine("</svg>");
                sw.Flush();
            }
        }
        else
        {
            SceneManager.LoadScene("GeneralError");
        }
    }

    LineRenderer LineRendererCreater()
    {
        lr = new GameObject().AddComponent<LineRenderer>();
        lr.gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        lr.SetWidth(0.4f, 0.4f); //nastaví tloušťku čáry
        return lr;
    }

    void DrawLine(Vector3[] positions)
    {
        LineRenderer line = LineRendererCreater();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.positionCount = positions.Length;
        line.SetPositions(positions);
    }

    void DrawQuadraticBezierCurve(Vector3 point1, Vector3 point2, Vector3 point3) // Napsáno podle návodu na theappguruz.com, viz dokumentace
    {
        for (int i = 1; i < numberPoints + 1; i++)
        {
            float t = i / (float)numberPoints;
            bezierPositions[i - 1] = CalculateQuadraticBezierPoint(t, point1, point2, point3);
        }
        DrawLine(bezierPositions);
    }

    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 point1, Vector3 point2, Vector3 point3) // Napsáno podle návodu na theappguruz.com, viz dokumentace
    {
        float z = 1 - t;
        float t2 = t * t;
        float z2 = z * z;
        Vector3 bezierPoint = (z2 * point1) + (2 * z * t * point2) + (t2 * point3);
        return bezierPoint;
    }


    void DrawLinearBezierCurve(Vector3 point1, Vector3 point2) // Napsáno podle návodu na theappguruz.com, viz dokumentace
    {
        for (int i = 0; i < numberPoints ; i++)
        {
            float t = i / (float)numberPoints;
            bezierPositions[i] = CalculateLinearBezierPoint(t, point1, point2);
        }
        DrawLine(bezierPositions);
    }

    Vector3 CalculateLinearBezierPoint(float t, Vector3 point1, Vector3 point2) // Napsáno podle návodu na theappguruz.com, viz dokumentace
    {
        Vector3 result = point1 + t * (point2 - point1);
        return result;
    }
}

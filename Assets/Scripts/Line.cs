using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public string lineType;
    public List<string> pointsList;
    public bool visible;
    
    public Line CreateLine(string lineType1, List<string> pointsList1, bool visible1)
    {
        Line line = new Line();
        line.lineType = lineType1;
        line.pointsList = pointsList1;
        line.visible = visible1;
        return line;
    }

    public List<Vector3> pointsValues(List<string> pointsList)
    {
        User user = new User();
        List<Vector3> vectorList = new List<Vector3>();
        PointsCalculation pCal = new PointsCalculation();
        Dictionary<string, Vector3> dict = pCal.Calculation(user);
        for (int i = 0; i < pointsList.Count; i++)
        {
            string tmp = pointsList[i];
            vectorList.Add(dict[tmp]);
        }
        return vectorList;
    }
}

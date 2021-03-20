using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;

public class PatternDrawing : MonoBehaviour
{
    // Údaje od uživatele + pomocné míry

    private LineRenderer line;
    static float vyskaPostavy = 168;
    static float obvodHrudnikuX = 96;
    
    static float obvodPasuX = 76;
    
    static float obvodSeduX = 100f;
    
    static float delkaZad = 41;
    static float delkaOdevu = 90;
    static float sirkaZadX = 35;
    
    static float sirkaRamene = 13;
    static float delkaRukavu = 60;

    static float obvodHrudniku12 = obvodHrudnikuX / 2;
    static float obvodPasu12 = obvodPasuX / 2;
    static float obvodSedu12 = obvodSeduX / 2;
    static float sirkaZad12 = sirkaZadX / 2;

    static float zhp = (obvodHrudniku12 / 5) + 10.5f + 1.5f;
    static float szv = sirkaZad12 + 0.5f;
    static float srv = sirkaRamene + 0.5f;
    static float pdr = delkaZad + 4.5f;
    static float hpb = (obvodHrudniku12 / 2) + 4; //přídavek v rozpětí 3-5cm  --> 4cm
    static float sprur = (obvodHrudniku12 / 4) + 0.5f;
    static float prs = (obvodHrudniku12 / 2) - 4 + 1.5f;


    //Výpočty zadní část

    static int vyskaPlatna = (int)System.Math.Round(delkaOdevu + 22 + pocatecniSouradniceY);
    static int sirkaPlatna = (int)System.Math.Round(obvodPasu12 * 3 + pocatecniSouradniceX);

    static int pocatecniSouradniceX = 12;
    static int pocatecniSouradniceY = 12;

    static int souradniceXbodX = pocatecniSouradniceX + 2;
    Vector3 bodX = new Vector3(pocatecniSouradniceX + 2, pocatecniSouradniceY, 0);
    static int souradniceYbodA = (int)System.Math.Round(pocatecniSouradniceY + delkaOdevu);
    Vector3 bodA = new Vector3(pocatecniSouradniceX, souradniceYbodA, 0);
    static int souradniceYbodC = (int)System.Math.Round(souradniceYbodA - zhp);

    static int souradniceXbodB = pocatecniSouradniceX + 1;
    Vector3 bodB = new Vector3(pocatecniSouradniceX + 1, souradniceYbodC, 0); //bod ZhpA
    static int souradniceXbodC = (int)System.Math.Round(pocatecniSouradniceX + szv + ((2 * sprur) / 3));
    Vector3 bodC = new Vector3(souradniceXbodC, souradniceYbodC, 0);

    static int souradniceYbodD = (int)System.Math.Round(souradniceYbodA - delkaZad);
    Vector3 bodD = new Vector3(souradniceXbodX, souradniceYbodD, 0);
    static int souradniceXbodE = souradniceXbodC - 1;
    Vector3 bodE = new Vector3(souradniceXbodE, souradniceYbodD, 0);

    static float souradniceYbodF = souradniceYbodD - 20;
    Vector3 bodF = new Vector3(souradniceXbodX, souradniceYbodF, 0);
    static int souradniceXbodG = (int)System.Math.Round(pocatecniSouradniceX + obvodSedu12 / 2 + 1.7f);
    Vector3 bodG = new Vector3(souradniceXbodG, souradniceYbodF, 0);

    static float souradniceXbodH = ((souradniceXbodG - pocatecniSouradniceX) / 3 * 2) + 20;
    Vector3 bodH = new Vector3(souradniceXbodH, pocatecniSouradniceY, 0);
    static int souradniceXbodI = souradniceXbodG + 2;
    //static int souradniceYbodI = (int)System.Math.Round(pocatecniSouradniceY + 0.5f);
    static int souradniceYbodI = pocatecniSouradniceY + 1;
    Vector3 bodI = new Vector3(souradniceXbodI, souradniceYbodI, 0);
    
    static int souradniceXbodK = (int)System.Math.Round(pocatecniSouradniceX + szv + 1 + 2);
    static int souradniceYbodK = souradniceYbodA - 2;
    Vector3 bodK = new Vector3(souradniceXbodK, souradniceYbodK, 0);
    static int souradniceXbodPomocneC = (int)System.Math.Round(souradniceXbodC - (2 * sprur / 3));
    Vector3 pomocneC = new Vector3(souradniceXbodC - (2 * sprur / 3), souradniceYbodC, 0);

    static int souradniceXbodJ = (int)System.Math.Round(pocatecniSouradniceX + (obvodHrudniku12 / 10) + 2 + 1);
    static int souradniceYbodJ = souradniceYbodA + 3;
    Vector3 bodJ = new Vector3(souradniceXbodJ, souradniceYbodJ, 0);
    Vector3 pomocneJ = new Vector3(souradniceXbodJ, souradniceYbodA);
    //--

    static float souradniceYbodL = souradniceYbodA - (((souradniceYbodA - souradniceYbodC) / 2) + 0.5f);
    static float souradniceXbodL = pocatecniSouradniceX + 1 + szv + 1; //souradniceXbodC - (2 * sprur / 3) - 1;
    Vector3 bodM = new Vector3(souradniceXzasevekA2, souradniceYbodL, 0);
    Vector3 bodL = new Vector3(souradniceXbodL, souradniceYbodL, 0);
    Vector3 bodL1 = new Vector3(souradniceXbodL, souradniceYbodL + 0.5f, 0);
    Vector3 bodL2 = new Vector3(souradniceXbodL, souradniceYbodL - 0.5f, 0);

    static float souradniceYzasevekA1 = souradniceYbodD - 15;
    static float souradniceYzasevekA2 = souradniceYbodD + 15;
    static float sirkaZasevkuA = 2.5f;
    static float souradniceXzasevekA2 = pocatecniSouradniceX + 1 + 1 + (szv / 2);
    static float souradniceXzasevekA3 = souradniceXzasevekA2 - (sirkaZasevkuA / 2);
    static float souradniceXzasevekA4 = souradniceXzasevekA3 + sirkaZasevkuA;
    Vector3 zasevekA1 = new Vector3(souradniceXzasevekA2, souradniceYzasevekA1, 0);
    Vector3 zasevekA2 = new Vector3(souradniceXzasevekA2, souradniceYzasevekA2, 0);
    Vector3 zasevekA3 = new Vector3(souradniceXzasevekA3, souradniceYbodD, 0);
    Vector3 zasevekA4 = new Vector3(souradniceXzasevekA4, souradniceYbodD, 0);

    //Výpočty přední část

    static float souradniceXbodN = souradniceXbodC + 8; //souradniceXbodO - (prs + sprur / 3);
    Vector3 bodN = new Vector3(souradniceXbodN, souradniceYbodC, 0);//(souradniceXbodC + 8, souradniceYbodC, 0);
    static float souradniceXbodO = souradniceXbodC + 8 + sprur / 3 + prs;
    Vector3 bodO = new Vector3(souradniceXbodO, souradniceYbodC, 0);
    Vector3 bodR = new Vector3(souradniceXbodO, pocatecniSouradniceY, 0);

    Vector3 bodQ = new Vector3(souradniceXbodO, souradniceYbodD - 20, 0);
    static float souradniceXbodT = souradniceXbodN - 1.7f; //souradniceXbodO - obvodSedu12 / 2 - 2f - 1.7f;
    Vector3 bodT = new Vector3(souradniceXbodT, souradniceYbodD - 20, 0);

    Vector3 bodS = new Vector3(souradniceXbodT - 2, pocatecniSouradniceY + 1, 0);
    static float souradniceXbodS2 = souradniceXbodO - (2 * (souradniceXbodO - 2 - souradniceXbodT) / 3);
    Vector3 bodS2 = new Vector3(souradniceXbodS2, pocatecniSouradniceY, 0);
    Vector3 bodS3 = new Vector3(souradniceXbodT - 2, pocatecniSouradniceY, 0);

    Vector3 bodU = new Vector3(souradniceXbodN + 1, souradniceYbodD, 0);
    Vector3 bodU1 = new Vector3(souradniceXbodN + 1, souradniceYbodD + 1, 0);
    Vector3 bodP = new Vector3(souradniceXbodO, souradniceYbodD, 0);
    Vector3 bodU2 = new Vector3(souradniceXbodT, souradniceYbodD + 1, 0);

    static float souradniceXzasevekB1 = souradniceXbodO - prs / 2;
    static float souradniceYzasevekB1 = souradniceYbodD - 15;
    Vector3 zasevekB1 = new Vector3(souradniceXzasevekB1, souradniceYzasevekB1, 0);
    Vector3 zasevekB2 = new Vector3(souradniceXzasevekB1, souradniceYzasevekB1 + 30, 0);
    Vector3 zasevekB3 = new Vector3(souradniceXzasevekB1 - 1.5f, souradniceYbodD, 0);
    Vector3 zasevekB4 = new Vector3(souradniceXzasevekB1 + 1.5f, souradniceYbodD, 0);

    Vector3 bodNO = new Vector3(souradniceXzasevekB1, souradniceYzasevekB1 + 32, 0);
    static float souradniceYbodXXXX = souradniceYzasevekB1 + 32 + hpb;
    Vector3 bodXXX = new Vector3(souradniceXzasevekB1, souradniceYbodXXXX, 0);
    Vector3 bodY = new Vector3(souradniceXbodO - (obvodHrudniku12 / 10 + 2), souradniceYbodXXXX + 1.5f, 0); //!!!!!! 1.5 tip


    //GameObject myCanvas;
    //GameObject line3;
    LineRenderer lr;
    static int numberPoints = 200;
    Vector3[] bezierPositions = new Vector3[numberPoints];

    void Zapis()
    {
      

    }

    void Start()
    {

        // přední část

        DrawLinearBezierCurve(bodA, bodD);
        DrawLinearBezierCurve(bodD, bodX);
        DrawLinearBezierCurve(bodB, bodC);
        DrawLinearBezierCurve(bodD, bodE);
        DrawLinearBezierCurve(bodF, bodG);
        Vector3[] stroke = { bodC, bodE, bodG, bodI };
        DrawLine(stroke);
        Vector3[] stroke2 = { bodX, bodH, bodI };
        DrawLine(stroke2);

        DrawQuadraticBezierCurve(bodC, pomocneC, bodK);
        DrawLinearBezierCurve(bodJ, bodK);
        DrawQuadraticBezierCurve(bodA, pomocneJ, bodJ);

        NakresliDvojityZasevek(zasevekA1, zasevekA2, zasevekA3, zasevekA4);
        DrawLinearBezierCurve(bodM, bodL); //test
        NakresliJednoduchyZasevek(bodL1, bodM, bodL2);

        //zápis

        //string umisteni = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Editor_strihu");
       // if (!Directory.Exists(umisteni))
          //  Directory.CreateDirectory(umisteni);

        souradniceYbodD = vyskaPlatna - souradniceYbodD;
        souradniceYbodF = vyskaPlatna - souradniceYbodF;
        souradniceYbodA = vyskaPlatna - souradniceYbodA;
        souradniceYbodC = vyskaPlatna - souradniceYbodC;
        souradniceYbodI = vyskaPlatna - souradniceYbodI;
        pocatecniSouradniceY = vyskaPlatna - pocatecniSouradniceY;
        souradniceYbodJ = vyskaPlatna - souradniceYbodJ;
        souradniceYbodK = vyskaPlatna - souradniceYbodK;
      

        using (StreamWriter sw = new StreamWriter(@"souboooor.svg", false))
        {
            sw.WriteLine("<svg height=\"400\" width=\"450\" xmlns=\"http://www.w3.org/2000/svg\">");
            sw.WriteLine("<path id=\"lineDE\" d=\"M {0} {1} L {2} {3}\" stroke=\"green\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodX, souradniceYbodD, souradniceXbodE, souradniceYbodD);
            sw.WriteLine("<path id=\"lineBC\" d=\"M {0} {1} L {2} {3}\" stroke=\"black\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodB, souradniceYbodC, souradniceXbodC, souradniceYbodC);
            sw.WriteLine("<path id=\"lineCEGI\" d=\"M {0} {1} L {2} {3} L {4} {5} L {6} {7}\" stroke=\"blue\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodC, souradniceYbodC, souradniceXbodE, souradniceYbodD, souradniceXbodG, souradniceYbodF, souradniceXbodI, souradniceYbodI);
            sw.WriteLine("<path id=\"lineFG\" d=\"M {0} {1} L {2} {3}\" stroke=\"blue\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodX, souradniceYbodF, souradniceXbodG, souradniceYbodF);
            sw.WriteLine("<path id=\"lineXDA\" d=\"M {0} {1} L {2} {3} L {4} {5} \" stroke=\"red\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodX, pocatecniSouradniceY, souradniceXbodX, souradniceYbodD, pocatecniSouradniceX, souradniceYbodA); //???
            sw.WriteLine("<path id=\"lineXHI\" d=\"M {0} {1} L {2} {3} L {4} {5}\" stroke=\"pink\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodX, pocatecniSouradniceY, souradniceXbodH, pocatecniSouradniceY, souradniceXbodI, souradniceYbodI);
            sw.WriteLine("<path id=\"lineJK\" d=\"M {0} {1} L {2} {3}\" stroke=\"black\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodJ, souradniceYbodJ, souradniceXbodK, souradniceYbodK);
            sw.WriteLine("");
            sw.WriteLine("<path id=\"QlineCK\" d=\"M {0} {1} A 7 13 0 0 1 {2} {3}\" stroke=\"orange\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodC, souradniceYbodC, souradniceXbodK, souradniceYbodK);
            sw.WriteLine("<path id=\"QlineAJ\" d=\"M {0} {1} A 7 3 0 0 0 {2} {3}\" stroke=\"orange\" stroke-width=\"1\" fill =\"none\" />", pocatecniSouradniceX, souradniceYbodA, souradniceXbodJ, souradniceYbodJ);
            sw.WriteLine("</svg>");
            sw.Flush();
        }


        //zadní část
        DrawLinearBezierCurve(bodN, bodO);
        DrawLinearBezierCurve(bodO, bodR);

        Vector3[] stroke3 = { bodQ, bodT };
        DrawLine(stroke3);

        DrawLinearBezierCurve(bodR, bodS2);
        DrawLinearBezierCurve(bodS2, bodS);
        DrawLinearBezierCurve(bodS, bodT);

        DrawLinearBezierCurve(bodP, bodU);
        DrawLinearBezierCurve(bodT, bodU1);
        DrawLinearBezierCurve(bodU1, bodN);

        NakresliDvojityZasevek(zasevekB1, zasevekB2, zasevekB3, zasevekB4);


    }
    void NakresliJednoduchyZasevek(Vector3 bod1, Vector3 vrchol, Vector3 bod2)
    {
        DrawLinearBezierCurve(bod1, vrchol);
        DrawLinearBezierCurve(vrchol, bod2);
    }

    void NakresliDvojityZasevek(Vector3 zasevek1, Vector3 zasevek2, Vector3 zasevek3, Vector3 zasevek4)
    {
        DrawLinearBezierCurve(zasevek1, zasevek2);
        DrawLinearBezierCurve(zasevek1, zasevek3);
        DrawLinearBezierCurve(zasevek1, zasevek4);
        DrawLinearBezierCurve(zasevek2, zasevek3);
        DrawLinearBezierCurve(zasevek2, zasevek4);
    }

    LineRenderer LineRendererCreater()
    {
        lr = new GameObject().AddComponent<LineRenderer>();
        //lr.gameObject.SetParent(transform, false); //resets position and rotation
        lr.gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        lr.SetWidth(0.4f, 0.4f); //nastaví tloušťku čáry
        return lr;
    }

    LineRenderer DifferentWidthLineRenderer(float width) //ještě nepoužito
    {
        lr = new GameObject().AddComponent<LineRenderer>();
        //lr.gameObject.SetParent(transform, false); //resets position and rotation
        lr.gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        lr.SetWidth(width, width); //nastaví tloušťku čáry
        return lr;
    }

    void DrawLine(Vector3[] positions)
    {
        LineRenderer line = LineRendererCreater();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.positionCount = positions.Length;
        line.SetPositions(positions);
    }

    void DrawQuadraticBezierCurve(Vector3 point1, Vector3 point2, Vector3 point3)
    {
        for (int i = 1; i < numberPoints + 1; i++)
        {
            float t = i / (float)numberPoints;
            bezierPositions[i - 1] = CalculateQuadraticBezierPoint(t, point1, point2, point3);
        }
        DrawLine(bezierPositions);
    }

    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 point1, Vector3 point2, Vector3 point3)
    {
        float z = 1 - t;
        float t2 = t * t;
        float z2 = z * z;
        Vector3 bezierPoint = (z2 * point1) + (2 * z * t * point2) + (t2 * point3);
        return bezierPoint;
    }

    /*void DrawLinearBezierCurve(Vector3 point1, Vector3 point2)
    {
        for (int i = 1; i < numberPoints + 1; i++)
        {
            float t = i / (float)numberPoints;
            bezierPositions[i - 1] = CalculateLinearBezierPoint(t, point1, point2);
        }
        DrawLine(bezierPositions);
    }*/

    void DrawLinearBezierCurve(Vector3 point1, Vector3 point2)
    {
        for (int i = 0; i < numberPoints ; i++)
        {
            float t = i / (float)numberPoints;
            bezierPositions[i] = CalculateLinearBezierPoint(t, point1, point2);
        }
        DrawLine(bezierPositions);
    }

    Vector3 CalculateLinearBezierPoint(float t, Vector3 point1, Vector3 point2)
    {
        Vector3 result = point1 + t * (point2 - point1);
        return result;
    }


}

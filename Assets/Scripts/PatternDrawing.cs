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
    //static User user;

    private LineRenderer line;
    static int vyskaPostavy; //168;
    static int obvodHrudnikuX; //96;

    static int obvodPasuX; //76;
    static int obvodSeduX; //100;

    static int delkaZad; //41;
    static int delkaOdevu; //90;
    static int sirkaZadX; //35;

    static int sirkaRamene; //13;
    static int delkaRukavu; //60;


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
        //user = FindObjectOfType<User>();
        User user = gameObject.AddComponent<User>();
        //user = User.GetData(DataTreatment.NoteUserMeasures().field);
        user.ReadData();
        vyskaPostavy = user.vyskaPostavy; //168;
        obvodHrudnikuX = user.obvodHrudniku; //96;
        obvodPasuX = user.obvodPasu; //76;
        obvodSeduX = user.obvodSedu; //100;
        delkaZad = user.delkaZad; //41;
        delkaOdevu = user.delkaOdevu; //90;
        sirkaZadX = user.sirkaZad; //35;
        sirkaRamene = user.sirkaRamene; //13;
        delkaRukavu = user.delkaRukavu; //60;

        Debug.Log("Pattern drawing");
        Debug.Log(user.obvodPasu);
        Debug.Log(user.vyskaPostavy);
        Debug.Log(user.obvodHrudniku);
        Debug.Log(user.obvodSedu);
        Debug.Log(user.delkaZad);
        Debug.Log(user.delkaOdevu);
        Debug.Log(user.sirkaZad);
        Debug.Log(user.sirkaRamene);
        Debug.Log(user.delkaRukavu);

        //ctrlV
        float obvodHrudniku12 = obvodHrudnikuX / 2;
        float obvodPasu12 = obvodPasuX / 2;
        float obvodSedu12 = obvodSeduX / 2;
        float sirkaZad12 = sirkaZadX / 2;

        float zhp = (obvodHrudniku12 / 5) + 10.5f + 1.5f;
        float szv = sirkaZad12 + 0.5f;
        float srv = sirkaRamene + 0.5f;
        float pdr = delkaZad + 4.5f;
        float hpb = (obvodHrudniku12 / 2) + 4; //přídavek v rozpětí 3-5cm  --> 4cm
        float sprur = (obvodHrudniku12 / 4) + 0.5f;
        float prs = (obvodHrudniku12 / 2) - 4 + 1.5f;


        //Výpočty zadní část
        int pocatecniSouradniceX = 12;
        int pocatecniSouradniceY = 12;

        int vyskaPlatna = delkaOdevu + 22 + pocatecniSouradniceY;
        int sirkaPlatna = (int)System.Math.Round(obvodPasu12 * 3 + pocatecniSouradniceX);

        

        int souradniceXbodX = pocatecniSouradniceX + 2;
        Vector3 bodX = new Vector3(pocatecniSouradniceX + 2, pocatecniSouradniceY, 0);
        int souradniceYbodA = pocatecniSouradniceY + delkaOdevu;
        Vector3 bodA = new Vector3(pocatecniSouradniceX, souradniceYbodA, 0);
        int souradniceYbodC = (int)System.Math.Round(souradniceYbodA - zhp);

        int souradniceXbodB = pocatecniSouradniceX + 1;
        Vector3 bodB = new Vector3(pocatecniSouradniceX + 1, souradniceYbodC, 0); //bod ZhpA
        int souradniceXbodC = (int)System.Math.Round(pocatecniSouradniceX + szv + ((2 * sprur) / 3));
        Vector3 bodC = new Vector3(souradniceXbodC, souradniceYbodC, 0);

        int souradniceYbodD = souradniceYbodA - delkaZad;
        Vector3 bodD = new Vector3(souradniceXbodX, souradniceYbodD, 0);
        int souradniceXbodE = souradniceXbodC - 1;
        Vector3 bodE = new Vector3(souradniceXbodE, souradniceYbodD, 0);

        float souradniceYbodF = souradniceYbodD - 20;
        Vector3 bodF = new Vector3(souradniceXbodX, souradniceYbodF, 0);
        int souradniceXbodG = (int)System.Math.Round(pocatecniSouradniceX + obvodSedu12 / 2 + 1.7f);
        Vector3 bodG = new Vector3(souradniceXbodG, souradniceYbodF, 0);

        float souradniceXbodH = ((souradniceXbodG - pocatecniSouradniceX) / 3 * 2) + 20;
        Vector3 bodH = new Vector3(souradniceXbodH, pocatecniSouradniceY, 0);
        int souradniceXbodI = souradniceXbodG + 2;
        //static int souradniceYbodI = (int)System.Math.Round(pocatecniSouradniceY + 0.5f);
        int souradniceYbodI = pocatecniSouradniceY + 1;
        Vector3 bodI = new Vector3(souradniceXbodI, souradniceYbodI, 0);

        int souradniceXbodK = (int)System.Math.Round(pocatecniSouradniceX + szv + 1 + 2);
        int souradniceYbodK = souradniceYbodA - 2;
        Vector3 bodK = new Vector3(souradniceXbodK, souradniceYbodK, 0);
        int souradniceXbodPomocneC = (int)System.Math.Round(souradniceXbodC - (2 * sprur / 3));
        Vector3 pomocneC = new Vector3(souradniceXbodC - (2 * sprur / 3), souradniceYbodC, 0);

        int souradniceXbodJ = (int)System.Math.Round(pocatecniSouradniceX + (obvodHrudniku12 / 10) + 2 + 1);
        int souradniceYbodJ = souradniceYbodA + 3;
        Vector3 bodJ = new Vector3(souradniceXbodJ, souradniceYbodJ, 0);
        int souradniceXbodPomocneJ = souradniceXbodJ;
        int souradniceYbodPomocneJ = souradniceYbodA;
        Vector3 pomocneJ = new Vector3(souradniceXbodPomocneJ, souradniceYbodPomocneJ);
        //--

        int souradniceYzasevekA1 = souradniceYbodD - 15;
        int souradniceYzasevekA2 = souradniceYbodD + 15;
        int sirkaZasevkuA = (int)System.Math.Round(2.5f);
        int souradniceXzasevekA2 = (int)System.Math.Round(pocatecniSouradniceX + 1 + 1 + (szv / 2));
        int souradniceXzasevekA1 = souradniceXzasevekA2;
        int souradniceYzasevekA3 = souradniceYbodD;
        int souradniceYzasevekA4 = souradniceYbodD;
        int souradniceXzasevekA3 = souradniceXzasevekA2 - (sirkaZasevkuA / 2);
        int souradniceXzasevekA4 = souradniceXzasevekA3 + sirkaZasevkuA;


        int souradniceYbodL = (int)System.Math.Round(souradniceYbodA - (((souradniceYbodA - souradniceYbodC) / 2) + 0.5f));
        int souradniceXbodL = (int)System.Math.Round(pocatecniSouradniceX + 1 + szv + 1); //souradniceXbodC - (2 * sprur / 3) - 1;
        int souradniceXbodM = souradniceXzasevekA2;
        Vector3 bodM = new Vector3(souradniceXbodM, souradniceYbodL, 0);
        int souradniceYbodM = souradniceYbodL;
        Vector3 bodL = new Vector3(souradniceXbodL, souradniceYbodL, 0);
        int souradniceYbodL1 = (int)Math.Round(souradniceYbodL + 0.6f);
        Vector3 bodL1 = new Vector3(souradniceXbodL, souradniceYbodL1, 0);
        int souradniceYbodL2 = (int)Math.Round(souradniceYbodL - 0.6f);
        Vector3 bodL2 = new Vector3(souradniceXbodL, souradniceYbodL2, 0);


        Vector3 zasevekA1 = new Vector3(souradniceXzasevekA1, souradniceYzasevekA1, 0);
        Vector3 zasevekA2 = new Vector3(souradniceXzasevekA2, souradniceYzasevekA2, 0);
        Vector3 zasevekA3 = new Vector3(souradniceXzasevekA3, souradniceYzasevekA3, 0);
        Vector3 zasevekA4 = new Vector3(souradniceXzasevekA4, souradniceYzasevekA4, 0);

        //Výpočty přední část

        int souradniceXbodN = souradniceXbodC + 8; //souradniceXbodO - (prs + sprur / 3);
        int souradniceYbodN = souradniceYbodC;
        int souradniceYbodO = souradniceYbodC;
        Vector3 bodN = new Vector3(souradniceXbodN, souradniceYbodN, 0);//(souradniceXbodC + 8, souradniceYbodC, 0);
        int souradniceXbodO = (int)System.Math.Round(souradniceXbodC + 8 + sprur / 3 + prs);
        Vector3 bodO = new Vector3(souradniceXbodO, souradniceYbodO, 0);
        int souradniceXbodR = souradniceXbodO;
        int souradniceYbodR = pocatecniSouradniceY;
        Vector3 bodR = new Vector3(souradniceXbodO, pocatecniSouradniceY, 0);

        int souradniceXbodQ = souradniceXbodO;
        int souradniceYbodQ = souradniceYbodD - 20;
        Vector3 bodQ = new Vector3(souradniceXbodQ, souradniceYbodQ, 0);
        int souradniceXbodT = (int)System.Math.Round(souradniceXbodN - 1.7f); //souradniceXbodO - obvodSedu12 / 2 - 2f - 1.7f;
        int souradniceYbodT = souradniceYbodD - 20;
        Vector3 bodT = new Vector3(souradniceXbodT, souradniceYbodT, 0);

        int souradniceXbodS = souradniceXbodT - 2;
        int souradniceYbodS = pocatecniSouradniceY + 1;
        Vector3 bodS = new Vector3(souradniceXbodS, souradniceYbodS, 0);
        int souradniceXbodS2 = souradniceXbodO - (2 * (souradniceXbodO - 2 - souradniceXbodT) / 3);
        int souradniceYbodS2 = pocatecniSouradniceY;
        Vector3 bodS2 = new Vector3(souradniceXbodS2, souradniceYbodS2, 0);
        Vector3 bodS3 = new Vector3(souradniceXbodT - 2, pocatecniSouradniceY, 0);

        int souradniceXbodU = souradniceXbodN + 1;
        int souradniceYbodU = souradniceYbodD;
        Vector3 bodU = new Vector3(souradniceXbodU, souradniceYbodU, 0);
        int souradniceXbodU1 = souradniceXbodN + 1;
        int souradniceYbodU1 = souradniceYbodD + 1;
        Vector3 bodU1 = new Vector3(souradniceXbodU1, souradniceYbodU1, 0);
        int souradniceXbodP = souradniceXbodO;
        int souradniceYbodP = souradniceYbodD;
        Vector3 bodP = new Vector3(souradniceXbodP, souradniceYbodP, 0);
        Vector3 bodU2 = new Vector3(souradniceXbodT, souradniceYbodD + 1, 0);

        int souradniceXzasevekB1 = (int)Math.Round(souradniceXbodO - prs / 2);
        int souradniceYzasevekB1 = souradniceYbodD - 15;
        Vector3 zasevekB1 = new Vector3(souradniceXzasevekB1, souradniceYzasevekB1, 0);
        int souradniceXzasevekB2 = souradniceXzasevekB1;
        int souradniceYzasevekB2 = souradniceYzasevekB1 + 30;
        Vector3 zasevekB2 = new Vector3(souradniceXzasevekB2, souradniceYzasevekB2, 0);
        int souradniceXzasevekB3 = (int)Math.Round(souradniceXzasevekB1 - 1.6f);
        int souradniceYzasevekB3 = souradniceYbodD;
        Vector3 zasevekB3 = new Vector3(souradniceXzasevekB3, souradniceYzasevekB3, 0);
        int souradniceXzasevekB4 = (int)Math.Round(souradniceXzasevekB1 + 1.6f);
        int souradniceYzasevekB4 = souradniceYbodD;
        Vector3 zasevekB4 = new Vector3(souradniceXzasevekB4, souradniceYzasevekB4, 0);

        int souradniceXbodNO = souradniceXzasevekB1;
        int souradniceYbodNO = souradniceYzasevekB2 + 2;
        Vector3 bodNO = new Vector3(souradniceXbodNO, souradniceYbodNO, 0);
        int souradniceYbodXXXX = (int)Math.Round(souradniceYzasevekB2 + 2 + hpb);
        int souradniceXbodXXXX = souradniceXzasevekB1;
        Vector3 bodXXX = new Vector3(souradniceXbodXXXX, souradniceYbodXXXX, 0);
        int souradniceXbodY = (int)Math.Round(souradniceXbodO - (obvodHrudniku12 / 10 + 2));
        int souradniceYbodY = (int)Math.Round(souradniceYbodXXXX + 1.6f);
        Vector3 bodY = new Vector3(souradniceXbodY, souradniceYbodY, 0); //!!!!!! 1.6 tip

        int souradniceXbodStred = (int)Math.Round(souradniceXbodY + obvodHrudniku12 / 10 + 2);
        int souradniceYbodStred = souradniceYbodY + 1;
        int souradniceXbodZ = souradniceXbodStred;
        int souradniceYbodZ = (int)Math.Round(souradniceYbodStred - (obvodHrudniku12 / 10 + 3.6f));

        int souradniceXbodV = souradniceXbodN;
        int souradniceYbodV = (int)Math.Round(souradniceYbodK - 2.4f);
        int souradniceXbodHH = (int)Math.Round(souradniceXbodN + sprur / 3);
        int souradniceYbodHH = souradniceYbodN;
        int souradniceXbodPR = souradniceXbodHH;
        int souradniceYbodPR = (int)Math.Round(souradniceYbodHH + (3 * sprur) / 8);
        int souradniceYbodW = souradniceYbodXXXX - 1; // -1

        float VW = souradniceYbodW - souradniceYbodV;
        float srv3 = srv - 3;
        int souradniceXbodW = (int)Math.Round(souradniceXbodV + Math.Sqrt(srv3 * srv3 - VW * VW));
        // odmocnina z (spur na druhou + V-W na druhou)

        //--konec ctrl



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

        //zápis

        string umisteni = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Editor_strihu");
        if (!Directory.Exists(umisteni))
            Directory.CreateDirectory(umisteni);

        souradniceYbodD = vyskaPlatna - souradniceYbodD;
        souradniceYbodF = vyskaPlatna - souradniceYbodF;
        souradniceYbodA = vyskaPlatna - souradniceYbodA;
        souradniceYbodC = vyskaPlatna - souradniceYbodC;
        souradniceYbodI = vyskaPlatna - souradniceYbodI;
        pocatecniSouradniceY = vyskaPlatna - pocatecniSouradniceY;
        souradniceYbodJ = vyskaPlatna - souradniceYbodJ;
        souradniceYbodK = vyskaPlatna - souradniceYbodK;
        souradniceYbodPomocneJ = vyskaPlatna - souradniceYbodPomocneJ;

        souradniceYbodL1 = vyskaPlatna - souradniceYbodL1;
        souradniceYbodL2 = vyskaPlatna - souradniceYbodL2;
        souradniceYbodM = vyskaPlatna - souradniceYbodM;

        souradniceYzasevekA1 = vyskaPlatna - souradniceYzasevekA1;
        souradniceYzasevekA2 = vyskaPlatna - souradniceYzasevekA2;
        souradniceYzasevekA3 = vyskaPlatna - souradniceYzasevekA3;
        souradniceYzasevekA4 = vyskaPlatna - souradniceYzasevekA4;
      
        souradniceYbodN = vyskaPlatna - souradniceYbodN;
        souradniceYbodO = vyskaPlatna - souradniceYbodO;
        souradniceYbodR = vyskaPlatna - souradniceYbodR;
        souradniceYbodS = vyskaPlatna - souradniceYbodS;
        souradniceYbodS2 = vyskaPlatna - souradniceYbodS2;
        souradniceYbodT = vyskaPlatna - souradniceYbodT;
        souradniceYbodQ = vyskaPlatna - souradniceYbodQ;
        souradniceYbodP = vyskaPlatna - souradniceYbodP;
        souradniceYbodU = vyskaPlatna - souradniceYbodU;
        souradniceYbodU1 = vyskaPlatna - souradniceYbodU1;

        souradniceYbodNO = vyskaPlatna - souradniceYbodNO;
        souradniceYbodXXXX = vyskaPlatna - souradniceYbodXXXX;
        souradniceYbodY = vyskaPlatna - souradniceYbodY;
        souradniceYbodZ = vyskaPlatna - souradniceYbodZ;
        souradniceYbodStred = vyskaPlatna - souradniceYbodStred;
        souradniceYbodW = vyskaPlatna - souradniceYbodW;
        souradniceYbodV = vyskaPlatna - souradniceYbodV;
        souradniceYbodHH = vyskaPlatna - souradniceYbodHH;
        souradniceYbodPR = vyskaPlatna - souradniceYbodPR;

        souradniceYzasevekB1 = vyskaPlatna - souradniceYzasevekB1;
        souradniceYzasevekB2 = vyskaPlatna - souradniceYzasevekB2;
        souradniceYzasevekB3 = vyskaPlatna - souradniceYzasevekB3;
        souradniceYzasevekB4 = vyskaPlatna - souradniceYzasevekB4;


        using (StreamWriter sw = new StreamWriter(@"souboooor.svg", false))
        {
            sw.WriteLine("<svg height=\"400\" width=\"450\" xmlns=\"http://www.w3.org/2000/svg\">");
            sw.WriteLine("<path id=\"lineDE\" d=\"M {0} {1} L {2} {3}\" stroke=\"grey\" stroke-width=\"1\" stroke-dasharray=\"1\" fill =\"none\" />", souradniceXbodX, souradniceYbodD, souradniceXbodE, souradniceYbodD);
            sw.WriteLine("<path id=\"lineBC\" d=\"M {0} {1} L {2} {3}\" stroke=\"grey\" stroke-width=\"1\" stroke-dasharray=\"1\"  fill =\"none\" />", souradniceXbodB, souradniceYbodC, souradniceXbodC, souradniceYbodC);
            sw.WriteLine("<path id=\"lineFG\" d=\"M {0} {1} L {2} {3}\" stroke=\"grey\" stroke-width=\"1\" stroke-dasharray=\"1\"  fill =\"none\" />", souradniceXbodX, souradniceYbodF, souradniceXbodG, souradniceYbodF);
            sw.WriteLine("<path id=\"lineCEGI\" d=\"M {0} {1} L {2} {3} L {4} {5} L {6} {7}\" stroke=\"black\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodC, souradniceYbodC, souradniceXbodE, souradniceYbodD, souradniceXbodG, souradniceYbodF, souradniceXbodI, souradniceYbodI);
            sw.WriteLine("<path id=\"lineXDA\" d=\"M {0} {1} L {2} {3} L {4} {5} \" stroke=\"black\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodX, pocatecniSouradniceY, souradniceXbodX, souradniceYbodD, pocatecniSouradniceX, souradniceYbodA); //???
            sw.WriteLine("<path id=\"lineXHI\" d=\"M {0} {1} L {2} {3} L {4} {5}\" stroke=\"black\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodX, pocatecniSouradniceY, souradniceXbodH, pocatecniSouradniceY, souradniceXbodI, souradniceYbodI);
            sw.WriteLine("<path id=\"lineJK\" d=\"M {0} {1} L {2} {3}\" stroke=\"black\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodJ, souradniceYbodJ, souradniceXbodK, souradniceYbodK);
            sw.WriteLine("<path id=\"zasevekA\" d=\"M {0} {1} L {2} {3} L {4} {5} L {6} {7} L {0} {1}\" stroke=\"black\" stroke-width=\"1\" fill =\"none\" />", souradniceXzasevekA1, souradniceYzasevekA1, souradniceXzasevekA3, souradniceYzasevekA3, souradniceXzasevekA2, souradniceYzasevekA2, souradniceXzasevekA4, souradniceYzasevekA4);
            sw.WriteLine("<path id=\"QlineCK\" d=\"M {0} {1} A 7 13 0 0 1 {2} {3}\" stroke=\"orange\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodC, souradniceYbodC, souradniceXbodK, souradniceYbodK);
            sw.WriteLine("<path id=\"BlineCK\" d=\"M {0} {1} Q {2} {3} {4} {5}\" stroke=\"pink\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodC, souradniceYbodC, souradniceXbodPomocneC, souradniceYbodC, souradniceXbodK, souradniceYbodK);
            sw.WriteLine("<path id=\"QlineAJ\" d=\"M {0} {1} A 7 3 0 0 0 {2} {3}\" stroke=\"orange\" stroke-width=\"1\" fill =\"none\" />", pocatecniSouradniceX, souradniceYbodA, souradniceXbodJ, souradniceYbodJ);
            sw.WriteLine("<path id=\"BlineAJ\" d=\"M {0} {1} Q {2} {3} {4} {5}\" stroke=\"pink\" stroke-width=\"1\" fill =\"none\" />", pocatecniSouradniceX, souradniceYbodA, souradniceXbodPomocneJ, souradniceYbodPomocneJ, souradniceXbodJ, souradniceYbodJ);
            sw.WriteLine("<path id=\"zasevekL1ML2\" d=\"M {0} {1} L {2} {3} L {4} {5}\" stroke=\"black\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodL, souradniceYbodL1, souradniceXbodM, souradniceYbodM, souradniceXbodL, souradniceYbodL2);
            sw.WriteLine("");
            sw.WriteLine("<path id=\"lineNO\" d=\"M {0} {1} L {2} {3}\" stroke=\"grey\" stroke-width=\"1\" stroke-dasharray=\"1\"  fill =\"none\" />", souradniceXbodN, souradniceYbodN, souradniceXbodO, souradniceYbodO);
            sw.WriteLine("<path id=\"linePU\" d=\"M {0} {1} L {2} {3}\" stroke=\"grey\" stroke-width=\"1\" stroke-dasharray=\"1\"  fill =\"none\" />", souradniceXbodP, souradniceYbodP, souradniceXbodU, souradniceYbodU);
            sw.WriteLine("<path id=\"lineTQ\" d=\"M {0} {1} L {2} {3}\" stroke=\"grey\" stroke-width=\"1\" stroke-dasharray=\"1\"  fill =\"none\" />", souradniceXbodT, souradniceYbodT, souradniceXbodQ, souradniceYbodQ);
            sw.WriteLine("<path id =\"lineZRS2ST\" d=\"M {0} {1} L {2} {3} L {4} {5} L {6} {7} L {8} {9}\" stroke=\"black\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodZ, souradniceYbodZ, souradniceXbodR, souradniceYbodR, souradniceXbodS2, souradniceYbodS2, souradniceXbodS, souradniceYbodS, souradniceXbodT, souradniceYbodT);
            sw.WriteLine("<path id=\"lineTU1N\" d=\"M {0} {1} L {2} {3} L {4} {5}\" stroke=\"black\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodT, souradniceYbodT, souradniceXbodU1, souradniceYbodU1, souradniceXbodN, souradniceYbodN);
            sw.WriteLine("<path id=\"zasevekA\" d=\"M {0} {1} L {2} {3} L {4} {5} L {6} {7} L {0} {1}\" stroke=\"black\" stroke-width=\"1\" fill =\"none\" />", souradniceXzasevekB1, souradniceYzasevekB1, souradniceXzasevekB3, souradniceYzasevekB3, souradniceXzasevekB2, souradniceYzasevekB2, souradniceXzasevekB4, souradniceYzasevekB4);

            //
            sw.WriteLine("<path id=\"linebodWbodNObodXXX\" d=\"M {0} {1} L {2} {3} L {4} {5}\" stroke=\"black\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodW, souradniceYbodW, souradniceXbodNO, souradniceYbodNO, souradniceXbodXXXX, souradniceYbodXXXX);
            sw.WriteLine("<path id=\"linebodXXXbodY\" d=\"M {0} {1} L {2} {3}\" stroke=\"grey\" stroke-width=\"1\"  fill =\"none\" />", souradniceXbodY, souradniceYbodY, souradniceXbodXXXX, souradniceYbodXXXX);
            sw.WriteLine("<path id=\"BlineYZ\" d=\"M {0} {1} Q {2} {3} {4} {5}\" stroke=\"pink\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodY, souradniceYbodY, souradniceXbodY, souradniceYbodZ, souradniceXbodZ, souradniceYbodZ);
            sw.WriteLine("<path id=\"lineWV\" d=\"M {0} {1} L {2} {3}\" stroke=\"black\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodW, souradniceYbodW, souradniceXbodV, souradniceYbodV);
            sw.WriteLine("<path id=\"BlineNbodPR\" d=\"M {0} {1} Q {2} {3} {4} {5}\" stroke=\"pink\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodN, souradniceYbodN, souradniceXbodHH, souradniceYbodHH, souradniceXbodPR, souradniceYbodPR);
            //sw.WriteLine("<path id=\"BlinebodPRbodV\" d=\"M {0} {1} Q {2} {3} {4} {5}\" stroke=\"pink\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodPR, souradniceYbodPR, souradniceXbodPR, souradniceYbodV, souradniceXbodV, souradniceYbodV);
            sw.WriteLine("<path id=\"linebodPRbodV\" d=\"M {0} {1} L {2} {3}\" stroke=\"pink\" stroke-width=\"1\" fill =\"none\" />", souradniceXbodPR, souradniceYbodPR, souradniceXbodV, souradniceYbodV);
            sw.WriteLine("</svg>");
            sw.Flush();
        }

// stroke-dasharray=\"2 1\"
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

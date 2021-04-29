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
    // Údaje od uživatele + pomocné míry
    static int obvodHrudnikuX;
    static int obvodPasuX;
    static int obvodSeduX;
    static int delkaZad;
    static int delkaOdevu;
    static int sirkaZadX;
    static int sirkaRamene;

    LineRenderer lr;
    static int numberPoints = 200;
    Vector3[] bezierPositions = new Vector3[numberPoints];

    void Start()
    {
        //user = FindObjectOfType<User>(); //User user = gameObject.AddComponent<User>();//user = User.GetData(DataTreatment.NoteUserMeasures().field);
        User user = new User();
        UserToLocal(user);
        Vector3[] vectors = Calculation();
        VectorDrawing(vectors);
        CreateSVGPattern(vectors);
    }
    void UserToLocal(User user)
    {
        user.ReadData();
        obvodHrudnikuX = user.obvodHrudniku;
        obvodPasuX = user.obvodPasu;
        obvodSeduX = user.obvodSedu;
        delkaZad = user.delkaZad;
        delkaOdevu = user.delkaOdevu;
        sirkaZadX = user.sirkaZad;
        sirkaRamene = user.sirkaRamene;
    }

    public Vector3[] Calculation()
    {
        float obvodHrudniku12 = obvodHrudnikuX / 2;
        float obvodPasu12 = obvodPasuX / 2;
        float obvodSedu12 = obvodSeduX / 2;
        float sirkaZad12 = sirkaZadX / 2;

        float zhp = (obvodHrudniku12 / 5) + 10.5f + 1.5f;
        float szv = sirkaZad12 + 0.5f;
        float srv = sirkaRamene + 0.5f;
        float hpb = (obvodHrudniku12 / 2) + 4; //přídavek v rozpětí 3-5cm  --> 4cm
        float sprur = (obvodHrudniku12 / 4) + 0.5f;
        float prs = (obvodHrudniku12 / 2) - 4 + 1.5f;

        int pocatecniSouradniceX = 10;
        int pocatecniSouradniceY = 10;
        Vector3 pocatecniSouradnice = new Vector3(pocatecniSouradniceX, pocatecniSouradniceY, 0);

        int vyskaPlatna = delkaOdevu + 22 + pocatecniSouradniceY;
        int sirkaPlatna = (int)Math.Round(obvodPasu12 * 3 + pocatecniSouradniceX);
        Vector3 rozmeryPlatna = new Vector3(sirkaPlatna, vyskaPlatna, 0);

        int souradniceXbodX = pocatecniSouradniceX + 2;
        Vector3 bodX = new Vector3(pocatecniSouradniceX + 2, pocatecniSouradniceY, 0);
        int souradniceYbodA = pocatecniSouradniceY + delkaOdevu;
        Vector3 bodA = new Vector3(pocatecniSouradniceX, souradniceYbodA, 0);
        int souradniceYbodC = (int)Math.Round(souradniceYbodA - zhp);

        int souradniceXbodB = pocatecniSouradniceX + 1;
        Vector3 bodB = new Vector3(souradniceXbodB, souradniceYbodC, 0); //bod ZhpA
        int souradniceXbodC = (int)Math.Round(pocatecniSouradniceX + szv + ((2 * sprur) / 3));
        Vector3 bodC = new Vector3(souradniceXbodC, souradniceYbodC, 0);

        int souradniceYbodD = souradniceYbodA - delkaZad;
        Vector3 bodD = new Vector3(souradniceXbodX, souradniceYbodD, 0);
        int souradniceXbodE = souradniceXbodC - 1;
        Vector3 bodE = new Vector3(souradniceXbodE, souradniceYbodD, 0);

        int souradniceYbodF = souradniceYbodD - 20;
        Vector3 bodF = new Vector3(souradniceXbodX, souradniceYbodF, 0);
        int souradniceXbodG = (int)Math.Round(pocatecniSouradniceX + obvodSedu12 / 2 + 1.7f);
        Vector3 bodG = new Vector3(souradniceXbodG, souradniceYbodF, 0);

        int souradniceXbodH = ((souradniceXbodG - pocatecniSouradniceX) / 3 * 2) + 20;
        Vector3 bodH = new Vector3(souradniceXbodH, pocatecniSouradniceY, 0);
        int souradniceXbodI = souradniceXbodG + 2;
        int souradniceYbodI = pocatecniSouradniceY + 1;
        Vector3 bodI = new Vector3(souradniceXbodI, souradniceYbodI, 0);

        int souradniceXbodK = (int)Math.Round(pocatecniSouradniceX + szv + 1 + 2);
        int souradniceYbodK = souradniceYbodA - 2;
        Vector3 bodK = new Vector3(souradniceXbodK, souradniceYbodK, 0);
        int souradniceXbodPomocneC = (int)Math.Round(souradniceXbodC - (2 * sprur / 3));
        Vector3 pomocneC = new Vector3(souradniceXbodPomocneC, souradniceYbodC, 0);

        int souradniceXbodJ = (int)Math.Round(pocatecniSouradniceX + (obvodHrudniku12 / 10) + 2 + 1);
        int souradniceYbodJ = souradniceYbodA + 3;
        Vector3 bodJ = new Vector3(souradniceXbodJ, souradniceYbodJ, 0);
        int souradniceXbodPomocneJ = souradniceXbodJ;
        int souradniceYbodPomocneJ = souradniceYbodA;
        Vector3 pomocneJ = new Vector3(souradniceXbodPomocneJ, souradniceYbodPomocneJ);
        
        int souradniceYzasevekA1 = souradniceYbodD - 15;
        int souradniceYzasevekA2 = souradniceYbodD + 15;
        int sirkaZasevkuA = (int)Math.Round(2.5f);
        int souradniceXzasevekA2 = (int)Math.Round(pocatecniSouradniceX + 1 + 1 + (szv / 2));
        int souradniceXzasevekA1 = souradniceXzasevekA2;
        int souradniceYzasevekA3 = souradniceYbodD;
        int souradniceYzasevekA4 = souradniceYbodD;
        int souradniceXzasevekA3 = souradniceXzasevekA2 - (sirkaZasevkuA / 2);
        int souradniceXzasevekA4 = souradniceXzasevekA3 + sirkaZasevkuA;

        int souradniceYbodL = (int)Math.Round(souradniceYbodA - (((souradniceYbodA - souradniceYbodC) / 2) + 0.5f));
        int souradniceXbodL = (int)Math.Round(pocatecniSouradniceX + 1 + szv + 1);
        int souradniceXbodM = souradniceXzasevekA2;
        int souradniceYbodM = souradniceYbodL;
        Vector3 bodM = new Vector3(souradniceXbodM, souradniceYbodM, 0);
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

        int souradniceXbodN = souradniceXbodC + 8; 
        int souradniceYbodN = souradniceYbodC;
        int souradniceYbodO = souradniceYbodC;
        Vector3 bodN = new Vector3(souradniceXbodN, souradniceYbodN, 0);
        int souradniceXbodO = (int)Math.Round(souradniceXbodC + 8 + sprur / 3 + prs);
        Vector3 bodO = new Vector3(souradniceXbodO, souradniceYbodO, 0);
        int souradniceXbodR = souradniceXbodO;
        int souradniceYbodR = pocatecniSouradniceY;
        Vector3 bodR = new Vector3(souradniceXbodR, souradniceYbodR, 0);

        int souradniceXbodQ = souradniceXbodO;
        int souradniceYbodQ = souradniceYbodD - 20;
        Vector3 bodQ = new Vector3(souradniceXbodQ, souradniceYbodQ, 0);
        int souradniceXbodT = (int)Math.Round(souradniceXbodN - 1.7f);
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
        Vector3 bodY = new Vector3(souradniceXbodY, souradniceYbodY, 0); 

        int souradniceXbodStred = (int)Math.Round(souradniceXbodY + obvodHrudniku12 / 10 + 2);
        int souradniceYbodStred = souradniceYbodY + 1;
        Vector3 bodStred = new Vector3(souradniceXbodStred, souradniceYbodY, 0);
        int souradniceXbodZ = souradniceXbodStred;
        int souradniceYbodZ = (int)Math.Round(souradniceYbodStred - (obvodHrudniku12 / 10 + 3.6f));
        Vector3 bodZ = new Vector3(souradniceXbodZ, souradniceYbodZ, 0);
        Vector3 bodPomocneYZ = new Vector3(souradniceXbodY, souradniceYbodZ, 0);

        int souradniceXbodV = souradniceXbodN;
        int souradniceYbodV = (int)Math.Round(souradniceYbodK - 2.4f);
        Vector3 bodV = new Vector3(souradniceXbodV, souradniceYbodV, 0);
        int souradniceXbodHH = (int)Math.Round(souradniceXbodN + sprur / 3);
        int souradniceYbodHH = souradniceYbodN;
        Vector3 bodHH = new Vector3(souradniceXbodHH, souradniceYbodHH, 0);
        int souradniceXbodPR = souradniceXbodHH;
        int souradniceYbodPR = (int)Math.Round(souradniceYbodHH + (3 * sprur) / 8);
        Vector3 bodPR = new Vector3(souradniceXbodPR, souradniceYbodPR, 0);

        Vector3 pomocneV = new Vector3(souradniceXbodPR, souradniceYbodV, 0);

        int souradniceYbodW = souradniceYbodXXXX - 1; // -1
        float VW = souradniceYbodW - souradniceYbodV;
        float srv3 = srv - 3;
        int souradniceXbodW;
        if ((srv3 * srv3) >= (VW * VW))
            souradniceXbodW = (int)Math.Round(souradniceXbodV + Math.Sqrt((srv3 * srv3) - (VW * VW)));
        else
            souradniceXbodW = (int)Math.Round(souradniceXbodV + Math.Sqrt((VW * VW) - (srv3 * srv3)));
        Vector3 bodW = new Vector3(souradniceXbodW, souradniceYbodW, 0);

        Vector3[] vectors = new Vector3[] {
            bodA, bodB, bodC, bodD, bodE, bodF, bodG, bodH, bodI, bodK, 
            pomocneC, bodJ, pomocneJ, bodM, bodL, bodL1, bodL2, zasevekA1, zasevekA2, zasevekA3, zasevekA4,
        bodN, bodO, bodR, bodQ, bodT, bodS, bodS2, bodS3, bodU, bodU1, bodP, bodU2, zasevekB1, zasevekB2, zasevekB3, zasevekB4,
        bodNO, bodXXX, bodY, bodStred, bodZ, bodPomocneYZ, bodV, bodHH, bodPR, bodW, bodX, pomocneV,
        pocatecniSouradnice, rozmeryPlatna
        };
        return vectors;
    }

    public void VectorDrawing(Vector3[] vectors)
    {
        //Zadní část
        DrawLinearBezierCurve(vectors[0], vectors[3]); 
        DrawLinearBezierCurve(vectors[3], vectors[47]);
        DrawLinearBezierCurve(vectors[1], vectors[2]);
        DrawLinearBezierCurve(vectors[3], vectors[4]);
        DrawLinearBezierCurve(vectors[5], vectors[6]);

        Vector3[] stroke = { vectors[2], vectors[4], vectors[6], vectors[8] };
        DrawLine(stroke);
        Vector3[] stroke2 = { vectors[47], vectors[7], vectors[8] };
        DrawLine(stroke2);

        DrawQuadraticBezierCurve(vectors[2], vectors[10], vectors[9]);
        DrawLinearBezierCurve(vectors[11], vectors[9]);
        DrawQuadraticBezierCurve(vectors[0], vectors[12], vectors[11]);

        DoubleDart(vectors[17], vectors[18], vectors[19], vectors[20]);
        DrawLinearBezierCurve(vectors[13], vectors[14]); //test?
        SimpleDart(vectors[15], vectors[13], vectors[16]);

        //Přední část
        DrawLinearBezierCurve(vectors[21], vectors[22]);
        DrawLinearBezierCurve(vectors[41], vectors[23]);

        Vector3[] stroke3 = { vectors[24], vectors[25] };//
        DrawLine(stroke3);

        DrawLinearBezierCurve(vectors[23], vectors[27]);
        DrawLinearBezierCurve(vectors[27], vectors[26]);
        DrawLinearBezierCurve(vectors[26], vectors[25]);

        DrawLinearBezierCurve(vectors[31], vectors[29]);
        DrawLinearBezierCurve(vectors[25], vectors[30]);
        DrawLinearBezierCurve(vectors[30], vectors[21]);

        DrawQuadraticBezierCurve(vectors[41], vectors[42], vectors[39]);
        DrawLinearBezierCurve(vectors[39], vectors[38]);
        SimpleDart(vectors[38], vectors[37], vectors[46]);
        DrawLinearBezierCurve(vectors[46], vectors[43]);
        DrawLinearBezierCurve(vectors[43], vectors[45]);
        DrawQuadraticBezierCurve(vectors[45], vectors[44], vectors[21]);

        DoubleDart(vectors[33], vectors[34], vectors[35], vectors[36]);
    }

    public int[] changeValueY(Vector3[] vectors)
    {
        int maxIndex = vectors.Length - 1;
        int[] vectorsY = new int[vectors.Length];
        for (int i = 0; i < maxIndex; i++)
        {
            vectorsY[i] = (int)Math.Round(vectors[maxIndex].y - vectors[i].y);
        }
        vectorsY[maxIndex] = (int)Math.Round(vectors[maxIndex].y);
        return vectorsY;
    }

    public int[] SplitX (Vector3[] vectors)
    {
        int maxIndex = vectors.Length - 1;
        int[] vectorsX = new int[vectors.Length];
        for (int i = 0; i <= maxIndex; i++)
        {
            vectorsX[i] = (int)Math.Round(vectors[i].x);
        }
        return vectorsX;
    }

   
    void SVGLine2(StreamWriter sw, int[] vectorsX, int[] vectorsY, int num1, int num2, string colour, int dasharray, string name)
    {
        string line = "<path id=\"{6}\" d=\"M {0} {1} L {2} {3}\" stroke=\"{4}\" stroke-width=\"1\" stroke-dasharray=\"{5}\"  fill =\"none\" />";
        sw.WriteLine(line, vectorsX[num1], vectorsY[num1], vectorsX[num2], vectorsY[num2], colour, dasharray, name);
    }
    void SVGLine3(StreamWriter sw, int[] vectorsX, int[] vectorsY, int num1, int num2, int num3, string name)
    {
        string line = "<path id=\"{6}\" d=\"M {0} {1} L {2} {3} L {4} {5}\" stroke=\"black\" stroke-width=\"1\"  fill =\"none\" />";
        sw.WriteLine(line, vectorsX[num1], vectorsY[num1], vectorsX[num2], vectorsY[num2], vectorsX[num3], vectorsY[num3], name);
    }
    void SVGLine4(StreamWriter sw, int[] vectorsX, int[] vectorsY, int num1, int num2, int num3, int num4, string name)
    {
        string line = "<path id=\"{8}\" d=\"M {0} {1} L {2} {3} L {4} {5} L {6} {7}\" stroke=\"black\" stroke-width=\"1\" fill =\"none\" />";
        sw.WriteLine(line, vectorsX[num1], vectorsY[num1], vectorsX[num2], vectorsY[num2], vectorsX[num3], vectorsY[num3], vectorsX[num4], vectorsY[num4], name);
    }
    void SVGDart2(StreamWriter sw, int[] vectorsX, int[] vectorsY, int num1, int num2, int num3, int num4, string name)
    {
        string line = "<path id=\"{8}\" d=\"M {0} {1} L {2} {3} L {4} {5} L {6} {7} L {0} {1}\" stroke=\"black\" stroke-width=\"1\" fill =\"none\" />";
        sw.WriteLine(line, vectorsX[num1], vectorsY[num1], vectorsX[num2], vectorsY[num2], vectorsX[num3], vectorsY[num3], vectorsX[num4], vectorsY[num4], name);
    }
    void SVGLineB(StreamWriter sw, int[] vectorsX, int[] vectorsY, int num1, int num2, int num3, string name)
    {
        string line = "<path id=\"{6}\" d=\"M {0} {1} Q {2} {3} {4} {5}\" stroke=\"black\" stroke-width=\"1\"  fill =\"none\" />";
        sw.WriteLine(line, vectorsX[num1], vectorsY[num1], vectorsX[num2], vectorsY[num2], vectorsX[num3], vectorsY[num3], name);
    }
    void CreateSVGPattern(Vector3[] vectors)
    {
        int[] vectorsX = SplitX(vectors);
        int[] vectorsY = changeValueY(vectors);

        User user = new User();
        string patName = user.GetFileName();
        if (patName != "error")
        {
            string pathSavePattern = user.CompleteFilePath(true, "svg", patName);
            using (StreamWriter sw = new StreamWriter(pathSavePattern, false))
            {
                sw.WriteLine("<svg height=\"{0}\" width=\"{1}\" xmlns=\"http://www.w3.org/2000/svg\">", vectorsY[50] * 38, vectorsX[50] * 38);
                sw.WriteLine("<g transform =\"scale(37.8)\">");
                sw.WriteLine("<rect x=\"5\" y=\"5\" width=\"10\" height=\"10\" stroke=\"grey\" fill =\"none\"/>"); //čtverec 10x10

                //Zadní část
                SVGLine2(sw, vectorsX, vectorsY, 1, 2, "grey", 1, "BC");
                SVGLine2(sw, vectorsX, vectorsY, 3, 4, "grey", 1, "DE");
                SVGLine2(sw, vectorsX, vectorsY, 5, 6, "grey", 1, "FG");
                SVGLine2(sw, vectorsX, vectorsY, 11, 9, "black", 0, "JK");
                SVGLine4(sw, vectorsX, vectorsY, 2, 4, 6, 8, "CEGI");
                SVGLine3(sw, vectorsX, vectorsY, 47, 3, 0, "XDA");
                SVGLine3(sw, vectorsX, vectorsY, 47, 7, 8, "XHI");
                SVGDart2(sw, vectorsX, vectorsY, 17, 19, 18, 20, "ZasevekA");
                SVGLine3(sw, vectorsX, vectorsY, 15, 13, 16, "L1ML2");
                SVGLineB(sw, vectorsX, vectorsY, 2, 10, 9, "CK");
                SVGLineB(sw, vectorsX, vectorsY, 0, 12, 11, "AJ");

                //Přední část
                SVGLine2(sw, vectorsX, vectorsY, 21, 22, "grey", 1, "NO");
                SVGLine2(sw, vectorsX, vectorsY, 31, 29, "grey", 1, "PU");
                SVGLine2(sw, vectorsX, vectorsY, 25, 24, "grey", 1, "TQ");
                SVGLine4(sw, vectorsX, vectorsY, 41, 23, 27, 26, "ZRS2S");
                SVGLine4(sw, vectorsX, vectorsY, 26, 25, 30, 21, "STU1N");
                SVGDart2(sw, vectorsX, vectorsY, 33, 35, 34, 36, "ZasevekB");
                SVGLine3(sw, vectorsX, vectorsY, 46, 37, 38, "W.NO.XXX");
                SVGLine2(sw, vectorsX, vectorsY, 38, 39, "black", 0, "XXXY");
                SVGLineB(sw, vectorsX, vectorsY, 39, 42, 41, "YZ");
                SVGLine2(sw, vectorsX, vectorsY, 43, 46, "black", 0, "VW");
                SVGLineB(sw, vectorsX, vectorsY, 21, 44, 45, "N.PR");
                SVGLineB(sw, vectorsX, vectorsY, 43, 48, 45, "V.PR");
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
    void SimpleDart(Vector3 bod1, Vector3 vrchol, Vector3 bod2)
    {
        DrawLinearBezierCurve(bod1, vrchol);
        DrawLinearBezierCurve(vrchol, bod2);
    }

    void DoubleDart(Vector3 zasevek1, Vector3 zasevek2, Vector3 zasevek3, Vector3 zasevek4)
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

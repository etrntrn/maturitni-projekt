using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCalculation : MonoBehaviour
{
    static int obvodHrudnikuX;
    static int obvodPasuX;
    static int obvodSeduX;
    static int delkaZad;
    static int delkaOdevu;
    static int sirkaZadX;
    static int sirkaRamene;

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

    public Dictionary<string,Vector3> Calculation(User user)
    {
        UserToLocal(user);
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
        Vector3 bodB = new Vector3(souradniceXbodB, souradniceYbodC, 0);
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
        int souradniceXbodW = 0;
        if ((srv3 * srv3) >= (VW * VW))
            souradniceXbodW = (int)Math.Round(souradniceXbodV + Math.Sqrt((srv3 * srv3) - (VW * VW)));
        else
            souradniceXbodW = (int)Math.Round(souradniceXbodV + Math.Sqrt((VW * VW) - (srv3 * srv3)));
        Vector3 bodW = new Vector3(souradniceXbodW, souradniceYbodW, 0);

        Dictionary<string, Vector3> vectors = new Dictionary<string, Vector3>
        {
            { nameof(bodA), bodA }, { nameof(bodB), bodB }, { nameof(bodC), bodC }, { nameof(bodD), bodD }, { nameof(bodE), bodE }, { nameof(bodF), bodF },
            { nameof(bodG), bodG }, { nameof(bodH), bodH }, { nameof(bodI), bodI }, { nameof(bodK), bodK },{ nameof(pomocneC), pomocneC }, { nameof(bodJ), bodJ },
            { nameof(pomocneJ), pomocneJ }, { nameof(bodM), bodM }, { nameof(bodL), bodL }, { nameof(bodL1), bodL1 }, { nameof(bodL2), bodL2 },
            { nameof(zasevekA1), zasevekA1 }, { nameof(zasevekA2), zasevekA2 }, { nameof(zasevekA3), zasevekA3 }, { nameof(zasevekA4), zasevekA4 },
            { nameof(bodN), bodN }, { nameof(bodO), bodO }, { nameof(bodR), bodR }, { nameof(bodQ), bodQ }, { nameof(bodT), bodT }, { nameof(bodS), bodS },
            { nameof(bodS2), bodS2 }, { nameof(bodS3), bodS3 }, { nameof(bodU), bodU }, { nameof(bodU1), bodU1 }, { nameof(bodP), bodP }, { nameof(bodU2),bodU2 },
            { nameof(zasevekB1), zasevekB1 }, { nameof(zasevekB2), zasevekB2 }, { nameof(zasevekB3), zasevekB3 }, { nameof(zasevekB4), zasevekB4 },
            { nameof(bodNO), bodNO }, { nameof(bodXXX), bodXXX }, { nameof(bodY), bodY }, { nameof(bodStred), bodStred }, { nameof(bodZ), bodZ },
            { nameof(bodPomocneYZ), bodPomocneYZ }, { nameof(bodV), bodV }, { nameof(bodHH), bodHH }, { nameof(bodPR), bodPR }, { nameof(bodW), bodW },
            { nameof(bodX), bodX }, { nameof(pomocneV), pomocneV }, { nameof(pocatecniSouradnice), pocatecniSouradnice }, { nameof(rozmeryPlatna), rozmeryPlatna }
        };

        return vectors;
    }

    /*public Dictionary<string, Vector3> ArrayToDictionary(Vector3[] vectors)
    {
        Dictionary<string, Vector3> vectorCol = new Dictionary<string, Vector3>();
        foreach (Vector3 vct in vectors)
        {
            vectorCol.Add(nameof(vct), vct);
        }
        return vectorCol;
    }*/

}

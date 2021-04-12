using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DataTreatment : MonoBehaviour
{
    User user;
    //*public User user;
     
   /* public int obvodPasu;
    public int vyskaPostavy;
    public int obvodHrudniku;
    public int obvodSedu;
    public int delkaZad;
    public int delkaOdevu;
    public int sirkaZad;
    public int sirkaRamene;
    public int delkaRukavu;*/

    public int[] measures;
    //public TextMeshProUGUI 
    //= inOp.GetComponent<Text>();
    
    
    public TMP_InputField inOp;
    public TMP_InputField inVp;
    public TMP_InputField inOh;
    public TMP_InputField inOs;
    public TMP_InputField inDz;
    public TMP_InputField inDo;
    public TMP_InputField inSz;
    public TMP_InputField inSr;
    public TMP_InputField inDr;

    public string tmp;
    public int tmp2;
    public string textFromInput;

    // Start is called before the first frame update
    void Start()
    {
         user = gameObject.AddComponent<User>(); //*
        //pokus = inOp.GetComponent<Text>().text;
        //inOp.GetComponent<Text>().text = "86";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NoteUserMeasures()
    {
        user.obvodPasu = TestingManager(inOp);
        user.vyskaPostavy = TestingManager(inVp);
        user.obvodHrudniku = TestingManager(inOh);
        user.obvodSedu = TestingManager(inOs);
        user.delkaZad = TestingManager(inDz);
        user.delkaOdevu = TestingManager(inDo);
        user.sirkaZad = TestingManager(inSz);
        user.sirkaRamene = TestingManager(inSr);
        user.delkaRukavu = TestingManager(inDr);
        Debug.Log(user.obvodPasu);
        Debug.Log(user.vyskaPostavy);
        Debug.Log(user.obvodHrudniku);
        Debug.Log(user.obvodSedu);
        Debug.Log(user.delkaZad);
        Debug.Log(user.delkaOdevu);
        Debug.Log(user.sirkaZad);
        Debug.Log(user.sirkaRamene);
        Debug.Log(user.delkaRukavu);
        //int[] field = { user.obvodPasu, user.obvodPasu, user.obvodPasu, user.obvodPasu, user.obvodPasu, user.obvodPasu, user.obvodPasu, user.obvodPasu, user.obvodPasu };

        if (user.obvodPasu <= 0 || user.vyskaPostavy <= 0 || user.obvodHrudniku <= 0 || user.obvodSedu <= 0 || user.delkaZad <= 0 || user.delkaOdevu <= 0 || user.sirkaZad <= 0 || user.sirkaRamene <= 0 || user.delkaRukavu <= 0)
        {
            //DisplayUserInput(inOp, obvodPasu);
            //user.GetData(field);
            SceneManager.LoadScene("InputErrorScene");
            //DisplayUserInput(inOp, obvodPasu); - to musí přijít k Zkusit znovu button
        }
        else
        {
            user.SaveData();
            SceneManager.LoadScene("DrawingScene");
        }

        //measures = { obvodPasu, vyskaPostavy, obvodHrudniku, obvodSedu, delkaZad, delkaOdevu, sirkaZad, sirkaRamene, delkaRukavu };
    }

    public void ZkusitZnovuButton()
    {
        SceneManager.LoadScene("Measures");
        //DisplayUserInput(inOp, obvodPasu); - no, tak na tomhle se ještě bude muset zapracovat
    }

    public void DisplayUserInput(TMP_InputField inputField, int number)
    {
        string text = number.ToString();
        inputField.text = text;
    }
    
    public int TestingManager(TMP_InputField inpField)
    {
        tmp = GetText(inpField);
        tmp2 = InputNumberTesting(tmp);
        Debug.Log(tmp2);
        return tmp2;
    }

    public string GetText(TMP_InputField inputField)
    {
        //if (inputField.GetComponent<Text>().text != null)
        //{
        //textFromInput = inputField.GetComponent<Text>().text;
        textFromInput = inputField.text;
        return textFromInput;
        //}
        //else return "0";
    }

    public int InputNumberTesting (string textFromInput)
    {
        int.TryParse(textFromInput, out int number);
        return number;
    }
    
    /*public string theName;
    public GameObject inputField;
    
    public void StoreName()
    {
        theName = inputField.GetComponent<Text>().text;
        Debug.Log(theName);
    }*/
}

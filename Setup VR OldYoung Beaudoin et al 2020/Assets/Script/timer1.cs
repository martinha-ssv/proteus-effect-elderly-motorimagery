using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System;

public class timer1 : MonoBehaviour {
    public Text TimerText;
    //private float startTime;
    public float initial = 0;
    public string Condition;
    public string SavePath = "C:/Users/Data.txt";
    private float count = 0;
    private float rep = 0;
    private float tempsRep = 0;
    private float onset = 0;
    public float delai = 0;
    public float timer = 2;
    public bool Flag = false;
    public GameObject chaise1;
    public GameObject chaise2;
    public GameObject chaise3;
    public GameObject chaise4;
    public GameObject chaise5;
    public GameObject rideau;
    float startTime = 0;
    float waitFor = 5;
    bool timerStart = false;

    // Use this for initialization
    void Start()
    {
        //startTime = Time.time;	
        rideau.SetActive(false);
        chaise1.SetActive(false);
        chaise2.SetActive(false);
        chaise3.SetActive(false);
        chaise4.SetActive(false);
        chaise5.SetActive(false);
        //	Debug.Log ("startTime" + startTime);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            chaise1.SetActive(false);
            rideau.SetActive(false);
            chaise2.SetActive(false);
            chaise3.SetActive(false);
            chaise4.SetActive(false);
            chaise5.SetActive(false);
            TimerText.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            chaise1.SetActive(false);
            chaise2.SetActive(false);
            rideau.SetActive(false);
            chaise3.SetActive(false);
            chaise4.SetActive(false);
            chaise5.SetActive(true);
            Condition = "distance5";
            TimerText.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            chaise1.SetActive(true);
            rideau.SetActive(false);
            chaise2.SetActive(false);
            chaise3.SetActive(false);
            chaise4.SetActive(false);
            chaise5.SetActive(false);
            Condition = "distance4";
            TimerText.color = Color.red;

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            chaise1.SetActive(false);
            chaise2.SetActive(true);
            rideau.SetActive(false);
            chaise3.SetActive(false);
            chaise4.SetActive(false);
            chaise5.SetActive(false);
            Condition = "distance3";
            TimerText.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            chaise1.SetActive(false);
            chaise2.SetActive(false);
            chaise3.SetActive(true);
            chaise4.SetActive(false);
            chaise5.SetActive(false);
            rideau.SetActive(false);
            Condition = "distance2";
            TimerText.color = Color.red;

        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            chaise1.SetActive(false);
            chaise2.SetActive(false);
            chaise3.SetActive(false);
            chaise4.SetActive(true);
            chaise5.SetActive(false);
            rideau.SetActive(false);
            Condition = "distance1";
            TimerText.color = Color.red;

        }




        if (Input.GetKeyDown(KeyCode.Space) && count == 0)
        {
            Flag = !Flag;
            initial = Time.time;
            Debug.Log("initial  : " + initial);
            count = count + 1;
            rideau.SetActive(true);

        }

        else if (Input.GetKeyDown(KeyCode.Space) && count == 1)
        {
            rep = Time.time;
            Flag = !Flag;
            Debug.Log("reponset   :  " + rep);
            tempsRep = rep - initial;
            Debug.Log("tempsRep  :  " + tempsRep);
            Debug.Log("initial  : " + initial);
            count = count - 1;
            rideau.SetActive(false);
            chaise1.SetActive(false);
            chaise2.SetActive(false);
            chaise3.SetActive(false);
            chaise4.SetActive(false);
            chaise5.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StreamWriter sw = new StreamWriter(SavePath, true); //"true" for appending the file
                                                                //write header at Trial 1
                                                                //Debug.Log("Trial : " + Trial);
                                                                // set color blue text 
            TimerText.color = Color.blue;

            string Data = string.Concat(Condition, "  ;  ", "temps de réponse :; ", tempsRep, "  ;   ", DateTime.Now.ToString("G"));
            sw.WriteLine(Data);
            rideau.SetActive(true);
            sw.Flush();
            sw.Close();

        }

        if (Flag)
        {
            float t = Time.time - initial;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            TimerText.text = minutes + ":" + seconds;
            //TimerText.text = "tempsRep"+tempsRep;	




        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System;

using LSL;
using Assets.LSL4Unity.Scripts;
using Assets.LSL4Unity.Scripts.Common;



public class timer1_LSL : MonoBehaviour {
    public Text TimerText;
    //private float startTime;
    public float initial = 0;
    public string Condition;
    public string SavePath = "C:/Users/Data.txt";
    private float count = 0;
	private float countemb = 0;
    private float rep = 0;
    private float tempsRep = 0;
    //private float onset = 0;
    public float delai = 0;
    public float timer = 2;
    public bool Flag = false;
    public GameObject chaise1;
    public GameObject chaise2;
    public GameObject chaise3;
    public GameObject chaise4;
    public GameObject chaise5;
    public GameObject rideau;
//    float startTime = 0;
//    float waitFor = 5;
//    bool timerStart = false;



	private const string unique_source_id = "D256CFBDBA3145978CFA641403219531";
	private liblsl.StreamOutlet outlet;
	private liblsl.StreamInfo streamInfo;
	private double nominal_srate = liblsl.IRREGULAR_RATE;

	public string StreamName = "BeMoBI.Unity.Orientation.<Add_a_entity_id_here>";
	private string StreamType = "Markers";
	private int ChannelCount = 1;
	private string[] sample;




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

		// initialize the array once
		sample = new string[ChannelCount];
		streamInfo = new liblsl.StreamInfo(StreamName, StreamType, ChannelCount, nominal_srate, liblsl.channel_format_t.cf_string, unique_source_id);
		outlet = new liblsl.StreamOutlet(streamInfo);

    }



    // Update is called once per frame
	void Update()
	{

		if (Input.GetKeyDown (KeyCode.W)) {
			chaise1.SetActive (false);
			rideau.SetActive (false);
			chaise2.SetActive (false);
			chaise3.SetActive (false);
			chaise4.SetActive (false);
			chaise5.SetActive (false);
			TimerText.color = Color.red;
		}


		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			chaise1.SetActive (false);
			chaise2.SetActive (false);
			rideau.SetActive (false);
			chaise3.SetActive (false);
			chaise4.SetActive (false);
			chaise5.SetActive (true);
			Condition = "distance5";
			TimerText.color = Color.red;

			Write ("D5");
		}


		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			chaise1.SetActive (false);
			rideau.SetActive (false);
			chaise2.SetActive (false);
			chaise3.SetActive (false);
			chaise4.SetActive (true);
			chaise5.SetActive (false);
			Condition = "distance4";
			TimerText.color = Color.red;

			Write ("D4");

		}


		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			chaise1.SetActive (false);
			chaise2.SetActive (false);
			rideau.SetActive (false);
			chaise3.SetActive (true);
			chaise4.SetActive (false);
			chaise5.SetActive (false);
			Condition = "distance3";
			TimerText.color = Color.red;

			Write ("D3");
		}


		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			chaise1.SetActive (false);
			chaise2.SetActive (true);
			chaise3.SetActive (false);
			chaise4.SetActive (false);
			chaise5.SetActive (false);
			rideau.SetActive (false);
			Condition = "distance2";
			TimerText.color = Color.red;

			Write ("D2");
		}


		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			chaise1.SetActive (true);
			chaise2.SetActive (false);
			chaise3.SetActive (false);
			chaise4.SetActive (false);
			chaise5.SetActive (false);
			rideau.SetActive (false);
			Condition = "distance1";
			TimerText.color = Color.red;

			Write ("D1");
		}




		if (Input.GetKeyDown (KeyCode.Space) && count == 0) {
			Flag = !Flag;
			initial = Time.time;
			Debug.Log ("initial  : " + initial);
			count = count + 1;
			rideau.SetActive (true);

			Write ("StartTrial");
		} else if (Input.GetKeyDown (KeyCode.Space) && count == 1) {
			rep = Time.time;
			Flag = !Flag;
			Debug.Log ("reponset   :  " + rep);
			tempsRep = rep - initial;
			Debug.Log ("tempsRep  :  " + tempsRep);
			Debug.Log ("initial  : " + initial);
			count = count - 1;
			rideau.SetActive (false);
			chaise1.SetActive (false);
			chaise2.SetActive (false);
			chaise3.SetActive (false);
			chaise4.SetActive (false);
			chaise5.SetActive (false);

			Write ("EndTrial");
		}



		// 
		if (Input.GetKeyDown (KeyCode.E) && countemb == 0) {
			Write ("StartEmb");
			countemb = countemb + 1;
		} else if (Input.GetKeyDown (KeyCode.E) && countemb == 1) {
			Write ("EndEmb");
			countemb = countemb - 1;
		}


			


		if (Input.GetKeyDown (KeyCode.Return)) {
			StreamWriter sw = new StreamWriter (SavePath, true); //"true" for appending the file
			//write header at Trial 1
			//Debug.Log("Trial : " + Trial);
			// set color blue text 
			TimerText.color = Color.blue;

			string Data = string.Concat (Condition, "  ;  ", "temps de réponse :; ", tempsRep, "  ;   ", DateTime.Now.ToString ("G"));
			sw.WriteLine (Data);
			rideau.SetActive (true);
			sw.Flush ();
			sw.Close ();

		}

		if (Flag) {
			float t = Time.time - initial;
			string minutes = ((int)t / 60).ToString ();
			string seconds = (t % 60).ToString ("f2");
			TimerText.text = minutes + ":" + seconds;
			//TimerText.text = "tempsRep"+tempsRep;	

		}
	}

	public void Write(string marker)
	{
		sample[0] = marker;
		outlet.push_sample(sample);
		Debug.Log("Sent LSL Marker: " + marker);
	}

	public void Write(string marker, double customTimeStamp)
	{
		sample[0] = marker;
		outlet.push_sample(sample, customTimeStamp);
	}

	public void Write(string marker, float customTimeStamp)
	{
		sample[0] = marker;
		outlet.push_sample(sample, customTimeStamp);
	}
    
}
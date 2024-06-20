using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine.VR;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
public delegate void SimpleDelegate();

public class R2V2 : MonoBehaviour
{

    private Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private byte[] _recieveBuffer = new byte[8142];
    //public GameObject Target;
    public GameObject[] Hands;
    private Vector3[] HandsPositions;
    private Vector3[] HandsRotations;
    private float tour = 1;
    private float compte = 0;
    public Text RecText;
    string result = "";
    //Process myProcess;
    System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
    //public Text txt;
    public GameObject myText;
    private string angle;
    string affiche = "";
    //string fileName = "MyFile.txt";
    string ligne;
    public string stringToEdit = "Hello World";
    public static string mouvement = "0";


    public static void setmouvement(string m)
    {
        mouvement = m;

    }

    void OnApplicationQuit()
    {

        //myProcess.Close();
        //System.Diagnostics.Process.LeaveDebugMode();
        if (myProcess.StartInfo.FileName == "C:\\PolhemusTCP\\Polhemus_Server_Ex.exe")
        {
            myProcess.Kill();
        }
    }





    // Use this for initialization
    void Start()
    {

        //Camera.main.transform.position = new Vector3(1.233307f, 2.882927f, 3.08115f);

        //System.Diagnostics.Process.Start("C:/PolhemusTCP/Polhemus_Server_Ex.exe");
        myProcess.StartInfo.FileName = "C:\\PolhemusTCP\\Polhemus_Server_Ex.exe";
        myProcess.Start();
        System.Threading.Thread.Sleep(5000);
        SetupServer();
        SendData(System.Text.Encoding.UTF8.GetBytes("1"));
        HandsPositions = new Vector3[Hands.Length];
        HandsRotations = new Vector3[Hands.Length];

        //txt = myText.GetComponent<Text>();
        //txt.text = "aaaaa";
        //myText.GetComponent<Text>().text = "ffff";



    }


    private void SetupServer()
    {
        try
        {
            _clientSocket.Connect(IPAddress.Parse("127.0.0.1"), 27016);
        }
        catch (SocketException ex)
        {
            print(ex.Message);
        }

        _clientSocket.BeginReceive(_recieveBuffer, 0, _recieveBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);

    }

    private void ReceiveCallback(IAsyncResult AR)
    {
        //Check how much bytes are recieved and call EndRecieve to finalize handshake
        int recieved = _clientSocket.EndReceive(AR);

        if (recieved <= 0)
            return;

        //Copy the recieved data into new buffer , to avoid null bytes
        byte[] recData = new byte[recieved];
        Buffer.BlockCopy(_recieveBuffer, 0, recData, 0, recieved);

        //Process data here the way you want , all your bytes will be stored in recData



        result = System.Text.Encoding.UTF8.GetString(recData);
        //print(result);
        // startUpdate(result);
        //print(result);

        SimpleDelegate simpleDelegate = new SimpleDelegate(startUpdate);

        // Invocation
        simpleDelegate();



        //Start receiving again
        _clientSocket.BeginReceive(_recieveBuffer, 0, _recieveBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
    }

    private void startUpdate()
    {
        //Camera.main.transform.position = new Vector3(1.233307f, 2.882927f, 3.08115f);
        UpdatePositionRotation(result);

    }






    void UpdatePositionRotation(string str)
    {
        // print("bonjour");
        if (str != "")
        {
            string[] val = str.Split(' ');
            string x = val[0];
            int signe = int.Parse(x);
            //print(x);
            //string mi = str.Substring(0, 56);
            //ligne = mi;
            ligne = int.Parse(val[0]) + " " + double.Parse(val[1]) + " " + double.Parse(val[2]) + " " + float.Parse(val[3]) + " " + float.Parse(val[4]) + " " + float.Parse(val[5]) + " " + float.Parse(val[6]) + " " + mouvement + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond;
            affiche = affiche + ligne + "\r";
            //print(affiche);

            if (signe == 1)
            {
                //HandsPositions[int.Parse(val[0]) - 1] = new Vector3(0, float.Parse(val[2]), 0);
                HandsPositions[int.Parse(val[0]) - 1] = new Vector3(float.Parse(val[1]), float.Parse(val[2]), float.Parse(val[3]));
                HandsRotations[int.Parse(val[0]) - 1] = new Vector3(float.Parse(val[5]), 0, 0);
                ////print(val[5]);
                float an = -float.Parse(val[5]);
                //int signe = int.Parse(x);
                angle = "" + an;
            }


            if (signe == 2)
            {
                //HandsPositions[int.Parse(val[0]) - 1] = new Vector3(0, float.Parse(val[2]), 0);
                HandsPositions[int.Parse(val[0]) - 1] = new Vector3(float.Parse(val[1]), float.Parse(val[2]), float.Parse(val[3]));
                HandsRotations[int.Parse(val[0]) - 1] = new Vector3(float.Parse(val[5]), 0, 0);
                ////print(val[5]);
                //angle = val[5];
            }

            //Matrix4x4 = (val[0]);

            /*string v0 = val[0];
            string v1 = val[1];
            string v2 = val[2];
            string v3 = val[3];
            string v4 = val[4];
            string v5 = val[5];
            string v6 = val[6];


            int s0 = int.Parse(v0);
            //float s1 = float.Parse(v1);
            string[] s1 = v1.Split(' ');
            string[] s2 = v2.Split(' ');
            string[] s3 = v3.Split(' ');
            string[] s4 = v4.Split(' ');
            string[] s5 = v5.Split(' ');
            string[] s6 = v6.Split('\t');
            //float s2 = float.Parse(v2);
            //float s3 = float.Parse(v3);
            //float s4 = float.Parse(v4);
            //float s5 = float.Parse(v5);
            //float s6 = float.Parse(v6);

            string path = @"MyTest.txt";

            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                // Create a file to write to.
                string createText = "New file created" + Environment.NewLine;
                File.WriteAllText(path, createText);
            }

            // This text is always added, making the file longer over time
            // if it is not deleted.
            //string appendText = val[6] + "\t" + val[1] + "\t" + val[2] + "\t" + val[3] + "\t" + val[4] + "\t" + val[5] + "\t" + val[6] + "\n";
            //string appendText = s0 + "\t" + s1[0] + "\t" + s2[0] + "\t" + s3[0] + "\t" + s4[0] + "\t" + s5[0] + "\t" + s6[0] + Environment.NewLine;
            string appendText = str + Environment.NewLine;
            //string appendText = s1[0];// + "\t" + s1 + "\t" + s2 + "\t" + s3 + "\t" + s4 + "\t" + s5 + "\t" + s6 + "\n";
            File.AppendAllText(path, appendText);

            // Open the file to read from.
            string readText = File.ReadAllText(path);
            Console.WriteLine(readText);*/




            //Target.transform.position = new Vector3(float.Parse(val[0]), float.Parse(val[1]), float.Parse(val[2]));

        }

        //     yield return new WaitForSeconds(0);

    }


    private void SendData(byte[] data)
    {
        SocketAsyncEventArgs socketAsyncData = new SocketAsyncEventArgs();
        socketAsyncData.SetBuffer(data, 0, data.Length);
        _clientSocket.SendAsync(socketAsyncData);
    }

    static void Apply(string filename1, string affiche1)
    {
        //Texture2D texture = Selection.activeObject as Texture2D;
        //if (texture == null)
        //{
        //    EditorUtility.DisplayDialog(
        //        "Select Texture",
        //        "You Must Select a Texture first!",
        //        "Ok");
        //    return;
        //}
#if UNITY_EDITOR
        //UnityEditor.EditorWindow.GetWindow<YourEditorWindow>(true, "My Editor Window");
        var path = EditorUtility.SaveFilePanel(
                "Save texture as PNG",
                "C:\\",
                filename1 + ".txt",
                "txt");

        string yyy = path;
        var sr = File.CreateText(yyy);
        //var sr = File.CreateText(path);
        sr.WriteLine(affiche1);
        sr.Close();
#endif
        //if (path.Length != 0)
        //{
        //    //var pngData = texture.EncodeToPNG();
        //    if (affiche != null)
        //        File.WriteAllBytes(path, pngData);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //print("aaaaa");
        //print(result);
        for (int i = 0; i < Hands.Length; i++)
        {
            Hands[i].transform.localRotation = Quaternion.Euler(HandsRotations[i]);
            //Hands[i].transform.localPosition = HandsPositions[i];
            ////print(val[5]);
            myText.GetComponent<Text>().text = angle;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            myProcess.Kill();
            //SceneManager.UnloadScene("R2V2_Scene");
            SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("Test_scene");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            compte = compte + 1;
           R2V2.setmouvement("1");
            RecText.color = Color.red;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
           R2V2.setmouvement("0");
            RecText.color = Color.blue;
        }
       
        if (Input.GetKeyDown(KeyCode.RightControl))
        {

            Apply(SceneManager.GetActiveScene().name, affiche);
            R2V2.setmouvement("0");
            //if (File.Exists(fileName))
            //{
            //    UnityEngine.Debug.Log(fileName + " already exists.");
            //    return;
            //}
            //var sr = File.CreateText(fileName);            
            //sr.WriteLine(affiche);
            //sr.Close();
        }



        //Target.transform.position = new Vector3(float.Parse(val[0]), float.Parse(val[1]), float.Parse(val[2]));

    }
}
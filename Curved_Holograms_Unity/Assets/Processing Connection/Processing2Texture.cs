/*---------------------------------------------------------
	# Processing To Texture
	version: 1.0.1
	author: Lucas Cassiano
	website: http://web.media.mit.edu/~cassiano/projects/processing2texture
	creation date: September 27, 2016

	## References
	http://msdn.microsoft.com/de-de/library/bb979228.aspx#ID0E3BAC
   
    ## Receive Udp Packages
    localhost : 8051
-----------------------------------------------------------*/

using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class Processing2Texture : MonoBehaviour {

	// receiving Thread
	private Thread receiveThread;

	// udpclient object
	private UdpClient client;

	public int port=8051; // define > init

	private bool updateTexture=false;
	private Texture2D texture;
	private byte[] receivedBytes;


	// start from shell
	private static void Main()
	{
		Processing2Texture receiveObj=new Processing2Texture();
		receiveObj.init();

		string text="";
		do
		{
			text = Console.ReadLine();
		}
		while(!text.Equals("exit"));
	}

	// start from unity3d
	public void Start()
	{

		init();
	}
		
	void Update(){
		if (updateTexture && receivedBytes!=null) {
			//Texture2D tex = new Texture2D(1000, 400);
			Texture2D tex = new Texture2D(700, 400,  TextureFormat.RGB24, false);
			tex.LoadImage(receivedBytes);
			//tex.LoadRawTextureData(receivedBytes);
			tex.Apply();
			GetComponent<Renderer>().material.mainTexture = tex;
			updateTexture = false;
		}
			
	}
	// init
	private void init()
	{
		receiveThread = new Thread(new ThreadStart(ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start();
	}

	// receive thread
	private  void ReceiveData()
	{

		client = new UdpClient(port);
		while (true)
		{

			try
			{
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
				byte[] data = client.Receive(ref anyIP);

				receivedBytes = data;

				//print(data);
				updateTexture = true;

			}
			catch (Exception err)
			{
				print(err.ToString());
			}
		}
	}

	public Texture2D getTextured(){
		return texture;
	}
		
}
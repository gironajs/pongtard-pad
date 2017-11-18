using System.Collections;
using UnityEngine;
using SocketIO;
using System.Collections.Generic;

public class TestSocketIO : MonoBehaviour
{
	private SocketIOComponent socket;
	private string playerName = "cacauet";

	public void Start() 
	{
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();

		socket.On("open", TestOpen);
		socket.On("boop", TestBoop);
		socket.On("error", TestError);
		socket.On("close", TestClose);
		socket.On("debuga", TestDebuga);
		
		StartCoroutine("BeepBoop");
	}

	private IEnumerator BeepBoop()
	{
		// wait 1 seconds and continue
		yield return new WaitForSeconds(1);
		
		socket.Emit("beep");
		
		//socket.Emit("playerName", playerName);

		// wait 3 seconds and continue
		yield return new WaitForSeconds(3);
		
		socket.Emit("beep");
		
		// wait 2 seconds and continue
		yield return new WaitForSeconds(2);
		
		socket.Emit("beep");
		
		// wait ONE FRAME and continue
		yield return null;
		
		socket.Emit("beep");
		socket.Emit("beep");
	}

	private void TestDebuga(SocketIOEvent e)
	{
		Debug.Log ("Hola Jeeep!!" + Time.timeSinceLevelLoad);
	}

	public void TestOpen(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
	}
	
	public void TestBoop(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Boop received: " + e.name + " " + e.data);

		if (e.data == null) { return; }

		Debug.Log(
			"#####################################################" +
			"THIS: " + e.data.GetField("this").str +
			"#####################################################"
		);
	}
	
	public void TestError(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
	}
	
	public void TestClose(SocketIOEvent e)
	{	
		Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
	}

	private void OnGUI()
	{
		if(GUI.Button (new Rect(100, 100, 100, 100),"Salta"))
		{
			Dictionary<string, string> data = new Dictionary<string, string>();
			data["mail"] = "manel@capdevila.com";
			data["pass"] = "tapundimuk";
			socket.Emit("salta", new JSONObject(data));
		}
		if(GUI.Button (new Rect(200, 100, 100, 100),"Pinta"))
		{
			socket.Emit("pinta");
		}
//		if(GUI.Button (new Rect(300, 100, 100, 100),"Close"))
//		{
//			socket.Close();
//		}
	}
}

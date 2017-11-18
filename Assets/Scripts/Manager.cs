using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class Manager : MonoBehaviour
{
    /*Add listener:
     *mainboard{msg:"noserver"} : registres pero no hi ha server 
     *mainboard{msg:"down"}     : mainboard caigut.
     *falta usuari desconnectat.
     */
    public Canvas padCanvas;
    public Canvas submitCanvas;
    public InputField input;

    public SocketIOComponent socket;

    public void registerPad()
    {
        
    }
    
    public void SubmitNick()
    {
        Debug.Log(input.text);

        if (input.text == "")
            input.text = "Default";

        StartCoroutine(checkConnection(input.text));
    }

    public void MoveUp()
    {
        socket.Emit("up");
    }

    public void MoveDown()
    {
        socket.Emit("down");
    }

    public void StopMove()
    {
        socket.Emit("stop");
    }

    private IEnumerator checkConnection(string name)
    {
        yield return new WaitForSeconds(1f);
        JSONObject json = new JSONObject();
        string nick = name;// "pad_Sim";
        json.AddField("nick", nick);
        socket.Emit("register", json);

        submitCanvas.gameObject.SetActive(false);
        padCanvas.gameObject.SetActive(true);
    }
    //Emit: up, down, stop;
}

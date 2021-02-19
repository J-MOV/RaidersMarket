
using WebSocketSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineConnection : MonoBehaviour
{
    WebSocket ws;
    void Start()
    {
        ws = new WebSocket("ws://localhost:8080");

        ws.OnOpen += (test, te) => {
            Debug.Log("Connected");

        };

        ws.OnClose += (test, t) => {
            Debug.Log("Connection closed");
        };

        ws.Connect();
    }

    void Update()
    {

        if (Input.GetKeyDown("a")) {
            Debug.Log("Trying to send a message");
            ws.Send("Test");
        }
        
    }
}

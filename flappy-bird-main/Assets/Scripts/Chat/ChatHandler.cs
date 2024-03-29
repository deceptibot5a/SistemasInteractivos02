using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;

public class ChatHandler : MonoBehaviour
{
    List<GameObject> existingMessages = new List<GameObject>();
    public MessageDatabase database;

    public TMP_Text senderIF;
    //public TMP_InputField senderIF;
    public TMP_InputField textIF;


    public GameObject messagePrefab;
    public Transform messageContainer;

    private void Start() {
        database.ListenForMessage(InstantiateMessage, Debug.Log);
    }

    public void SendMessage() => database.PostMessage(new Message(senderIF.text, textIF.text),
        callback:() => Debug.Log("Message was sent"), Debug.Log);

    private void InstantiateMessage(Message message) {
        var newMessage = Instantiate(messagePrefab, transform.position, Quaternion.identity);
        newMessage.transform.SetParent(messageContainer, false);
        newMessage.GetComponent<TextMeshProUGUI>().text =  $"{message.sender}: {message.text}";
        existingMessages.Add(newMessage);
        MessageRefresh();
    }

    private void MessageRefresh() {
        if (existingMessages.Count > 18) {
            Destroy(existingMessages[0]);
            existingMessages.RemoveAt(0);
        }
    }   
}





using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatHandler : MonoBehaviour
{
    public MessageDatabase database;

    public TMP_InputField senderIF;
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
    }
}





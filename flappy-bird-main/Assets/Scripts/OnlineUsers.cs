using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class OnlineUsers : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;

    private void Reset() {
        _label = GetComponent<TMP_Text>();
    }

    void Start() {
        var dref = FirebaseDatabase.DefaultInstance.GetReference("users-online");

        dref.ChildAdded += HandleChildAdded;
        dref.ChildRemoved += HandleChildRemoved;
    }
    private void HandleChildAdded(object sender, ChildChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        _label.text = (string)args.Snapshot.Value;
        Debug.Log("Usuario conectado: " + args.Snapshot.Value);
    }
    private void HandleChildRemoved(object sender, ChildChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        Debug.Log("Usuario desconectado: " + args.Snapshot.Value);
    }

    void OnApplicationQuit() {
        FirebaseDatabase.DefaultInstance.RootReference.Child("users-online").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).SetValueAsync(null);

    }
}
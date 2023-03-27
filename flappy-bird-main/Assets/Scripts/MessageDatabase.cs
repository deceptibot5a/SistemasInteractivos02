using Firebase.Database;
using Firebase;
using UnityEngine;
using System;

public class MessageDatabase : MonoBehaviour
{
    private DatabaseReference reference;

    private void Awake() {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void PostMessage(Message message, Action callback, Action<AggregateException> fallback) {

        var messageJSON = StringSerialization.Serialize(typeof(Message), message);
        reference.Child("messages").Push().SetRawJsonValueAsync(messageJSON).ContinueWith(task => {
            if (task.IsCanceled || task.IsFaulted) fallback(task.Exception);
            else callback();
        });
    }

    public void ListenForMessage(Action<Message> callback, Action<AggregateException> fallback) {

        void CurrentListener(object o, ChildChangedEventArgs args) {
            if (args.DatabaseError != null) fallback(new AggregateException(new Exception(args.DatabaseError.Message)));
            else callback(StringSerialization.Deserialize(typeof(Message), args.Snapshot.GetRawJsonValue()) as Message);
        }
        reference.Child("messages").ChildAdded += CurrentListener;
    }

}

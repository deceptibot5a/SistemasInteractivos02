using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int score = 0;
    public static int HighestScore = 0;

    void Start() {
        score = 0;
        SetHighestScore();
    }
    void Update() {
        GetComponent<UnityEngine.UI.Text>().text = score.ToString();
    }
    private void SetHighestScore() {
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        FirebaseDatabase.DefaultInstance
            .GetReference("users/" + userId + "/score")
            .GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted) {
                    Debug.Log(task.Exception);
                    HighestScore = 0;
                } else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;

                    Debug.Log(snapshot.Value);

                    HighestScore = (int)snapshot.Value;
                }
            });
    }
    public static void WriteNewScore() {
        if(Score.score > HighestScore) {
            HighestScore = Score.score;
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
            string UserId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
            reference.Child("users").Child(UserId).Child("score").SetValueAsync(Score.score);
        }
    }
    public void GetUsersHighestScores() {
        FirebaseDatabase.DefaultInstance.GetReference("users").OrderByChild("score").LimitToLast(5).
            GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted) {
                    //Handle the error...
                } else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    Debug.Log(snapshot);
                    foreach (var userDoc in (Dictionary<string, object>)snapshot.Value) {
                        var userObject = (Dictionary<string, object>)snapshot.Value;
                        Debug.Log(userObject["username"] + ":" + userObject["score"]);
                    }
                }
            });
    }
}

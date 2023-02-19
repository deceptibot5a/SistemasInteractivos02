using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignUpButton : MonoBehaviour
{
    [SerializeField] private Button _signUpButton;
    private Coroutine _signUpCoroutine;

    private void Reset() {
        _signUpButton = GetComponent<Button>();
    }
    void Start() {
        _signUpButton.onClick.AddListener(HandleRegistrationButtonClicked);  
    }
    private void HandleRegistrationButtonClicked() {
        string email = GameObject.Find("InputEmail").GetComponent<TMP_InputField>().text;
        string password = GameObject.Find("InputPassword").GetComponent<TMP_InputField>().text;

        _signUpCoroutine = StartCoroutine(SignUpUser(email, password));
    }
    private IEnumerator SignUpUser(string email, string password) {
        var auth = FirebaseAuth.DefaultInstance;
        var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => registerTask.IsCompleted);

        if (registerTask.Exception != null) {
            Debug.LogWarning($"Failed to register task {registerTask.Exception}");
        } else {
            Debug.Log($"Succesfully registered user {registerTask.Result.Email}");
            //Registrar los datos del usuario en el database
        }
    }
    void Update() {
        
    }
}

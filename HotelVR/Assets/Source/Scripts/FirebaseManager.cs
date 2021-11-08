using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase;
using TMPro;
using Firebase.Database;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance;

    // Firebase Variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;

    public DatabaseReference dbRef;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void Start()
    {
        UIManager.instance.ShowLoadingScreen();
        StartCoroutine(CheckAndFixDependencies());

        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private IEnumerator CheckAndFixDependencies()
    {
        var checkAndFixDependenciesTask = FirebaseApp.CheckAndFixDependenciesAsync();
        yield return new WaitUntil(predicate: () => checkAndFixDependenciesTask.IsCompleted);

        var dependenciesResult = checkAndFixDependenciesTask.Result;
        if (dependenciesResult == DependencyStatus.Available)
        {
            InitializeFirebase();
        }
        else
        {
            Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
        }
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");

        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        if (PrefInfo.GetAutoLogin())
        {
            StartCoroutine(CheckAutoLogin());

            auth.StateChanged += AuthStateChanged;
            AuthStateChanged(this, null);
        }
        else
        {
            UIManager.instance.ShowLoginScreen();
        }
    }

    #region Login function
    private void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != User)
        {
            bool signedIn = User != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && User != null)
            {
                Debug.Log("Signed out " + User.UserId);
            }

            User = auth.CurrentUser;
            if (signedIn)
            {
                Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            }
        }
    }

    private IEnumerator CheckAutoLogin()
    {
        yield return new WaitForEndOfFrame();

        if (User != null)
        {
            var reloadUserTask = User.ReloadAsync();
            yield return new WaitUntil(predicate: () => reloadUserTask.IsCompleted);
            AutoLogin();
        }
        else
        {
            UIManager.instance.ShowLoginScreen();
        }
    }

    private void AutoLogin()
    {
        if (User != null)
        {
            UIManager.instance.ShowMenuScreen();
        }
        else
        {
            UIManager.instance.ShowLoginScreen();
        }
    }

    //Function for the login button
    public void Login(string emailLogin, string passwordLogin)
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(DoLogin(emailLogin, passwordLogin));
    }

    private IEnumerator DoLogin(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }

            MessageUI.instance.ShowWarning(message);
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            MessageUI.instance.ShowConfirm("Logged in");
            UIManager.instance.ShowMenuScreen();
        }
    }

    #endregion

    #region Register function
    //Function for the register button
    public void RegisterButton(string emailRegister, string passwordRegister, string passwordVerifyRegister, string usernameRegister)
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(DoRegister(emailRegister, passwordRegister, passwordVerifyRegister, usernameRegister));
    }

    private IEnumerator DoRegister(string _email, string _password, string _passwordVerify, string _username)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            MessageUI.instance.ShowWarning("Missing Username");
        }
        else if (_password != _passwordVerify)
        {
            //If the password does not match show a warning
            MessageUI.instance.ShowWarning("Password Does Not Match!");
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                MessageUI.instance.ShowWarning(message);
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        MessageUI.instance.ShowWarning("Username Set Failed!");
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        UIManager.instance.ShowLoginScreen();
                        MessageUI.instance.ShowConfirm("Register Success!");
                    }
                }
            }
        }
    }

    /*// Function for logout button
    public void LogoutButton()
    {

    }

    private IEnumerator DoLogout()
    {
        auth
    }*/

    #endregion

    #region WriteData

    public void WriteNewResultTesting(float mark)
    {
        string username = User.DisplayName;
        TestClass tmp = new TestClass(username, mark);

        string json = JsonUtility.ToJson(tmp);

        // create a new node with new username and new mark
        //dbRef.Child("Mark").Child(User.UserId).SetRawJsonValueAsync(json);

        // update new value for username reference
        dbRef.Child("Mark").Child(User.UserId).Child("username").SetValueAsync("newName");
    }

    [SerializeField] private float mark;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(User.DisplayName);
            WriteNewResultTesting(mark);
        }

        /*if (Input.GetKeyDown(KeyCode.L))
        {
            PrefInfo.SetAutoLogin(1);
            Login("test@gmail.com", "123456");
        }*/
    }

    #endregion
}

[System.Serializable]
public class TestClass
{
    public string username;
    public float mark;

    public TestClass(string username, float mark)
    {
        this.username = username;
        this.mark = mark;
    }
}
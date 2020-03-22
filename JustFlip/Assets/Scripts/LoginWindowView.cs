using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics ;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;



public class LoginWindowView : MonoBehaviour {
    //Debug Flag to simulate a reset
    public bool clearPlayerPrefs;

    //Meta fields for objects in the UI
    public InputField username;
    public InputField password;
    public InputField confirmPassword;
    public InputField nickname ;
    
    public Button loginButton;
    public Button registerButton;
    public Button cancelRegisterButton;
    public Button startGameButton ;
    public Toggle rememberMe;

    public Text loginProgressInformation ;
    public Text loginErrorInformation ;
    public Text passwordCorrectionInformation ;
    public Text passwordPlaceHolder ;
    public Text passwordCorrectionPlaceHolder ;
    public Text loginPlaceHolder ;
    public Text registerSignUpText ;
    public Text backButtonText ;
    public Text startGameButtonText ;
    public Text rememberMeLabel ;
    public Text registerStatusInformation ;
    public Text nicknameInputPlaceHolder ;
    public Text nicknameStatusInformation ;

    //Meta references to panels we need to show / hide
    public GameObject registerPanel;
    public GameObject loginPanel;
    public GameObject nicknamePanel ;

    //Settings for what data to get from playfab on login.
    public GetPlayerCombinedInfoRequestParams InfoRequestParams;

    //Reference to our Authentication service
    private PlayFabAuthService _AuthService = PlayFabAuthService.Instance;
    private static string playFabId;

    
   
    
    
    public void Awake()
    {

        if (clearPlayerPrefs)
        {
            _AuthService.UnlinkSilentAuth();
            _AuthService.ClearRememberMe();
            _AuthService.AuthType = Authtypes.None;
        }

        //Set our remember me button to our remembered state.
        rememberMe.isOn = _AuthService.RememberMe;

        //Subscribe to our Remember Me toggle
        rememberMe.onValueChanged.AddListener((toggle) =>
        {
            _AuthService.RememberMe = toggle;
        });
    }

    public void Start()
    {
        //Hide all our panels until we know what UI to display
        //loginPanel.SetActive(false);
        registerPanel.SetActive(false);
        nicknamePanel.SetActive(false);

        //Subscribe to events that happen after we authenticate
        PlayFabAuthService.OnDisplayAuthentication += OnDisplayAuthentication;
        PlayFabAuthService.OnLoginSuccess += OnLoginSuccess;
        PlayFabAuthService.OnPlayFabError += OnPlayFaberror;


        //Bind to UI buttons to perform actions when user interacts with the UI.
        loginButton.onClick.AddListener(OnLoginClicked);
        registerButton.onClick.AddListener(OnRegisterButtonClicked);
        cancelRegisterButton.onClick.AddListener(OnCancelRegisterButtonClicked);
        startGameButton.onClick.AddListener(OnStartGameButtonClicked);

        if (Tutorial.languageEnglish)
        {
            loginProgressInformation.text = "Logging in..." ;
            passwordPlaceHolder.text = "Password..." ;
            rememberMeLabel.text = "Remember Me" ;
            loginPlaceHolder.text = "Sign In / Sign Up" ;
            passwordCorrectionPlaceHolder.text = "Enter your password again..." ;
            registerSignUpText.text = "Sign Up" ;
            backButtonText.text = "Back" ;
            registerStatusInformation.text = "Registering..." ;
            startGameButtonText.text = "Start Game" ;
            nicknameInputPlaceHolder.text = "Enter your nickname..." ;
        }
        else
        {
            loginProgressInformation.text = "Giriş yapılıyor..." ;
            passwordPlaceHolder.text = "Parola..." ;
            rememberMeLabel.text = "Beni Hatırla" ;
            loginPlaceHolder.text = "Giriş Yap / Uye Ol" ;
            passwordCorrectionPlaceHolder.text = "Parolanızı tekrar giriniz..." ;
            registerSignUpText.text = "Kayıt Ol" ;
            backButtonText.text = "Geri" ;
            registerStatusInformation.text = "Kaydediliyor..." ;
            startGameButtonText.text = "Gönder" ;
            nicknameInputPlaceHolder.text = "Bir nickname giriniz..." ;
        }
        

        //Set the data we want at login from what we chose in our meta data.
        _AuthService.InfoRequestParams = InfoRequestParams;

        //Start the authentication process.
        _AuthService.Authenticate();
    }


    /// <summary>
    /// Login Successfully - Goes to next screen.
    /// </summary>
    /// <param name="result"></param>
    private void OnLoginSuccess(PlayFab.ClientModels.LoginResult result)
    {
        if (registerStatusInformation.gameObject.activeSelf)
        {
            nicknamePanel.SetActive(true);
            JustFlipClient.GetInstance(GetType()).SetAllStatisticsForRegister();
            return;
        }
        
        TempStarter.startTutorial = true ;
        gameObject.SetActive(false);
    }
    

    /// <summary>
    /// Error handling for when Login returns errors.
    /// </summary>
    /// <param name="error"></param>
    private void OnPlayFaberror(PlayFabError error)
    {
        //There are more cases which can be caught, below are some
        //of the basic ones.
        switch (error.Error)
        {
            case PlayFabErrorCode.InvalidEmailAddress:
            case PlayFabErrorCode.InvalidPassword:
            case PlayFabErrorCode.InvalidEmailOrPassword:

                if (Tutorial.languageEnglish)
                {
                    loginErrorInformation.text = "Invalid E-mail address or password" ;
                }
                else
                {
                    loginErrorInformation.text = "E-mail adresi veya parola yanlış" ;
                }
                loginErrorInformation.gameObject.SetActive(true);
                loginProgressInformation.gameObject.SetActive(false);
                loginPanel.transform.Find("EmailInput").gameObject.SetActive(true);
                loginPanel.transform.Find("PasswordInput").gameObject.SetActive(true);
                loginPanel.transform.Find("SignInSignUpButton").gameObject.SetActive(true);
                loginPanel.transform.Find("RememberMe").gameObject.SetActive(true);
                
                break;
            case PlayFabErrorCode.UnableToConnectToDatabase:
                if (Tutorial.languageEnglish)
                {
                    loginErrorInformation.text = "Please check your internet connection." ;
                }
                else
                {
                    loginErrorInformation.text = "Lütfen internet bağlantınızı kontrol edin." ;
                }
                loginErrorInformation.gameObject.SetActive(true);
                loginProgressInformation.gameObject.SetActive(false);
                loginPanel.transform.Find("EmailInput").gameObject.SetActive(true);
                loginPanel.transform.Find("PasswordInput").gameObject.SetActive(true);
                loginPanel.transform.Find("SignInSignUpButton").gameObject.SetActive(true);
                loginPanel.transform.Find("RememberMe").gameObject.SetActive(true);
                break;
            
            case PlayFabErrorCode.ServiceUnavailable:
                if (Tutorial.languageEnglish)
                {
                    loginErrorInformation.text = "Please check your internet connection." ;
                }
                else
                {
                    loginErrorInformation.text = "Lütfen internet bağlantınızı kontrol edin." ;
                }
                loginErrorInformation.gameObject.SetActive(true);
                loginProgressInformation.gameObject.SetActive(false);
                loginPanel.transform.Find("EmailInput").gameObject.SetActive(true);
                loginPanel.transform.Find("PasswordInput").gameObject.SetActive(true);
                loginPanel.transform.Find("SignInSignUpButton").gameObject.SetActive(true);
                loginPanel.transform.Find("RememberMe").gameObject.SetActive(true);
                break;
            
            case PlayFabErrorCode.InvalidParams:
                if (Tutorial.languageEnglish)
                {
                    loginErrorInformation.text = "Password or e-mail does not comply with the rules" ;
                }
                else
                {
                    loginErrorInformation.text = "Parola veya e-mail kurallara uygun değil" ;
                }
                loginErrorInformation.gameObject.SetActive(true);
                loginProgressInformation.gameObject.SetActive(false);
                loginPanel.transform.Find("EmailInput").gameObject.SetActive(true);
                loginPanel.transform.Find("PasswordInput").gameObject.SetActive(true);
                loginPanel.transform.Find("SignInSignUpButton").gameObject.SetActive(true);
                loginPanel.transform.Find("RememberMe").gameObject.SetActive(true);
                break;

            case PlayFabErrorCode.AccountNotFound:
                
                registerPanel.SetActive(true);
                passwordCorrectionInformation.gameObject.SetActive(true);
                if (Tutorial.languageEnglish)
                {
                    passwordCorrectionInformation.text = "Account not found, Register?" ;
                }
                else
                {
                    passwordCorrectionInformation.text = "Hesap bulunamadı. Kayıt olmak ister misin?" ;
                }
                
                loginErrorInformation.gameObject.SetActive(false);
                return;
            
            default:
                if (Tutorial.languageEnglish)
                {
                    loginErrorInformation.text = "Unknown error" ;
                }
                else
                {
                    loginErrorInformation.text = "Bilinmeyen hata" ;
                }
                
                loginErrorInformation.gameObject.SetActive(true);
                loginProgressInformation.gameObject.SetActive(false);
                loginPanel.transform.Find("EmailInput").gameObject.SetActive(true);
                loginPanel.transform.Find("PasswordInput").gameObject.SetActive(true);
                loginPanel.transform.Find("SignInSignUpButton").gameObject.SetActive(true);
                loginPanel.transform.Find("RememberMe").gameObject.SetActive(true);
                break;
                
        }

    }

    /// <summary>
    /// Choose to display the Auth UI or any other action.
    /// </summary>
    private void OnDisplayAuthentication()
    {
        loginPanel.SetActive(true);
    }

   

    /// <summary>
    /// Login Button means they've selected to submit a username (email) / password combo
    /// Note: in this flow if no account is found, it will ask them to register.
    /// </summary>
    private void OnLoginClicked()
    {
        
        if (username.text == String.Empty || password.text == String.Empty)
        {
            if (Tutorial.languageEnglish)
            {
                loginErrorInformation.text = "Please fill required fields" ;
            }
            else
            {
                loginErrorInformation.text = "Lütfen gerekli alanları doldurun" ;
            }
            
            loginErrorInformation.gameObject.SetActive(true);
        }
        else if (password.text.Length < 6)
        {
            if (Tutorial.languageEnglish)
            {
                loginErrorInformation.text = "Password can't be smaller than 6 characters" ;
            }
            else
            {
                loginErrorInformation.text = "Parola 6 karakterden küçük olamaz" ;
            }
            loginErrorInformation.gameObject.SetActive(true);
        }
        else
        {
            _AuthService.Email = username.text;
            _AuthService.Password = password.text;
        
            loginPanel.transform.Find("EmailInput").gameObject.SetActive(false);
            loginPanel.transform.Find("PasswordInput").gameObject.SetActive(false);
            loginPanel.transform.Find("SignInSignUpButton").gameObject.SetActive(false);
            loginPanel.transform.Find("RememberMe").gameObject.SetActive(false);
            loginErrorInformation.gameObject.SetActive(false);

            if (Tutorial.languageEnglish)
            {
                loginProgressInformation.text = "Logging in..." ;
            }
            else
            {
                loginProgressInformation.text = "Giriş yapılıyor..." ;
            }
            
            loginProgressInformation.gameObject.SetActive(true);
      
            _AuthService.Authenticate(Authtypes.EmailAndPassword);
        }
       
    }

    /// <summary>
    /// No account was found, and they have selected to register a username (email) / password combo.
    /// </summary>
    private void OnRegisterButtonClicked()
    {
        if (password.text != confirmPassword.text)
        {
            if (Tutorial.languageEnglish)
            {
                passwordCorrectionInformation.text = "Wrong password !" ;
            }
            else
            {
                passwordCorrectionInformation.text = "Parola yanlış !" ;
            }
            
            passwordCorrectionInformation.gameObject.SetActive(true);
            
            return;
        }
        
        _AuthService.Email = username.text;
        _AuthService.Password = password.text;
        _AuthService.Authenticate(Authtypes.RegisterPlayFabAccount);
        cancelRegisterButton.gameObject.SetActive(false);
        registerButton.gameObject.SetActive(false);
        confirmPassword.gameObject.SetActive(false);
        passwordCorrectionInformation.gameObject.SetActive(false);
        registerStatusInformation.gameObject.SetActive(true);
    }

    /// <summary>
    /// They have opted to cancel the Registration process.
    /// Possibly they typed the email address incorrectly.
    /// </summary>
    private void OnCancelRegisterButtonClicked()
    {
        //Reset all forms
        username.text = string.Empty;
        password.text = string.Empty;
        confirmPassword.text = string.Empty;
        //Show panels
        passwordCorrectionInformation.gameObject.SetActive(false);
        registerPanel.SetActive(false);
        loginProgressInformation.gameObject.SetActive(false);
        loginPanel.transform.Find("EmailInput").gameObject.SetActive(true);
        loginPanel.transform.Find("PasswordInput").gameObject.SetActive(true);
        loginPanel.transform.Find("SignInSignUpButton").gameObject.SetActive(true);
        loginPanel.transform.Find("RememberMe").gameObject.SetActive(true);
        
    }

    private void OnStartGameButtonClicked()
    {
        nicknameStatusInformation.gameObject.SetActive(true);
        
        if (nickname.text.Trim() == String.Empty)
        {
            if (Tutorial.languageEnglish)
            {
                nicknameStatusInformation.text = "Enter your nickname." ;
            }
            else
            {
                nicknameStatusInformation.text = "Lütfen bir nickname belirleyin." ;
            }
           
            return;
        }
        
        for (byte i = 0; i < nickname.text.Trim().Length; i++)
        {
            if (nickname.text.Trim()[i] == ' ')
            {
                if (Tutorial.languageEnglish)
                {
                    nicknameStatusInformation.text = "Nickname can't contain space character." ;
                }
                else
                {
                    nicknameStatusInformation.text = "Nickname boşluk karakteri içeremez." ;
                }
                
                return;
            }
        }

        StartCoroutine(StartGameCoroutine()) ;
        
    }

    private IEnumerator StartGameCoroutine()
    {
        JustFlipClient.nicknameChangeLog = String.Empty;
        JustFlipClient.GetInstance(this.GetType()).ChangeDisplayName(nickname.text.Trim());

        if (Tutorial.languageEnglish)
        {
            nicknameStatusInformation.text = "Checking..." ;
        }
        else
        {
            nicknameStatusInformation.text = "Kontrol ediliyor..." ; 
        }
            

        while (true)
        {
            if (JustFlipClient.nicknameChangeLog == String.Empty )
            {
                yield return null ;
            }
            else if (JustFlipClient.nicknameChangeLog == "Nickname atandı.")
            {
                JustFlipClient.nicknameChangeLog = String.Empty ;
                TempStarter.startTutorial = true ;
                gameObject.SetActive(false);
                yield break;
            }
            else
            {
                nicknameStatusInformation.text = JustFlipClient.nicknameChangeLog ;
                yield break;
            }
        }
        
        
    }
    
}

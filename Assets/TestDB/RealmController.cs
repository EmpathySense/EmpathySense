using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;
using Realms.Sync;
using Realms.Sync.Exceptions;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class RealmController : MonoBehaviour
{

    public static RealmController Instance;

    public string RealmAppId = "empathy-sense-app-owaao";

    private Realm _realm;
    private App _realmApp;
    private User _realmUser;

    void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    void OnDisable()
    {
        if (_realm != null)
        {
            _realm.Dispose();
        }
    }

    public async Task<string> Login(string email, string password)
    {
        if (email != "" && password != "")
        {
            _realmApp = App.Create(new AppConfiguration(RealmAppId)
            {
                MetadataPersistenceMode = MetadataPersistenceMode.NotEncrypted
            });
            try
            {
                if (_realmUser == null)
                {
                    _realmUser = await _realmApp.LogInAsync(Credentials.EmailPassword(email, password));
                    _realm = await Realm.GetInstanceAsync(new SyncConfiguration(email, _realmUser));
                }
                else
                {
                    _realm = Realm.GetInstance(new SyncConfiguration(email, _realmUser));
                }
            }
            catch (ClientResetException clientResetEx)
            {
                if (_realm != null)
                {
                    _realm.Dispose();
                }
                clientResetEx.InitiateClientReset();
            }
            return _realmUser.Id;
        }
        return "";
    }

    public Users GetUser()
    {
        Users _playerProfile = _realm.Find<Users>(_realmUser.Id);
        if (_playerProfile == null)
        {
            _realm.Write(() => {
                _playerProfile = _realm.Add(new Users(_realmUser.Id));
            });
        }
        return _playerProfile;
    }

    public Prefs GetPrefs(){
        var _prefs = _realm.All<Prefs>();
        Prefs _newPrefs = new Prefs();
        foreach (var _pref in _prefs)
        {
            _newPrefs.Id = _pref.Id;
            _newPrefs.InfoI = _pref.InfoI;
            _newPrefs.InfoA = _pref.InfoA;
            _newPrefs.InfoB = _pref.InfoB;
            _newPrefs.InfoC = _pref.InfoC;
            _newPrefs.InfoD = _pref.InfoD;
            _newPrefs.InfoE = _pref.InfoE;
        }
        return _newPrefs;

    }

    public Prefs CreatePrefs()
    {
        Prefs _prefs = new Prefs();
        _realm.Write(() => {
            _realm.Add(_prefs);
        });
        return _prefs;
    }

    public void IsCreated()
    {
        Users _playerProfile = _realm.Find<Users>(_realmUser.Id);
        if (_playerProfile == null)
        {
            _realm.Write(() => {
                _playerProfile = _realm.Add(new Users(_realmUser.Id));
            });

            Prefs _prefs = RealmController.Instance.CreatePrefs();
            SceneManager.LoadScene("SignupScene");
        }
        else
        {   

            Prefs _prefs = GetPrefs();
            if (_prefs.InfoI)
            {
                SceneManager.LoadScene("Introduccion"); //nombre escena de introducción
            }
            else
            {
                SceneManager.LoadScene("HistoryScene");
            }
            // SceneManager.LoadScene("EscenarioSimulacion");
        }
    }

    public void UpdateUser(Users user)
    {
        _realm.Write(() => {
            _realm.Add(user, true); //this will update an existing user with the same id or create a new one if it doesn't exist
        });
    }

    public void UpdatePrefs(Prefs prefs)
    {
        _realm.Write(() => {
            _realm.Add(prefs, true); //this will update an existing user with the same id or create a new one if it doesn't exist
        });
    }


    public History CreateHistory( int a, int b, int c, int d, int e)
    {
        History _history = new History(a, b, c, d, e);
        _realm.Write(() => {
            _realm.Add(_history);
        });
        return _history;
    }


    public void UpdatePrefs(string name)
    {
        Prefs _prefs = RealmController.Instance.GetPrefs();
        if(name == "intro") _prefs.InfoI = false;
        if(name == "a") _prefs.InfoA = false;
        if(name == "b") _prefs.InfoB = false;
        if(name == "c") _prefs.InfoC = false;
        if(name == "d") _prefs.InfoD = false;
        if(name == "e") _prefs.InfoE = false;
        RealmController.Instance.UpdatePrefs(_prefs);
    }
}
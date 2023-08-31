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

    public void IsCreated()
    {
        Users _playerProfile = _realm.Find<Users>(_realmUser.Id);
        if (_playerProfile == null)
        {
            _realm.Write(() => {
                _playerProfile = _realm.Add(new Users(_realmUser.Id));
            });
            SceneManager.LoadScene("SignUpScene");
        }
        else
        {   
            if (PlayerPrefs.HasKey("FirstSesion"))
            {
            SceneManager.LoadScene("MenúPrincipal");
                
            }
            else
            {
                SceneManager.LoadScene("Introduccion");
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

    public History CreateHistory( int a, int b, int c, int d, int e)
    {
        History _history = new History(a, b, c, d, e);
        _realm.Write(() => {
            _realm.Add(_history);
        });
        return _history;
    }


    //not needed, just for testing
    public void GetHistory()
    {
        var _history = _realm.All<History>();
        foreach (var item in _history)
        {
            Debug.Log("user_id: " + GetUser().UserId);
            Debug.Log("id: " + item.Id);
            Debug.Log(item.TotalScore);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;
using Realms.Sync;
using Realms.Sync.Exceptions;
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


    public void UpdateUser(Users user)
    {
        _realm.Write(() => {
            _realm.Add(user, true); //this will update an existing user with the same id or create a new one if it doesn't exist
        });
    }
}
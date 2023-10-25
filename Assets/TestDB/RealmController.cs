using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;
using Realms.Sync;
using Realms.Sync.Exceptions;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Linq;
using System;


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

    public Prefs GetPrefs()
    {
    // Obtén el primer documento de la colección (o el que necesites)
    var _pref = _realm.All<Prefs>().FirstOrDefault();

    if (_pref != null)
    {
        return new Prefs
        {
            Id = _pref.Id,
            InfoI = _pref.InfoI,
            InfoA = _pref.InfoA,
            InfoB = _pref.InfoB,
            InfoC = _pref.InfoC,
            InfoD = _pref.InfoD,
            InfoE = _pref.InfoE,
            InfoSim = _pref.InfoSim,
            Volumen = _pref.Volumen,
        };
    }
    else
    {
        // Manejar el caso en el que no se encuentra ningún documento
        Debug.LogError("No se encontró ningún documento Prefs en la base de datos.");
        return null;
    }
    }

    public Achievements[] GetAchievements(){
        var _ach = _realm.All<Achievements>();
        int j = 0;
        foreach (var _count in _ach)
        {
            j++;
        }
        Achievements[] _newAch = new Achievements[j];
        int i = 0;
        foreach (var _a in _ach)
        {
            _newAch[i] = new Achievements(_a.Id, _a.Name, _a.Description, _a.Date, _a.State);
            i++;
        }
        return _newAch;
    }

    public Prefs CreatePrefs()
    {
        Prefs _prefs = new Prefs();
        _realm.Write(() => {
            _realm.Add(_prefs);
        });
        return _prefs;
    }

    public Achievements CreateAchievements(string _id, string _name, string _description)
    {
        Debug.Log("SE mete pa acá");
        Achievements _ach = new Achievements(_id, _name, _description);
        _realm.Write(() => {
            _realm.Add(_ach);
        });
        return _ach;
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
            RealmController.Instance.CreateAchievements("PAP-1-", "Un paso a la vez", "Completa el paso A del PAP");
            RealmController.Instance.CreateAchievements("PAP-2-", "Un paso a la vez", "Completa el paso B del PAP");
            RealmController.Instance.CreateAchievements("PAP-3-", "Un paso a la vez", "Completa el paso C del PAP");
            RealmController.Instance.CreateAchievements("PAP-4-", "Un paso a la vez", "Completa el paso D del PAP");
            RealmController.Instance.CreateAchievements("PAP-5-", "Un paso a la vez", "Completa el paso E del PAP");
            RealmController.Instance.CreateAchievements("PAP-", "Un paso a la vez", "Completa todos los pasos del PAP");
            RealmController.Instance.CreateAchievements("SimA-", "Simulación", "Completa la simulación 'lugar público'");
            RealmController.Instance.CreateAchievements("SimA-100-", "Simulación", "Completa la simulación 'lugar público' con un 100% de aciertos");
            RealmController.Instance.CreateAchievements("SimB-", "Simulación", "Completa la simulación 'lugar cerrado'");
            RealmController.Instance.CreateAchievements("SimB-100-", "Simulación", "Completa la simulación 'lugar cerrado' con un 100% de aciertos");
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
                SceneManager.LoadScene("MenúPrincipal");
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

    public void UpdatePref(Prefs prefs)
    {
        _realm.Write(() => {
            _realm.Add(prefs, true); //this will update an existing user with the same id or create a new one if it doesn't exist
        });
    }

    public void UpdateAchievements(Achievements ach)
    {
        _realm.Write(() => {
            _realm.Add(ach, true); //this will update an existing user with the same id or create a new one if it doesn't exist
        });
    }

    public void UpdateLogros(string id){
        Achievements[] _achis = GetAchievements();
        foreach (var _ach in _achis)
        {
            Achievements _aux = new Achievements();
            if(_ach.Id == id){
                _aux.Id = _ach.Id;
                _aux.Name = _ach.Name;
                _aux.Description = _ach.Description;
                _aux.Date = DateTimeOffset.Now;
                _aux.State = true;
            }
            UpdateAchievements(_aux);
        }

    }

      public History CreateHistory( int a, int b, int c, int d, int e, int a2, int b2, int c2, int d2, int e2, int porcentaje, string scene, string feedback)
    {
        History _history = new History(a, b, c, d, e, a2, b2, c2, d2, e2, porcentaje, scene, feedback);
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
        if(name == "s") _prefs.InfoSim = false;
        RealmController.Instance.UpdatePref(_prefs);
        Debug.Log("PREFS: actualizadas");
    }

    public int CountFalse()
    {
        Achievements[] _achis = RealmController.Instance.GetAchievements();
        int count = 0;
        foreach (var _ach in _achis)
        {
            if (_ach.State) count++;
        }
        return count;
    }

    public void UpdateVolume(int volume)
    {
        Prefs _prefs = RealmController.Instance.GetPrefs();
        _prefs.Volumen = volume;
        RealmController.Instance.UpdatePref(_prefs);
        Debug.Log("PREFS: actualizadas");
    }

    public History[] GetHistory(){
        var _history = _realm.All<History>();
        int j = 0;
        foreach (var _count in _history)
        {
            j++;
        }
        History[] _newHistory = new History[j];
        int i = 0;
        foreach (var _h in _history)
        {
            _newHistory[i] = new History(_h.Id, _h.ScoreA, _h.ScoreB, _h.ScoreC, _h.ScoreD, _h.ScoreE,_h.TotalA, _h.TotalB,  _h.TotalC,  _h.TotalD,  _h.TotalE,_h.TotalScore, _h.Scene, _h.Feedback, _h.Date);
            i++;
        }
        return _newHistory;
    } 
    public History HistoryById(string id){
        History _history = _realm.Find<History>(id);
        if (_history == null)
        {
            return null;
        }
        return _history;
    }
}
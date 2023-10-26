using Realms;
using Realms.Sync;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Prefs : RealmObject
{

    [PrimaryKey]
    [BsonRepresentation(BsonType.ObjectId)]
    [MapTo("_id")]
    public string Id { get; set; }

    [MapTo("info_i")]
    public bool InfoI { get; set; }

    [MapTo("info_a")]
    public bool InfoA { get; set; }

    [MapTo("info_b")]
    public bool InfoB { get; set; }

    [MapTo("info_c")]
    public bool InfoC { get; set; }

    [MapTo("info_d")]
    public bool InfoD { get; set; }

    [MapTo("info_e")]
    public bool InfoE { get; set; }

    [MapTo("info_sim")]
    public bool InfoSim { get; set; }
    
    [MapTo("volume")]
    public int Volumen { get; set; }

    public Prefs()
    {
        this.Id = ObjectId.GenerateNewId().ToString();
        this.InfoI = true;
        this.InfoA = false;
        this.InfoB = true;
        this.InfoC = true;
        this.InfoD = true;
        this.InfoE = true;
        this.InfoSim =true;
        this.Volumen = 100;

    }

}
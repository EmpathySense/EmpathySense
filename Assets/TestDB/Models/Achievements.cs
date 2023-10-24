using Realms;
using Realms.Sync;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Achievements : RealmObject
{

    [PrimaryKey]
    [BsonRepresentation(BsonType.ObjectId)]
    [MapTo("_id")]
    public string Id { get; set; }

    [MapTo("pap")]
    public bool Pap { get; set; }

    [MapTo("sim_01")]
    public bool Sim_01 { get; set; }

    [MapTo("sim_02")]
    public bool Sim_02 { get; set; }

    public Achievements()
    {
        this.Id = ObjectId.GenerateNewId().ToString();
        this.Pap = false;
        this.Sim_01 = false;
        this.Sim_02 = false;
    }

}
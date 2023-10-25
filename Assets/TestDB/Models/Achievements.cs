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

    [MapTo("name")]
    public string Name { get; set; }

    [MapTo("description")]
    public string Description { get; set; }

    [MapTo("date")]
    public DateTimeOffset Date { get; set; }

    public Achievements() { }
    public Achievements(string _id, string _name, string _description)
    {
        this.Id = _id;
        this.Name = _name;
        this.Description = _description;
        this.Date = DateTimeOffset.Now;
    }

    public Achievements(string _id, string _name, string _description, DateTimeOffset _date)
    {
        this.Id = _id;
        this.Name = _name;
        this.Description = _description;
        this.Date = _date;
    }


}
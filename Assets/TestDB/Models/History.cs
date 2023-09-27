using Realms;
using Realms.Sync;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class History : RealmObject
{

    [PrimaryKey]
    [BsonRepresentation(BsonType.ObjectId)]
    [MapTo("_id")]
    public string Id { get; set; }

    [MapTo("score_a")]
    public int ScoreA { get; set; }

    [MapTo("score_b")]
    public int ScoreB { get; set; }

    [MapTo("score_c")]
    public int ScoreC { get; set; }

    [MapTo("score_d")]
    public int ScoreD { get; set; }

    [MapTo("score_e")]
    public int ScoreE { get; set; }

    [MapTo("scene")]
    public string Scene { get; set; }

    [MapTo("feedback")]
    public string Feedback { get; set; }
    
    [MapTo("date")]
    public DateTimeOffset Date { get; set; }

    public History() { }

    public History(int a, int b, int c, int d, int e)
    {
        this.Id = ObjectId.GenerateNewId().ToString();
        this.ScoreA = a;
        this.ScoreB = b;
        this.ScoreC = c;
        this.ScoreD = d;
        this.ScoreE = e;
        this.Scene = "scene";
        this.Feedback = "feedback";
        this.Date = DateTimeOffset.Now;

    }

      public History(int a, int b, int c, int d, int e, string scene, string feedback)
    {
        this.Id = ObjectId.GenerateNewId().ToString();
        this.ScoreA = a;
        this.ScoreB = b;
        this.ScoreC = c;
        this.ScoreD = d;
        this.ScoreE = e;
        this.Scene = scene;
        this.Feedback = feedback;
        this.Date = DateTimeOffset.Now;

    }

        public History(string id, int a, int b, int c, int d, int e, string scene, string feedback, DateTimeOffset date)
    {
        this.Id = id;
        this.ScoreA = a;
        this.ScoreB = b;
        this.ScoreC = c;
        this.ScoreD = d;
        this.ScoreE = e;
        this.Scene = scene;
        this.Feedback = feedback;
        this.Date = date;

    }

}
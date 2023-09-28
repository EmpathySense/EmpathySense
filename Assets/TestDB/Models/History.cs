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

    [MapTo("total_a")]
    public int TotalA { get; set; }

    [MapTo("total_b")]
    public int TotalB { get; set; }

    [MapTo("total_c")]
    public int TotalC { get; set; }

    [MapTo("total_d")]
    public int TotalD { get; set; }

    [MapTo("total_e")]
    public int TotalE { get; set; }
    [MapTo("total_score")]
    public int TotalScore { get; set; }

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
        this.TotalA = 0;
        this.TotalB = 0;
        this.TotalC = 0;
        this.TotalD = 0;
        this.TotalE = 0;
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

    public History(int a, int b, int c, int d, int e,int a2, int b2, int c2, int d2, int e2, int porcentaje, string scene, string feedback)
    {
        this.Id = ObjectId.GenerateNewId().ToString();
        this.ScoreA = a;
        this.ScoreB = b;
        this.ScoreC = c;
        this.ScoreD = d;
        this.ScoreE = e;
        this.TotalA = a2;
        this.TotalB = b2;
        this.TotalC = c2;
        this.TotalD = d2;
        this.TotalE = e2;
        this.TotalScore = porcentaje;
        this.Scene = scene;
        this.Feedback = feedback;
        this.Date = DateTimeOffset.Now;

    }

        public History(string id, int a, int b, int c, int d, int e,int a2, int b2, int c2, int d2, int e2,int porcentaje, string scene, string feedback, DateTimeOffset date)
    {
        this.Id = id;
        this.ScoreA = a;
        this.ScoreB = b;
        this.ScoreC = c;
        this.ScoreD = d;
        this.ScoreE = e;
        this.TotalA = a2;
        this.TotalB = b2;
        this.TotalC = c2;
        this.TotalD = d2;
        this.TotalE = e2;
        this.TotalScore = porcentaje;
        this.Scene = scene;
        this.Feedback = feedback;
        this.Date = date;
    }

}
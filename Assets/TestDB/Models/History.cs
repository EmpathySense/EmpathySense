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

    [MapTo("total_score")]
    public int TotalScore { get; set; }
    
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
        this.TotalScore = a + b + c + d + e;
        this.Date = DateTimeOffset.Now;

    }

}
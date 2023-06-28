using Realms;
using Realms.Sync;
using System;

public class Users : RealmObject

{
    [PrimaryKey]
    [MapTo("_id")]
    public string UserId { get; set; }

    [MapTo("firstname")]
    public string FirstName { get; set; }

    [MapTo("lastname")]
    public string LastName { get; set; }

    [MapTo("age")]
    public int Age { get; set; }

    [MapTo("role")]
    public string Role { get; set; }

    [MapTo("organization")]
    public string Organization { get; set; }

    [MapTo("creation_date")]
    public DateTimeOffset CreationDate { get; set; }

    public Users() { }

    public Users(string userId)
    {
        this.UserId = userId;
        this.FirstName = "N/A";
        this.LastName = "N/A";
        this.Age = 0;
        this.Role = "N/A";
        this.Organization = "N/A";
        this.CreationDate = DateTimeOffset.Now;
    }

}
using AutoVerhuurProject.Domein.Models;

namespace AutoVerhuurProject.Domein.Tests;

public class KlantTests
{
    //Email adres tests
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Test_Constructor_Email_NietIngevuld_Exception(string email) {
        Assert.Throws<ArgumentException>(() => (new Klant(email, "Voornaam", "Achternaam", "Straatnaam", "1001", "Woonplaats", "Land")));
    }

    [Fact]
    public void Test_Constructor_Email_BestaatAl_Exception() {
        Klant klant1 = new Klant("test@example.com", "voornaam", "achternaam", "straatnaam", "1000","woonplaats", "land");

        Assert.Throws<ArgumentException>(() => (new Klant("test@example.com", "Voornaam", "Achternaam", "Straatnaam", "1001", "Woonplaats", "Land")));
    }

    [Fact]
    public void Test_Constructor_Email_Geldig() {
        Klant klant1 = new Klant("test2@example.com", "voornaam", "achternaam", "straatnaam", "1000", "woonplaats", "land");

        Assert.Null(Record.Exception(() => new Klant("test3@example.com", "voornaam", "achternaam", "straatnaam", "1000", "woonplaats", "land")));
    }


    //Voornaam tests
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Test_Constructor_Voornaam_NietIngevuld_Exception(string voornaam) {
        Assert.Throws<ArgumentException>(() => (new Klant(Guid.NewGuid().ToString(), voornaam, "Achternaam", "Straatnaam", "1001", "Woonplaats", "Land")));
    }

    [Theory]
    [InlineData("voornaam")]
    [InlineData("A")]
    public void Test_Constructor_Voornaam_Ingevuld(string voornaam) {
        Assert.Null(Record.Exception(() => new Klant(Guid.NewGuid().ToString(), voornaam, "achternaam", "straatnaam", "1000", "woonplaats", "land")));
    }


    //Achternaam tests
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Test_Constructor_Achternaam_NietIngevuld_Exception(string achternaam) {
        Assert.Throws<ArgumentException>(() => (new Klant(Guid.NewGuid().ToString(), "voornaam", achternaam, "Straatnaam", "1001", "Woonplaats", "Land")));
    }

    [Theory]
    [InlineData("achternaam")]
    [InlineData("A")]
    public void Test_Constructor_Achternaam_Ingevuld(string achternaam) {
        Assert.Null(Record.Exception(() => new Klant(Guid.NewGuid().ToString(), "voornaam", achternaam, "straatnaam", "1000", "woonplaats", "land")));
    }
}

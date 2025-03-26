using AutoVerhuurProject.Domein.Models;

namespace AutoVerhuurProject.Domein.Tests;

public class VestigingTests
{
    //Straat tests
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Test_Constructor_Straat_NietIngevuld_Exception(string straat) {
        Assert.Throws<ArgumentException>(() => (new Vestiging(Guid.NewGuid().ToString(), straat, "1000", "plaats", "land")));
    }

    [Theory]
    [InlineData("Straat")]
    [InlineData("A")]
    public void Test_Constructor_Straat_Ingevuld(string straat) {
        Assert.Null(Record.Exception(() => new Vestiging(Guid.NewGuid().ToString(), straat, "1000", "plaats", "land")));
    }


    //Postcode tests
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Test_Constructor_Postcode_NietIngevuld_Exception(string postcode) {
        Assert.Throws<ArgumentException>(() => (new Vestiging(Guid.NewGuid().ToString(), "straat", postcode, "plaats", "land")));
    }

    [Theory]
    [InlineData("1000")]
    [InlineData("A")]
    public void Test_Constructor_Postcode_Ingevuld(string postcode) {
        Assert.Null(Record.Exception(() => new Vestiging(Guid.NewGuid().ToString(), "straat", postcode, "plaats", "land")));
    }


    //Plaats tests
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Test_Constructor_Plaats_NietIngevuld_Exception(string plaats) {
        Assert.Throws<ArgumentException>(() => (new Vestiging(Guid.NewGuid().ToString(), "straat", "postcode", plaats, "land")));
    }

    [Theory]
    [InlineData("Plaats")]
    [InlineData("A")]
    public void Test_Constructor_Plaats_Ingevuld(string plaats) {
        Assert.Null(Record.Exception(() => new Vestiging(Guid.NewGuid().ToString(), "straat", "postcode", plaats, "land")));
    }


    //Land tests
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Test_Constructor_Land_NietIngevuld_Exception(string land) {
        Assert.Throws<ArgumentException>(() => (new Vestiging(Guid.NewGuid().ToString(), "straat", "postcode", "plaats", land)));
    }

    [Theory]
    [InlineData("Land")]
    [InlineData("A")]
    public void Test_Constructor_Land_Ingevuld(string land) {
        Assert.Null(Record.Exception(() => new Vestiging(Guid.NewGuid().ToString(), "straat", "postcode", "plaats", land)));
    }


    //Luchthaven tests
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Test_Constructor_Luchthaven_NietIngevuld_Exception(string luchthaven) {
        Assert.Throws<ArgumentException>(() => (new Vestiging(luchthaven, "straat", "postcode", "plaats", "land")));
    }

    [Theory]
    [InlineData("Luchthaven")]
    [InlineData("A")]
    public void Test_Constructor_Luchthaven_Ingevuld(string luchthaven) {
        Assert.Null(Record.Exception(() => new Vestiging(luchthaven, "straat", "postcode", "plaats", "land")));
    }

    [Fact]
    public void Test_Constructor_Luchthaven_BestaatAl_Exception() {
        Vestiging vest = new Vestiging("a", "straat", "postcode", "plaats", "land");
        Assert.Throws<ArgumentException>(() => new Vestiging("a", "straat", "postcode", "plaats", "land"));
    }
}

using AutoVerhuurProject.Domein.Models;

namespace AutoVerhuurProject.Domein.Tests;

public class ReservatieTests
{
    //Email tests
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Test_Constructor_Email_NietIngevuld_Exception(string email) {
        Assert.Throws<ArgumentException>(() => (new Reservatie(Guid.NewGuid(), email, "abc", "1", DateTime.Now.AddDays(1), DateTime.Now.AddDays(3))));
    }

    [Theory]
    [InlineData("test@example.com")]
    [InlineData("A")]
    public void Test_Constructor_Email_Ingevuld(string email) {
        Assert.Null(Record.Exception(() => new Reservatie(Guid.NewGuid(), email, "abc", "1", DateTime.Now.AddDays(1), DateTime.Now.AddDays(3))));
    }


    //Nummerplaat tests
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Test_Constructor_Nummerplaat_NietIngevuld_Exception(string nrplaat) {
        Assert.Throws<ArgumentException>(() => (new Reservatie(Guid.NewGuid(), "email", nrplaat, "1", DateTime.Now.AddDays(1), DateTime.Now.AddDays(3))));
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("A")]
    public void Test_Constructor_Nummerplaat_Ingevuld(string nrplaat) {
        Assert.Null(Record.Exception(() => new Reservatie(Guid.NewGuid(), "email", nrplaat, "1", DateTime.Now.AddDays(1), DateTime.Now.AddDays(3))));
    }


    //VestigingsID tests
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Test_Constructor_VestigingsId_NietIngevuld_Exception(string vestId) {
        Assert.Throws<ArgumentException>(() => (new Reservatie(Guid.NewGuid(), "email", "abc", vestId, DateTime.Now.AddDays(1), DateTime.Now.AddDays(3))));
    }

    [Theory]
    [InlineData("123")]
    [InlineData("A")]
    public void Test_Constructor_VestigingsId_Ingevuld(string vestId) {
        Assert.Null(Record.Exception(() => new Reservatie(Guid.NewGuid(), "email", "abc", vestId, DateTime.Now.AddDays(1), DateTime.Now.AddDays(3))));
    }


    //Verhuurperiode tests
    [Fact]
    public void Test_Constructor_StartTijdstip_NietInToekomst_Exception() {
        Assert.Throws<ArgumentException>(() => (new Reservatie(Guid.NewGuid(), "email", "abc", "1", DateTime.Now.AddDays(-1), DateTime.Now.AddDays(3))));
    }
    [Fact]
    public void Test_Constructor_EindTijdstip_VoorBeginTijdStip_Exception() {
        Assert.Throws<ArgumentException>(() => (new Reservatie(Guid.NewGuid(), "email", "abc", "1", DateTime.Now.AddDays(3), DateTime.Now.AddDays(1))));
    }
    [Fact]
    public void Test_Constructor_Verhuurperiode_MinderDan1Dag_Exception() {
        Assert.Throws<ArgumentException>(() => (new Reservatie(Guid.NewGuid(), "email", "abc", "1", DateTime.Now.AddDays(1), DateTime.Now.AddHours(3))));
    }
}

using AutoVerhuurProject.Domein.Models;
using System.Reflection;

namespace AutoVerhuurProject.Domein.Tests;

public class AutoTests {
    //model tests
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Test_Constructor_Model_NietIngevuld_Exception(string model) {
        Assert.Throws<ArgumentException>(() => (new Auto(Guid.NewGuid().ToString(), model, 5, MotorTypes.Benzine, "luchthaven")));
    }

    [Theory]
    [InlineData("Volvo XC60")]
    [InlineData("Nissan Leaf")]
    [InlineData("Fiat 500")]
    public void Test_Constructor_Model_GeldigeInput(string model) {
        Assert.Null(Record.Exception(() => new Auto(Guid.NewGuid().ToString(), model, 5, MotorTypes.Benzine, "luchthaven")));
    }


    //ziplaatsen tests
    [Theory]
    [InlineData(-5)]
    [InlineData(0)]
    [InlineData(1)]
    public void Test_Constructor_Zitplaatsen_FouteInput_Exception(int zitplaatsen) {
        Assert.Throws<ArgumentException>(() => (new Auto(Guid.NewGuid().ToString(), "Nissan Leaf", zitplaatsen, MotorTypes.Benzine, "luchthaven")));
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(100)]
    public void Test_Constructor_Zitplaatsen_GeldigeInput(int zitplaatsen) {
        Assert.Null(Record.Exception(() => new Auto(Guid.NewGuid().ToString(), "Nissan Leaf", zitplaatsen, MotorTypes.Benzine, "luchthaven")));
    }


    //nummerplaat tests
    [Fact]
    public void Test_Constructor_Nummerplaat_BestaatAl_Exception() {
        Auto auto1 = new Auto("abc", "Nissan Leaf", 5, MotorTypes.Benzine, "luchthaven");

        Assert.Throws<ArgumentException>(() => (new Auto("abc", "Volvo XC50", 4, MotorTypes.Diesel, "luchthaven")));
    }
    
    [Fact]
    public void Test_Constructor_Nummerplaat_Geldig() {
        Auto auto1 = new Auto("def", "Nissan Leaf", 5, MotorTypes.Benzine, "luchthaven");

        Assert.Null(Record.Exception(() => new Auto("xyz", "Nissan Leaf", 6, MotorTypes.Benzine, "luchthaven")));
    }
}
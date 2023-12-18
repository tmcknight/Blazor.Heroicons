namespace Blazor.Heroicons.Tests;

[TestClass]
public class RandomHeroiconTests : BunitTestContext
{
    [TestMethod]
    public void RendersWithDefaultAttributes()
    {
        // Arrange 
        //Act
        var cut = RenderComponent<RandomHeroicon>();
        // Assert
        Assert.IsFalse(cut.Find("svg").HasAttribute("class"));
        Assert.AreEqual(HeroiconStyle.Outline, cut.Instance.Style);
    }

    [TestMethod]
    public void ChangingIconTypeGetsNewRandom()
    {
        // Arrange 
        var cut = RenderComponent<RandomHeroicon>();
        var originalType = cut.Instance.Style;
        // Assert
        Assert.AreEqual(HeroiconStyle.Outline, cut.Instance.Style);
        //Act
        cut.SetParametersAndRender(parameters => parameters
            .Add(p => p.Style, HeroiconStyle.Solid));
        //Assert
        Assert.AreNotEqual(originalType, cut.Instance.Style);
    }

    [TestMethod]
    public void ChangingUnmatchedAttributeRetainsIcon()
    {
        // Arrange 
        var cut = RenderComponent<RandomHeroicon>(parameters => parameters
            .Add(p => p.Style, HeroiconStyle.Solid));
        var originalType = cut.Instance.Style;

        //Act
        cut.SetParametersAndRender(parameters => parameters
            .AddUnmatched("class", "h-10 w-10"));

        // Assert
        Assert.AreEqual("h-10 w-10", cut.Find("svg").GetAttribute("class"));
        Assert.AreEqual(HeroiconStyle.Solid, cut.Instance.Style);
        Assert.AreEqual(originalType, cut.Instance.Style);
    }
}
using Blazor.Heroicons.Outline;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace Blazor.Heroicons.Tests;
[TestClass]
public class HeroiconTests : BunitTestContext
{
    [TestMethod]
    public void RendersWithDefaultAttributes()
    {
        // Arrange 
        //Act
        var cut = RenderComponent<Heroicon>();
        // Assert
        var sparklesIcon = RenderComponent<SparklesIcon>();
        Assert.IsFalse(cut.Find("svg").HasAttribute("class"));
        Assert.AreEqual(HeroiconName.Sparkles, cut.Instance.Name);
        Assert.AreEqual(HeroiconStyle.Outline, cut.Instance.Style);
        Assert.AreEqual(sparklesIcon.Markup, cut.Markup);
    }

    [TestMethod]
    public void RendersWithAdditionalAttribute()
    {
        // Arrange
        // Act
        var cut = RenderComponent<Heroicon>(parameters => parameters
            .AddUnmatched("added-attribute", "wahoo"));

        // Assert
        Assert.AreEqual("wahoo", cut.Find("svg").GetAttribute("added-attribute"));
    }

    [TestMethod]
    public void UnknownNameRendersQuestionMark()
    {
        // Arrange
        var iconName = "MythicalIcon";
        // Act
        var cut = RenderComponent<Heroicon>(parameters => parameters
            .Add(p => p.Name, iconName));
        var questionMark = RenderComponent<QuestionMarkCircleIcon>();
        //Assert
        Assert.AreEqual(questionMark.Markup, cut.Markup);
    }

    [TestMethod]
    [DataRow("HandThumbUpIcon", DisplayName = "Fully qualified name")]
    [DataRow("HandThumbUp", DisplayName = "Name without Icon suffix")]
    [DataRow("handthumbup", DisplayName = "Lowercase name")]
    [DataRow("hand-thumb-up", DisplayName = "Name with hyphens")]
    public void NameRendersCorrectly(string iconName)
    {
        // Arrange
        // Act
        var cut = RenderComponent<Heroicon>(parameters => parameters
            .Add(p => p.Name, iconName));
        //Assert
        cut.MarkupMatches("<svg diff:ignore></svg>");
    }

    [TestMethod]
    [DataRow(HeroiconStyle.Solid)]
    [DataRow(HeroiconStyle.Outline)]
    [DataRow(HeroiconStyle.Mini)]
    [DataRow(HeroiconStyle.Micro)]
    public void AllIconsRenderCorrectly(HeroiconStyle heroiconStyle)
    {
        // Arrange
        var icons = Assembly.GetExecutingAssembly().GetTypes()
                            .Where(t => t.Namespace == $"Blazor.Heroicons.{heroiconStyle}")
                            .ToList();

        // Act
        foreach (var icon in icons)
        {
            // Act
            var cut = RenderComponent<DynamicComponent>(parameters => parameters
                .Add(p => p.Type, icon));
            //Assert
            cut.MarkupMatches("<svg diff:ignore></svg>");
        }
    }

    [TestMethod]
    public void HeroiconRendersNameChange()
    {
        // Arrange 
        //Act
        var cut = RenderComponent<Heroicon>();
        // Assert
        Assert.AreEqual(HeroiconName.Sparkles, cut.Instance.Name);
        var sparkles = cut.Markup;
        cut.SetParametersAndRender(parameters => parameters
            .Add(p => p.Name, "HandThumbUpIcon"));
        Assert.AreEqual("HandThumbUpIcon", cut.Instance.Name);
        Assert.AreNotEqual(sparkles, cut.Markup);
    }

    [TestMethod]
    public void HeroiconRendersTypeChange()
    {
        // Arrange 
        //Act
        var cut = RenderComponent<Heroicon>();
        // Assert
        Assert.AreEqual(HeroiconStyle.Outline, cut.Instance.Style);
        var sparkles = cut.Markup;
        cut.SetParametersAndRender(parameters => parameters
            .Add(p => p.Style, HeroiconStyle.Solid));
        Assert.AreEqual(HeroiconStyle.Solid, cut.Instance.Style);
        Assert.AreNotEqual(sparkles, cut.Markup);
    }

    [TestMethod]
    public void ChangingUnmatchedAttributeRetainsIcon()
    {
        // Arrange 
        var cut = RenderComponent<Heroicon>(parameters => parameters
            .Add(p => p.Name, "HandThumbUpIcon")
            .Add(p => p.Style, HeroiconStyle.Solid));

        //Act
        cut.SetParametersAndRender(parameters => parameters
            .AddUnmatched("class", "h-10 w-10"));

        // Assert
        Assert.AreEqual("h-10 w-10", cut.Find("svg").GetAttribute("class"));
        Assert.AreEqual("HandThumbUpIcon", cut.Instance.Name);
        Assert.AreEqual(HeroiconStyle.Solid, cut.Instance.Style);
    }
}
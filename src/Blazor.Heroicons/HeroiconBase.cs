using Microsoft.AspNetCore.Components;

namespace Blazor.Heroicons;

public abstract class HeroiconBase : ComponentBase
{
    /// <summary>
    /// Gets or sets a collection of additional attributes that will be applied to the created element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> AdditionalAttributes { get; set; } = new();

    protected static HeroiconStyle GetStyleFromType(HeroiconType? type)
    {
        return type switch
        {
            HeroiconType.Outline => HeroiconStyle.Outline,
            HeroiconType.Solid => HeroiconStyle.Solid,
            HeroiconType.Mini => HeroiconStyle.Mini,
            HeroiconType.Micro => HeroiconStyle.Micro,
            _ => HeroiconStyle.Outline
        };
    }
}

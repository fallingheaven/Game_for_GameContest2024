using System.Collections.Generic;
using Utility;

public class Fusion
{
    public static readonly Dictionary<Element, Dictionary<Element, Element>> FusionMap = new Dictionary<Element, Dictionary<Element, Element>>
    {
        { Element.Fire, new Dictionary<Element, Element>
            {
                { Element.Soil, Element.Rock },
                { Element.Wood, Element.Ember },
                { Element.Water, Element.Steam }
            }
        },
        { Element.Soil, new Dictionary<Element, Element>
            {
                { Element.Fire, Element.Rock },
                { Element.Water, Element.Wetland }
            }
        },
        { Element.Wood, new Dictionary<Element, Element>
            {
                { Element.Fire, Element.Ember }
            }
        },
        { Element.Water, new Dictionary<Element, Element>
            {
                { Element.Fire, Element.Steam },
                { Element.Soil, Element.Wetland }
            }
        }
    };
}

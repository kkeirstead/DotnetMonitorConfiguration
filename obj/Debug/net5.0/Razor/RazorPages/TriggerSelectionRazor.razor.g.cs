#pragma checksum "C:\Users\kkeirstead\source\repos\DotnetMonitorConfiguration\RazorPages\TriggerSelectionRazor.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ccf38c8cc8da4f595b2c51f07948525d6758f725"
// <auto-generated/>
#pragma warning disable 1591
namespace DotnetMonitorConfiguration.RazorPages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\kkeirstead\source\repos\DotnetMonitorConfiguration\RazorPages\TriggerSelectionRazor.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\kkeirstead\source\repos\DotnetMonitorConfiguration\RazorPages\TriggerSelectionRazor.razor"
using DotnetMonitorConfiguration.Models.Collection_Rules;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\kkeirstead\source\repos\DotnetMonitorConfiguration\RazorPages\TriggerSelectionRazor.razor"
using DotnetMonitorConfiguration.Models;

#line default
#line hidden
#nullable disable
    public partial class TriggerSelectionRazor : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 5 "C:\Users\kkeirstead\source\repos\DotnetMonitorConfiguration\RazorPages\TriggerSelectionRazor.razor"
 foreach(var triggerType in General._triggerTypes)
{

#line default
#line hidden
#nullable disable
            __builder.OpenElement(0, "button");
            __builder.AddAttribute(1, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 7 "C:\Users\kkeirstead\source\repos\DotnetMonitorConfiguration\RazorPages\TriggerSelectionRazor.razor"
                      (e => SelectTrigger(triggerType))

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(2, "data-toggle", "modal");
            __builder.AddAttribute(3, "data-target", "#productModal");
            __builder.AddAttribute(4, "class", "button");
            __builder.AddContent(5, 
#nullable restore
#line 8 "C:\Users\kkeirstead\source\repos\DotnetMonitorConfiguration\RazorPages\TriggerSelectionRazor.razor"
                                                                        triggerType

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
#nullable restore
#line 9 "C:\Users\kkeirstead\source\repos\DotnetMonitorConfiguration\RazorPages\TriggerSelectionRazor.razor"
        Console.WriteLine("Hit1");
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 13 "C:\Users\kkeirstead\source\repos\DotnetMonitorConfiguration\RazorPages\TriggerSelectionRazor.razor"
 
    void SelectTrigger(string triggerType)
    {
        CollectionRule temp = new CollectionRule(Guid.NewGuid().ToString());
        General._collectionRules.Append(temp);
        Console.WriteLine("Test");
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591

#pragma checksum "C:\Users\Aibek_Shulembekov\source\repos\.NET-A1\Logging\BrainstormSessions\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "beeb73c0dbfc2f5d425a541012e40ff0d7aa6b4d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"beeb73c0dbfc2f5d425a541012e40ff0d7aa6b4d", @"/Views/Home/Index.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<BrainstormSessions.ViewModels.StormSessionViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Aibek_Shulembekov\source\repos\.NET-A1\Logging\BrainstormSessions\Views\Home\Index.cshtml"
  
    ViewBag.Title = "Brainstormer";
    Layout = "_Layout";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2>Brainstorm Sessions</h2>\r\n<div class=\"row\">\r\n    <div class=\"col-md-9\">\r\n");
#nullable restore
#line 11 "C:\Users\Aibek_Shulembekov\source\repos\.NET-A1\Logging\BrainstormSessions\Views\Home\Index.cshtml"
         foreach (var storm in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"panel panel-default\">\r\n                <div class=\"panel-heading\">\r\n                    <a");
            BeginWriteAttribute("href", " href=\"", 387, "\"", 444, 1);
#nullable restore
#line 15 "C:\Users\Aibek_Shulembekov\source\repos\.NET-A1\Logging\BrainstormSessions\Views\Home\Index.cshtml"
WriteAttributeValue("", 394, Url.Action("Index","Session", new {id=@storm.Id}), 394, 50, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 15 "C:\Users\Aibek_Shulembekov\source\repos\.NET-A1\Logging\BrainstormSessions\Views\Home\Index.cshtml"
                                                                            Write(storm.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a> - ");
#nullable restore
#line 15 "C:\Users\Aibek_Shulembekov\source\repos\.NET-A1\Logging\BrainstormSessions\Views\Home\Index.cshtml"
                                                                                              Write(storm.DateCreated.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n                <div class=\"panel-body\">\r\n                    ");
#nullable restore
#line 18 "C:\Users\Aibek_Shulembekov\source\repos\.NET-A1\Logging\BrainstormSessions\Views\Home\Index.cshtml"
               Write(storm.IdeaCount);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ideas\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 21 "C:\Users\Aibek_Shulembekov\source\repos\.NET-A1\Logging\BrainstormSessions\Views\Home\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </div>
    <div class=""col-md-3"">
        <div class=""panel panel-primary"">
            <div class=""panel-heading"">
                Add New Session
            </div>
            <div class=""panel-body"">
                <form method=""post"" asp-controller=""Home"" asp-action=""Index"">
                    <fieldset class=""form-group"">
                        <label for=""sessionName"">New Session Name</label>
                        <input type=""text"" class=""form-control"" id=""sessionName"" name=""SessionName"" placeholder=""Name"" required />
                    </fieldset>
                    <input type=""submit"" value=""Save"" id=""SaveButton"" name=""SaveButton"" class=""btn btn-primary"" />
                </form>
            </div>
        </div>
    </div>
</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<BrainstormSessions.ViewModels.StormSessionViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591

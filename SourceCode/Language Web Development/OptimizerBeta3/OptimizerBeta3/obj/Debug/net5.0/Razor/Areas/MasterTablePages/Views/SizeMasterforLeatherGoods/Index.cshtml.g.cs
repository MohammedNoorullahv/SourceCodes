#pragma checksum "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\SizeMasterforLeatherGoods\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f97b275ebf681675dd0a45bb4c664376adea8d91"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_MasterTablePages_Views_SizeMasterforLeatherGoods_Index), @"mvc.1.0.view", @"/Areas/MasterTablePages/Views/SizeMasterforLeatherGoods/Index.cshtml")]
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
#nullable restore
#line 1 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\_ViewImports.cshtml"
using OptimizerBeta3;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\_ViewImports.cshtml"
using OptimizerBeta3.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f97b275ebf681675dd0a45bb4c664376adea8d91", @"/Areas/MasterTablePages/Views/SizeMasterforLeatherGoods/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"527eff3ca97d4db5b47de684df7bc995c022f187", @"/Areas/MasterTablePages/Views/_ViewImports.cshtml")]
    public class Areas_MasterTablePages_Views_SizeMasterforLeatherGoods_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<OptimizerBeta3.Models.MasterTables.SizeMasterforLeatherGoods>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_CreateButtonPartialView", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_TableButtonPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\SizeMasterforLeatherGoods\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"border backgroudColor\">\r\n    <div class=\"row\">\r\n        <div class=\"col-6\">\r\n            <h2 class=\"text-info\">Size Master for Leather Goods - Index</h2>\r\n        </div>\r\n        <div class=\"col-6 text-right\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "f97b275ebf681675dd0a45bb4c664376adea8d914691", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n    <div></div>\r\n    <br />\r\n\r\n    <div>\r\n");
#nullable restore
#line 20 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\SizeMasterforLeatherGoods\Index.cshtml"
         if (Model.Count() > 0)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <table class=\"table table-striped border\">\r\n                <tr class=\"table-secondary\">\r\n                    <th>\r\n                        ");
#nullable restore
#line 25 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\SizeMasterforLeatherGoods\Index.cshtml"
                   Write(Html.DisplayNameFor(m => m.Code));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 28 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\SizeMasterforLeatherGoods\Index.cshtml"
                   Write(Html.DisplayNameFor(m => m.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 31 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\SizeMasterforLeatherGoods\Index.cshtml"
                   Write(Html.DisplayNameFor(m => m.ShortDescription));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 34 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\SizeMasterforLeatherGoods\Index.cshtml"
                   Write(Html.DisplayNameFor(m => m.Measurement));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th class=\"text-center\"><label>Actions</label></th>\r\n                </tr>\r\n");
#nullable restore
#line 38 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\SizeMasterforLeatherGoods\Index.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
#nullable restore
#line 42 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\SizeMasterforLeatherGoods\Index.cshtml"
                       Write(Html.DisplayFor(m => item.Code));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 45 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\SizeMasterforLeatherGoods\Index.cshtml"
                       Write(Html.DisplayFor(m => item.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 48 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\SizeMasterforLeatherGoods\Index.cshtml"
                       Write(Html.DisplayFor(m => item.ShortDescription));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 51 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\SizeMasterforLeatherGoods\Index.cshtml"
                       Write(Html.DisplayFor(m => item.Measurement));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td style=\"width:150px\" class=\"text-center\">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "f97b275ebf681675dd0a45bb4c664376adea8d9110212", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 54 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\SizeMasterforLeatherGoods\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = item.Id;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 57 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\SizeMasterforLeatherGoods\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </table>\r\n");
#nullable restore
#line 59 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\SizeMasterforLeatherGoods\Index.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <p> No Size Master for Leather Goods exists.....</p>\r\n");
#nullable restore
#line 63 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\SizeMasterforLeatherGoods\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<OptimizerBeta3.Models.MasterTables.SizeMasterforLeatherGoods>> Html { get; private set; }
    }
}
#pragma warning restore 1591
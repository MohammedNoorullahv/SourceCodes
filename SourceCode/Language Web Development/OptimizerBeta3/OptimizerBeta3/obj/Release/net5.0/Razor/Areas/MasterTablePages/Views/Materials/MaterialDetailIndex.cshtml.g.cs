#pragma checksum "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bbc39e7ce6830f01c4490509d36e3b811cfcc343"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_MasterTablePages_Views_Materials_MaterialDetailIndex), @"mvc.1.0.view", @"/Areas/MasterTablePages/Views/Materials/MaterialDetailIndex.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bbc39e7ce6830f01c4490509d36e3b811cfcc343", @"/Areas/MasterTablePages/Views/Materials/MaterialDetailIndex.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"527eff3ca97d4db5b47de684df7bc995c022f187", @"/Areas/MasterTablePages/Views/_ViewImports.cshtml")]
    public class Areas_MasterTablePages_Views_Materials_MaterialDetailIndex : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<OptimizerBeta3.Models.MasterTables.MaterialDetails>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-info text-white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "MaterialDetailCreate", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success text-white  form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary text-white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "MaterialDetailEdit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success text-white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "MaterialDetailDetail", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger text-white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "MaterialDetailDelete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<br />
<div class=""border backgroudColor"">
    <div class=""backgroudWhite10Px"">
        <div class=""row"">
            <div class=""col-6"">
                <h2 class=""text-success"">Materials Info for Details Updates</h2>
            </div>
        </div>
        <div class=""row"">
            <div class=""col-3"">
                <label class=""col-form-label text-danger"">Code</label>
            </div>
            <div class=""col-3"">
                ");
#nullable restore
#line 20 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
           Write(TempData["Code"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n            <div class=\"col-3\">\r\n                <label class=\"col-form-label text-danger\">Category</label>\r\n            </div>\r\n            <div class=\"col-3\">\r\n                ");
#nullable restore
#line 26 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
           Write(TempData["Category"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n        <div class=\"row\">\r\n            <div class=\"col-3\">\r\n                <label class=\"col-form-label text-danger\">Type</label>\r\n            </div>\r\n            <div class=\"col-3\">\r\n                ");
#nullable restore
#line 34 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
           Write(TempData["Type"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n            <div class=\"col-3\">\r\n                <label class=\"col-form-label text-danger\">Sub Type</label>\r\n            </div>\r\n            <div class=\"col-3\">\r\n                ");
#nullable restore
#line 40 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
           Write(TempData["SubType"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n        <div class=\"row\">\r\n            <div class=\"col-3\">\r\n                <label class=\"col-form-label text-danger\">Description</label>\r\n            </div>\r\n            <div class=\"col-9\">\r\n                ");
#nullable restore
#line 48 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
           Write(TempData["Description"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </div>
        </div>
    </div>
</div>


<div class=""border backgroudColor"">
    <div class=""row"">
        <div class=""col-4"">
            <h2 class=""text-info"">Material Detail - Index</h2>
        </div>
        <div class=""col-4 text-center"">
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bbc39e7ce6830f01c4490509d36e3b811cfcc34310414", async() => {
                WriteLiteral("\r\n                <i class=\"fas fa-plus\"></i>\r\n                Create\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 61 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
                                                                                  WriteLiteral(TempData["Id"]);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n        <div class=\"col-4 text-center\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bbc39e7ce6830f01c4490509d36e3b811cfcc34312922", async() => {
                WriteLiteral("\r\n                Back to Materials\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n    <div></div>\r\n    <br />\r\n\r\n    <div>\r\n");
#nullable restore
#line 76 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
         if (Model.Count() > 0)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <table class=\"table table-striped border\">\r\n                <tr class=\"table-secondary\">\r\n                    <th>\r\n                        ");
#nullable restore
#line 81 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
                   Write(Html.DisplayNameFor(m => m.FKParty));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 84 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
                   Write(Html.DisplayNameFor(m => m.FKCurrency));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 87 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
                   Write(Html.DisplayNameFor(m => m.Rate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 90 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
                   Write(Html.DisplayNameFor(m => m.IsPrimeSupplier));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 93 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
                   Write(Html.DisplayNameFor(m => m.ApplicableFrom));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 96 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
                   Write(Html.DisplayNameFor(m => m.ApplicableTo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th><label>Actions</label></th>\r\n                </tr>\r\n");
#nullable restore
#line 100 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
#nullable restore
#line 104 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
                       Write(Html.DisplayFor(m => item.FKParty));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 107 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
                       Write(Html.DisplayFor(m => item.FKCurrency));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 110 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
                       Write(Html.DisplayFor(m => item.Rate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 113 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
                       Write(Html.DisplayFor(m => item.IsPrimeSupplier));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 116 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
                       Write(Html.DisplayFor(m => item.ApplicableFrom));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 119 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
                       Write(Html.DisplayFor(m => item.ApplicableTo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td style=\"width:150px\">\r\n                            <div class=\"btn-group\" role=\"group\">\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bbc39e7ce6830f01c4490509d36e3b811cfcc34320298", async() => {
                WriteLiteral("\r\n                                    <i class=\"fas fa-edit\"></i>\r\n                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-Id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 123 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
                                                                                                        WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-Id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bbc39e7ce6830f01c4490509d36e3b811cfcc34322797", async() => {
                WriteLiteral("\r\n                                    <i class=\"fas fa-list-alt\"></i>\r\n                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-Id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 127 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
                                                                                                          WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-Id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bbc39e7ce6830f01c4490509d36e3b811cfcc34325302", async() => {
                WriteLiteral("\r\n                                    <i class=\"fas fa-trash-alt\"></i>\r\n                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-Id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 131 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
                                                                                                         WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-Id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            </div>\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 137 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </table>\r\n");
#nullable restore
#line 139 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <p> No Materials exists.....</p>\r\n");
#nullable restore
#line 143 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\MasterTablePages\Views\Materials\MaterialDetailIndex.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>\r\n\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<OptimizerBeta3.Models.MasterTables.MaterialDetails>> Html { get; private set; }
    }
}
#pragma warning restore 1591
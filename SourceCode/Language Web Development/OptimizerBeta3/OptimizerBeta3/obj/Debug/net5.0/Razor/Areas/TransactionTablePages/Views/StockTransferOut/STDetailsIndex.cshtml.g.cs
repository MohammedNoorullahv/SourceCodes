#pragma checksum "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "86542f83846fb4f8205fa6a2396bca0dc91f0151"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_TransactionTablePages_Views_StockTransferOut_STDetailsIndex), @"mvc.1.0.view", @"/Areas/TransactionTablePages/Views/StockTransferOut/STDetailsIndex.cshtml")]
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
#line 1 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\_ViewImports.cshtml"
using OptimizerBeta3;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\_ViewImports.cshtml"
using OptimizerBeta3.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"86542f83846fb4f8205fa6a2396bca0dc91f0151", @"/Areas/TransactionTablePages/Views/StockTransferOut/STDetailsIndex.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"527eff3ca97d4db5b47de684df7bc995c022f187", @"/Areas/TransactionTablePages/Views/_ViewImports.cshtml")]
    public class Areas_TransactionTablePages_Views_StockTransferOut_STDetailsIndex : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<OptimizerBeta3.Models.TransactionTables.StockTransferDetail>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-info text-white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CreateTransferDetailForScanning", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success text-white  form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary text-white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger text-white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Cancel", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 7 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
  
    var stocktransfer = (OptimizerBeta3.Models.TransactionTables.StockTransfer)TempData["StockTransfer"];

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<br />
<div class=""border backgroudColor"">
    <div class=""backgroudWhite10Px"">
        <div class=""row"">
            <div class=""col-6"">
                <h2 class=""text-success"">Stock Transfer Info</h2>
            </div>
        </div>
        <div class=""row"">
            <div class=""col-3"">
                <label class=""col-form-label text-danger"">Stock Transfer No</label>
            </div>
            <div class=""col-3"">
                ");
#nullable restore
#line 23 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
           Write(stocktransfer.STNo);

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n            </div>\r\n            <div class=\"col-3\">\r\n                <label class=\"col-form-label text-danger\">Stock Transfer Date</label>\r\n            </div>\r\n            <div class=\"col-3\">\r\n                ");
#nullable restore
#line 29 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
           Write(stocktransfer.STDt);

#line default
#line hidden
#nullable disable
            WriteLiteral(@";
            </div>
        </div>
    </div>
</div>


<div>
    <div class=""row"">
        <div class=""col-6"">
            <h2 class=""text-info"">Stock Transfer Detail [ Out ] - Index</h2>
        </div>
        <div class=""col-3 text-center"">
");
#nullable restore
#line 46 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
             if (Convert.ToBoolean(stocktransfer.IsEntryCompleted) == false)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "86542f83846fb4f8205fa6a2396bca0dc91f01518883", async() => {
                WriteLiteral("\r\n                    <i class=\"fas fa-plus\"></i>\r\n                    Create\r\n                ");
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
#line 48 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
                                                                                                  WriteLiteral(stocktransfer.Id);

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
            WriteLiteral("\r\n");
#nullable restore
#line 52 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div class=\"col-3 text-center\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "86542f83846fb4f8205fa6a2396bca0dc91f015111710", async() => {
                WriteLiteral("\r\n                Back to Stock Transfer\r\n            ");
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
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n<div>\r\n");
#nullable restore
#line 65 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
     if (Model.Count() > 0)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("    <table class=\"table table-success table-striped table-hover table-bordered\">\r\n        <tr class=\"table-success text-info text-center\">\r\n            <th> ");
#nullable restore
#line 69 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
            Write(Html.DisplayNameFor(m => m.STNo));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </th>\r\n            <th> ");
#nullable restore
#line 70 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
            Write(Html.DisplayNameFor(m => m.STDt));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </th>\r\n            <th> ");
#nullable restore
#line 71 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
            Write(Html.DisplayNameFor(m => m.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </th>\r\n            <th> ");
#nullable restore
#line 72 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
            Write(Html.DisplayNameFor(m => m.DispatchedQuantity));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </th>\r\n            <th> ");
#nullable restore
#line 73 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
            Write(Html.DisplayNameFor(m => m.ReceivedQuantity));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </th>\r\n            <th> <label>Actions</label> </th>\r\n        </tr>\r\n\r\n");
#nullable restore
#line 77 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td> ");
#nullable restore
#line 80 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
                Write(Html.DisplayFor(m => item.STNo));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                <td> ");
#nullable restore
#line 81 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
                Write(Html.DisplayFor(m => item.STDt));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                <td> ");
#nullable restore
#line 82 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
                Write(Html.DisplayFor(m => item.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                <td> ");
#nullable restore
#line 83 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
                Write(Html.DisplayFor(m => item.DispatchedQuantity));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                <td> ");
#nullable restore
#line 84 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
                Write(Html.DisplayFor(m => item.ReceivedQuantity));

#line default
#line hidden
#nullable disable
            WriteLiteral(" td>\r\n                <td style=\"width:150px\">\r\n                    <div class=\"btn-group\" role=\"group\">\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "86542f83846fb4f8205fa6a2396bca0dc91f015117594", async() => {
                WriteLiteral("\r\n                            <i class=\"fas fa-edit\"></i>\r\n                        ");
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
#line 87 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
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
            WriteLiteral("\r\n\r\n\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "86542f83846fb4f8205fa6a2396bca0dc91f015120057", async() => {
                WriteLiteral("\r\n                            <i class=\"far fa-times-circle\"></i>\r\n                        ");
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
#line 92 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
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
            WriteLiteral("\r\n\r\n                    </div>\r\n\r\n\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 101 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </table>\r\n");
#nullable restore
#line 103 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p> No Stock Transfer exists.....</p>\r\n");
#nullable restore
#line 107 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsIndex.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<OptimizerBeta3.Models.TransactionTables.StockTransferDetail>> Html { get; private set; }
    }
}
#pragma warning restore 1591

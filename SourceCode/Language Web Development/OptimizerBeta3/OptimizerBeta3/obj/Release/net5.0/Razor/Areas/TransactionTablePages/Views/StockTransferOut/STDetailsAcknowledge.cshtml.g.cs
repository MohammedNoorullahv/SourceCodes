#pragma checksum "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d112324b659fe7cba62df9e468604f1c52603293"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_TransactionTablePages_Views_StockTransferOut_STDetailsAcknowledge), @"mvc.1.0.view", @"/Areas/TransactionTablePages/Views/StockTransferOut/STDetailsAcknowledge.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d112324b659fe7cba62df9e468604f1c52603293", @"/Areas/TransactionTablePages/Views/StockTransferOut/STDetailsAcknowledge.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"527eff3ca97d4db5b47de684df7bc995c022f187", @"/Areas/TransactionTablePages/Views/_ViewImports.cshtml")]
    public class Areas_TransactionTablePages_Views_StockTransferOut_STDetailsAcknowledge : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<OptimizerBeta3.Models.TransactionTables.StockTransferDetail>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-info text-white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "STDAcknowledgeTotalQty", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success text-white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "STDAcknowledge", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "UpdateStockTransferIn", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Pending", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
  
    ViewData["Title"] = "STDetailsAcknowledge";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 7 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
  
    var stocktransfer = (OptimizerBeta3.Models.TransactionTables.StockTransfer)TempData["StockTransfer"];

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div>\r\n    <div class=\"row\">\r\n        <div class=\"col-6\">\r\n            <h2 class=\"text-info\">Stock Transfer [ In ] - Acknowledge</h2>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<div>\r\n");
#nullable restore
#line 20 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
     if (Model.Count() > 0)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <table class=\"table table-success table-striped table-hover table-bordered\">\r\n            <tr class=\"table-success text-info text-center\">\r\n                <th> ");
#nullable restore
#line 24 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
                Write(Html.DisplayNameFor(m => m.EANCode));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </th>\r\n                <th> ");
#nullable restore
#line 25 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
                Write(Html.DisplayNameFor(m => m.StockNo));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </th>\r\n                <th> ");
#nullable restore
#line 26 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
                Write(Html.DisplayNameFor(m => m.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </th>\r\n                <th> ");
#nullable restore
#line 27 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
                Write(Html.DisplayNameFor(m => m.Colour));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </th>\r\n                <th> ");
#nullable restore
#line 28 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
                Write(Html.DisplayNameFor(m => m.Size));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </th>\r\n                <th> ");
#nullable restore
#line 29 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
                Write(Html.DisplayNameFor(m => m.DispatchedQuantity));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </th>\r\n                <th> ");
#nullable restore
#line 30 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
                Write(Html.DisplayNameFor(m => m.ReceivedQuantity));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </th>\r\n                <th> <label>Actions</label> </th>\r\n            </tr>\r\n\r\n");
#nullable restore
#line 34 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td> ");
#nullable restore
#line 37 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
                    Write(Html.DisplayFor(m => item.EANCode));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                    <td> ");
#nullable restore
#line 38 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
                    Write(Html.DisplayFor(m => item.StockNo));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                    <td> ");
#nullable restore
#line 39 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
                    Write(Html.DisplayFor(m => item.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                    <td> ");
#nullable restore
#line 40 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
                    Write(Html.DisplayFor(m => item.Colour));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                    <td> ");
#nullable restore
#line 41 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
                    Write(Html.DisplayFor(m => item.Size));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                    <td> ");
#nullable restore
#line 42 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
                    Write(Html.DisplayFor(m => item.DispatchedQuantity));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                    <td> ");
#nullable restore
#line 43 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
                    Write(Html.DisplayFor(m => item.ReceivedQuantity));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n\r\n                    <td style=\"width:150px\">\r\n                        <div class=\"btn-group\" role=\"group\">\r\n");
#nullable restore
#line 47 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
                             if (item.ReceivedQuantity == 0)
                            {


#line default
#line hidden
#nullable disable
            WriteLiteral("                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d112324b659fe7cba62df9e468604f1c5260329313402", async() => {
                WriteLiteral("\r\n                                    <i class=\"fas fa-check\"></i>\r\n                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-Id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 50 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
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
            WriteLiteral("\r\n");
#nullable restore
#line 53 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
                            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 54 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
                             if (item.ReceivedQuantity < item.DispatchedQuantity)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d112324b659fe7cba62df9e468604f1c5260329316565", async() => {
                WriteLiteral("\r\n                                    <i class=\"fas fa-edit\"></i>\r\n                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-Id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 56 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
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
            WriteLiteral("\r\n");
#nullable restore
#line 59 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </div>\r\n\r\n\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 65 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </table>\r\n");
            WriteLiteral("        <div class=\"form-group row\">\r\n            <div class=\"col-6 offset-3\">\r\n\r\n                <div class=\"row\">\r\n                    <div class=\"col-4\">\r\n");
            WriteLiteral("                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d112324b659fe7cba62df9e468604f1c5260329320000", async() => {
                WriteLiteral("\r\n                            <i class=\"far fa-times-circle\"></i>\r\n                            Save\r\n                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-Id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 74 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
                                                                                                WriteLiteral(stocktransfer.Id);

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
            WriteLiteral("\r\n                    </div>\r\n                    <div class=\"col-4\">\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d112324b659fe7cba62df9e468604f1c5260329322603", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 85 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p> No Stock Transfer exists.....</p>\r\n");
#nullable restore
#line 89 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\StockTransferOut\STDetailsAcknowledge.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n\r\n");
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

#pragma checksum "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bcd6c782f1d1f7fa8f223be3cfe7abe9470f99de"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_TransactionTablePages_Views_AllTransaction_Index), @"mvc.1.0.view", @"/Areas/TransactionTablePages/Views/AllTransaction/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bcd6c782f1d1f7fa8f223be3cfe7abe9470f99de", @"/Areas/TransactionTablePages/Views/AllTransaction/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"527eff3ca97d4db5b47de684df7bc995c022f187", @"/Areas/TransactionTablePages/Views/_ViewImports.cshtml")]
    public class Areas_TransactionTablePages_Views_AllTransaction_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<OptimizerBeta3.Models.TransactionTables.AllTransaction>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 7 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
  
    var invoice = (OptimizerBeta3.Models.TransactionTables.InvoiceToPerson)TempData["Invoice"];

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n\r\n<div>\r\n");
#nullable restore
#line 13 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
     using (Html.BeginForm("Index", "AllTransaction", FormMethod.Post))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <div class=""form-group row"">
            <div class=""col-4"">
                <label>Stock No / EAN Code</label>
            </div>
            <div class=""col-2"">
                <input id=""EANCode"" name=""EANCode"" type=""text"" autofocus=""autofocus"" />
            </div>
            <div class=""col-1"">
                <input id=""submit1"" type=""submit"" value=""Insert"" />
            </div>
        </div>
");
#nullable restore
#line 26 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n\r\n<h3>Invoice Details</h3>\r\n");
#nullable restore
#line 30 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
 if (Model.Count() > 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("<table class=\"table table-success table-striped table-hover table-bordered\">\r\n    <tr class=\"table-success text-info\">\r\n        <th> ");
#nullable restore
#line 34 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
        Write(Html.DisplayNameFor(m => m.TranDate));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </th>\r\n        <th> ");
#nullable restore
#line 35 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
        Write(Html.DisplayNameFor(m => m.TranRefNo));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n        <th> ");
#nullable restore
#line 36 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
        Write(Html.DisplayNameFor(m => m.TransactionType));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n        <th> ");
#nullable restore
#line 37 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
        Write(Html.DisplayNameFor(m => m.InwardQuantity));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n        <th> ");
#nullable restore
#line 38 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
        Write(Html.DisplayNameFor(m => m.OutwardQuantity));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n        <th> ");
#nullable restore
#line 39 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
        Write(Html.DisplayNameFor(m => m.BalanceQuantity));

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n    </tr>\r\n\r\n");
#nullable restore
#line 42 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("<tr>\r\n    <td> ");
#nullable restore
#line 45 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
    Write(item.TranDate.ToString("dd-MMM-yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("  </td>\r\n    <td> ");
#nullable restore
#line 46 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
    Write(Html.DisplayFor(m => item.TranRefNo));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n    <td> ");
#nullable restore
#line 47 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
    Write(Html.DisplayFor(m => item.TransactionType));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n    <td> ");
#nullable restore
#line 48 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
    Write(Html.DisplayFor(m => item.InwardQuantity));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n    <td> ");
#nullable restore
#line 49 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
    Write(Html.DisplayFor(m => item.OutwardQuantity));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n    <td> ");
#nullable restore
#line 50 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
    Write(Html.DisplayFor(m => item.BalanceQuantity));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n</tr>\r\n");
#nullable restore
#line 52 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>\r\n");
#nullable restore
#line 54 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p> No Stock Transfer Details exists.....</p>\r\n");
#nullable restore
#line 58 "E:\Noor\Language Application\25-Aug-21\11-Nov-21\OptimizerBeta3\OptimizerBeta3\Areas\TransactionTablePages\Views\AllTransaction\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<OptimizerBeta3.Models.TransactionTables.AllTransaction>> Html { get; private set; }
    }
}
#pragma warning restore 1591

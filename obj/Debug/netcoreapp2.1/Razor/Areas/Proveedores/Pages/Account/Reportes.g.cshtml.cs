#pragma checksum "C:\Users\AJpag\Downloads\Sistem_Ventas\Sistem_Ventas\Areas\Proveedores\Pages\Account\Reportes.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e8493b32fca66d3e67c6c0cbb817ad70ea77ee0a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Sistem_Ventas.Areas.Proveedores.Pages.Account.Areas_Proveedores_Pages_Account_Reportes), @"mvc.1.0.razor-page", @"/Areas/Proveedores/Pages/Account/Reportes.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Areas/Proveedores/Pages/Account/Reportes.cshtml", typeof(Sistem_Ventas.Areas.Proveedores.Pages.Account.Areas_Proveedores_Pages_Account_Reportes), null)]
namespace Sistem_Ventas.Areas.Proveedores.Pages.Account
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\AJpag\Downloads\Sistem_Ventas\Sistem_Ventas\Areas\Proveedores\Pages\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e8493b32fca66d3e67c6c0cbb817ad70ea77ee0a", @"/Areas/Proveedores/Pages/Account/Reportes.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"89107043feccb5be17d278b79a74a16038801bc4", @"/Areas/Proveedores/Pages/_ViewImports.cshtml")]
    public class Areas_Proveedores_Pages_Account_Reportes : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString(" responsive-img"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("placeholder", new global::Microsoft.AspNetCore.Html.HtmlString("Monto a pagar"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "Proveedores", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_Ticket", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "C:\Users\AJpag\Downloads\Sistem_Ventas\Sistem_Ventas\Areas\Proveedores\Pages\Account\Reportes.cshtml"
  
    var imagen = Model.Input.Email + ".png";
    var nombre = Model.Input.Proveedor ;

#line default
#line hidden
            BeginContext(124, 506, true);
            WriteLiteral(@"<div class=""container2"">
    <div class=""modal-content"">
        <center>
            <h5>Reportes proveedor</h5>
        </center>
        <div class=""row"">
            <div class=""col s3 m3"">
                <div class=""card "">
                    <div class=""card-content white-text"">
                        <center>
                            <span class=""card-title blue-grey-text  "">Foto</span>
                            <output id=""proveedorReporte"">
                                ");
            EndContext();
            BeginContext(630, 72, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "3bcc904bc41e471f9640b05b187c9555", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 664, "~/images/fotos/Proveedores/", 664, 27, true);
#line 19 "C:\Users\AJpag\Downloads\Sistem_Ventas\Sistem_Ventas\Areas\Proveedores\Pages\Account\Reportes.cshtml"
AddHtmlAttributeValue("", 691, imagen, 691, 7, false);

#line default
#line hidden
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(702, 349, true);
            WriteLiteral(@"
                            </output>
                        </center>
                    </div>
                </div>
            </div>
            <div class=""col s6 m6"">
                <table class=""table"">
                    <thead>
                        <tr>
                            <th>
                                ");
            EndContext();
            BeginContext(1052, 51, false);
#line 30 "C:\Users\AJpag\Downloads\Sistem_Ventas\Sistem_Ventas\Areas\Proveedores\Pages\Account\Reportes.cshtml"
                           Write(Html.DisplayNameFor(model => model.Input.Proveedor));

#line default
#line hidden
            EndContext();
            BeginContext(1103, 223, true);
            WriteLiteral("\r\n                            </th>\r\n                        </tr>\r\n                    </thead>\r\n                    <tbody>\r\n                        <tr>\r\n                            <td>\r\n                                ");
            EndContext();
            BeginContext(1327, 47, false);
#line 37 "C:\Users\AJpag\Downloads\Sistem_Ventas\Sistem_Ventas\Areas\Proveedores\Pages\Account\Reportes.cshtml"
                           Write(Html.DisplayFor(model => model.Input.Proveedor));

#line default
#line hidden
            EndContext();
            BeginContext(1374, 416, true);
            WriteLiteral(@"
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class=""col s6 m6"">
                <table class=""table"">
                    <tbody>
                        <tr>
                            <td>
                                <div class=""input-field col s12"">
                                    ");
            EndContext();
            BeginContext(1790, 1071, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f9a16592ff0347ccb5b54f3c8e8e9e73", async() => {
                BeginContext(1833, 187, true);
                WriteLiteral("\r\n                                        <div class=\"row\">\r\n                                            <div class=\"input-field col s6\">\r\n                                                ");
                EndContext();
                BeginContext(2020, 59, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "3e4002d7d47846ffb0577453598197f6", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#line 52 "C:\Users\AJpag\Downloads\Sistem_Ventas\Sistem_Ventas\Areas\Proveedores\Pages\Account\Reportes.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Input1.Pago);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2079, 50, true);
                WriteLiteral("\r\n                                                ");
                EndContext();
                BeginContext(2129, 37, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "414863007c39493c8b9db734d3c8cc2b", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#line 53 "C:\Users\AJpag\Downloads\Sistem_Ventas\Sistem_Ventas\Areas\Proveedores\Pages\Account\Reportes.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Input1.Pago);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2166, 50, true);
                WriteLiteral("\r\n                                                ");
                EndContext();
                BeginContext(2216, 66, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "949a2f0ad3714d4983b7459c728a40ad", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#line 54 "C:\Users\AJpag\Downloads\Sistem_Ventas\Sistem_Ventas\Areas\Proveedores\Pages\Account\Reportes.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Input1.Pago);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2282, 101, true);
                WriteLiteral("\r\n                                                <span id=\"Message\" class=\"red-text text-darken-4 \">");
                EndContext();
                BeginContext(2384, 43, false);
#line 55 "C:\Users\AJpag\Downloads\Sistem_Ventas\Sistem_Ventas\Areas\Proveedores\Pages\Account\Reportes.cshtml"
                                                                                              Write(Html.DisplayFor(m => m.Input1.ErrorMessage));

#line default
#line hidden
                EndContext();
                BeginContext(2427, 427, true);
                WriteLiteral(@"  </span>
                                            </div>
                                            <div class=""input-field col s6"">
                                                <button type=""submit"" class=""waves-effect waves-light btn-small light-green darken-3"">Pagar</button>
                                            </div>
                                        </div>
                                    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Area = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2861, 346, true);
            WriteLiteral(@"
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div class=""row"">
            <a class=""waves-effect waves-light orange darken-2 btn-small"" onclick=""printThisDiv('ticket')"">Ticket</a>
");
            EndContext();
#line 71 "C:\Users\AJpag\Downloads\Sistem_Ventas\Sistem_Ventas\Areas\Proveedores\Pages\Account\Reportes.cshtml"
             if (0 < Model.Input.ProveedoresReport.Count)
            {
                foreach (var item in Model.Input.ProveedoresReport)
                {

#line default
#line hidden
            BeginContext(3369, 71, true);
            WriteLiteral("                    <span>Deuda:<span class=\"green-text text-darken-3\">");
            EndContext();
            BeginContext(3441, 10, false);
#line 75 "C:\Users\AJpag\Downloads\Sistem_Ventas\Sistem_Ventas\Areas\Proveedores\Pages\Account\Reportes.cshtml"
                                                                  Write(item.Deuda);

#line default
#line hidden
            EndContext();
            BeginContext(3451, 16, true);
            WriteLiteral("</span></span>\r\n");
            EndContext();
#line 76 "C:\Users\AJpag\Downloads\Sistem_Ventas\Sistem_Ventas\Areas\Proveedores\Pages\Account\Reportes.cshtml"
                }
               
            }
            else
            {

#line default
#line hidden
            BeginContext(3551, 62, true);
            WriteLiteral("                <a class=\"btn-flat disabled\">Deuda:$00.0</a>\r\n");
            EndContext();
#line 82 "C:\Users\AJpag\Downloads\Sistem_Ventas\Sistem_Ventas\Areas\Proveedores\Pages\Account\Reportes.cshtml"
            }

#line default
#line hidden
            BeginContext(3628, 12, true);
            WriteLiteral("            ");
            EndContext();
            BeginContext(3640, 26, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "cca0b7d761e24ec995e1bdef7c35158d", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3666, 118, true);
            WriteLiteral("\r\n        </div>\r\n        <div class=\"modal-footer\">\r\n            <label id=\"Message\" class=\"red-text text-darken-4 \">");
            EndContext();
            BeginContext(3785, 42, false);
#line 86 "C:\Users\AJpag\Downloads\Sistem_Ventas\Sistem_Ventas\Areas\Proveedores\Pages\Account\Reportes.cshtml"
                                                           Write(Html.DisplayFor(m => m.Input.ErrorMessage));

#line default
#line hidden
            EndContext();
            BeginContext(3827, 48, true);
            WriteLiteral("  </label>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(3893, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
                BeginContext(3900, 52, false);
#line 91 "C:\Users\AJpag\Downloads\Sistem_Ventas\Sistem_Ventas\Areas\Proveedores\Pages\Account\Reportes.cshtml"
Write(await Html.PartialAsync("_ValidationScriptsPartial"));

#line default
#line hidden
                EndContext();
                BeginContext(3952, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            BeginContext(3957, 2, true);
            WriteLiteral("\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ReportesModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ReportesModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ReportesModel>)PageContext?.ViewData;
        public ReportesModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591

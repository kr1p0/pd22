#pragma checksum "C:\Users\kr1p0\Desktop\PracaDyplomowaM\PracaDyplomowa\PracaDyplomowa\Pages\Actions\AddAction.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "83a595215cc0d58c36c3ffce69fde2267f3149f4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(PracaDyplomowa.Pages.Actions.Pages_Actions_AddAction), @"mvc.1.0.razor-page", @"/Pages/Actions/AddAction.cshtml")]
namespace PracaDyplomowa.Pages.Actions
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
#line 1 "C:\Users\kr1p0\Desktop\PracaDyplomowaM\PracaDyplomowa\PracaDyplomowa\Pages\_ViewImports.cshtml"
using PracaDyplomowa;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\kr1p0\Desktop\PracaDyplomowaM\PracaDyplomowa\PracaDyplomowa\Pages\Actions\AddAction.cshtml"
using PracaDyplomowa.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"83a595215cc0d58c36c3ffce69fde2267f3149f4", @"/Pages/Actions/AddAction.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c849560f95a95ddb1e773f5dac795c2f6362a18e", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Actions_AddAction : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("menuBackIcon"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/img/white/arrow-return-left.svg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "/Actions/ActionOverview", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width: 100%; margin-top: 50px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/img/white/cone-striped.svg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("addActionForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("displaySpinner"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onload", new global::Microsoft.AspNetCore.Html.HtmlString("onloadAddAction()"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<!-- For Ajax-->\r\n");
#nullable restore
#line 9 "C:\Users\kr1p0\Desktop\PracaDyplomowaM\PracaDyplomowa\PracaDyplomowa\Pages\Actions\AddAction.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "83a595215cc0d58c36c3ffce69fde2267f3149f47546", async() => {
                WriteLiteral("\r\n    <ul class=\"nav nav-tabs mainNavTabs \">\r\n        <li");
                BeginWriteAttribute("class", " class=\"", 236, "\"", 244, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n            <p style=\"color:#fff; font-size:1em\">Dodaj akcje</p>\r\n        </li>\r\n\r\n        <li class=\" \" style=\"margin-left: auto; margin-right: 0; \">\r\n            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "83a595215cc0d58c36c3ffce69fde2267f3149f48198", async() => {
                    WriteLiteral("\r\n                ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "83a595215cc0d58c36c3ffce69fde2267f3149f48477", async() => {
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\r\n            ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n        </li>\r\n    </ul>\r\n\r\n    <div class=\"innerContainer2\">\r\n\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "83a595215cc0d58c36c3ffce69fde2267f3149f410759", async() => {
                    WriteLiteral(@"
            <div id=""reminderViewWindow"" class=""m-3"">
                <div class=""form-row justify-content-center "" style=""margin-top: 25px; margin-bottom:20px; width:100%"">

                    <!--FIRST COLUMN-->

                    <div class=""fitstFrom2Column"">
                        <div style=""padding:3%"">
                            ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "83a595215cc0d58c36c3ffce69fde2267f3149f411388", async() => {
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral(@"
                        </div>
                        <div class=""input-group justify-content-center mt-1"">
                            <input type=""submit"" class=""btn btn-light btn-sm m-1"" value=""Zapisz""/>
                        </div>


                    </div>

                    <!--SECOND COLUMN-->
                    <div class=""secondFrom2Column"">

                        <!--ALTERNATIVE 4 FIRST COLUMN-->
                        <div class=""alternativeElement"" style=""margin:-20px 0 20px 0; background-color:#343a40 ;
                        padding:6px; border-radius:6px"">
                            <input type=""submit"" class=""btn btn-light btn-sm mr-2"" value=""Zapisz"" />
                        </div>
                        <!--ALTERNATIVE-->

                        <div class=""form-group mb-3  form-inline "" style=""margin-top:-12px"">
                            <label for=""DodajAkcjeTypZdarzenia"" class=""mb-1 labelInline"">Typ zdarzenia</label>
                            <in");
                    WriteLiteral(@"put type=""text"" class=""form-control customFormInputv2 inputInline"" id=""DodajAkcjeTypZdarzenia""
                                   required>
                        </div>


                        <div class=""form-group mb-0  form-inline "" style=""margin-top:-12px"">
                            <label for=""DodajAkcjeDataZdarzenia"" class=""mb-1 labelInline"">Rozpoczecie</label>
                            <input type=""date"" class=""form-control customFormInputv2 inputInline"" id=""DodajAkcjeDataZdarzenia""");
                    BeginWriteAttribute("value", "\r\n                                   value=", 2700, "", 2779, 1);
#nullable restore
#line 64 "C:\Users\kr1p0\Desktop\PracaDyplomowaM\PracaDyplomowa\PracaDyplomowa\Pages\Actions\AddAction.cshtml"
WriteAttributeValue("", 2743, DateTime.Now.ToString("yyyy-MM-dd"), 2743, 36, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(@" required />
                        </div>

                        <div class=""form-group  mb-3  form-inline "" style=""margin-top:-12px"">
                            <label for=""EPrzypDataRozpoczecia"" class=""mb-1 labelInline""></label>
                            <input type=""time"" class=""form-control customFormInputv2 inputInline"" id=""DodajAkcjeCzasZdarzenia""
                                   required>
                        </div>

                        <div class=""form-group  mb-0  form-inline "" style=""margin-top:-12px"">
                            <label for=""DodajAkcjeDataZakonczenia"" class=""mb-1 labelInline"">Zakonczenie</label>
                            <input type=""date"" class=""form-control customFormInputv2 inputInline"" id=""DodajAkcjeDataZakonczenia""");
                    BeginWriteAttribute("value", "\r\n                                   value=", 3564, "", 3643, 1);
#nullable restore
#line 76 "C:\Users\kr1p0\Desktop\PracaDyplomowaM\PracaDyplomowa\PracaDyplomowa\Pages\Actions\AddAction.cshtml"
WriteAttributeValue("", 3607, DateTime.Now.ToString("yyyy-MM-dd"), 3607, 36, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(@" required>

                        </div>
                        <div class=""form-group  mb-3  form-inline "" style=""margin-top:-12px"">
                            <label for=""DodajAkcjeCzasZakonczenia"" class=""mb-1 labelInline""></label>
                            <input type=""time"" class=""form-control customFormInputv2 inputInline"" id=""DodajAkcjeCzasZakonczenia""
                                   required>
                        </div>


                        <div class=""form-group  mb-32  form-inline "" style=""margin-top:-12px"">
                            <label for=""DodajAkcjeLiczebnosc"" class=""mb-1 labelInline"">Liczebność</label>
                            <input type=""text"" class=""form-control customFormInputv2 inputInline"" id=""DodajAkcjeLiczebnosc""
                                   readonly>
                        </div>



                        <div class=""form-group   form-inline "" style=""margin-top:-12px"">
                            <label for=""addActionUserTable"" class=""");
                    WriteLiteral("mb-1 labelInline\">Grupa</label>\r\n\r\n                            <div class=\"ActionUserTableContainer\">\r\n                                <table class=\"ActionUserTable\" id=\"addActionUserTable\">\r\n                                    <thead></thead>\r\n");
#nullable restore
#line 100 "C:\Users\kr1p0\Desktop\PracaDyplomowaM\PracaDyplomowa\PracaDyplomowa\Pages\Actions\AddAction.cshtml"
                                      
                                        for (int i = 0; i < Model.ListOfUsers.Count; i++)
                                        {
                                            if (@Model.ListOfUsers[i].Zatwierdzony == "n")
                                            {
                                                continue;
                                            }

#line default
#line hidden
#nullable disable
                    WriteLiteral("                                            <tbody>\r\n                                                <tr");
                    BeginWriteAttribute("class", " class=\"", 5435, "\"", 5443, 0);
                    EndWriteAttribute();
                    WriteLiteral(@" style="" cursor: pointer"" onclick=""addMemberActionTableOnClick(this)"">

                                                    <td data-label=""Dodaj"" class=""mr-2"">
                                                        <input type=""checkbox"" class=""ActionCheckBox"" onclick=""return false;""  />
                                                    </td>

                                                    <td>
                                                        <input type=""hidden""");
                    BeginWriteAttribute("value", " value=\"", 5934, "\"", 5976, 1);
#nullable restore
#line 115 "C:\Users\kr1p0\Desktop\PracaDyplomowaM\PracaDyplomowa\PracaDyplomowa\Pages\Actions\AddAction.cshtml"
WriteAttributeValue("", 5942, Model.ListOfUsers[i].UzytkownikId, 5942, 34, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(" />\r\n                                                    </td>\r\n\r\n                                                    <td>\r\n                                                        <input type=\"hidden\"");
                    BeginWriteAttribute("value", " value=\"", 6177, "\"", 6219, 1);
#nullable restore
#line 119 "C:\Users\kr1p0\Desktop\PracaDyplomowaM\PracaDyplomowa\PracaDyplomowa\Pages\Actions\AddAction.cshtml"
WriteAttributeValue("", 6185, Model.ListOfUsers[i].Jednostka_id, 6185, 34, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(" />\r\n                                                    </td>\r\n\r\n                                                    <td data-label=\"\">");
#nullable restore
#line 122 "C:\Users\kr1p0\Desktop\PracaDyplomowaM\PracaDyplomowa\PracaDyplomowa\Pages\Actions\AddAction.cshtml"
                                                                 Write(CustomStr.Adjust(Model.ListOfUsers[i].Imie));

#line default
#line hidden
#nullable disable
                    WriteLiteral("</td>\r\n\r\n                                                    <td data-label=\"\">");
#nullable restore
#line 124 "C:\Users\kr1p0\Desktop\PracaDyplomowaM\PracaDyplomowa\PracaDyplomowa\Pages\Actions\AddAction.cshtml"
                                                                 Write(CustomStr.Adjust(Model.ListOfUsers[i].Nazwisko));

#line default
#line hidden
#nullable disable
                    WriteLiteral("</td>\r\n                                                </tr>\r\n                                            </tbody>\r\n");
#nullable restore
#line 127 "C:\Users\kr1p0\Desktop\PracaDyplomowaM\PracaDyplomowa\PracaDyplomowa\Pages\Actions\AddAction.cshtml"
                                        }
                                    

#line default
#line hidden
#nullable disable
                    WriteLiteral("                                </table>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_6.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    </div>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PracaDyplomowa.Pages.Actions.AddActionModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<PracaDyplomowa.Pages.Actions.AddActionModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<PracaDyplomowa.Pages.Actions.AddActionModel>)PageContext?.ViewData;
        public PracaDyplomowa.Pages.Actions.AddActionModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
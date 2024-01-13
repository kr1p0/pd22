#pragma checksum "C:\Users\kr1p0\Desktop\PracaDyplomowaM\PracaDyplomowa\PracaDyplomowa\Pages\QRCode\QRCodeGenerate.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "edbf28a7a0cdd2f7dc621494705aa1ff5d06f21b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(PracaDyplomowa.Pages.QRCode.Pages_QRCode_QRCodeGenerate), @"mvc.1.0.razor-page", @"/Pages/QRCode/QRCodeGenerate.cshtml")]
namespace PracaDyplomowa.Pages.QRCode
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"edbf28a7a0cdd2f7dc621494705aa1ff5d06f21b", @"/Pages/QRCode/QRCodeGenerate.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c849560f95a95ddb1e773f5dac795c2f6362a18e", @"/Pages/_ViewImports.cshtml")]
    public class Pages_QRCode_QRCodeGenerate : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("mb-1"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("height:26px;margin-left:40% "), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/img/white/link.svg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("height:100%"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/img/black/qr-code.svg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<script src=""https://cdn.rawgit.com/davidshimjs/qrcodejs/gh-pages/qrcode.min.js""></script>
<script>

    var equipmentList = null;
    var selectedEquipmentItem = null;

    document.addEventListener(""DOMContentLoaded"", function () {
        AjaxGetConterPartyNames();
        animateQrPanelOnStart();

    });

    function animateQrPanelOnStart() {
        setTimeout(function () {
            document.getElementById(""a4DiemensionsWin"").style.marginLeft = ""25px"";
        }, 400);

        setTimeout(function () {
            document.getElementById(""a4DiemensionsWin"").style.marginLeft = ""5px"";
        }, 1000);
    }

    function generateQr() {


        document.getElementById(""generatedGrcodePreview"").innerHTML = """";
        document.getElementById(""generatedGrcodeTrueSize"").innerHTML = """";


        if (selectedEquipmentItem == null || selectedEquipmentItem == undefined) {
            alert(""Należy wybrać urządzenie"")
            return;
        }


        new QRCode(d");
            WriteLiteral(@"ocument.getElementById(""generatedGrcodePreview""), {
            text: selectedEquipmentItem,
            width: 260,
            height: 260,
            colorDark: ""#5868bf"",
            colorLight: ""transparent"",
            correctLevel: QRCode.CorrectLevel.H
        });


        new QRCode(document.getElementById(""generatedGrcodeTrueSize""), {
            text: selectedEquipmentItem,
            width: document.getElementById(""displayQrSizeX"").value,
            height: document.getElementById(""displayQrSizeY"").value,
            colorDark: ""#000"",
            colorLight: ""#ffffff"",
            correctLevel: QRCode.CorrectLevel.H
        });

        $(""#generatedGrcodePreviewContainer"").slideDown(300);
        document.getElementById(""innerContainer2"").style.filter = 'blur(5px)';
        document.getElementById(""innerContainer2"").style.pointerEvents = ""none"";

    }


    /* Ajax on Get // Gets EquipmentList for select field */
    function AjaxGetConterPartyNames() {
        ");
            WriteLiteral(@"var options = {};
        options.url = ""/QRCode/QRCodeGenerate?handler=SelectAllEquipmentInfo"";
        options.type = ""GET"";
        options.dataType = ""json"";
        options.success = function (data) {
            equipmentList = data;
            data.forEach(function (element) {
                $(""#selectItemForQr"").append(""<option>""
                    + element.nazwa + ""</option>"");  /* Why z zmienna z malej?*/
            });
        };
        options.error = function () {
            alert(""Error while making Ajax call!"");
        };
        $.ajax(options);
    }

    function getSelectedEquipmentItem(sender) {
        selectedEquipmentItem = equipmentList[sender.selectedIndex - 1].id
    }



    function copyQrSizeValue(sender) {
        if (sender > 2199) {
            document.getElementById(""displayQrSizeX"").value = 2200;
            document.getElementById(""displayQrSizeY"").value = 2200;
        }
        document.getElementById(""displayQrSizeY"").value = document.");
            WriteLiteral(@"getElementById(""displayQrSizeX"").value;
        document.getElementById(""compareQrDiemensionsToA4"").style.width = document.getElementById(""displayQrSizeX"").value / 8 + ""px"";
        document.getElementById(""compareQrDiemensionsToA4"").style.height = document.getElementById(""displayQrSizeY"").value / 8 + ""px"";
    }


    function printQr() {
        document.getElementById(""generatedGrcodeTrueSize"").style.display = """";
        window.print();
        document.getElementById(""generatedGrcodeTrueSize"").style.display = ""none"";
    }

    function closeQrPreviewWin() {
        $(""#generatedGrcodePreviewContainer"").slideUp(300);
        document.getElementById(""innerContainer2"").style.filter = 'blur(0)';
        document.getElementById(""innerContainer2"").style.pointerEvents = """";
    }
</script>

<style>
    .slider {
        -webkit-appearance: none;
        width: 30%;
        height: 10px;
        border-radius: 5px;
        background: #d3d3d3;
        outline: none;
        opacity: 0");
            WriteLiteral(@".7;
    }

        .slider::-webkit-slider-thumb {
            -webkit-appearance: none;
            appearance: none;
            width: 25px;
            height: 25px;
            background: #04AA6D;
            cursor: pointer;
        }

        .slider::-moz-range-thumb {
            width: 25px;
            height: 25px;
            border-radius: 50%;
            background: #04AA6D;
            cursor: pointer;
        }
</style>

<div class=""innerContainer"">

    <div id=""innerContainer2"" class=""hideOnPrint"">
        <div class=""row  justify-content-center"" style=""margin: 40px 0 10px 0;"">


            <!-- First column -->
            <div class="" bg-dark mb-1"" style=""border-radius: 8px ; padding:20px 10px;"">

                <div class=""mb-3"">
                    <label for=""selectItemForQr"" class=""text-white""> Wybierz urządzenie</label>
                    <select class=""form-control  bg-white mb-2"" id=""selectItemForQr""
                            style=""width: 80");
            WriteLiteral("%\" oninput=\"getSelectedEquipmentItem(this)\">\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "edbf28a7a0cdd2f7dc621494705aa1ff5d06f21b10697", async() => {
                WriteLiteral("Wybierz...");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("disabled", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                    </select>
                </div>

                <div>
                    <label for=""selectItemForQr"" class=""text-white"">Rozmiar(px)</label>

                    <input type=""number"" id=""displayQrSizeX"" value=""256"" class=""input-group-text bg-white""
                           style=""text-align:right ;width:40%"" oninput=""copyQrSizeValue(this.value)"" />

                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "edbf28a7a0cdd2f7dc621494705aa1ff5d06f21b12904", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

                    <input id=""displayQrSizeY"" value=""256"" style=""text-align: center; width: 40%""
                           class=""input-group-text bg-cs2 "" readonly />

                </div>

                <button class=""btn bg-light mt-4"" style=""width:80%"" onclick=""generateQr()"">Generuj kod QR</button>


            </div>


            <!-- Second column -->
            <div");
            BeginWriteAttribute("class", " class=\"", 6204, "\"", 6212, 0);
            EndWriteAttribute();
            WriteLiteral(" style=\"float:left\">\r\n\r\n                <div class=\"panelTransitions shadow secondCOlumnQrGenerator\" id=\"a4DiemensionsWin\">\r\n\r\n                    <div id=\"compareQrDiemensionsToA4\" style=\" width:35px;height:35px\">\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "edbf28a7a0cdd2f7dc621494705aa1ff5d06f21b14915", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
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

                    <div style=""position: absolute; border-radius: 0 0 6px 6px; color: #fff;
                        bottom: 0px; font-size: 16px; width:100% ; background-color: #646a70;
                        font-family: 'Segoe UI', sans-serif;  height:30px; text-align:center;
                            margin-left:-10px"">
                        <p> Format A4</p>
                    </div>

                </div>

            </div>

        </div>

    </div>


    <!-- Qr code preview -->
    <div id=""generatedGrcodePreviewContainer"" class=""hideOnPrint"" style=""top: 0; display: none; position: absolute;
        background-color:#242628; opacity:0.95;padding:10px 20px ;left: 50%; transform: translate(-50%, 0);
        border-radius:10px"">
        <div id=""generatedGrcodePreview"" style=""padding:10px 20px;margin-bottom:7px"">
        </div>
        <input type=""button"" class=""btn btn-secondary"" value=""Zamknij"" style=""float:right; margin: 0 10px""
       ");
            WriteLiteral("        onclick=\"closeQrPreviewWin()\" />\r\n        <input type=\"button\" class=\"btn btn-secondary\" value=\"Drukuj\" style=\"float:right\" onclick=\"printQr()\" />\r\n    </div>\r\n\r\n\r\n    <div id=\"generatedGrcodeTrueSize\" style=\"display:none;\"></div>\r\n\r\n\r\n\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PracaDyplomowa.Pages.QRCode.QRCodeGenerateModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<PracaDyplomowa.Pages.QRCode.QRCodeGenerateModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<PracaDyplomowa.Pages.QRCode.QRCodeGenerateModel>)PageContext?.ViewData;
        public PracaDyplomowa.Pages.QRCode.QRCodeGenerateModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
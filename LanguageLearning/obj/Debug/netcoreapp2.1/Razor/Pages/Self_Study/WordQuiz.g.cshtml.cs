#pragma checksum "C:\Users\Barry\Documents\Dev\LanguageLearning\LanguageLearning\Pages\Self_Study\WordQuiz.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1d1a6945ab3e261da86aae0a5178f9a4db2fe765"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(LanguageLearning.Pages.Self_Study.Pages_Self_Study_WordQuiz), @"mvc.1.0.razor-page", @"/Pages/Self_Study/WordQuiz.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Pages/Self_Study/WordQuiz.cshtml", typeof(LanguageLearning.Pages.Self_Study.Pages_Self_Study_WordQuiz), null)]
namespace LanguageLearning.Pages.Self_Study
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\Barry\Documents\Dev\LanguageLearning\LanguageLearning\Pages\_ViewImports.cshtml"
using LanguageLearning;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1d1a6945ab3e261da86aae0a5178f9a4db2fe765", @"/Pages/Self_Study/WordQuiz.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e20943b50f15efff37ef5294a1acb71ab75e4470", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Self_Study_WordQuiz : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/MemoriseGame.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/WordQuiz/WordQuiz.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "C:\Users\Barry\Documents\Dev\LanguageLearning\LanguageLearning\Pages\Self_Study\WordQuiz.cshtml"
  
    ViewData["Title"] = "Word Quiz";

#line default
#line hidden
            BeginContext(108, 22, true);
            WriteLiteral("\r\n<h2>Word Quiz</h2>\r\n");
            EndContext();
            BeginContext(130, 78, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2cb121f980b6411391a942983843135e", async() => {
                BeginContext(136, 8, true);
                WriteLiteral("  \r\n    ");
                EndContext();
                BeginContext(144, 55, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "36d30250e7e34beabbda252dffe4f075", async() => {
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
                EndContext();
                BeginContext(199, 2, true);
                WriteLiteral("\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(208, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(210, 3670, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "809ff86331bc4946930b5287223cf0b2", async() => {
                BeginContext(216, 3096, true);
                WriteLiteral(@"
    <div id=""timerHistoryField"">
        <h3>Time taken to answer correctly</h3>
        <div class=""content""></div>
    </div>
    <div id=""timerText"">
        <p><span id=""timerMinutes""></span>:<span id=""timerSeconds""></span>:<span id=""timerTens""></span></p>
    </div>
    <br />
    <div id=""gameField"">
        <div id=""gameType"">
            <div id=""practice"">
                <button class=""off""><span>Practice</span></button>
                <span style=""margin-left: 10px;"">Practice mode: <span id=""practiceStatus"">Off</span></span>
                <div class=""description"">
                    Practice mode allows you to reselect your answer if you choose incorrectly, and the same word may appear more than once
                </div>
            </div>
            <div class=""language flex"">
                <button class=""languageButton hover"">Japanese</button>
                <button class=""languageButton hover"">Korean</button>
            </div>
            <div class=""languageOpt");
                WriteLiteral(@"ion flex"" style=""display: none"">
                <button id=""toEnglish"" class=""translateTypeButton hover""></button>
                <button id=""fromEnglish"" class=""translateTypeButton hover""></button>
                <button id=""kanaButton"" class=""translateTypeButton hover"">Kana</button>
            </div>
            <div class=""kanaOption flex"" style=""display: none"">
                <button class=""kanaType hover"">Hiragana</button>
                <button class=""kanaType hover"">Katakana</button>
            </div>
            <div class=""kanaTranslateOption flex"" style=""display: none"">
                <button id=""fromKana"" class=""kanaTranslateType hover""></button>
                <button id=""toKana"" class=""kanaTranslateType hover""></button>
            </div>
            <button id=""startButton"" class=""button hover"" style=""display: none"">Start game</button>
        </div>
        <div id=""game"" style=""display:none"">
            <div id=""gameTypeDisplay""></div>
            <div class=""questio");
                WriteLiteral(@"ns"">
                <div id=""randomWordText""></div>
            </div>
            <div id=""answerField"">
                <div class=""container"">
                    <p>
                        <button class=""answerButton neutral"" id=""answerButton1""></button>
                        <button class=""answerButton neutral"" id=""answerButton2""></button>
                    </p>
                    <p>
                        <button class=""answerButton neutral"" id=""answerButton3""></button>
                        <button class=""answerButton neutral"" id=""answerButton4""></button>
                    </p>
                </div>
            </div>
            <button id=""replayButton"">Start Again</button>
            <button id=""gameResetButton"">Change game mode</button>
            <div id=""scoreboard"">
                <div class=""scoreCount"">
                    <text id=""answerScore"">Game has not started</text>
                </div>
            </div>
        </div>
    </div>
    <div clas");
                WriteLiteral("s=\"space20\"></div>\r\n    ");
                EndContext();
                BeginContext(3312, 49, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "064bf97a2d0842caaf5f5cf6c4f1ea33", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3361, 512, true);
                WriteLiteral(@"
    <script>
        $.get(""/Self_Study/WordQuiz?handler=ClaimsWordQuiz"", function (result) {
            if (result === ""Invalid"") {
                $(""#loginButton"").show();
                $(""#accountButton"").hide();
            } else {
                var claims = JSON.parse(JSON.stringify(result));
                $(""#userGreeting"").html(""Hello, "" + claims.sub);
                $(""#loginButton"").hide();
                $(""#accountButton"").show();
            }
        });
    </script>
");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3880, 6, true);
            WriteLiteral("\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LanguageLearning.Pages.Self_Study.WordQuizModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<LanguageLearning.Pages.Self_Study.WordQuizModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<LanguageLearning.Pages.Self_Study.WordQuizModel>)PageContext?.ViewData;
        public LanguageLearning.Pages.Self_Study.WordQuizModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591

#pragma checksum "C:\Users\Barry\Documents\Dev\LanguageLearning\LanguageLearning\Pages\Japanese\Kana\HiraganaIndex.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5a2745c82c4a7eac63221cecaecee1a998b84add"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(LanguageLearning.Pages.Japanese.Kana.Pages_Japanese_Kana_HiraganaIndex), @"mvc.1.0.razor-page", @"/Pages/Japanese/Kana/HiraganaIndex.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Pages/Japanese/Kana/HiraganaIndex.cshtml", typeof(LanguageLearning.Pages.Japanese.Kana.Pages_Japanese_Kana_HiraganaIndex), null)]
namespace LanguageLearning.Pages.Japanese.Kana
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5a2745c82c4a7eac63221cecaecee1a998b84add", @"/Pages/Japanese/Kana/HiraganaIndex.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e20943b50f15efff37ef5294a1acb71ab75e4470", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Japanese_Kana_HiraganaIndex : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(71, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 4 "C:\Users\Barry\Documents\Dev\LanguageLearning\LanguageLearning\Pages\Japanese\Kana\HiraganaIndex.cshtml"
  
    ViewData["Title"] = "Hiragana";

#line default
#line hidden
            BeginContext(117, 27, true);
            WriteLiteral("<h2>List of Hiragana</h2>\r\n");
            EndContext();
            BeginContext(144, 896, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "450d8e26ec714ba68ae50ca2f1f3de33", async() => {
                BeginContext(150, 110, true);
                WriteLiteral("    \r\n    <table class=\"table\">\r\n        <thead>\r\n            <tr>\r\n                <th>\r\n                    ");
                EndContext();
                BeginContext(261, 52, false);
#line 13 "C:\Users\Barry\Documents\Dev\LanguageLearning\LanguageLearning\Pages\Japanese\Kana\HiraganaIndex.cshtml"
               Write(Html.DisplayNameFor(model => model.Hiragana[0].Kana));

#line default
#line hidden
                EndContext();
                BeginContext(313, 87, true);
                WriteLiteral("\r\n                </th>\r\n                <th>                    \r\n                    ");
                EndContext();
                BeginContext(401, 54, false);
#line 16 "C:\Users\Barry\Documents\Dev\LanguageLearning\LanguageLearning\Pages\Japanese\Kana\HiraganaIndex.cshtml"
               Write(Html.DisplayNameFor(model => model.Hiragana[0].Romaji));

#line default
#line hidden
                EndContext();
                BeginContext(455, 134, true);
                WriteLiteral("\r\n                </th>                \r\n                <th></th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>            \r\n");
                EndContext();
#line 22 "C:\Users\Barry\Documents\Dev\LanguageLearning\LanguageLearning\Pages\Japanese\Kana\HiraganaIndex.cshtml"
             foreach (var item in Model.ListHiragana)
            {

#line default
#line hidden
                BeginContext(659, 72, true);
                WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
                EndContext();
                BeginContext(732, 39, false);
#line 26 "C:\Users\Barry\Documents\Dev\LanguageLearning\LanguageLearning\Pages\Japanese\Kana\HiraganaIndex.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Kana));

#line default
#line hidden
                EndContext();
                BeginContext(771, 79, true);
                WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
                EndContext();
                BeginContext(851, 41, false);
#line 29 "C:\Users\Barry\Documents\Dev\LanguageLearning\LanguageLearning\Pages\Japanese\Kana\HiraganaIndex.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Romaji));

#line default
#line hidden
                EndContext();
                BeginContext(892, 90, true);
                WriteLiteral("\r\n                    </td>                                      \r\n                </tr>\r\n");
                EndContext();
#line 32 "C:\Users\Barry\Documents\Dev\LanguageLearning\LanguageLearning\Pages\Japanese\Kana\HiraganaIndex.cshtml"
            }

#line default
#line hidden
                BeginContext(997, 36, true);
                WriteLiteral("        </tbody>\r\n    </table>    \r\n");
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
            BeginContext(1040, 2, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LanguageLearning.Pages.Japanese.Kana.HiraganaIndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<LanguageLearning.Pages.Japanese.Kana.HiraganaIndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<LanguageLearning.Pages.Japanese.Kana.HiraganaIndexModel>)PageContext?.ViewData;
        public LanguageLearning.Pages.Japanese.Kana.HiraganaIndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591

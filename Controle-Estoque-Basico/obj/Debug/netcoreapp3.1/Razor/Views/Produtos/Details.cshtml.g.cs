#pragma checksum "D:\PROJETOS\Control.Basic.Stock\Controle-Estoque-Basico\Views\Produtos\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ad057208bee17a6e27dff831b02c0f504ebc8b8e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Produtos_Details), @"mvc.1.0.view", @"/Views/Produtos/Details.cshtml")]
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
#line 1 "D:\PROJETOS\Control.Basic.Stock\Controle-Estoque-Basico\Views\_ViewImports.cshtml"
using Controle_Estoque_Basico;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\PROJETOS\Control.Basic.Stock\Controle-Estoque-Basico\Views\_ViewImports.cshtml"
using Controle_Estoque_Basico.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ad057208bee17a6e27dff831b02c0f504ebc8b8e", @"/Views/Produtos/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"66a165672bd21048dce26a95445f24f40bfc3a56", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Produtos_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Controle_Estoque_Basico.ViewModels.ProdutosViewModel>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn bg-gradient-secondary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\PROJETOS\Control.Basic.Stock\Controle-Estoque-Basico\Views\Produtos\Details.cshtml"
  
    ViewData["Title"] = "Detalhe do Produto";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"content-wrapper\">\r\n\r\n    <section class=\"content-header\">\r\n        <div class=\"container-fluid\">\r\n            <div class=\"row mb-12\">\r\n                <div class=\"col-sm-12\">\r\n                    <h1 class=\"text-center\">");
#nullable restore
#line 13 "D:\PROJETOS\Control.Basic.Stock\Controle-Estoque-Basico\Views\Produtos\Details.cshtml"
                                       Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h1>
                </div>
            </div>
        </div>
    </section>

    <div class=""content"">
        <div class=""row"">
            <div class=""col-md-12"">
                <div class=""row"">              
                    <div class=""col-md-12"">
                        <div class=""card card-secondary card-outline"">
                            <div class=""card-body box-profile"">
                                <div class=""text-center"">
                                    <img class=""profile-user-img img-fluid img-circle""");
            BeginWriteAttribute("src", " src=\"", 921, "\"", 986, 1);
#nullable restore
#line 27 "D:\PROJETOS\Control.Basic.Stock\Controle-Estoque-Basico\Views\Produtos\Details.cshtml"
WriteAttributeValue("", 927, Url.Content("~/Imagens/"+Model.Produto.ImagemProdutoModel), 927, 59, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"height:200px;width:auto;\" />\r\n                                </div>\r\n\r\n                                <h3 class=\"text text-center\">");
#nullable restore
#line 30 "D:\PROJETOS\Control.Basic.Stock\Controle-Estoque-Basico\Views\Produtos\Details.cshtml"
                                                        Write(Html.DisplayFor(model => model.Produto.PRO_NOME));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n\r\n                                <p class=\"text-muted text-center\">");
#nullable restore
#line 32 "D:\PROJETOS\Control.Basic.Stock\Controle-Estoque-Basico\Views\Produtos\Details.cshtml"
                                                             Write(Html.DisplayFor(model => model.Produto.PRO_DESCRICAO));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n\r\n                                <ul class=\"list-group list-group-unbordered mb-3\">\r\n                                    <li class=\"list-group-item\">\r\n                                        <b>");
#nullable restore
#line 36 "D:\PROJETOS\Control.Basic.Stock\Controle-Estoque-Basico\Views\Produtos\Details.cshtml"
                                      Write(Html.DisplayNameFor(model => model.Produto.PRO_QUANTIDADE));

#line default
#line hidden
#nullable disable
            WriteLiteral("</b> <a class=\"float-right\">");
#nullable restore
#line 36 "D:\PROJETOS\Control.Basic.Stock\Controle-Estoque-Basico\Views\Produtos\Details.cshtml"
                                                                                                                             Write(Html.DisplayFor(model => model.Produto.PRO_QUANTIDADE));

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n                                    </li>\r\n                                    <li class=\"list-group-item\">\r\n                                        <b>");
#nullable restore
#line 39 "D:\PROJETOS\Control.Basic.Stock\Controle-Estoque-Basico\Views\Produtos\Details.cshtml"
                                      Write(Html.DisplayNameFor(model => model.Produto.PRO_DATAENTRADA));

#line default
#line hidden
#nullable disable
            WriteLiteral("</b> <a class=\"float-right\">");
#nullable restore
#line 39 "D:\PROJETOS\Control.Basic.Stock\Controle-Estoque-Basico\Views\Produtos\Details.cshtml"
                                                                                                                              Write(Html.DisplayFor(model => model.Produto.PRO_DATAENTRADA));

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n                                    </li>\r\n                                    <li class=\"list-group-item\">\r\n                                        <b>");
#nullable restore
#line 42 "D:\PROJETOS\Control.Basic.Stock\Controle-Estoque-Basico\Views\Produtos\Details.cshtml"
                                      Write(Html.DisplayNameFor(model => model.Produto.PRO_VALIDADE));

#line default
#line hidden
#nullable disable
            WriteLiteral("</b> <a class=\"float-right\">");
#nullable restore
#line 42 "D:\PROJETOS\Control.Basic.Stock\Controle-Estoque-Basico\Views\Produtos\Details.cshtml"
                                                                                                                           Write(Html.DisplayFor(model => model.Produto.PRO_VALIDADE));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</a>
                                    </li>
                                </ul>
                            </div>
                            <div class=""row col-md-12 mt-3"">
                                <div class=""btn"">
                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ad057208bee17a6e27dff831b02c0f504ebc8b8e10143", async() => {
                WriteLiteral("Voltar");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ad057208bee17a6e27dff831b02c0f504ebc8b8e11418", async() => {
                WriteLiteral("Editar");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 49 "D:\PROJETOS\Control.Basic.Stock\Controle-Estoque-Basico\Views\Produtos\Details.cshtml"
                                                           WriteLiteral(Model.Produto.PRO_ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Controle_Estoque_Basico.ViewModels.ProdutosViewModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591

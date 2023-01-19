#pragma checksum "D:\PROJETOS\Control.Basic.Stock\Controle-Estoque-Basico\Views\Shared\Componentes\ModalBaixaProdutos.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "565bcfda3904341c68b3e128d3fa1768a970cabc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Componentes_ModalBaixaProdutos), @"mvc.1.0.view", @"/Views/Shared/Componentes/ModalBaixaProdutos.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"565bcfda3904341c68b3e128d3fa1768a970cabc", @"/Views/Shared/Componentes/ModalBaixaProdutos.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"66a165672bd21048dce26a95445f24f40bfc3a56", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared_Componentes_ModalBaixaProdutos : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""modal fade"" id=""modal-default-ModalBaixaProdutos"">
    <div class=""modal-dialog"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h4 class=""modal-title"">Baixar Produto</h4>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
                <div class=""row p-1"">
                    <div class=""col-md-12"">
                        <div class=""row"">
                            <label for=""exampleFormControlInput1"" class=""form-label"">Quantidade a ser baixada:</label>
                            <input type=""number"" class=""form-control"" id=""qtdProduto"" placeholder=""0"">
                        </div>
                    </div>
                </div>

            </div>
            <div class=""modal-footer justify-content-between"">
                <button type=");
            WriteLiteral(@"""button"" class=""btn btn-danger"" data-dismiss=""modal"">Cancelar</button>
                <button type=""button"" class=""btn btn-primary"" onclick=""TesteBaixaProduto()"">Confirmar</button>
            </div>
        </div>
    </div>
</div>

<script>

    var idProduto = 0;

    function ShowModalBaixaProdutos(id) {
        $(""#modal-default-ModalBaixaProdutos"").modal('show');

        idProduto = id;
    }

    function TesteBaixaProduto() {

        var url = ""/SaidaProdutos/InformarBaixaProduto"";
        var qtd = $(""#qtdProduto"").val();

        $.ajax({
            url: url,
            type: 'POST',
            datatype: 'JSON',
            data: { _id: idProduto, _qtd: qtd },
            beforeSend: function() {
            },
            success: function(data) {

                ToastCustom(1, ""success"", ""Item baixado com sucesso"");

                // $(""#listaProdutosRegistros"").html('');
                //$(""#listaProdutosRegistros"").html(data);

            },
      ");
            WriteLiteral("      error: function(data) {\r\n\r\n                ToastCustom(1, \"error\", data.responseText);\r\n            }\r\n        });\r\n    }\r\n\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
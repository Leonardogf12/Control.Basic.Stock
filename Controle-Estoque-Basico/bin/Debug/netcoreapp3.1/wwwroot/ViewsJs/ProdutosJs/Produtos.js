

$(document).ready(function () {

    CriaDataTableProdutos();
});

function CriaDataTableProdutos() {

    $("#dtProdutos").DataTable({                   
        "responsive":true,
        "lengthChange": true,
        "autoWidth": true,         
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],       
        'lengthMenu': [[10, 20, 30, 40, -1], [10, 20, 30, 40, "Todos"]],
        columnDefs: [
            { width: 10, targets: 0 },
        ],
        "language": {
            "buttons": {
                "copy": "Copiar",
                "csv": "CSV",
                "excel": "Excel",
                "pdf": "PDF",
                "print": "Print",
                "colvis": "Ordenar Colunas",
            },
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "Nenhum registro foi encontrado",
            "sEmptyTable": "Nenhum dado disponivel para a tabela",
            "sInfo": "Registros de _START_ a _END_ de um total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros de 0 a 0 de um total de 0 registros",
            "sInfoFiltered": "(filtrado de um total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Carregando...",
            "oPaginate": {
                "sFirst": "Primeiro",
                "sLast": "Último",
                "sNext": "Próximo",
                "sPrevious": "Anterior"
            },
            "oAria": {
                "sSortAscending": ": Ativar para ordenar a coluna de maneira ascendente",
                "sSortDescending": ": Ativar para ordenar a coluna de maneira crescente"
            },
        },
    }).buttons().container().appendTo('#dtProdutos_wrapper .col-md-6:eq(0)');
}

$('#mycheck').on('ifClicked', function (event) { checkAll(); });

$("#checkAll").click(function () {
    $('input:checkbox').not(this).prop('checked', this.checked);
});

function checkAll() {
    var checkboxes = document.getElementsByName('LISTACHECK');
    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].type == 'checkbox') {
            alert($('#mycheck').checked);
            if ($('#mycheck').checked == true) {
                $('#' + checkboxes[i].id).attr('checked', true);
            }
            else {
                $('#' + checkboxes[i].id).attr('checked', false);
            }
        }
    }
}

function ExcluirProdutosPartial(id) {
    ExcluirVarios(id);
}

function ExcluirProdutosIndex() {
    var checkboxes = document.getElementsByName('LISTACHECK');

    var registro = '';

    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].checked == true) {
            registro = registro + checkboxes[i].id + ",";
        }
    }

    if (registro == '') {
        ToastCustom(1, "error", "Selecione ao menos um registro.");

    } else {
        ExcluirVarios(registro)
    }
}

function ExcluirVarios(registro) {
    var url = "/Produtos/ExcluirVarios/";

    $.ajax({
        url: url,
        type: 'POST',
        datatype: 'JSON',
        data: { _registros: registro },
        beforeSend: function () {
        },
        success: function (data) {

            ToastCustom(1, "success", "Produto excluido com sucesso");

            //2nd empty html
            //$("#dtProdutos" + " tbody").empty();
            //$("#dtProdutos" + " thead").empty();


            
            //var table = $("#dtProdutos").DataTable();

            $("#listaProdutosRegistros").html('');
            $("#listaProdutosRegistros").html(data);
            //CriaDataTableProdutos();
           

            //document.location.reload(true);
        },
        error: function (data) {

            ToastCustom(1, "error", data.responseText);
        }
    });
}

function InformarVenda(registro) {
    var url = "/SaidaProdutos/InformarVendaProduto";

    $.ajax({
        url: url,
        type: 'POST',
        datatype: 'JSON',
        data: { _id: registro },
        beforeSend: function () {
        },
        success: function (data) {

            ToastCustom(1, "success", "Produto excluido com sucesso");

            //2nd empty html
            //$("#dtProdutos" + " tbody").empty();
            //$("#dtProdutos" + " thead").empty();



            //var table = $("#dtProdutos").DataTable();

            $("#listaProdutosRegistros").html('');
            $("#listaProdutosRegistros").html(data);
            //CriaDataTableProdutos();


            //document.location.reload(true);
        },
        error: function (data) {

            ToastCustom(1, "error", data.responseText);
        }
    });
}

function ToastCustom(modelo, tipo, mensagem) {

    var Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 4000
    });

    if (modelo == 1 && tipo == "success") {
        toastr.success(mensagem);
        return;
    }
    if (modelo == 2 && tipo == "success") {
        Toast.fire({
            icon: tipo,
            title: mensagem
        });
        return;
    }

    if (modelo == 1 && tipo == "info") {
        toastr.info(mensagem);
        return;
    }
    if (modelo == 2 && tipo == "info") {
        Toast.fire({
            icon: tipo,
            title: mensagem
        });
        return;
    }

    if (modelo == 1 && tipo == "warning") {
        toastr.warning(mensagem);
        return;
    }
    if (modelo == 2 && tipo == "warning") {
        Toast.fire({
            icon: tipo,
            title: mensagem
        });
        return;
    }

    if (modelo == 1 && tipo == "error") {
        toastr.error(mensagem);
        return;
    }
    if (modelo == 2 && tipo == "error") {
        Toast.fire({
            icon: tipo,
            title: mensagem
        });
        return;
    }

    if (modelo == 2 && tipo == "question" || modelo == 1 && tipo == "question") {
        Toast.fire({
            icon: tipo,
            title: mensagem
        });
        return;
    }

}




$(document).ready(function () {

    CriaDataTableProdutos();   
});

function CriaDataTableProdutos() {

    $("#dtProdutos").DataTable({
        "responsive": true,
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

//* FUNCAO DO BOTAO EXCLUIR DA INDEX.
function ExcluirProdutosIndex() {

    Swal.fire({
        icon: 'question',
        title: '<h3>Excluir</h3>',
        text: 'Deseja realmente excluir este(s) registro(s)?',
        showCancelButton: true,
        confirmButtonColor: '#36c6d3',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Confirmar',
        footer: '<p>Obs: Não será possível recuperar um registro excluído.</label>'
    }).then(function (result) {
        if (result.value) {

            ConfirmaExcluir();
        }
    });

}

//* VERIFICA SE EXISTE ITEMS CHECKADOS/MARCADOS NA LISTA PARA EXCLUIR.
function ConfirmaExcluir() {

    var checkboxes = document.getElementsByName('LISTACHECK');

    var registro = '';

    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].checked == true) {
            registro = registro + checkboxes[i].id + ",";
        }
    }

    if (registro == '') {

        swal.fire({
            icon: 'info',
            title: "<h3>Atenção</h3>",
            text: 'Selecione ao menos um registro.',
            showCancelButton: true,
            showConfirmButton: false,
            cancelButtonColor: '#d33',
            cancelButtonText: 'Fechar'
        });

    } else {
        ExcluirVarios(registro)
    }
}

//* FUNCAO DO BOTAO EXCLUIR DA ListaProdutosPatial.
function ExcluirProdutosPartial(id) {

    Swal.fire({
        icon: 'question',
        title: '<h3>Excluir</h3>',
        text: 'Deseja realmente excluir este(s) registro(s)?',
        showCancelButton: true,
        confirmButtonColor: '#36c6d3',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Cancelar',
        confirmButtonText: 'Confirmar',
        footer: '<p>Obs: Não será possível recuperar um registro excluído.</label>'
    }).then(function (result) {
        if (result.value) {

            ExcluirVarios(id);
        }

    });
}

//* FUNCAO PARA CHAMAR CONTROLLER.
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

            Swal.fire({
                icon: 'success',
                title: '<h3>Sucesso</h3>',
                text: 'Registro excluído com sucesso.',
                showConfirmButton: false,
                timer: 2000
            }).then(function () {

                $('#dtProdutos').DataTable().clear().destroy();
                
                $("#listaProdutosRegistros").html('');
                $("#listaProdutosRegistros").html(data);

                CriaDataTableProdutos();

            });
        },
        error: function (data) {

            swal.fire({
                icon: 'error',
                title: "<h3>Excluir</h3>",
                text: data.responseJSON,
                showCancelButton: true,
                showConfirmButton: false,
                cancelButtonColor: '#d33',
                cancelButtonText: 'Fechar',
                footer: '<a href="Produtos/Index">Lista de Produtos</label>'
            });

            //ToastCustom(1, "error", data.responseText);
        }
    });
}

function ShowSwalBaixaProdutos(id) {

    var qtd = 0;

    Swal.fire({
        title: 'Digite a quantidade a ser baixada:',
        input: 'text',
        inputAttributes: {
            autocapitalize: 'off'
        },
        showCancelButton: true,
        confirmButtonText: 'Confirmar',
        cancelButtonText: 'Cancelar',
        showLoaderOnConfirm: true,
        preConfirm: (valor) => {


            if (isNaN(valor)) {
                ToastCustom(1, "error", 'Digite um valor válido');
                return false;
            }

            if (valor == 0 || valor < 0) {
                ToastCustom(1, "error", 'Digite um valor maior que zero.');
                return false;
            }

            if (valor == 0) {
                ToastCustom(1, "error", 'Digite um valor maior que zero.');
                return false;
            }

            qtd = valor;
        },
       
    }).then((result) => {

        if (result.isConfirmed) {

            var url = "/Produtos/InformarBaixaProduto";

            var idProduto = id;

            $.ajax({
                url: url,
                type: 'POST',
                datatype: 'JSON',
                data: { _id: idProduto, _qtd: qtd },
                beforeSend: function () {
                },
                success: function (data) {

                    $('#dtProdutos').DataTable().clear().destroy();

                    $("#listaProdutosRegistros").html('');
                    $("#listaProdutosRegistros").html(data);

                    CriaDataTableProdutos();

                    ToastCustom(1, "success", "Item baixado com sucesso");
                },
                error: function (data) {

                    ToastCustom(1, "error", data.responseJSON);

                }
            });
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

           
            $("#listaProdutosRegistros").html('');
            $("#listaProdutosRegistros").html(data);
           
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

$(".custom-file-input").on("change", function () {
    var fileName = $(this).val().split("\\").pop();
    $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
});



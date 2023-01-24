


$(document).ready(function () {
    CarregaCardQuantidadeEtoque();
    CarregaCardQuantidadeCategoria();
    CarregaCardQuantidadeSaidaProdutos();
    FiltraCardProdutosAVencerIndex();  
});

//CARD 1
function CarregaCardQuantidadeEtoque() {

    $.ajax({
        url: '/Produtos/TotalProdutosEmEstoque/',
        type: 'GET',
        datatype: 'JSON',
        beforeSend: function () {
        },
        success: function (data) {

            $("#quantidadeProdutos").html(data);
        },
        error: function (data) {

            ToastCustom("error", data.responseText);
        }
    });
}

//CARD 2
function CarregaCardQuantidadeCategoria() {

    $.ajax({
        url: '/Categorias/TotalCategorias/',
        type: 'GET',
        datatype: 'JSON',
        beforeSend: function () {
        },
        success: function (data) {

            $("#quantidadeCategorias").html(data);
        },
        error: function (data) {

            ToastCustom("error", data.responseText);
        }
    });
}

//CARD 3
function CarregaCardQuantidadeSaidaProdutos() {

    $.ajax({
        url: '/SaidaProdutos/TotalSaidaProdutos/',
        type: 'GET',
        datatype: 'JSON',
        beforeSend: function () {
        },
        success: function (data) {
            console.log(data);
            $("#quantidadeSaidaProdutos").html(data);
        },
        error: function (data) {

            ToastCustom("error", data.responseText);
        }
    });
}

//CARD 4
function FiltraCardProdutosAVencerIndex() {

    var url = "/Home/TotalProdutosAVencer";

    $.ajax({
        url: url,
        type: 'POST',
        datatype: 'JSON',
        data: { _dataDe: "", _dataAte: ""},
        beforeSend: function () {
        },
        success: function (data) {

            $('#quantidadeProdutosAVencer').html(data);                                                        
        },
        error: function (data) {

            ToastCustom(1, "error", data.responseJSON);

        }
    });

}

//CARD 4
function FiltraCardProdutosAVencer() {

    console.log("HOME.JS");

    var dataDe = "";
    var dataAte = "";

    Swal.fire({
        title: '<h3>Produtos á vencer</h3>',
        icon: 'info',
        html: 'Informe Data Início e Fim para saber a ' +
            'quantidade de produtos a vencer. </br>' +
            '</br>Data De: <input id="dataDe" type="date"/><p>' +
            '</br>Data Até: <input id="dataAte" type="date"/>',
        showCancelButton: true,
        focusConfirm: false,
        confirmButtonText: 'Confirmar',
        cancelButtonText: 'Cancelar',
        cancelButtonAriaLabel: 'Thumbs down',
        preConfirm: (valor) => {

            dataDe = $("#dataDe").val();
            dataAte = $("#dataAte").val();

            if (dataDe == "") {
                ToastCustom(1, "error", 'Digite uma data início.');
                return false;
            }
            if (dataAte == "") {
                ToastCustom(1, "error", 'Digite uma data fim.');
                return false;
            }

            let isValidDateDe = Date.parse(dataDe);
            let isValidDateAte = Date.parse(dataAte);

            if (isNaN(isValidDateDe)) {
                ToastCustom(1, "error", 'Data início não válida.');
                return false;
            }

            if (isNaN(isValidDateAte)) {
                ToastCustom(1, "error", 'Data fim não válida.');
                return false;
            }

        },
    }).then((result) => {

        if (result.isConfirmed) {

            var url = "/Home/TotalProdutosAVencer";

            $.ajax({
                url: url,
                type: 'POST',
                datatype: 'JSON',
                data: { _dataDe: dataDe, _dataAte: dataAte },
                beforeSend: function () {
                },
                success: function (data) {

                    $('#quantidadeProdutosAVencer').html(data);

                    $('#dataDeFiltroInicial').html(ConverteStringToDate(dataDe));
                    $('#dataAteFiltroInicial').html(ConverteStringToDate(dataAte));     

                    console.log("HOME.JS");
                    FiltraGraficoProdutosAVencer(dataDe, dataAte);
                },
                error: function (data) {

                    ToastCustom(1, "error", data.responseJSON);

                }
            });
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

function ConverteStringToDate(texto) {
   
    const [ano, mes, dia] = texto.split('-');
    
    return [dia, mes, ano].join('/');  
}

$(function CarregaGraficoProdutosAVencer() {

    var areaChartData = {
        labels: [], //*PERIODO
        datasets: [
            {
                label: 'Quantidade',
                backgroundColor: 'rgba(60,141,188,0.9)',
                borderColor: 'rgba(60,141,188,0.8)',
                pointRadius: true,
                pointColor: '#c94242',
                pointStrokeColor: 'rgba(60,141,188,1)',
                pointHighlightFill: '#fff',
                pointHighlightStroke: 'rgba(60,141,188,1)',
                data: [] //*DADOS
            }
        ]
    }

    var areaChartOptions = {
        maintainAspectRatio: false,
        responsive: true,
        legend: {
            display: false
        },
        scales: {
            xAxes: [{
                gridLines: {
                    display: false,
                }
            }],
            yAxes: [{
                gridLines: {
                    display: false,
                }
            }]
        }
    }

    //*Isso obterá o primeiro nó retornado na coleção jQuery.
    new Chart(areaChartCanvas, {
        type: 'line',
        data: areaChartData,
        options: areaChartOptions
    })

    //*LINE CHART
    var lineChartCanvas = $('#lineChart').get(0).getContext('2d')
    var lineChartOptions = $.extend(true, {}, areaChartOptions)
    var lineChartData = $.extend(true, {}, areaChartData)
    lineChartData.datasets[0].fill = false;
    lineChartOptions.datasetFill = false

    var lineChart = new Chart(lineChartCanvas, {
        type: 'line',
        data: lineChartData,
        options: lineChartOptions
    })
})



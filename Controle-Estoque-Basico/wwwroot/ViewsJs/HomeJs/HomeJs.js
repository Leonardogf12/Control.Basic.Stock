
var areaChartCanvas = "";

$(document).ready(function () {
    FiltraGraficoProdutosAVencerIndex();
    CarregaCardQuantidadeEtoque();
    CarregaCardQuantidadeSaidaProdutos();
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

//*CARREGA O GRAFICO COM O PERIODO CHUMBADO DE UM MES PARA TRAZ E UM MES PARA FRENTE.
function FiltraGraficoProdutosAVencerIndex() {

    //*CARREGA GRAFICO DE PRODUTOS A VENCER POR PERIODO POREM (FILTRO CHUMBADO).
    $.ajax({
        url: "/Home/CarregaDadosGraficoDeLinhasComFiltro",
        type: 'POST',
        datatype: 'JSON',
        data: { _dataDe: "", _dataAte: "" },
        beforeSend: function () {
        },
        success: function (result) {

            for (var i = 0; i < result.length; i++) {
                var shortCode = result[i].shortCode;
                var m = result[i].meses;
                var v = result[i].valores;
            }

            CarregaDadosGraficoDeLinhas(m, v);
        },
        error: function (data) {
            ToastCustom(1, "error", data.responseJSON);
        }
    });
}

//*FILTRO GERAL DA HOME.
function FiltroHomeGeral() {

    var de = "";
    var ate = "";

    Swal.fire({
        title: '<h3>Filtro Geral</h3>',
        icon: 'info',
        html: 'Informe a data início e data fim para filtrar os cadrs. </br> ' +
            '</br>De: <input id="idDataDe" type="date"/><p>' +
            '</br>Até: <input id="idDataAte" type="date"/>',
        showCancelButton: true,
        focusConfirm: false,
        confirmButtonText: 'Confirmar',
        cancelButtonText: 'Cancelar',
        cancelButtonAriaLabel: 'Thumbs down',
        preConfirm: (valor) => {

            de = $("#idDataDe").val();
            ate = $("#idDataAte").val();

            if (de == "") {
                ToastCustom(1, "error", 'Digite uma data início.');
                return false;
            }
            if (ate == "") {
                ToastCustom(1, "error", 'Digite uma data fim.');
                return false;
            }

            let isValidDateDe = Date.parse(de);
            let isValidDateAte = Date.parse(ate);

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

            //*AJAX CARREGA QUANTIDADE NO CARD 4 DE PRODUTOS A VENCER.
            $.ajax({
                url: "/Home/TotalProdutosAVencer",
                type: 'POST',
                datatype: 'JSON',
                data: { _dataDe: de, _dataAte: ate },
                beforeSend: function () {
                },
                success: function (data) {

                    $('#quantidadeProdutosAVencer').html(data);

                    $('#dataDeFiltroInicial').html(ConverteStringToDate(de));
                    $('#dataAteFiltroInicial').html(ConverteStringToDate(ate));

                },
                error: function (data) {

                    ToastCustom(1, "error", data.responseJSON);

                }
            });

            //*AJAX CARREGA GRAFICO DE PRODUTOS A VENCER POR PERIODO.
            $.ajax({
                url: "/Home/CarregaDadosGraficoDeLinhasComFiltro",
                type: 'POST',
                datatype: 'JSON',
                data: { _dataDe: de, _dataAte: ate },
                beforeSend: function () {
                },
                success: function (result) {

                    for (var i = 0; i < result.length; i++) {
                        var shortCode = result[i].shortCode;
                        var m = result[i].meses;
                        var v = result[i].valores;
                    }

                    $('#dataDeGraf').html(ConverteStringToDate(de));
                    $('#dataAteGraf').html(ConverteStringToDate(ate));

                    CarregaDadosGraficoDeLinhas(m, v);
                },
                error: function (data) {

                    ToastCustom(1, "error", data.responseJSON);

                }
            });
        }
    });
}

//*PREENCHE DADOS NO GRAFICO.
function CarregaDadosGraficoDeLinhas(meses, valores) {
   
    var areaChartData = {
        labels: meses, //*PERIODO
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
                data: valores //*DADOS
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
}

//*DESENHO DO GRÁFICO.
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

//*TOAST ALERT - CUSTOMIZADO.
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

//*CONVERSOR DE DATAS.
function ConverteStringToDate(texto) {
   
    const [ano, mes, dia] = texto.split('-');
    
    return [dia, mes, ano].join('/');  
}




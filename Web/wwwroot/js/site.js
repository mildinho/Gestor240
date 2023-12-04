// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//const { Swal } = require("../lib/limonte-sweetalert2/sweetalert2");


// https://sweetalert2.github.io/

// Write your JavaScript code.
$(document).ready(function () {

    $('.dinheiro').mask('000.000.000.000.000,0000', { reverse: true });

    frm_manutencao_delete();

    $('[data-toggle="tooltip"]').tooltip();

    $('.select2-single').select2();


    $("#frm_manutencao_update").submit(function (event) {
        document.getElementById('PrecoVenda').value = AjustaPreco('PrecoVenda');
        document.getElementById('PrecoMedio').value = AjustaPreco('PrecoMedio');
        document.getElementById('PrecoCusto').value = AjustaPreco('PrecoCusto');
    });

    $('#dataTable').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/pt-BR.json',
        },
        processing: true,
    });

    $('#dataTableHover').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/pt-BR.json',
        },
        processing: true,

    });

    $('#dataTableContaCorrente').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/pt-BR.json',
        },
        scrollY: true,
        processing: true,
    });


});

function AjustaPreco(campo) {
    var Conteudo = document.getElementById(campo).value;
    var resultado = "";
    var validacao;
    for (let i = 0; i < Conteudo.length; i = i + 1) {
        validacao = Conteudo.substring(i, i + 1);

        if ('0123456789.,'.indexOf(validacao) != -1) {
            if (validacao == ",") {
                resultado = resultado + ".";
            }
            else if (validacao == ".") {

            } else {
                resultado = resultado + validacao;
            }
        }

    }

    //document.getElementById(campo).value = resultado;
    return resultado;
};


function justNumbers(text) {
    var numbers = text.replace(/[^0-9]/g, '');
    return parseInt(numbers);
};


function buscar_cep() {

    var campo = document.getElementById('cep_beneficiario');
    campo = justNumbers(campo.value);


    $.ajax({
        url: "https://viacep.com.br/ws/" + campo + "/json/",
        dataType: 'json',
        success: function (response) {
            console.log(response);


            if (response.erro != true) {
                console.log(response.bairro);
                console.log(response.localidade);
                console.log(response.uf);


                document.getElementById('endereco_beneficiario').value = response.logradouro;
                document.getElementById('bairro_beneficiario').value = response.bairro;

                document.getElementById('select2-select2SingleUF-container').value = response.uf;
            } else if (response.erro == true) {
                Swal.fire(
                    {
                        title: 'CEP Não Encontrado!',
                        text: 'Verifique o CEP Informado!',
                        icon: 'error'
                    }
                );
            }
        }
    });
}

function frm_manutencao_delete() {
    $(".gestor240-btn-excluir").click(function (e) {

        e.preventDefault();

        Swal.fire({
            title: 'Deseja Realmente Excluir?',
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sim',
            cancelButtonText: 'Não',
        }).then((result) => {
            if (result.isConfirmed) {

                //Swal.fire(
                //    {
                //        title: 'Excluido!',
                //        text: 'Registro Excluido do Sistema.',
                //        icon: 'success'
                //    }
                //);

                document.getElementById("frm_manutencao_delete").submit();
            }
        });
    })
};


function alertsw(mensagem, type) {
    Swal.fire(
        {
            title: ' ATENÇÃO ',
            text: mensagem,
            icon: type
        }
    );

}

function alertswinformativo(mensagem, type) {
    Swal.fire({
        position: 'top-end',
        icon: type,
        title: mensagem,
        showConfirmButton: false,
        timer: 1500
    });
}
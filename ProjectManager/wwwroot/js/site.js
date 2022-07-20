// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    listarAlunos(""); //Carrega todos os alunos sem filtro

    //Função click para buscar aluno informado no input busca
    $("#btn-buscar").click(function () {
        var parteDoNome = $("#parteDoNome").val(); //recebera o valor do texto digitado no input de busca
        listarAlunos(parteDoNome);
    });

    function listarAlunos(parteDoNome) { //Função responsável por fazer a requisição na controller metodo listaDeAlunos

        var opcao = $("input[name='opcoesdefiltro']:checked").val();
        $.ajax({
            type: "GET",
            url: '/Aluno/ListaDeAlunos?parteDoNome=' + parteDoNome + '&opcao=' + opcao,
            cache: false,
            contentType: "application/json; charset=utf8",
            success: function (ajax) {
                $('#listaDeAlunos').html(ajax); //seta o html que o metodo retornor para dentro da div lista de alunos
            }
        });
    }
});
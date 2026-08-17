//Esse primeiro comando espera o html terminar de carregar para depois executar esse codigo
$(document).ready(function () {
    ListarClientes();
    //Sempre limpa os campos quando clicar no botao "novo"
    $("#btnNovo").click(function (e) {
        e.preventDefault();
        LimparCamposForm();
        $("#myModal").modal("show");
    });

    
});

function SalvarCadastro() {
    var IdclientesVal = $("#Idclientes").val();
    var Idclientes = IdclientesVal === "" ? 0 : parseInt(IdclientesVal, 10);
    var Documento = $("#Documento").val();
    var Nome = $("#Nome").val();
    var Email = $("#email").val();
    var Telefone = $("#telefone").val();
    var Sexo = $("input[name='Sexo']:checked").val();
    var UF = $("#uf").val();
  
    var validar = 1;

    $("#Documento, #Nome").css("background-color", "");


    //Não deixa esses dois campos vazios pois são obrigatorios
    if (Documento === "") {
        validar = 0;
        alert("O campo Documento é obrigatório");
        $("#Documento").css("background-color", "lightcoral");
    }

    if (Nome === "") {
        validar = 0;
        alert("O campo Nome é obrigatório");
        $("#Nome").css("background-color", "lightcoral");
    }

    if (validar == 1) {
        //Criando os objetos
        var Clientes = {
            Idclientes: Idclientes,
            Documento: Documento,
            Nome: Nome,
            Sexo: Sexo,
            Email: Email,
            Telefone: Telefone,
            UF: UF
        };

        $.ajax({
            type: "POST",
            url: "https://localhost:7064/api/Clientes/Salvar",
                  //transforma um objeto em texto json
            data: JSON.stringify(Clientes),
            contentType: "application/json; charset=utf-8",
            dataType: "json",

            success: function (msg) {

                LimparCamposForm();
                $("#myModal").modal("hide");
                ListarClientes();

            },

            error: function (msg) {
                alert("Erro ao salvar o cliente.");
            }
        });
    }
}

function AdicionarLinhaTabela(Idclientes, Documento, Nome, Email, Telefone) {
    //Essa função recebe os dados de um cliente e cria um linha nova no html

    var linhaNova = '<tr id="Linha' + Documento + '"><td>';

    linhaNova += Idclientes +
        '</td><td>' + Documento +
        '</td><td>' + Nome +
        '</td><td>' + Email +
        '</td><td>' + Telefone +
        '</td><td><a href="#" onclick="javascript:GetClient(\'' + Idclientes + '\');" class="btn btn-info"><span class="glyphicon glyphicon-edit" aria-hidden="true"></span> Editar</a><a href="#" onclick="javascript:RemoveLinha(' + Idclientes + ');" class="btn btn-danger"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span> Excluir</a></td></tr>';
    //Por fim adciona o cliente na tabela com o prepend sempre o cliente mais recente ficara no topo
    $("#ListaCadastro").prepend(linhaNova);
}

function LimparCamposForm() {
    $("#Idclientes").val("");
    $("#Documento").val("");
    $("#Nome").val("");
    $("#email").val("");
    $("#telefone").val("");
    $("#uf").val("");
    $("input[name='Sexo']").prop("checked", false);
}

function RemoveLinha(Idclientes) {
    $.ajax({
        type: "DELETE",
        url: "https://localhost:7064/api/Clientes/Deletar?Idclientes=" + Idclientes,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (OBJ) {
            alert('Excluído com sucesso!');
            ListarClientes();
        },
        error: function (msg) {
            alert("Erro");
        }
    });
}

function GetClient(Idclientes) {
    //Essa função é chamada quando você clica em editar
    $.ajax({
        type: "GET",
        url: "https://localhost:7064/api/Clientes/GetClient?Idclientes=" + Idclientes ,
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (OBJ) {
            //Abre o modal
            $("#myModal").modal().show();
            //Pega os dados da api e coloca na tela
            $("#Idclientes").val(OBJ.idclientes);
            $("#Documento").val(OBJ.documento);
            $("#Nome").val(OBJ.nome);
            $("input[name='Sexo'][value='" + OBJ.sexo + "']").prop("checked", true);
            
            $("#email").val(OBJ.email);
            $("#telefone").val(OBJ.telefone);
            $("#uf").val(OBJ.uf);

        },

        error: function (msg) {
            alert("ERROR");
        }
    });
}

//Essa função faz o GET da lista inteira
function ListarClientes() {

    //lista todos os clientes da lista ele remove todos 
    $("#ListaCadastro tr:gt(0)").remove();

    $.ajax({
        type: "GET",
        url: "https://localhost:7064/api/Clientes/Listar",
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (OBJ) {
            //Esse loop for percorre toda a lista de clientes e toda vez chama a funcção de add linha na tabela
            for (var i = 0; i < OBJ.length; i++) {
                console.log(OBJ[i]);
                AdicionarLinhaTabela(
                    OBJ[i].idclientes,
                    OBJ[i].documento,
                    OBJ[i].nome,
                    OBJ[i].email,
                    OBJ[i].telefone
                    
                );
            }
        },

        error: function (msg) {
            alert("Erro ao listar os clientes.");
        }
    });
}
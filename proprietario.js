function cadastrarAlterar(){
	var data = {
		idade: document.getElementById('nmIdade').value,
		nome: document.getElementById('txtNome').value,
		email: document.getElementById('txtEmail').value,
		telefone: document.getElementById('txtTelefone').value,
		genero: pegarGenero()
	  };
	  

$.ajax({
   method: "POST",
   url: "http://localhost:5129/api/pessoa/gravar",
   dataType: 'json',
   contentType: "application/json; charset=utf-8",
   data: JSON.stringify(data),
	 }).done(function( msg ) {
     alert( "Data Saved: " + msg );
   });  
}

function pegarGenero(){
	if(document.getElementById('dot-1').checked)
		return 1;
	if(document.getElementById('dot-2').checked)
		return 2;
	if(document.getElementById('dot-3').checked)
		return 0;
}
function pegarGeneroDescricao(genero){
	if(genero == 1)
		return "Homem";
	if(genero == 2)
		return "Mulher";
	
	return "Não Informado"
}

function buscarTodos(){
	$.ajax({
   method: "GET",
   url: "http://localhost:5129/api/pessoa/BuscarTodos",
   dataType: 'json',
   contentType: "application/json; charset=utf-8",
   success: function(data){
	   data.forEach(function(current, index){ 
	    var tabela = document.getElementById('tabelaProprietarios');
	    var tr = document.createElement('tr');
		var tdNome = document.createElement('td');
		var tdEmail = document.createElement('td');
		var tdTelefone = document.createElement('td');
		var tdIdade = document.createElement('td');
		var tdGenero = document.createElement('td');
		
		tdNome.innerHTML = current.nome;
		tdEmail.innerHTML = current.email;
		tdTelefone.innerHTML = current.telefone;
		tdIdade.innerHTML = current.idade;
		tdGenero.innerHTML = pegarGeneroDescricao(current.genero);
		
		tr.appendChild(tdNome);
		tr.appendChild(tdEmail);
		tr.appendChild(tdTelefone);
		tr.appendChild(tdIdade);
		tr.appendChild(tdGenero);
		tabela.appendChild(tr);
	   })
	}
	}).done(function( msg ) {
     alert( "Data Saved: " + msg );
   });  
}
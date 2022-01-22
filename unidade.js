function cadastrarAlterar(){
	var data = {
		proprietario: document.getElementById('txtProprieario').value,
		endereco: document.getElementById('txtEndereco').value,
		numero: document.getElementById('txtNumero').value,
		nomeCondominio: document.getElementById('txtNomeCondominio').value,
	  };
	  

$.ajax({
   method: "POST",
   url: "http://localhost:5129/api/imovel/gravar",
   dataType: 'json',
   contentType: "application/json; charset=utf-8",
   data: JSON.stringify(data),
	 }).done(function( msg ) {
     alert( "Data Saved: " + msg );
   });  
}


function buscarTodos(){
	$.ajax({
   method: "GET",
   url: "http://localhost:5129/api/imovel/BuscarTodos",
   dataType: 'json',
   contentType: "application/json; charset=utf-8",
   success: function(data){
	   data.forEach(function(current, index){ 
	    var tabela = document.getElementById('tabelaUnidades');
	    var tr = document.createElement('tr');
		var tdProprietario = document.createElement('td');
		var tdEndereco = document.createElement('td');
		var tdNumero = document.createElement('td');
		var tdNomeCondominio = document.createElement('td');
		
		tdProprietario.innerHTML = current.proprietario;
		tdEndereco.innerHTML = current.endereco;
		tdNumero.innerHTML = current.numero;
		tdNomeCondominio.innerHTML = current.nomeCondominio;
		
		tr.appendChild(tdProprietario);
		tr.appendChild(tdEndereco);
		tr.appendChild(tdNumero);
		tr.appendChild(tdNomeCondominio);
		tabela.appendChild(tr);
		
	   })
	}
	}).done(function( msg ) {
     alert( "Execução do ajax completa ");
   });  
}
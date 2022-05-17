function consultaCep() {
    try {
        document.getElementById("consultacep").value = "Consultando...";
        var $cep = document.getElementById("Cep").value.replace(/\D/g, '');
        var url = 'https://viacep.com.br/ws/' + $cep + '/json/';
        var request = new XMLHttpRequest();

        request.open('GET', url);
        request.onerror = function (e) {
            alert('API Offline ou Cep Inválido');
            //document.getElementById("consultacep").value = "Consulta CEP";
        }

        request.onload = () => {
            var response = JSON.parse(request.responseText);

            if (response.erro === true) {
                alert('CEP não encontrado');
            }
            else {
                document.getElementById("Logradouto").value = response.logradouro;
                document.getElementById("Cidade").value = response.localidade;
            }
            //document.getElementById("consultacep").value = "Consulta CEP";
        }
        request.send();
    }
    finally {
        document.getElementById("consultacep").value = "Consulta CEP";
    }
}
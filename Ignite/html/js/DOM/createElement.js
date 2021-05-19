// criando e adicionando elementos

//createelement criando div
const p = document.createElement('p');
p.innerHTML = "Olá devs";

//adicionar na pagina
//selecionando o body para adicionar algo depois da tag
const body = document.querySelector('body');

body.append(p);




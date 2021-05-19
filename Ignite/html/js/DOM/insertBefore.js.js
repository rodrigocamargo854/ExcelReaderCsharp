// criando e adicionando elementos

//createelement criando div
const p = document.createElement('p');
p.innerHTML = "Olá devs";

//adicionar na pagina
//selecionando o body para adicionar algo depois da tag
const body = document.querySelector('body');
const script = document.querySelector('script');
//inserir entre
body.insertBefore(p,script)

//caso seja nessário inserir depois

const header = body.querySelector('header');
body.insertBefore(div,header.nextElementSibling);





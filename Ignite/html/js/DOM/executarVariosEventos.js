//executar eventos


const h1 = document.querySelector('h1');
const input = document.querySelector('input');
//ouvindo evento no h1 (tipo do evento, funcao executada)
h1.addEventListener('click',print)
input.addEventListener('keydown',print)
//são varios eventos listados 

function print(){
    h1.style.color = 'blue';
}


// mais um modo de excutar eventos
//passa a funcção como um atalho 
h1.onclick = print;


h1.addEventListener('click', function(){
    console.log('another moment');
})
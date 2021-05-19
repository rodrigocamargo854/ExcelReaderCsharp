//por meio do event podemos capturar o tipo de evento
// por exmplo, no evento tecla digita
//podemnos capturar a tecla digitada

const input = document.querySelector('input');
input.onkeypress = function (element){
    //capturando a tecla 
    console.log(element.key);
    //capturando o elemento
    console.log(element.currentTarget);
    //capturando o target
    console.log(element.target);
    //capturando todas as teclas
    console.log(element.currentTarget.value);
    


}
//Separando strings 

// separe um texto que contem espaços em um novo 
//array onde cada texto é uma posição do array
//Depois disso , transforme o array em um texto e onde eram espaços coloeque _

let texto = "Lorem Ipsum é simplesmente uma simulação de texto da indústria tipográfica e de impressos, e vem sendo utilizado desde o século XVI, quando um impressor desconhecido pegou uma bandeja de tipos e os embaralhou para fazer um livro de modelos de tipos. Lorem Ipsum sobreviveu não só a cinco séculos, como também ao salto para a editoração eletrônica, permanecendo essencialmente inalterado. Se popularizou na década de 60, quando a Letraset lançou decalques contendo passagens de Lorem Ipsum, e mais recentemente quando passou a ser integrado a softwares de editoração eletrônica como Aldus PageMaker."
let myarray = texto.split(" ")
let phraseWithUnderscore = myarray.join('_')
console.log(phraseWithUnderscore)
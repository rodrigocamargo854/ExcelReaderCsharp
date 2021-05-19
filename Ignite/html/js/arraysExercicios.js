let techs = ["html","css","js"]

//adicionar item no final
console.log(techs)   
//Adicionar item no começo
techs.unshift("sql")
//remover item
techs.pop()
//remover do começo
techs.shift()

//pegar somente alguns elementos do arary(posicao inicial, posição final)
console.log(techs.slice(1,3))
//remover um ou mais itens em qualquer posição do array
techs.splice(1,1)
//encontrar a posicção de um array
let index = techs.indexOf('css')

console.log(index)   




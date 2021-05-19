//transformar numero em string  e string em numero

let string = "123"

console.log(Number(string))


let word = "paralelepipedo"

console.log(word.length)

let number = 123456

console.log(String(number).length)

//trocar um numero quebrado com 2 casas decimais e trocar por ponto e virgula

let number2 = 76464.4564

console.log(number2.toFixed(2).replace(".",','))

//NAN porque a virgula nao e um numero
// conseguimos atrelar funcoes em funcoes
console.log(Number(number2.toFixed(2).replace(".",',')))

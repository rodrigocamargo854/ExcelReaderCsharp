//function expression ou funcao anonima

//funcao quando criado , recebe parametros
const sum = function(a,b){
return a+b
}

sum(5,3) //quando chamada recebe argumentos
console.log(sum(5,3))
console.log(sum(55,3))

let n1 = 54
let n2 = 2123

console.log(sum(n1,n2))


function fazerSuco(fruta1,fruta2){
    return fruta1+fruta2

}

const copo = fazerSuco('banana' , 'maçã')

console.log(copo)

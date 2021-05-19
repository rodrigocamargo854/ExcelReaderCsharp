//Operadores de comparação estritamente igual a
let one = 1;
let two = 2 ;
// ele compara o tipo ==  e o valor =
// tipovalor  ===
console.log(one === '1');
console.log(one === 1);

//estritamente difenrente de !==

console.log(two !== '2')
console.log(two !== 2)
 
//Ternario

//condição ? valor1 : valor2

let pao = true;
let queijo = true;

const breakFastTop = pao && queijo ?  'cafe top' : ' cafe ruim'

console.log(breakFastTop)



let biscoito = true;
let presunto = false;

const breakFast = biscoito || presunto ?  'cafe top' : ' cafe ruim'

console.log(breakFast)

let age = 16;

const canDrive = age>= 18 ? 'can drive' : 'cant drive';

console.log(canDrive)

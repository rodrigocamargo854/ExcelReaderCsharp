//1 declare uma variavel de nome weight
let weight = 85;
//2 Que tipo de dadoa [e a variavel acima?
console.log(typeof weight)
//undefined
//3 declare uma variavel e atribua valores para cada um dos dados?

const student2 = {}
//4- A variavel Student é que tipo?
//Object

//5
let student = {
    name: 'Rodrigo',
    age: 41,
    stars: 2.5,
    isSubstscribed: true,
    weight: 85
}

console.log(`${student.name} de idade, ${student.age}, pesa ${student.weight} `)


//5 declare uma variavel do tipo array e n declare nenhum valor
let students = [];

//6 Inserir o objeto criado dentro do array vazio
students.push(student.age)
students.push(student.name)
students.push(student.isSubstscribed)
students.push(student.weight)

console.log(students)

//7 Coloque o console na posição zero do valor acima
console.log(students[0])

//8 Crie um novo student e coloque na posição 1 do Array students
 
const newStudent = {
    name: 'Pedro',
    age: 25,
    stars: 2.5,
    isSubstscribed: true,
    weight: 65
}

students.push(newStudent)
console.log(students)








// sayMyName()


//funcao do tipo statement
// // const sayMyName = function sayMyName() {
// //     console.log('Maik')
    
// // }

//arrow function expression

// const sayMyName = (name) => {
//     console.log(name)
// }

// sayMyName('Rodrigo')


//CallBack Function
// O parametro name pode ser qualquer tipo de variavel ate uma arrow function
function tarefasDia(tarde) {
    console.log("Estudar")
    console.log("trabalhar")
    tarde()
    console.log("jogar")
}

tarefasDia(() => {
    console.log('trabalhar')
})
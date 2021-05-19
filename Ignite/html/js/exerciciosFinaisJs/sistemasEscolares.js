// Transformar notas escolares

//Crie um algoritimo que transforme as notas do sistema 
//numerico para sistema de notas em caracteres tipo A B C

// de 90 para cima - A
// entre 80 - 89 - B
// entre 70 - 79 - C
// entre 60 - 89 - D
// entre 80 - 69 - E
// menor que 60 - F

function calculaNota(nota){

    let notaA = nota >= 90 && nota <= 100;
    let notaB = nota > 80 && nota < 89;
    let notaC = nota > 70 && nota < 79;
    let notaD = nota > 60 && nota < 69;
    let notaF = nota > nota < 60 ;
    
    if(notaA){
    return console.log("Sua nota foi A")
    }
    else if(notaB ){
    return console.log("Sua nota foi B")
    }
    else if(notaC){
    return console.log("Sua nota foi C")
    }
    else if(notaD){
    return console.log("Sua nota foi D")
    }
    else if (notaF){
    return console.log("Sua nota foi F")
    }

    else if (nota != Number) {
        throw new Error('Digite um número por favor')
    }


}

calculaNota(10)
calculaNota(50)
calculaNota(2)
calculaNota(78)
calculaNota(77)
calculaNota(411)
calculaNota(22)
calculaNota(5)

// Control flow
//Throw

let name = ''
function sayMyName(name) {
    if (name === '') {
        throw new Error('nome é necessário')
    }
    console.log('depois do erro')

//quando ela sai do throw ela arremessa para o try e catch
    //try catch

    try {
            sayMyName()
    }
    catch (e) {
        // aqui ele pega o erro e mostra 
        console.log(e)

    }

}


//Try..catch


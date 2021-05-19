//Crie uma função que receba uma string em celsius
//ou fahrenheit e faça a transformação de uma 
// unidade para outra

//C = (F-32) 5/9
//F = C* 9/5 +32

function converteTemp(temp,escale){

   if(escale == 'F' || escale == 'f'){
        let convertion = (temp-32)*5/9 
        return `${convertion} fahreinheit` 

    }else if(escale == 'C' || escale == 'c'){
        let convertion = (temp * 9/5) + 32
        return `${convertion} celsius`      
    
    }else if(temp != Number){
        throw new Error('temperatura precisa ser um numero')
    }

    try {
        converteTemp(temp,escale)
        
    } catch (error) {
        console.log(error.message) 
               
    }
}

console.log(converteTemp('c','f'));
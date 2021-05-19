

function transformDegree(degree){
    const celsiusExists = degree.toUpperCase().includes('C')
    const fahrenheitExits = degree.toUpperCase().includes('F')

        if(!celsiusExists && !fahrenheitExits){
           throw new Error('Grau não identificado') 
        }

        //caminho ideal F => C
        let updateDegree = Number(degree.toUpperCase.replace("F",""));
        let formula = fahrenheit => (fahrenheit - 32) * 5/9;
        let degreeSign = 'C'; 

        if(celsiusExists){
            updatedDegree = Number(degree.toUpperCase().replace("F",""));
            formula = celsius => celsius * 9/5 + 32;
            degreeSign = 'F';
        }
        return formula(updateDegree) + degreeSign

}

try {
    transformDegree('50F');
    transformDegree('50Z');
} catch (error) {
    console.log(error.message)
}
const animals = [
    'Lion',
    'Monkey',
    'Cat',
    'dog',
    'bird',
    'pig',
    'squirrel',
    'shark',
    'fish',
    {
    name: 'dog'
    }
]

console.log(animals[5])
console.log(animals.length)
console.log(animals[0].name)

//criar array com construtor

let myArray = ['a','b','c']


let myNewArray = new Array(10)

console.log(myNewArray)

console.log(["a",
    function(){
        return "alo"
    }][2])



    // contar elementos de um array
    console.log(['a','b','c'].length)


    //transformar cadeia de caracteres e, elemnts de um array

    let word = "manipulação"

console.log(Array.from(word))




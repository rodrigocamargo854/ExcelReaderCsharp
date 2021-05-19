

function Person(name) {
    this.name = name
    this.walk = function() {
        return `${name} andando`
        
    }
}
    const mayk = new Person('Maik')
    const Rodrigo = new Person('Rodrigo')

    console.log(mayk.walk())


    

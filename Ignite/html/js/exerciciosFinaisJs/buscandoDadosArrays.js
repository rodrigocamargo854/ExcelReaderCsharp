//Buscando e contando dados em arrays

//Baseado no array de livros de categoria abaixo, faça os seguintes desafios

//Conta o numero de categorias e o numero de livros em cada categoria
//Contar o numero de autores
//Mostrar livros de Autor Augusto Cury
//Transformar a função acima em uma função que irá receber o nome 
//do autor e devolver os livros desse autor.


const booksByCategory = [
    {
        category: "Riqueza",
        books: [
            {
                title: "Os segredos da mente milionaria",
                author: "T. Harv Eker",
            },
            {
                title: "O homem mais rico da Babilonia",
                author: "George S. Clason",
            },
            {
                title: "Pai rico e Pai Pobre",
                author: "Robert T. Kiyosaki e Sharon L. Lechter",
            },
        ],
    },
    {
        category: "Inteligência Emocional",
        books: [
            {
                title: "Você é insubstituivel",
                author: "Augusto Cury",
            },
            {
                title: "Ansiedade - Como enfrentar o Mal do Século",
                author: "Augusto Cury",
            },
            {
                title: "Os 7 hábitos Mais eficazes",
                author: "Stephen R, Covey",
            },
        ],
    },
];


const categoryCount = booksByCategory.length
console.log(`são ${categoryCount} categorias`)
console.log("################################")


function CategoryCount() {

    let categoryCounter;
    let livrosCount;

    for (let item of booksByCategory) {
        categoryCounter = item.category;
        livrosCount = item.books.length;

        console.log(`Total de livros da categoria ${categoryCounter} são ${livrosCount}`)

    }

}

function countAuthors() {
    //Total de autores
    const author = [];

    for (let item of booksByCategory) {
        for (let autores of item.books) {
            author.push(autores.author)
        }
    }

    //Deixando o array authors sem repetição
    const totalAuthors = [... new Set(author)]
    console.log(`O número de autores são ${totalAuthors.length}`)
}

function findBooksOfAuthor(author) {
    //Total de autores
    const autores = [];
    const titlesByAuthor = [];
    
    for (let item of booksByCategory) {
        for (let autores of item.books) {
            if(autores.author == author)
                titlesByAuthor.push(autores.title)
        }
       
    }

    //Deixando o array authors sem repetição
    const totalAuthors = [... new Set(autores)]
    console.log(`Os titulos do autor ${author} são ${titlesByAuthor.join(',')}`)   

}



CategoryCount();
countAuthors();
findBooksOfAuthor('George S. Clason');








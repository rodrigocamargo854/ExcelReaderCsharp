//alterando estilos
// o queryselector pega a tag
const element = document.querySelector('body')
//adicionar uma classe
element.classList.add("active" , "green")
console.log(element.classList)
//remove a classe
element.classList.remove("active")
//togle, remove se tem a classe e adiciona se n tem
element.classList.toggle('active')





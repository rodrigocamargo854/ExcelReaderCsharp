//manipulando elementos pelo atributo
//setar um id par uma tag

const header = document.querySelector('header')

header.setAttribute('id','header')

const headerId = document.querySelector('#header')

console.log(headerId.getAttribute('class'))

header.removeAttribute('id')



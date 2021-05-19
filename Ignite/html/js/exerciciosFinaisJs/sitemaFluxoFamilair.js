
let renda = {
    receitas: [200, 500, 1000],
    despesas: [100, 200, 54]
}

function sum(array) {
    let total = 0;
    for (const iterator of array) {
        total += iterator
    }

    return total
}

function calculoDeRendaMensal() {
    const calcReceitas = sum(renda.receitas);
    const calcDespesas = sum(renda.despesas);
    const totalDespesas = calcReceitas - calcDespesas
    const situacao = totalDespesas > 0 ? 'situação normal' : 'situação negativa';

    return console.log(situacao)
}

calculoDeRendaMensal()


const transactionsUl = document.querySelector("#transactions");
const incomeDisplay = document.querySelector("#money-plus");
const expenseDisplay = document.querySelector("#money-minus");
const balanceDisplay = document.querySelector("#balance");
const form = document.querySelector('#form')
const inputTransictionName = document.querySelector('#text')
const inputTransictionAmount = document.querySelector('#amount')

const dummyTransactions = [
  { id: 1, name: 'Bolo de brigadeiro', amount: -209 },
  { id: 2, name: 'Salário', amount: 300 },
  { id: 3, name: 'Torta de frango', amount: -10 },
  { id: 4, name: 'Violão', amount: 1509 },
]

const addTransactionIntoDOM = transaction => {
  const operator = transaction.amount < 0 ? "-" : "+";
  const CSSClass = transaction.amount < 0 ? "minus" : "plus";
  const amountWithoutoperator = Math.abs(transaction.amount);
  const li = document.createElement("li");

  li.classList.add(CSSClass);
  li.innerHTML = `
${transaction.name} <span>${operator} R$ ${amountWithoutoperator}</span><button class="delete-btn"
`;

    transactionsul.append(li);
};

const updateBalancevalues = () => {
    const transactionsAmounts = dummyTransactions
    .map(transaction => transaction.amount)
    const total = transactionsAmounts
    .reduce((accumulator, transaction) => accumulator + transaction, 0)
    .toFixed(2)
    const income = transactionsAmounts
    .filter(value => value > 0)
    .reduce((accumulator, value) => accumulator + value, 0)
    .toFixed(2)
    const expense = transactionsAmounts
    .filter(value => value < 0)
    .reduce((accumulator, value) => accumulator + value, 0)
    .toFixed(2)

    balanceDisplay.textContent = `R$ ${total}`
    incomeDisplay.textcontent = `R$ ${income}`
    expenseDisplay.textContent = `R$ ${expense}`
    }

const init = () => {
    transactionsUl.innerHTML = ''

  dummyTransactions.forEach(addTransactionIntoDOM);
  updateBalancevalues()
};

init();

const generateID = () => Math.round(Math.random()* 1000)

form.addEventListener('submit', event => {
    event.preventDefault()

    if (inputTransictionName.value.trim() === '' || inputTransictionAmount.value.trim() === '') {
        alert('Preencha o nome e o valor desejado')
        return
    }

    const transaction = {id: generateID(), name: transactionName, amount: Number(transactionAmount)}

    dummyTransactions.push(transaction)
    init()


} )

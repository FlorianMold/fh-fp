/**
 * Computes the sum of all odd numbers in the array. Uses an imperative style.
 *
 * @param {number[]} input
 * @returns {number} Sum of all odd items.
 */
function computeSumOfAllOddNumbersImperative(input) {
    let result = 0

    for (const item of input) {
        result += item % 2 ? item : 0
    }

    return result
}

/**
 * Computes the sum of all odd numbers in the array. Uses a more functional style.
 *
 * @param {number[]} input
 * @returns {number} Sum of all odd items.
 */
function computeSumOfAllOddNumbersFunctional(input) {
    return input
        .filter(value => value % 2)
        .reduce((previousValue, currentValue) => previousValue + currentValue, 0)
}

const testData = [
    {data: [1, 3, 5, 2], exp: 9},
    {data: [0, 2, 4, 6], exp: 0},
    {data: [1, 3, 7, 6], exp: 11}
]

const functions = [
    {fn: computeSumOfAllOddNumbersImperative, approach: 'imperative'},
    {fn: computeSumOfAllOddNumbersFunctional, approach: 'functional'}
]

testData.forEach(row => {
    console.log('Data: %s, Expected result: %s', row.data, row.exp)
    functions.forEach(app => {
        const res = app.fn(row.data)

        console.assert(res === row.exp, `The result of the ${app.approach} approach is incorrect!`)
        console.log(`Computed sum of all odd numbers (${app.approach}): %d`, res)
    })
    console.log()
})

Vue.prototype.$http = axios
axios.defaults.baseURL = 'http://localhost:54775/api/'

Vue.use(window.vuelidate.default)
const { required } = window.validators

Vue.filter('currency', function (value) {
    var options = {
        symbol: "$",
        decimal: ".",
        thousand: ",",
        precision: 2,
        format: {
            pos: "%s%v",
            neg: "(%s%v)",
            zero: "%s  -- "
        }
    }

    return accounting.formatMoney(value, options)
})

Vue.filter('formatDate', function (value) {
    return moment(value).format('M/D/YYYY h:mm:ss A')
})

function greaterThanZero(value) {
    return value > 0
}

new Vue({
    el: '#app',
    data: {
        charges: [],
        chargeTableHeaders: [
            { title: 'Amount' },
            { title: 'Type' },
            { title: 'Transaction Date' }
        ],
        chargeTypes: [],
        form: {
            amount: null,
            selected: null
        },
        totals: [],
        totalsTableHeaders: [
            { title: 'Type', icon: '' },
            { title: 'Amount', icon: 'fas fa-sort-amount-down' }
        ]
    },

    created: function () {
        this.getChargeTypes()
        this.getCharges()
        this.getTotalsByType()
    },

    methods: {
        getChargeTypes() {
            axios.get('chargetypes/')
                .then(response => {
                    this.chargeTypes = response.data
                }, error => {
                    console.log(error)
                })
        },

        getCharges() {
            axios.get('charges/')
                .then(response => {
                    this.charges = response.data
                }, error => {
                    console.log(error)
                })
        },

        getTotalsByType() {
            axios.get('totals/')
                .then(response => {
                    this.totals = response.data
                }, error => {
                    console.log(error)
                })
        },

        handleSubmit() {
            var params = {
                Amount: this.form.amount,
                ChargeTypeID: this.form.selected
            }

            axios.post('charges/', params)
                .then(response => {
                    console.log('added')
                    this.refreshPage()
                }, error => {
                    console.log(error)
                })
        },

        refreshPage() {
            this.form.amount = null,
                this.form.selected = null,
                this.getCharges()
            this.getTotalsByType()
        }
    },

    computed: {
        chargeTotals: function () {
            if (!this.charges || this.charges.length == 0)
                return 0

            var total = 0

            for (var i = 0; i < this.charges.length; i++) {
                total += this.charges[i].Amount
            }

            //this.charge.forEach(function (item) {
            //    total += item.Amount
            //})

            return total
        },

        errorMessage1: function() {
            return (this.$v.form.amount.$dirty && !this.$v.form.amount.required) ? 'Amount Required.' :
                (this.$v.form.amount.$dirty && !this.$v.form.amount.greaterThanZero) ? 'Amount greater than 0.' : null
        },

        errorMessage2: function() {
            return (this.$v.form.selected.$dirty && !this.$v.form.selected.required) ? 'Charge Type Required.' : null
        }
    },

    validations: {
        form: {
            amount: {
                required,
                greaterThanZero
            },
            selected: {
                required
            }
        }
    }
})
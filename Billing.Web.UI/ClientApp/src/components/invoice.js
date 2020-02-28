import React, {Component} from 'react'
import { Button } from 'reactstrap'

export class Invoice extends Component {
    constructor(props){
        super(props);
        this.state = {paramId: props.match.params.id, invoice: {}};
        this.addExpense = this.addExpense.bind(this);
    }

    componentDidMount(){
        fetch('https://localhost:44333/api/invoice/'+ this.state.paramId)
        .then(res => res.json())
        .then(data => {
            this.setState({invoice: data});
        });
    }

    addExpense(){
        fetch('https://localhost:44333/api/invoice/'+ this.state.paramId + '/expense', {method: 'POST'})
    }

    render(){
        return (
            <div>
                <h1>Invoice {this.props.match.params.id}</h1>
                <p>
                    Invoice Id: {this.state.invoice.invoiceId}
                </p>
                <Button onClick={this.addExpense}>Add Expense</Button>
            </div>
        )
    }
}
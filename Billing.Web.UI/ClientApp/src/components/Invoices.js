import React, {Component} from 'react'
import { Button } from 'reactstrap'

export class Invoices extends Component{
    constructor(props){
        super(props);
        this.state = {invoices: []};
        this.addInvoice = this.addInvoice.bind(this);
        this.redirectToDetails = this.redirectToDetails.bind(this);
    }

    componentDidMount(){
        this.loadInvoicesData();
    }
    
    addInvoice() {
        console.log("Add Invoice Clicked");
        fetch("https://localhost:44333/api/invoice", {method: 'POST'})
        .then(res => res.json())
        .then(data => this.redirectToDetails(data.invoiceId));
    }

    redirectToDetails(id) {
        console.log("Redirect to invoice details for key " + id);
        this.props.history.push('/invoice/' + id);
    }

    loadInvoicesData(){
        fetch("https://localhost:44333/api/invoice")
        .then(res => res.json())
        .then(data => {
            this.setState({invoices: data});
        });
    }

    render(){
        return (
            <div>
                <h2>Invoices</h2>
                {this.state.invoices.map((invoice) => (
                    <div className="card" key={invoice.invoiceId} onClick={ () => this.redirectToDetails(invoice.invoiceId)}>
                        <div className="card-body">
                            <p className="card-text">
                                {invoice.invoiceId} - expenses {invoice.numberOfExpenses}
                            </p>
                        </div>
                    </div>
                ))}
                <Button onClick={this.addInvoice}>Add Invoice</Button>
            </div>
        );
    }
}
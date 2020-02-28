import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Invoices } from './components/Invoices'
import { Invoice } from './components/invoice'

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route exact path='/invoices' component={Invoices} />
        <Route exact path='/invoice/:id' component={Invoice} />
      </Layout>
    );
  }
}

import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Meallist } from './components/Meallist';
import { Create } from './components/Create';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Meallist} />
        <Route path='/create-data' component={Create} />
      </Layout>
    );
  }
}

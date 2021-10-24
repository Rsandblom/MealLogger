import React, { Component, useState } from 'react';

export class Meallist extends Component {
  static displayName = Meallist.name;

  constructor(props) {
    super(props);
      this.state = { meals: [], loading: true };
  }

  componentDidMount() {
    this.populateMealData();
    }

            
    static deleteMeal = (props) => {
        alert(props);
        fetch('api/meals/' + props,
            {
                method: 'DELETE'
            }).then((result) => {
                result.json().then((resp) => {
                    console.warn(resp)
                })
            });
    }

   

  static renderMeallistTable(meals) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Type of meal</th>
            <th>Meal description</th>
            <th>Remove</th>
          </tr>
        </thead>
        <tbody>
          {meals.map(meal =>
            <tr key={meal.id}>
              <td>{meal.typeOfMeal}</td>
                  <td>{meal.mealDescription}</td>
                  <td><button className="btn btn-danger" onClick={() => this.deleteMeal(meal.id)}>Delete</button></td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
        ? <p><em>Loading...</em></p>
        : Meallist.renderMeallistTable(this.state.meals);

    return (
      <div>
        <h1 id="tabelLabel" >Meal list</h1>
        <p>List of logged meals.</p>
        {contents}
      </div>
    );
  }

  async populateMealData() {
      const response = await fetch('api/meals');
      const data = await response.json();
    this.setState({ meals: data, loading: false });
  }
}

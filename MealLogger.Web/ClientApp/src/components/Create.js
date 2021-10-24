import React, { Component } from 'react';


export class Create extends Component {
    static displayName = Create.name;

    constructor(props) {
        super(props);
        this.state = { typeOfMeal: 'Breakfast' };
        this.state = { mealDescription: '' };
        this.handleTypeChange = this.handleTypeChange.bind(this);
        this.handleDescChange = this.handleDescChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleTypeChange(event) {
        this.setState({ typeOfMeal: event.target.value });
    }

    handleDescChange(event) {
        this.setState({ mealDescription: event.target.value });
    }


    async handleSubmit(event) {
        event.preventDefault();
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ typeOfMeal: this.state.typeOfMeal, mealDescription: this.state.mealDescription })
        };
        await fetch('api/meals', requestOptions);
        alert('Meal logged');

    }


    render() {
        return (
            
            <div>
                <h1>Log Meal</h1>
                <form onSubmit={this.handleSubmit}>
                    <div className="mb-3">
                        <label className="form-label">Pick your type of meal.</label>
                    </div>
                    <div className="mb-3">
                        <select value={this.state.typeOfMeal} onChange={this.handleTypeChange}>
                            <option value="Breakfast">Breakfast</option>
                            <option value="Lunch">Lunch</option>
                            <option value="Dinner">Dinner</option>
                        </select>
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Meal decription</label>
                        <input className="form-control" type="text" value={this.state.mealDescription} onChange={this.handleDescChange} />
                    </div>
                    <div className="mb-3">
                        <input className="btn btn-primary" type="submit" value="Submit" />
                    </div>
                </form>

            </div>
        );
    }
}


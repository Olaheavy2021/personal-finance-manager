import { AnyAction } from "@reduxjs/toolkit";
import { ActionTypes, ExpensesState } from "../models/types";

const initialState: ExpensesState = {
  expenses: [],
};

export function expensesReducer(
  state: ExpensesState = initialState,
  action: AnyAction
): ExpensesState {
  switch (action.type) {
    case ActionTypes.SET_EXPENSES:
      return {
        ...state,
        expenses: action.payload,
      };
    case ActionTypes.NEW_EXPENSE:
      return {
        ...state,
        expenses: [action.payload, ...state.expenses]
      }
    case ActionTypes.EDIT_EXPENSE:
      var expenses = state.expenses.map(expense => {
        if(expense.id === action.payload.id){
          expense = action.payload;
        }
        return expense;
      });
      return {
        ...state,
        expenses: [... expenses]
      }
    case ActionTypes.DELETE_EXPENSE:
      var expenses = state.expenses.filter(expense => expense.id != action.payload.id)
      return{
        ... state, expenses: [... expenses]
      }
    default:
      return state;
  }
}
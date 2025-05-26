import { Dispatch } from "@reduxjs/toolkit/react"
import { ActionCreators } from "../models/actions"
import { Expense, ExpensesAction } from "../models/types"

export const expenses: Expense[] = [
  {
    id: 1,
    description: "Groceries",
    amount: 23.16,
    date: "2025-05-01",
  },
  {
    id: 2,
    description: "Gas",
    amount: 18.52,
    date: "2025-05-02",
  },
  {
    id: 3,
    description: "Student Loans",
    amount: 600,
    date: "2025-05-03",
  },
]

export const GetExpenses = async (
  dispatch: Dispatch<ExpensesAction>,
): Promise<void> => {
  try {
    // pretend we fetched `expenses` from an API
    dispatch(ActionCreators.setExpenses(expenses))
  } catch {
    console.error("Error fetching expenses")
  }
}

export const NewExpense = async (
  dispatch: Dispatch<ExpensesAction>,
  expense: Omit<Expense, 'date'>
): Promise<void> => {
  try {
    // simulate API call…
    const saved: Expense = {
      ...expense,
      date: new Date().toISOString(), // ensure we supply a date
    };
    console.log(saved);

    dispatch(ActionCreators.newExpense(saved));
  } catch (err) {
    console.error('Error creating expense', err);
  }
};

export const EditExpense = async (dispatch : Dispatch<ExpensesAction>, expense: Expense)  => {
  try{
    //api call
    dispatch(ActionCreators.editExpense(expense));
  }catch(err){
     console.error('Error creating expense', err);
  }
}

export const DeleteExpense = async (dispatch : Dispatch<ExpensesAction>, expense: Expense)  => {
  try{
    //api call
    dispatch(ActionCreators.deleteExpense(expense));
  }catch(err){
     console.error('Error creating expense', err);
  }
}
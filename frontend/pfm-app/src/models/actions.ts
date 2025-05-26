import { Expense, SetExpensesAction, ActionTypes, NewExpenseAction, EditExpenseAction, DeleteExpenseAction } from './types';

export const ActionCreators = {
  setExpenses: (payload: Expense[]): SetExpensesAction => ({
    type: ActionTypes.SET_EXPENSES,
    payload,
  }),
  newExpense:(payload: Expense) : NewExpenseAction => ({
    type: ActionTypes.NEW_EXPENSE,
    payload
  }),
  editExpense : (payload: Expense) : EditExpenseAction => ({
    type: ActionTypes.EDIT_EXPENSE,
    payload
  }),
  deleteExpense : (payload: Expense) : DeleteExpenseAction => ({
    type: ActionTypes.DELETE_EXPENSE,
    payload
  })
};

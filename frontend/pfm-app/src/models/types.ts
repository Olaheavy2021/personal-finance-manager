export interface Expense {
  id?: number;
  description: string;
  amount: number;
  date?: string;  
}

export interface ExpensesState {
  expenses: Expense[];
}

export enum ActionTypes {
  SET_EXPENSES = 'SET_EXPENSES',
  NEW_EXPENSE = 'NEW_EXPENSE',
  EDIT_EXPENSE = 'EDIT_EXPENSE',
  DELETE_EXPENSE = 'DELETE_EXPENSE'
}

export interface SetExpensesAction {
  type: ActionTypes.SET_EXPENSES;
  payload: Expense[];
}

export interface NewExpenseAction{
  type: ActionTypes.NEW_EXPENSE
  payload: Expense
}

export interface EditExpenseAction{
  type: ActionTypes.EDIT_EXPENSE
  payload: Expense
}

export interface DeleteExpenseAction{
  type: ActionTypes.DELETE_EXPENSE
  payload: Expense
}

export type ExpensesAction = SetExpensesAction | NewExpenseAction | EditExpenseAction | DeleteExpenseAction;

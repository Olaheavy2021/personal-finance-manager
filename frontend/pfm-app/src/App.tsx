import "./App.css"
import ExpenseForm from "./components/ExpenseForm"
import ExpenseList from "./components/expenseList"

export const App = () => (
  <div style={{ width: "60%", margin: "auto" }}>
    <h3>New Expenses</h3>
    <ExpenseForm />
    <hr style={{ border: "1px solid grey" }} />
    <h3>Your Expenses</h3>
    <ExpenseList />
  </div>
)

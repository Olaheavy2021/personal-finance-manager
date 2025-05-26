import React, { useEffect, useState } from "react"
import { useDispatch, useSelector } from "react-redux"
import { GetExpenses } from "../services/expenses"
import { Button, Col, Row } from "react-bootstrap"
import { Expense } from "../models/types"
import { AppDispatch, RootState } from "../app/store"
import ExpenseForm from "./ExpenseForm"

const ExpensesList: React.FC = () => {
  const dispatch = useDispatch<AppDispatch>()

  // select the array from Redux state
  const expenses = useSelector((state: RootState) => state.expenses.expenses)

  useEffect(() => {
    dispatch(GetExpenses)
  }, [dispatch])

  return (
    <>
      {expenses.map(e => (
        <div key={e.id} style={{ marginBottom: "1rem" }}>
          <ListRow expense={e} />
        </div>
      ))}
    </>
  )
}

export default ExpensesList

type ListRowProps = {
  expense: Expense
}

export const ListRow: React.FC<ListRowProps> = ({ expense }) => {
  const [isEditing, setIsEditing] = useState(false)

  return isEditing ? (
    <ExpenseForm expense={expense} setIsEditing={setIsEditing} />
  ) : (
    <div>
      <Row>
        <Col>{expense.description}</Col>
        <Col>£{expense.amount}</Col>
        <Col>
          <Button variant="warning" onClick={() => setIsEditing(!isEditing)}>
            Edit
          </Button>
        </Col>
      </Row>
      <hr />
    </div>
  )
}

import React, { useEffect, useState } from "react"
import { Form, Row, Col, Button } from "react-bootstrap"
import { DeleteExpense, EditExpense, NewExpense } from "../services/expenses"
import { useDispatch } from "react-redux"
import { Expense } from "../models/types"

interface ExpenseFormProps {
  expense: Expense
  setIsEditing: React.Dispatch<React.SetStateAction<boolean>>
}

const ExpenseForm: React.FC<ExpenseFormProps> = ({ expense, setIsEditing }) => {
  const descriptions = [
    "Groceries",
    "Credit Card",
    "Student Loans",
    "Eating Out",
    "Gas",
  ]

  const [amount, setAmount] = useState<number>(0)
  const [description, setDescription] = useState<string>(descriptions[0])
  const [isNewExpense, setIsNewExpense] = useState(true)
  const dispatch = useDispatch()

  useEffect(() => {
    if (expense !== undefined) {
      setIsNewExpense(false)
      setAmount(expense.amount)
      setDescription(expense.description)
    } else {
      setIsNewExpense(true)
    }
  }, [expense])

  return (
    <Form
      onSubmit={event => {
        event.preventDefault()
        if (isNewExpense) {
          NewExpense(dispatch, {
            id: 10,
            description: description,
            amount: amount,
          })
        } else {
          //edit expense
          EditExpense(dispatch, {
            id: expense?.id,
            description: description,
            amount: amount,
          })
          setIsEditing(false)
        }
      }}
    >
      <Row>
        <Col>
          <Form.Group controlId="descriptionSelect">
            <Form.Label>Description</Form.Label>
            <Form.Select
              value={description}
              onChange={(e: React.ChangeEvent<HTMLSelectElement>) =>
                setDescription(e.target.value)
              }
            >
              {descriptions.map((d, i) => (
                <option key={i} value={d}>
                  {d}
                </option>
              ))}
            </Form.Select>
          </Form.Group>
        </Col>

        <Col>
          <Form.Group controlId="amountInput">
            <Form.Label>Amount</Form.Label>
            <Form.Control
              type="number"
              value={amount}
              step="0.01"
              onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
                const v = e.target.value
                setAmount(v === "" ? 0 : parseFloat(v))
              }}
            />
          </Form.Group>
        </Col>

        <Col md={3} className="d-flex align-items-end">
          {isNewExpense ? (
            <Button variant="primary" type="submit">
              Add
            </Button>
          ) : (
            <>
              <Button
                style={{ marginRight: "2px" }}
                variant="danger"
                onClick={() => DeleteExpense(dispatch, expense)}
              >
                Delete
              </Button>
              <Button
                style={{ marginRight: "2px" }}
                variant="success"
                type="submit"
              >
                Save
              </Button>
              <Button
                style={{ marginRight: "2px" }}
                variant="secondary"
                onClick={() => setIsEditing(false)}
              >
                Cancel
              </Button>
            </>
          )}
        </Col>
      </Row>
    </Form>
  )
}
export default ExpenseForm

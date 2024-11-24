import React, { useState } from "react";
import { Form, Button, Container } from "react-bootstrap";

const Lab = ({ index, handleSubmit, errorMessage, children }) => {
  const [inputData, setInputData] = useState("");
  const [inputError, setInputError] = useState("");

  const handleChange = (e) => {
    const { value } = e.target;
    setInputData(value);

    if (!value) {
      setInputError("This field is required");
    } else {
      setInputError("");
    }
  };

  return (
    <Container className="mt-5">
      <h2>Run Lab №{index}</h2>
      <pre>{children}</pre>
      <Form onSubmit={handleSubmit}>
        <Form.Group className="mb-3" controlId="formInputData">
          <Form.Label>Введіть вхідні дані для лаб</Form.Label>
          <Form.Control
            as="textarea"
            rows={10}
            cols={50}
            value={inputData}
            onChange={handleChange}
            isInvalid={!!inputError}
            required
          />
          <Form.Control.Feedback type="invalid">
            {inputError}
          </Form.Control.Feedback>
        </Form.Group>
        <Button variant="primary" type="submit">
          Ок
        </Button>
        <div className="text-danger">{errorMessage}</div>
      </Form>
    </Container>
  );
};

export { Lab };

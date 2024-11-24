import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Form, Button, Container } from "react-bootstrap";
import { getCookie, removeCookie } from "../../helpers/helpers";
import { LabResult } from "../components";

const Lab = ({ index, endpoint, defaultValue, children }) => {
  const navigate = useNavigate();
  const [inputData, setInputData] = useState(defaultValue);
  const [inputError, setInputError] = useState("");
  const [error, setError] = useState(null);
  const [result, setResult] = useState(null);

  const handleLogout = () => {
    document.cookie = removeCookie("AuthToken");
    navigate("/login");
  };

  const handleChange = (e) => {
    const { value } = e.target;
    setInputData(value);

    if (!value) {
      setInputError("This field is required");
    } else {
      setInputError("");
    }
  };

  const onSubmit = async (e) => {
    e.preventDefault();

    if (!inputData) {
      setInputError("This field is required");
      return;
    }

    try {
      const accessToken = getCookie("AuthToken");
      const response = await fetch(endpoint, {
        method: "POST",
        headers: {
          Authorization: `Bearer ${accessToken}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ inputData }),
      });

      if (response.status === 401) {
        handleLogout();
      }

      if (!response.ok) {
        throw new Error("Failed to run lab");
      }

      const result = await response.json();
      setResult(result);
    } catch (error) {
      setError(error.message);
    }
  };

  return (
    <Container className="mt-5">
      <h2>
        {result ? "Output" : "Run"} Lab №{index}
      </h2>
      <pre>{children}</pre>
      {result ? (
        <LabResult result={result} />
      ) : (
        <Form onSubmit={onSubmit}>
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
          <div className="text-danger">{error}</div>
        </Form>
      )}
    </Container>
  );
};

export { Lab };

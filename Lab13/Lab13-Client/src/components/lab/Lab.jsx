import { Form, Button, Container } from "react-bootstrap";

const Lab = ({ index, handleSubmit, errorMessage, children }) => {
  return (
    <Container className="mt-5">
      <h2>Run Lab №{index}</h2>
      <pre>{children}</pre>
      <Form onSubmit={handleSubmit}>
        <Form.Group className="mb-3" controlId="formInputData">
          <Form.Label>Введіть вхідні дані для лаб</Form.Label>
          <Form.Control as="textarea" rows={10} cols={50} />
          <Form.Text className="text-danger"></Form.Text>
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

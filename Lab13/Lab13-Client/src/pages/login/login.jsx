import { Form, Button, Container } from 'react-bootstrap';

const Login = () => {
  return (
    <Container className="mt-5">
      <h2>Login</h2>
      <Form>
        <Form.Group className="mb-3" controlId="formEmail">
          <Form.Label>Email</Form.Label>
          <Form.Control type="email" placeholder="Enter email" />
          <Form.Text className="text-danger">
            {/* Validation message for Email */}
          </Form.Text>
        </Form.Group>

        <Form.Group className="mb-3" controlId="formPassword">
          <Form.Label>Password</Form.Label>
          <Form.Control type="password" placeholder="Password" />
          <Form.Text className="text-danger">
            {/* Validation message for Password */}
          </Form.Text>
        </Form.Group>

        <Button variant="primary" type="submit">
          Login
        </Button>
        <div className="text-danger mb-3">
          {/* Validation summary */}
        </div>
      </Form>
    </Container>
  );
};

export { Login};
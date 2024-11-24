import { Container, Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

import { AppRoute } from "../../constants/constants";

const LabResult = ({ result }) => {
  const { answer, number, inputData } = result;
  const navigate = useNavigate();

  const handleGoHome = () => {
    navigate(AppRoute.ROOT);
  };

  return (
    <>
      <h4>{answer}</h4>
      <p>The output is for Lab №{number} with input:</p>
      <pre>{inputData}</pre>
      <Button variant="primary" onClick={handleGoHome}>
        Go home
      </Button>
    </>
  );
};

export { LabResult };

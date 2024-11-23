import { Form, Button, Container } from 'react-bootstrap';

const Lab1 = () => {
  return (
    <Container className="mt-5">
      <h2>Run Lab №1</h2>
      <pre>
        Розміщенням порядку K називають підмножина елементів деякої перестановки порядку N. Наприклад, (1, 3) – розміщення порядку 2 для перестановки (1, 2, 3) порядку 3.

        Потрібно за заданим розміщенням визначити його позицію в лексикографічному порядку всіх можливих розміщень, утворених з різних перестановок порядку N.

        Наприклад, лексикографічна послідовність всіляких розміщень для K=2 і N=3 виглядає так:

        (1,2), (1,3), (2,1), (2,3), (3,1), (3,2)

        Таким чином, переміщення (2,3) має номер 4 у цій послідовності.

        Вхідні дані

        У першому рядку вхідного файлу INPUT.TXT знаходяться числа N і K (1 ≤ K ≤ N ≤ 12). У другому рядку записані K чисел з діапазону від 1 до N – розміщення.

        Вихідні дані

        У вихідний файл OUTPUT.TXT виведіть однину - номер даного розміщення.
      </pre>
      <Form>
        <Form.Group className="mb-3" controlId="formInputData">
          <Form.Label>Введіть вхідні дані для лаб</Form.Label>
          <Form.Control as="textarea" rows={10} cols={50} />
          <Form.Text className="text-danger">
            {/* Validation message for InputData */}
          </Form.Text>
        </Form.Group>
        <Button variant="primary" type="submit">
          Ок
        </Button>
        <div className="text-danger">
          {/* Validation summary */}
        </div>
      </Form>
    </Container>
  );
};

export { Lab1 };
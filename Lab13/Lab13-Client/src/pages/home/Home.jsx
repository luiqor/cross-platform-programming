import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Container, Row, Col, Card, Button } from 'react-bootstrap';

import 'bootstrap/dist/css/bootstrap.min.css';
import { AppRoute, LabRoute } from '../../constants/constants';

const Home = () => {
  const [message, setMessage] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    fetch('/api/home')
      .then(response => response.json())
      .then(data => setMessage(data.message))
      .catch(error => console.error('Error fetching data:', error));
  }, []);

  return (
    <Container>
      <section>
        <h3>Підключення до серверу?</h3>
        {message ? <p>{message}</p> : <p>Loading...</p>}
      </section>
      <section className="text-center">
        <h1>Лабораторна №5</h1>
      </section>
      <section>
        <h2>Веб застосунок (ASP.NET Core MVC) складається з двох проектів:</h2>
        <p>a. Безпосередньо веб додаток</p>
        <p>b. Бібліотека класів, що дає змогу запускати практичні 1, 2 або 3</p>
      </section>
      <section>
        <h3>Сторінка логіну, реєстрації та профілю користувача:</h3>
        <Container className="mt-3 mb-4">
          <Row>
            <Col md={4}>
              <Card className="text-center">
                <Card.Body>
                  <Card.Title>Логін</Card.Title>
                  <Card.Text>Увійдіть до свого облікового запису.</Card.Text>
                  <Button onClick={() => { navigate(AppRoute.LOGIN)}} variant="primary">Увійти</Button>
                </Card.Body>
              </Card>
            </Col>
            <Col md={4}>
              <Card className="text-center">
                <Card.Body>
                  <Card.Title>Реєстрація</Card.Title>
                  <Card.Text>Створіть новий обліковий запис.</Card.Text>
                  <Button onClick={() => { navigate(AppRoute.REGISTER)}} variant="primary">Зареєструватися</Button>
                </Card.Body>
              </Card>
            </Col>
            <Col md={4}>
              <Card className="text-center">
                <Card.Body>
                  <Card.Title>Профіль користувача</Card.Title>
                  <Card.Text>Перегляньте свій профіль.</Card.Text>
                  <Button onClick={() => { navigate(AppRoute.PROFILE)}} variant="primary">Профіль</Button>
                </Card.Body>
              </Card>
            </Col>
          </Row>
        </Container>
      </section>
      <section>
        <h3>Сторінки практичних завдань:</h3>
        <Container className="mt-3">
          <Row>
            <Col md={4}>
              <Card className="text-center">
                <Card.Body>
                  <Card.Title>Лаб №1</Card.Title>
                  <Card.Text>Запустіть практичну 1 та отримайте результат.</Card.Text>
                  <Button onClick={() => { navigate(`${AppRoute.LAB}/${LabRoute.LAB1}`)}} variant="primary">Го</Button>
                </Card.Body>
              </Card>
            </Col>
            <Col md={4}>
              <Card className="text-center">
                <Card.Body>
                  <Card.Title>Лаб №2</Card.Title>
                  <Card.Text>Запустіть практичну 2 та отримайте результат.</Card.Text>
                  <Button onClick={() => { navigate(`${AppRoute.LAB}/${LabRoute.LAB2}`)}} variant="primary">Го</Button>
                </Card.Body>
              </Card>
            </Col>
            <Col md={4}>
              <Card className="text-center">
                <Card.Body>
                  <Card.Title>Лаб №3</Card.Title>
                  <Card.Text>Запустіть практичну 3 та отримайте результат.</Card.Text>
                  <Button onClick={() => { navigate(`${AppRoute.LAB}/${LabRoute.LAB3}`)}} variant="primary">Го</Button>
                </Card.Body>
              </Card>
            </Col>
          </Row>
        </Container>
      </section>
    </Container>
  );
}

export { Home };